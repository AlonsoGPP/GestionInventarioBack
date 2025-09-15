using GestionInventario.Application.Contracts.Persistence;
using GestionInventario.Application.Contracts.Security;
using GestionInventario.Application.Util;
using GestionInventario.Domain.DTOs.Base;
using GestionInventario.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionInventario.Application.Features.Auth.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, ResponseBase>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtGenerator _tokenService;
        private readonly ILogger<LoginQueryHandler> _logger;

        public LoginQueryHandler(
            IUnitOfWork unitOfWork,
            IPasswordHasher passwordHasher,
            IJwtGenerator tokenService,
            ILogger<LoginQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
            _logger = logger;
        }

        public async Task<ResponseBase> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var userRepo = _unitOfWork.Repository<User>();
                var user = (await userRepo.GetAsync(u => u.Username.Value == request.Username))
                           .FirstOrDefault();

                if (user == null)
                    return ResponseUtil.NotFoundRequest("Usuario no encontrado");

                if (!user.IsActive)
                    return ResponseUtil.BadRequest("Usuario inactivo");

                if (!_passwordHasher.VerifyPassword(request.Password, user.PasswordHash.Value))
                    return ResponseUtil.BadRequest("Credenciales inválidas");

                var token = _tokenService.GenerateToken(
                    user.Id,
                    user.Username.Value,
                    user.Email.Value,
                    user.Role.ToString()
                );

                var payload = new
                {
                    user.Id,
                    Username = user.Username.Value,
                    Email = user.Email.Value,
                    Role = user.Role.ToString(),
                    Token = token
                };

                return ResponseUtil.OK(payload, "Login exitoso");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en login para usuario {Username}", request.Username);
                return ResponseUtil.InternalError("Ocurrió un error durante el login");
            }
        }
    }
}
