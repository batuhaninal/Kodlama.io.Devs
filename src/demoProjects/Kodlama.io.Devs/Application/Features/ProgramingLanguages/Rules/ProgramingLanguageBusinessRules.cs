using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProgramingLanguages.Rules
{
    public class ProgramingLanguageBusinessRules
    {
        private readonly IProgramingLanguageRepository _programingLanguageRepository;

        public ProgramingLanguageBusinessRules(IProgramingLanguageRepository programingLanguageRepository)
        {
            _programingLanguageRepository = programingLanguageRepository;
        }

        public async Task ProgramingLanguageNameCanNotBeDuplicated(string name)
        {
            IPaginate<ProgramingLanguage> result = await _programingLanguageRepository.GetListAsync(x => x.Name == name);
            if (result.Items.Any()) throw new BusinessException("Language name already exists.");
        }

        public async Task ProgramingLanguageShouldExistWhenRequestedById(int id)
        {
            ProgramingLanguage result = await _programingLanguageRepository.GetAsync(x => x.Id == id);
            if (result == null) throw new BusinessException("Programing language not found.");
        }
    }
}
