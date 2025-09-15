using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionInventario.Application.Contracts.Security
{
    public interface IJwtGenerator
    {
        string GenerateToken(Guid userId, string username, string email, string role);
    }
}
