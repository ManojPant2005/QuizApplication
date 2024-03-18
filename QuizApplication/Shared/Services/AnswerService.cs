using Microsoft.AspNetCore.Components;
using QuizApplication.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace QuizApplication.Shared.Services
{
    public class AnswerService
    {
        private AnswersQuestionRequestDto? answersQuestion;
        private string? errorMessage;
        private readonly HttpClient _http;
        private QuestionStateContainer _questionStateContainer;
        private readonly NavigationManager _navigation;

        public AnswerService(HttpClient httpClient, QuestionStateContainer questionStateContainer, NavigationManager navigation)
        {
            _http = httpClient;
            _questionStateContainer = questionStateContainer;
            _navigation = navigation;
        }

        public async Task AnswersUploadAsync(AnswersQuestionRequestDto answersQuestion)
        {
            if (string.IsNullOrEmpty(answersQuestion.Path))
            {
                answersQuestion.Path = _questionStateContainer?.Value?.QuestionPath;
                ;
            }

            var response = await _http.PostAsJsonAsync("api/Answers/upload", answersQuestion);

            if (response.IsSuccessStatusCode)
            {
                _navigation.NavigateTo("/myquizzes");
            }
            else
            {
                errorMessage = response.ReasonPhrase;
                throw new Exception($"There was an error in the response! {errorMessage}, \nStatusCode {response.StatusCode}, \nresponse Content {response.Content},  \nresponse Headers {response.Headers}  ");
            }
        }
    }
}
