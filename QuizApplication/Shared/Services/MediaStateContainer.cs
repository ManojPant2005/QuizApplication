using QuizApplication.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApplication.Shared.Services
{
    public class MediaStateContainer
    {
        public MediaFileResponseDto? Value { get; set; }
        public event Action? OnStateChange;
        public void SetValue(MediaFileResponseDto? value)
        {
            this.Value = value;
            NotifyStateChanged();
        }
        private void NotifyStateChanged() => OnStateChange?.Invoke();
    }
}
