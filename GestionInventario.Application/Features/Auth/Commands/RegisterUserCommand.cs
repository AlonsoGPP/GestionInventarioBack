using GestionInventario.Domain.DTOs.Base;
using GestionInventario.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionInventario.Application.Features.Auth.Commands
{
    public class RegisterUserCommand : IRequest<ResponseBase>
    {
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!; 
        public UserRole Role { get; set; } = UserRole.Employee;
    }
}
