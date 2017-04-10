using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Worklio.Repositories
{
    public interface IBaseRepository<TEntity> : IDisposable where TEntity : class
    {
        Task<IList<TEntity>> GetAll();
        TEntity Get(int id);

    }
}