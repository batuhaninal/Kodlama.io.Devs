using Application.Features.ProgramingLanguages.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProgramingLanguages.Commands.UpdateProgramingLanguage
{
    public class UpdateProgramingLanguageCommandValidator : AbstractValidator<UpdatedProgramingLanguageDto>
    {
        public UpdateProgramingLanguageCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(1)
                .MaximumLength(50);
        }
    }
}
