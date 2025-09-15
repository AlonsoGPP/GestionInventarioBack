using GestionInventario.Domain.DTOs.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionInventario.Application.Features.Auth.Queries.Login
{
    public class LoginQuery : IRequest<ResponseBase>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
