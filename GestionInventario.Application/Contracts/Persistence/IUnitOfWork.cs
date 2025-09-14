using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionInventario.Application.Contracts.Persistence
{
    public interface IUnitOfWork : IDisposable

    {


        IRepository<TEntity> Repository<TEntity>() where TEntity : class;


        Task<int> Complete();
        Task<ITransaction> BeginTransactionAsync();
    }
}
