using AutoMapper;
using ProjectHydraAPI.Models;
using ProjectHydraAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectHydraAPI.MapProfiles
{
    public class RankVmToDbMappingProfile : Profile
    {
        public RankVmToDbMappingProfile()
        {
            CreateMap<RankCreate, Rank>().ForMember(x => x.Id, opt => opt.Ignore());
            CreateMap<Rank, RankSelect>();
        }
    }
}
