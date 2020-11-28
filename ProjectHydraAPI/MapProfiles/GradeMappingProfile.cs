using AutoMapper;
using ProjectHydraAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectHydraAPI.MapProfiles
{
    public class GradeMappingProfile: Profile
    {
        public GradeMappingProfile()
        {
            CreateMap<Grade, GradeVM>()
                .ForMember(vm => vm.LessonName, opt =>
                {
                    opt.PreCondition(g => g.Lesson != null);
                    opt.MapFrom(g => g.Lesson.Topic);
                })
                .ForMember(vm => vm.UserName, opt =>
                {
                    opt.PreCondition(g => g.User != null);
                    opt.MapFrom(g => g.User.FirstName + g.User.LastName);
                });
        }
	
	}

}
