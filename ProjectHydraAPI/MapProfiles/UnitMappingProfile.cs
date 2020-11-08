using AutoMapper;
using ProjectHydraAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectHydraAPI.MapProfiles
{
    public class UnitMappingProfile : Profile
    {
        public UnitMappingProfile()
        {
            CreateMap<Unit, UnitVM>()
                .ForMember(vm => vm.CommanderName, opt => {
                    opt.PreCondition(u => (u.Commander != null));
                    opt.MapFrom(u => u.Commander.Rank.Name + " " + u.Commander.LastName);
                })
                .ForMember(vm => vm.DeputyCommanderName, opt => {
                    opt.PreCondition(u => (u.DeputyCommander != null));
                    opt.MapFrom(u => u.DeputyCommander.Rank.Name + " " + u.DeputyCommander.LastName);
                })
                .ForMember(vm => vm.ParentUnitName, opt =>
                {
                    opt.PreCondition(u => (u.Parent != null));
                    opt.MapFrom(u => u.Parent.Name);
                });
        }
    }
}
