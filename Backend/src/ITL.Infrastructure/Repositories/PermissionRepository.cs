using ITL.Domain.Entities;
using ITL.Domain.Repositories;
using ITL.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace ITL.Infrastructure.Repositories;

public class PermissionRepository : Repository<Permission>, IPermissionRepository
{
    public PermissionRepository(KCTestContext context) : base(context.Permissions)
    {
    }
}
