using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Technologies.Rules
{
    public class TechnologyBusinessRules
    {
        private readonly ITechnologyRepository _repo;

        public TechnologyBusinessRules(ITechnologyRepository repo)
        {
            _repo = repo;
        }


        public async Task TechnologyNameCanNotBeDuplicated(string name)
        {
            IPaginate<Technology> result = await _repo.GetListAsync(x => x.Name == name);
            if (result.Items.Any()) throw new BusinessException("Technology name already exist");
        }

        public async Task TechnologyShouldExistWhenRequestedById(int id)
        {
            var result = await _repo.GetAsync(x => x.Id == id);
            if (result == null) throw new BusinessException("Technology not found");
        }
    }
}
