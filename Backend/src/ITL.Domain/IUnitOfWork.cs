using ITL.Domain.Repositories;
using System;
using System.Threading.Tasks;

namespace ITL.Domain;

public interface IUnitOfWork : IDisposable
{
    IPermissionRepository PermissionRepository { get; }
    IPermissionTypeRepository PermissionTypeRepository { get; }
    Task<int> SaveAsync();
}
