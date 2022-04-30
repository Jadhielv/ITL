using ITL.Domain.Entities;
using ITL.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ITL.Infrastructure.Repositories;

public class PermissionRepository : Repository<Permission>, IPermissionRepository
{
    public PermissionRepository(DbSet<Permission> permissions) : base (permissions)
    {
    }
}
