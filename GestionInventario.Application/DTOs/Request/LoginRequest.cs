using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionInventario.Application.DTOs.Request
{
    public record LoginRequest(string Username, string Password);
}
