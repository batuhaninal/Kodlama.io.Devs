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

namespace Application.Features.ProgramingLanguages.Commands.DeleteProgramingLanguage
{
    public class DeleteProgramingLanguageCommand : IRequest<DeletedProgramingLanguageDto>
    {
        public int Id { get; set; }

        public class DeleteProgramingLanguageCommandHandler : IRequestHandler<DeleteProgramingLanguageCommand, DeletedProgramingLanguageDto>
        {
            private readonly IMapper _mapper;
            private readonly IProgramingLanguageRepository _programingLanguageRepository;
            private readonly ProgramingLanguageBusinessRules _programingLanguageBusinessRules;

            public DeleteProgramingLanguageCommandHandler(IMapper mapper, IProgramingLanguageRepository programingLanguageRepository, ProgramingLanguageBusinessRules programingLanguageBusinessRules)
            {
                _mapper = mapper;
                _programingLanguageRepository = programingLanguageRepository;
                _programingLanguageBusinessRules = programingLanguageBusinessRules;
            }

            public async Task<DeletedProgramingLanguageDto> Handle(DeleteProgramingLanguageCommand request, CancellationToken cancellationToken)
            {
                await _programingLanguageBusinessRules.ProgramingLanguageShouldExistWhenRequestedById(request.Id);
                var foundData = await _programingLanguageRepository.GetAsync(x=> x.Id == request.Id);
                ProgramingLanguage deletedProgramingLanguage = await _programingLanguageRepository.DeleteAsync(foundData);
                DeletedProgramingLanguageDto deletedProgramingLanguageDto = _mapper.Map<DeletedProgramingLanguageDto>(deletedProgramingLanguage);

                return deletedProgramingLanguageDto;
            }
        }
    }
}
