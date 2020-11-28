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
    public class MyGradesViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly IGradesService _gradesService;
        private readonly IAuthModel _authModel;

        public MyGradesViewModel()
        {
            _gradesService = ServiceProviderFactory.ServiceProvider.GetRequiredService<IGradesService>();
            _authModel = ServiceProviderFactory.ServiceProvider.GetRequiredService<IAuthModel>();
            _ = LoadUserGrades();
        }

        private async Task LoadUserGrades()
        {
            Grades = await _gradesService.GetUserGrades(_authModel.UserId);
        }

        private IEnumerable<GradeVM> _grades;

        public IEnumerable<GradeVM> Grades
        {
            get { return _grades; }
            set
            {
                _grades = value;
                OnPropertyChanged(nameof(Grades));
            }
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
