using ITL.Domain.Entities;
using ITL.Domain.Repositories;
using ITL.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace ITL.Infrastructure.Repositories;

public class PermissionTypeRepository : Repository<PermissionType>, IPermissionTypeRepository
{
    public PermissionTypeRepository(KCTestContext context) : base(context.PermissionTypes)
    {
    }
}
