using AutoMapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Application.Activities
{
    public class MappingProfile :Profile
    {

        public MappingProfile()
        {
            CreateMap<Activity, ActivityDto>();
            // CreateMap< ActivityDto , Activity>();
            CreateMap<UserActivity, AttendeeDto>()
                .ForMember(d => d.Username, o => o.MapFrom(s => s.AppUser.UserName))
                .ForMember(d => d.DisplayName, o => o.MapFrom(s => s.AppUser.DisplayName))
                .ForMember(d =>d.Image, o => o.MapFrom(s => s.AppUser.Photos.FirstOrDefault(x =>x.IsMain).Url));

        }
    }
}
