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

namespace Application.Features.Technologies.Commands.UpdateTechnology
{
    public class UpdateTechnologyCommand : IRequest<UpdatedTechnologyDto>
    {
        public int Id { get; set; }
        public int ProgramingLanguageId { get; set; }
        public string Name { get; set; }

        public class UpdateTechnologyCommandHandler : IRequestHandler<UpdateTechnologyCommand, UpdatedTechnologyDto>
        {
            protected readonly ITechnologyRepository _repo;
            protected readonly IMapper _mapper;
            protected readonly TechnologyBusinessRules _businessRules;

            public UpdateTechnologyCommandHandler(ITechnologyRepository repo, IMapper mapper, TechnologyBusinessRules businessRules)
            {
                _repo = repo;
                _mapper = mapper;
                _businessRules = businessRules;
            }

            public async Task<UpdatedTechnologyDto> Handle(UpdateTechnologyCommand request, CancellationToken cancellationToken)
            {
                await _businessRules.TechnologyNameCanNotBeDuplicated(request.Name);
                await _businessRules.TechnologyShouldExistWhenRequestedById(request.Id);

                Technology mappedTech = _mapper.Map<Technology>(request);

                Technology updatedTech = await _repo.UpdateAsync(mappedTech);

                Technology fetchData = _repo.GetList(x => x.Id == updatedTech.Id,
                                                        include:c=>c.Include(p=>p.ProgramingLanguage)).Items.FirstOrDefault();

                UpdatedTechnologyDto updatedDto = _mapper.Map<UpdatedTechnologyDto>(fetchData);

                return updatedDto;

            }
        }
    }
}
