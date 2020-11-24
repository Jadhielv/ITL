using KCTest.Domain;
using KCTest.Domain.Repositories;
using KCTest.Infrastructure.Database;
using KCTest.Infrastructure.Repositories;
using System.Threading.Tasks;

namespace KCTest.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        public IPermissionRepository PermissionRepository { get; set; }
        public IPermissionTypeRepository PermissionTypeRepository { get; set; }

        private readonly KCTestContext _kCTestContext;

        public UnitOfWork(KCTestContext kCTestContext)
        {
            _kCTestContext = kCTestContext;

            PermissionRepository = new PermissionRepository(_kCTestContext.Permissions);
            PermissionTypeRepository = new PermissionTypeRepository(_kCTestContext.PermissionTypes);
        }

        public async Task<int> SaveAsync() => await _kCTestContext.SaveChangesAsync();

        public void Dispose() => _kCTestContext.Dispose();
    }
}