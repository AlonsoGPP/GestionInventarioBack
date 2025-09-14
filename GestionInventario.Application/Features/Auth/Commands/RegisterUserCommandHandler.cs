using GestionInventario.Application.Contracts.Persistence;
using GestionInventario.Application.Contracts.Security;
using GestionInventario.Application.Util;
using GestionInventario.Domain.DTOs.Base;
using GestionInventario.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionInventario.Application.Features.Auth.Commands
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, ResponseBase>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;

        public RegisterUserCommandHandler(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
        }

        public async Task<ResponseBase> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var repo = _unitOfWork.Repository<User>();

                var existsByUsername = (await repo.GetAsync(u => u.Username.Value == request.Username)).Any();
                if (existsByUsername)
                    return ResponseUtil.Conflict(null, "El username ya está en uso");

                var existsByEmail = (await repo.GetAsync(u => u.Email.Value == request.Email)).Any();
                if (existsByEmail)
                    return ResponseUtil.Conflict(null, "El email ya está registrado");

                var hashedPassword = _passwordHasher.HashPassword(request.Password);

                var user = User.Create(request.Username, request.Email, hashedPassword, request.Role);

                repo.AddEntity(user);
                await _unitOfWork.Complete();

                var payload = new
                {
                    user.Id,
                    Username = user.Username.Value,
                    Email = user.Email.Value,
                    Role = user.Role.ToString()
                };

                return ResponseUtil.Created(payload, "Usuario registrado correctamente");
            }
            catch (Exception ex)
            {
                return ResponseUtil.InternalError($"Error al registrar usuario: {ex.Message}");
            }
        }
    }
}
