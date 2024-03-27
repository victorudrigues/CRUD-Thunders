using AutoMapper;
using CRUD_Thunders.Application.DTOs;
using CRUD_Thunders.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_Thunders.Application.Mappings
{
    //Mapeando as entidades
    //Criando mapeamento de ida e volta sem precisar fazer manualmente
    public class MappingProfile: Profile
    {
        public MappingProfile() 
        {
            CreateMap<User, UserDTO>()
                .ForMember(x => x.Activity, opt => opt.MapFrom(src => src.Activities));
            CreateMap<Activity, ActivityDTO>().ReverseMap();
        }
    }
}
