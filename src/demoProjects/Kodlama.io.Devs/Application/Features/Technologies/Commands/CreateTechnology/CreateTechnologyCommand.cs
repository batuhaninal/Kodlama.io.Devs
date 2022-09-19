using Application.Features.Technologies.Dtos;
using Application.Features.Technologies.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Technologies.Commands.CreateTechnology
{
    public class CreateTechnologyCommand : IRequest<CreatedTechnologyDto>
    {
        public string Name { get; set; }
        public int ProgramingLanguageId { get; set; }

        public class CreateTechnologyCommandHandler : IRequestHandler<CreateTechnologyCommand, CreatedTechnologyDto>
        {
            protected readonly ITechnologyRepository _repository;
            protected readonly IMapper _mapper;
            protected readonly TechnologyBusinessRules _rules;

            public CreateTechnologyCommandHandler(ITechnologyRepository repository, IMapper mapper, TechnologyBusinessRules rules)
            {
                _repository = repository;
                _mapper = mapper;
                _rules = rules;
            }

            public async Task<CreatedTechnologyDto> Handle(CreateTechnologyCommand request, CancellationToken cancellationToken)
            {
                await _rules.TechnologyNameCanNotBeDuplicated(request.Name);

                Technology mappedTech = _mapper.Map<Technology>(request);
                Technology createdTech = await _repository.AddAsync(mappedTech);

                Technology tech = _repository.GetList(include: i => i.Include(j => j.ProgramingLanguage)).Items.LastOrDefault();

                CreatedTechnologyDto createdDto = _mapper.Map<CreatedTechnologyDto>(tech);

                return createdDto;
            }
        }
    }
}
