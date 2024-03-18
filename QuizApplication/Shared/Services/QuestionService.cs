using QuizApplication.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace QuizApplication.Shared.Services
{
    public class QuestionService
    {
        private readonly HttpClient _http;
        private MediaStateContainer _mediaStateContainer;
        private QuestionStateContainer _questionStateContainer;
        private List<AnswersQuestionResponseDto>? questions;
        private QuestionRequestDto? requestModel;
        private QuestionRequestDto? responseContent = new();
        private string? errorMessage = "No questions here";

        public QuestionService(HttpClient httpClient, MediaStateContainer mediaStateContainer, QuestionStateContainer questionStateContainer)
        {
            _http = httpClient;
            _mediaStateContainer = mediaStateContainer;
            _questionStateContainer = questionStateContainer;
        }

        public async Task<List<AnswersQuestionResponseDto>> FetchQuestionsAsync()
        {
            var response = await _http.GetAsync("/api/Questions");

            if (response.IsSuccessStatusCode)
            {
                questions = await response.Content.ReadFromJsonAsync<List<AnswersQuestionResponseDto>>();

                if (questions == null)
                {
                    errorMessage = "There was an error deserializing the response!";
                    throw new Exception($"There was an error deserializing the response! {errorMessage}, \nStatusCode {response.StatusCode}, \nresponse Content {response.Content},  \nresponse Headers {response.Headers}  ");
                }
                else
                {
                    return questions;
                }
            }
            else
            {
                errorMessage = response.ReasonPhrase;
                throw new Exception($"There was an error in the response! {errorMessage}, \nStatusCode {response.StatusCode}, \nresponse Content {response.Content},  \nresponse Headers {response.Headers}  ");
            }
        }

        public async Task<List<AnswersQuestionResponseDto>> FetchQuestionsWithAnswersAsync()
        {
            var response = await _http.GetAsync("/api/Questions/questions-with-answers");

            if (response.IsSuccessStatusCode)
            {
                questions = await response.Content.ReadFromJsonAsync<List<AnswersQuestionResponseDto>>();


                if (questions == null)
                {
                    errorMessage = "There was an error deserializing the response!";
                    throw new Exception($"There was an error deserializing the response! {errorMessage}, \nStatusCode {response.StatusCode}, \nresponse Content {response.Content},  \nresponse Headers {response.Headers}  ");
                }
                else
                {
                    return questions;
                }
            }
            else
            {
                errorMessage = response.ReasonPhrase;
                throw new Exception($"There was an error in the response! {errorMessage}, \nStatusCode {response.StatusCode}, \nresponse Content {response.Content},  \nresponse Headers {response.Headers}  ");
            }
        }

        public async Task<QuestionRequestDto> QuestionUploadAsync(QuestionRequestDto requestModel, string? successMessage, bool success)
        {
            if (string.IsNullOrEmpty(requestModel.MediaFileName))
            {
                requestModel.MediaFileName = _mediaStateContainer?.Value?.StoredFileName;
            }

            var response = await _http.PostAsJsonAsync("api/Questions/upload", requestModel);

            if (response.IsSuccessStatusCode)
            {
                success = true;
                successMessage = "File uploaded successfully!";
                responseContent = await response.Content.ReadFromJsonAsync<QuestionRequestDto>();

                if (responseContent != null)
                {
                    _questionStateContainer.SetValue(responseContent);
                    return responseContent;
                }
                else
                {
                    errorMessage = "There was an error deserializing the response!";
                    throw new Exception($"There was an error deserializing the response! {errorMessage}, \nStatusCode {response.StatusCode}, \nresponse Content {response.Content},  \nresponse Headers {response.Headers}  ");
                }
            }
            else
            {
                success = false;
                errorMessage = response.ReasonPhrase;
                throw new Exception($"There was an error in the response! {errorMessage}, \nStatusCode {response.StatusCode}, \nresponse Content {response.Content},  \nresponse Headers {response.Headers}  ");
            }
        }

        public async Task<List<AnswersQuestionResponseDto>> PublishQuestionAsync(string questionPath)
        {
            var response = await _http.PutAsJsonAsync($"/api/Questions/question/{questionPath}", questionPath);

            if (response.IsSuccessStatusCode)
            {
                questions = await FetchQuestionsWithAnswersAsync();
                return questions;
            }
            else
            {
                errorMessage = response.ReasonPhrase;
                throw new Exception($"There was an error in the response! {errorMessage}, \nStatusCode {response.StatusCode}, \nresponse Content {response.Content},  \nresponse Headers {response.Headers}  ");
            }
        }

        public async Task<List<AnswersQuestionResponseDto>> DeleteQuestionAsync(string questionPath)
        {
            var response = await _http.DeleteAsync($"/api/Questions/question/{questionPath}");

            if (response.IsSuccessStatusCode)
            {
                questions = await FetchQuestionsWithAnswersAsync();
                return questions;
            }
            else
            {
                errorMessage = response.ReasonPhrase;
                throw new Exception($"There was an error in the response! {errorMessage}, \nStatusCode {response.StatusCode}, \nresponse Content {response.Content},  \nresponse Headers {response.Headers}  ");
            }
        }

    }
}
