using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionInventario.Infrastructure.Persistence.Extension
{
    public class ConnectionStringExtension
    {
        public static string GetConnectionString(IConfiguration configuration)
        {
            string? dbConnection = configuration["DbConnection"];
            if (string.IsNullOrEmpty(dbConnection)) throw new ArgumentException("Undefined database", nameof(configuration));
            return dbConnection;
        }
    }
}
