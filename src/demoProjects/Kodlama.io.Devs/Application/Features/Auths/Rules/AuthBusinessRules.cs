using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auths.Rules
{
    public class AuthBusinessRules
    {
        private readonly IUserRepository _userRepository;

        public AuthBusinessRules(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task EmailCannotBeDuplicated(string email)
        {
            var result = await _userRepository.GetAsync(u => u.Email == email);
            if (result != null) throw new BusinessException("Bu mail adresine kayıtlı bir kullanıcı bulunmaktadır. Lütfen başka bir mail adresi giriniz"); 
        }
    }
}
