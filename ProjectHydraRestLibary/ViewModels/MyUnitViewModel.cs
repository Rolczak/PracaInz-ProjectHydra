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
    public class MyUnitViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly IUnitService _unitService;
        private readonly IAuthModel _authModel;
        private readonly IUserService _userService;
        public MyUnitViewModel()
        {
            _unitService = ServiceProviderFactory.ServiceProvider.GetRequiredService<IUnitService>();
            _authModel = ServiceProviderFactory.ServiceProvider.GetRequiredService<IAuthModel>();
            _userService = ServiceProviderFactory.ServiceProvider.GetRequiredService<IUserService>();
            _ = LoadUserUnit();
        }

        private async Task LoadUserUnit()
        {
            var userDetails = await _userService.GetUserDetails(_authModel.UserId);
            UnitDetails = await _unitService.GetUserUnit(userDetails.UnitId);
        }
        private UnitDetails unitDetails;

        public UnitDetails UnitDetails
        {
            get { return unitDetails; }
            set { 
                unitDetails = value;
                OnPropertyChanged("UnitDetails");
            }
        }
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
