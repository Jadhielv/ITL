using KCTest.Domain.Repositories;
using System;
using System.Threading.Tasks;

namespace KCTest.Domain
{
    public interface IUnitOfWork : IDisposable
    {
        IPermissionRepository PermissionRepository { get; set; }
        IPermissionTypeRepository PermissionTypeRepository { get; set; }
        Task<int> SaveAsync();
    }
}