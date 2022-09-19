using Application.Features.Technologies.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Technologies.Queries.GetListTechnology
{
    public class GetListTechnologyQuery : IRequest<TechnologyListModel>
    {
        public PageRequest PageRequest { get; set; }

        public class GetListTechnologyQueryHandler : IRequestHandler<GetListTechnologyQuery, TechnologyListModel>
        {
            protected readonly ITechnologyRepository _repo;
            protected readonly IMapper _mapper;

            public GetListTechnologyQueryHandler(ITechnologyRepository repo, IMapper mapper)
            {
                _repo = repo;
                _mapper = mapper;
            }

            async Task<TechnologyListModel> IRequestHandler<GetListTechnologyQuery, TechnologyListModel>.Handle(GetListTechnologyQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Technology> technologies = await _repo.GetListAsync(index: request.PageRequest.Page,
                                                                                size: request.PageRequest.PageSize,
                                                                                include: i=>i.Include(p=>p.ProgramingLanguage));

                TechnologyListModel mappedTechs = _mapper.Map<TechnologyListModel>(technologies);

                return mappedTechs;
            }
        }
    }
}
