using AutoMapper;
using ProjectHydraAPI.Models;
using ProjectHydraAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectHydraAPI.MapProfiles
{
    public class AppUserMappingProfile : Profile
    {
        public AppUserMappingProfile()
        {
            CreateMap<AppUser, UserViewModel>()
                .ForMember(vm => vm.RankName, map => map.MapFrom(u => u.Rank.Name));

            CreateMap<AppUser, UserDetailsModel>()
                .ForMember(vm => vm.RankName, map => map.MapFrom(u => u.Rank.Name))
                .ForMember(vm => vm.Birthday, map => map.MapFrom(u => u.Birthday.ToString("yyyy-MM-dd")))
                .ForMember(vm => vm.UnitName, map => map.MapFrom(u => u.Unit.Name));

        }
    }

}
