using KCTest.Domain;
using KCTest.Domain.Repositories;
using KCTest.Infrastructure.Database;
using System.Threading.Tasks;

namespace KCTest.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        public IPermissionRepository PermissionRepository { get; set; }
        public IPermissionTypeRepository PermissionTypeRepository { get; set; }

        private readonly KCTestContext _kCTestContext;

        public UnitOfWork(KCTestContext kCTestContext, IPermissionRepository permissionRepository, IPermissionTypeRepository permissionTypeRepository)
        {
            _kCTestContext = kCTestContext;

            PermissionRepository = permissionRepository;
            PermissionTypeRepository = permissionTypeRepository;
        }

        public async Task<int> SaveAsync() => await _kCTestContext.SaveChangesAsync();

        public void Dispose() => _kCTestContext.Dispose();
    }
}