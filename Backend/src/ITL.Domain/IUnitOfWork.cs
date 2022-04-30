using ITL.Domain.Repositories;
using System;
using System.Threading.Tasks;

namespace ITL.Domain;

public interface IUnitOfWork : IDisposable
{
    IPermissionRepository PermissionRepository { get; set; }
    IPermissionTypeRepository PermissionTypeRepository { get; set; }
    Task<int> SaveAsync();
}
