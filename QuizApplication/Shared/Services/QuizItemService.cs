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
    public class QuizItemService
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigation;
        private List<QuizItemQuestionResponseDto>? quizItems;
        private string? errorMessage = "";
        public QuizItemService(HttpClient httpClient, NavigationManager navigation)
        {
            _http = httpClient;
            _navigation = navigation;
        }

        public async Task<List<QuizItemQuestionResponseDto>> FetchParticipantsAsync()
        {
            var response = await _http.GetAsync($"api/QuizItems");

            if (response.IsSuccessStatusCode)
            {
                quizItems = await response.Content.ReadFromJsonAsync<List<QuizItemQuestionResponseDto>>();

                if (quizItems == null)
                {
                    throw new Exception("There was an error fetching the quizItems or the quizItems were null");
                }
                return quizItems;
            }
            else
            {
                throw new Exception($"There was an error in the response! {response.ReasonPhrase}, \nStatusCode {response.StatusCode}, \nresponse Content {response.Content},  \nresponse Headers {response.Headers}");
            }
        }

        public async Task UploadQuizItemAsync(QuizItemQuestionResquestDto quizItemQuestionResquestDto)
        {
            var response = await _http.PostAsJsonAsync("api/QuizItems/upload", quizItemQuestionResquestDto);

            if (response.IsSuccessStatusCode)
            {
                GoPlayQuiz();
            }
            else
            {
                errorMessage = response.ReasonPhrase;
                throw new Exception($"There was an error in the response! {errorMessage}, \nStatusCode {response.StatusCode}, \nresponse Content {response.Content.ReadAsStringAsync()},  \nresponse Headers {response.Headers}");
            }
        }

        public async Task<int> FetchUserScoreAsync()
        {
            var response = await _http.GetAsync("/api/QuizItems/score");

            if (response.IsSuccessStatusCode)
            {
                var score = await response.Content.ReadFromJsonAsync<int>();
                return score;
            }
            else
            {
                errorMessage = response.ReasonPhrase;
                throw new Exception($"There was an error in the response! {errorMessage}, \nStatusCode {response.StatusCode}, \nresponse Content {response.Content},  \nresponse Headers {response.Headers}  ");
            }
        }

        private void GoPlayQuiz()
        {
            _navigation.NavigateTo($"playquiz");
        }
    }
}
