using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YurtApps.Domain.IRepositories;

namespace YurtApps.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IEFRepository<T> Repository<T>() where T : class;

        Task<int> CommitAsync();
    }
}
