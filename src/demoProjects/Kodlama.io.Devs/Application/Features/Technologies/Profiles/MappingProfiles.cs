using Application.Features.Technologies.Commands.CreateTechnology;
using Application.Features.Technologies.Commands.UpdateTechnology;
using Application.Features.Technologies.Dtos;
using Application.Features.Technologies.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Technologies.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Technology, CreateTechnologyCommand>().ReverseMap();
            CreateMap<Technology, CreatedTechnologyDto>().ForMember(c => c.ProgramingLanguageName, opt => opt.MapFrom(c => c.ProgramingLanguage.Name)).ReverseMap();

            CreateMap<Technology, UpdateTechnologyCommand>().ReverseMap();
            CreateMap<Technology, UpdatedTechnologyDto>().ForMember(c => c.ProgramingLanguageName, opt => opt.MapFrom(c => c.ProgramingLanguage.Name)).ReverseMap();

            CreateMap<Technology, DeletedTechnologyDto>().ForMember(p=>p.ProgramingLanguageName, opt=>opt.MapFrom(c=>c.ProgramingLanguage.Name)).ReverseMap();

            CreateMap<Technology, TechnologyListDto>().ForMember(c => c.ProgramingLanguageName, opt => opt.MapFrom(c => c.ProgramingLanguage.Name)).ReverseMap();
            CreateMap<IPaginate<Technology>, TechnologyListModel>().ReverseMap();


        }
    }
}
