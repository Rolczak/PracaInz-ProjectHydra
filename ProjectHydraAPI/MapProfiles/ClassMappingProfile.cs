using AutoMapper;
using ProjectHydraAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectHydraAPI.MapProfiles
{
    public class ClassMappingProfile : Profile
    {
        public ClassMappingProfile()
        {
            CreateMap<Class, ClassVM>()
                .ForMember(vm => vm.TeacherName, map => {
                    map.PreCondition(c => c.Teacher != null);
                    map.MapFrom(c => c.Teacher.Rank.Name + " "
                    + c.Teacher.FirstName + " " + c.Teacher.LastName); })
                .ForMember(vm => vm.Unit, map => {
                    map.PreCondition(c => c.Unit != null);
                    map.MapFrom(c => c.Unit.Name);
                });
        }
    }
}
