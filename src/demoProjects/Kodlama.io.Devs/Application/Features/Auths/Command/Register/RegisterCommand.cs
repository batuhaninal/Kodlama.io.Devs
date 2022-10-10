using Application.Features.Auths.Dtos;
using Application.Features.Auths.Rules;
using Application.Services.AuthService;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.Hashing;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auths.Command.Register
{
    public class RegisterCommand : IRequest<RegisteredDto>
    {
        public UserForRegisterDto UserForRegisterDto { get; set; }
        public string IpAddress { get; set; }

        public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisteredDto>
        {
            private readonly AuthBusinessRules _authBusinessRules;
            private readonly IAuthService _authService;
            private readonly IUserRepository _userRepository;
            private readonly IMapper _mapper;

            public RegisterCommandHandler(AuthBusinessRules authBusinessRules, IAuthService authService, IUserRepository userRepository, IMapper mapper)
            {
                _authBusinessRules = authBusinessRules;
                _authService = authService;
                _userRepository = userRepository;
                _mapper = mapper;
            }

            public async Task<RegisteredDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
            {
                await _authBusinessRules.EmailCannotBeDuplicated(request.UserForRegisterDto.Email);
                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash(request.UserForRegisterDto.Password, out passwordHash, out passwordSalt);

                User user = new()
                {
                    Email = request.UserForRegisterDto.Email,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    FirstName = request.UserForRegisterDto.FirstName,
                    LastName = request.UserForRegisterDto.LastName,
                    Status = true
                };

                User createdUser = await _userRepository.AddAsync(user);


                var accessToken = await _authService.CreateAccessToken(createdUser);
                var refreshToken = await _authService.CreateRefreshToken(createdUser, request.IpAddress);
                var addedToken = await _authService.AddRefreshToken(refreshToken);

                RegisteredDto registeredDto = new()
                {
                    RefreshToken = addedToken,
                    AccessToken = accessToken
                };

                return registeredDto;
            }
        }
    }
}
