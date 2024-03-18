using QuizApplication.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApplication.Shared.Services
{
    public class QuestionStateContainer
    {
        public QuestionRequestDto? Value { get; set; }
        public event Action? OnStateChange;
        public void SetValue(QuestionRequestDto? value)
        {
            this.Value = value;
            NotifyStateChanged();
        }
        private void NotifyStateChanged() => OnStateChange?.Invoke();
    }
}
