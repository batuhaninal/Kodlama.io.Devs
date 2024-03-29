﻿using Application.Features.Auths.Dtos;
using Application.Features.ProgramingLanguages.Commands.CreateProgramingLanguage;
using Application.Features.ProgramingLanguages.Commands.DeleteProgramingLanguage;
using Application.Features.ProgramingLanguages.Commands.UpdateProgramingLanguage;
using Application.Features.ProgramingLanguages.Dtos;
using Application.Features.ProgramingLanguages.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Core.Security.Dtos;
using Core.Security.Entities;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProgramingLanguages.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<ProgramingLanguage, CreateProgramingLanguageCommand>().ReverseMap();
            CreateMap<ProgramingLanguage, CreatedProgramingLanguageDto>().ReverseMap();

            //CreateMap<ProgramingLanguage, DeleteProgramingLanguageCommand>().ReverseMap();
            CreateMap<ProgramingLanguage, DeletedProgramingLanguageDto>().ReverseMap();

            //CreateMap<ProgramingLanguage, UpdateProgramingLanguageCommand>().ReverseMap();
            CreateMap<ProgramingLanguage, UpdatedProgramingLanguageDto>().ReverseMap();

            CreateMap<ProgramingLanguage, ProgramingLanguageListDto>().ReverseMap();
            CreateMap<IPaginate<ProgramingLanguage>, ProgramingLanguageListModel>().ReverseMap();

            CreateMap<ProgramingLanguage, ProgramingLanguageGetByIdDto>().ReverseMap();
        }
    }
}
