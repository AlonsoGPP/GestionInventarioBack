using GestionInventario.Application.Contracts.Persistence;
using GestionInventario.Application.Contracts.Security;
using GestionInventario.Infrastructure.Persistance;
using GestionInventario.Infrastructure.Persistence.Extension;
using GestionInventario.Infrastructure.Repositories;
using GestionInventario.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GestionInventario.Infrastructure.IoC
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {

            string? dbConnection = ConnectionStringExtension.GetConnectionString(configuration);
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(dbConnection));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IJwtGenerator, JwtGenerator>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            //repositories personalizados aqui


            return services;
        }
    }
}
