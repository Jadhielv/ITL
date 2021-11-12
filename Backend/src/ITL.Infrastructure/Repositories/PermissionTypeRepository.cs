using KCTest.Domain.Entities;
using KCTest.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace KCTest.Infrastructure.Repositories
{
    public class PermissionTypeRepository : Repository<PermissionType>, IPermissionTypeRepository
    {
        public PermissionTypeRepository(DbSet<PermissionType> permissionTypes) : base(permissionTypes)
        {
        }
    }
}