using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProgramingLanguages.Commands.CreateProgramingLanguage
{
    public class CreateProgramingLanguageCommandValidator : AbstractValidator<CreateProgramingLanguageCommand>
    {
        public CreateProgramingLanguageCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty();
        }
    }
}
