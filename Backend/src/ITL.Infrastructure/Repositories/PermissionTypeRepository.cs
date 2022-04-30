using ITL.Domain.Entities;
using ITL.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ITL.Infrastructure.Repositories;

public class PermissionTypeRepository : Repository<PermissionType>, IPermissionTypeRepository
{
    public PermissionTypeRepository(DbSet<PermissionType> permissionTypes) : base(permissionTypes)
    {
    }
}
