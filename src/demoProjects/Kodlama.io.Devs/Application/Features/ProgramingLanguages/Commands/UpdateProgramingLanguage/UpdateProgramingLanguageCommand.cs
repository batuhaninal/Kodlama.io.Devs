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

namespace Application.Features.ProgramingLanguages.Commands.UpdateProgramingLanguage
{
    public class UpdateProgramingLanguageCommand : IRequest<UpdatedProgramingLanguageDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public class UpdateProgramingLanguageCommandHandler : IRequestHandler<UpdateProgramingLanguageCommand, UpdatedProgramingLanguageDto>
        {
            private readonly IMapper _mapper;
            private readonly IProgramingLanguageRepository _programingLanguageRepository;
            private readonly ProgramingLanguageBusinessRules _businessRules;

            public UpdateProgramingLanguageCommandHandler(IMapper mapper, IProgramingLanguageRepository programingLanguageRepository, ProgramingLanguageBusinessRules businessRules)
            {
                _mapper = mapper;
                _programingLanguageRepository = programingLanguageRepository;
                _businessRules = businessRules;
            }

            public async Task<UpdatedProgramingLanguageDto> Handle(UpdateProgramingLanguageCommand request, CancellationToken cancellationToken)
            {
                await _businessRules.ProgramingLanguageShouldExistWhenRequestedById(request.Id);
                await _businessRules.ProgramingLanguageNameCanNotBeDuplicated(request.Name);

                var fetchData = await _programingLanguageRepository.GetAsync(x => x.Id == request.Id);
                fetchData.Name = request.Name;
                //ProgramingLanguage mappedProgramingLanguage = _mapper.Map<ProgramingLanguage>(request);
                ProgramingLanguage updatedProgramingLanguage = await _programingLanguageRepository.UpdateAsync(fetchData);
                UpdatedProgramingLanguageDto updatedProgramingLanguageDto = _mapper.Map<UpdatedProgramingLanguageDto>(updatedProgramingLanguage);

                return updatedProgramingLanguageDto;
            }
        }
    }
}
