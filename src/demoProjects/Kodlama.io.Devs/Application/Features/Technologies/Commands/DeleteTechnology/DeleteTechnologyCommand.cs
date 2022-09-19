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

namespace Application.Features.Technologies.Commands.DeleteTechnology
{
    public class DeleteTechnologyCommand : IRequest<DeletedTechnologyDto>
    {
        public int Id { get; set; }

        public class DeleteTechnologyCommandHandler : IRequestHandler<DeleteTechnologyCommand, DeletedTechnologyDto>
        {
            protected readonly ITechnologyRepository _repo;
            protected readonly IMapper _mapper;
            protected readonly TechnologyBusinessRules _rules;

            public DeleteTechnologyCommandHandler(ITechnologyRepository repo, IMapper mapper, TechnologyBusinessRules rules)
            {
                _repo = repo;
                _mapper = mapper;
                _rules = rules;
            }

            public async Task<DeletedTechnologyDto> Handle(DeleteTechnologyCommand request, CancellationToken cancellationToken)
            {
                await _rules.TechnologyShouldExistWhenRequestedById(request.Id);
                Technology fetchData = _repo.GetList(x => x.Id == request.Id,
                                                            include: y => y.Include(i => i.ProgramingLanguage)).Items.FirstOrDefault();
                Technology deletedTech = await _repo.DeleteAsync(fetchData);
                DeletedTechnologyDto mappedResult = _mapper.Map<DeletedTechnologyDto>(deletedTech);

                return mappedResult;
            }
        }
    }
}
