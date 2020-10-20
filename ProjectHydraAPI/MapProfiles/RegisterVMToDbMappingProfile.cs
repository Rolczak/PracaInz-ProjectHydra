using AutoMapper;
using ProjectHydraAPI.Models;
using ProjectHydraAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectHydraAPI.MapProfiles
{
    public class RegisterVMToDbMappingProfile : Profile
    {
        public RegisterVMToDbMappingProfile()
        {
            CreateMap<RegistrationViewModel, AppUser>().ForMember(u => u.UserName, map => map.MapFrom(vm => vm.Email));
        }
    }
}
