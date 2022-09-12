using Application.Features.ProgramingLanguages.Dtos;
using Application.Features.ProgramingLanguages.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProgramingLanguages.Queries.GetByIdProgramingLanguage
{
    public class GetByIdProgramingLanguageQuery : IRequest<ProgramingLanguageGetByIdDto>
    {
        public int Id { get; set; }

        public class GetByIdProgramingLanguageQueryHandler : IRequestHandler<GetByIdProgramingLanguageQuery, ProgramingLanguageGetByIdDto>
        {
            private readonly IMapper _mapper;
            private readonly IProgramingLanguageRepository _programingLanguageRepository;
            private readonly ProgramingLanguageBusinessRules _businessRules;

            public GetByIdProgramingLanguageQueryHandler(IMapper mapper, IProgramingLanguageRepository programingLanguageRepository, ProgramingLanguageBusinessRules businessRules)
            {
                _mapper = mapper;
                _programingLanguageRepository = programingLanguageRepository;
                _businessRules = businessRules;
            }

            public async Task<ProgramingLanguageGetByIdDto> Handle(GetByIdProgramingLanguageQuery request, CancellationToken cancellationToken)
            {
                await _businessRules.ProgramingLanguageShouldExistWhenRequestedById(request.Id);

                ProgramingLanguage programingLanguage = await _programingLanguageRepository.GetAsync(x=> x.Id == request.Id);
                ProgramingLanguageGetByIdDto mappedLanguage = _mapper.Map<ProgramingLanguageGetByIdDto>(programingLanguage);

                return mappedLanguage;
            }
        }
    }
}
