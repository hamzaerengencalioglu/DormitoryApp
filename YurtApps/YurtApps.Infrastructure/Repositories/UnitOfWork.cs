using YurtApps.Application.Interfaces;
using YurtApps.Domain.IRepositories;

namespace YurtApps.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _appDbContext;
        private readonly Dictionary<Type, object> _repositories = new();

        public UnitOfWork(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<int> CommitAsync()
        {
            return await _appDbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _appDbContext.Dispose();
        }

        public IEFRepository<T> Repository<T>() where T : class
        {
            var type = typeof(T);
            if (!_repositories.ContainsKey(type))
            {
                var repoInstance = new EFRepository<T>(_appDbContext);
                _repositories.Add(type, repoInstance);
            }

            return (IEFRepository<T>)_repositories[type];
        }
    }
}