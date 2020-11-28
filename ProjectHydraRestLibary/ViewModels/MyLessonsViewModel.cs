using Microsoft.Extensions.DependencyInjection;
using ProjectHydraRestLibary.Models;
using ProjectHydraRestLibary.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHydraRestLibary.ViewModels
{
    public class MyLessonsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly ILessonService _lessonService;
        private readonly IAuthModel _authModel;


        public MyLessonsViewModel()
        {
            _lessonService = ServiceProviderFactory.ServiceProvider.GetRequiredService<ILessonService>();
            _authModel = ServiceProviderFactory.ServiceProvider.GetRequiredService<IAuthModel>();
            _ = LoadUserLesson();
        }

        private async Task LoadUserLesson()
        {
            Lessons = await _lessonService.GetUserLessons(_authModel.UserId);
        }

        private IEnumerable<LessonVM> _lessons;

        public IEnumerable<LessonVM> Lessons
        {
            get { return _lessons; }
            set 
            {
                _lessons = value;
                OnPropertyChanged("Lessons");
            }
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}
