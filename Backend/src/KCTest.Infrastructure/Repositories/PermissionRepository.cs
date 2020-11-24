using KCTest.Domain.Entities;
using KCTest.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace KCTest.Infrastructure.Repositories
{
    public class PermissionRepository : Repository<Permission>, IPermissionRepository
    {
        public PermissionRepository(DbSet<Permission> permissions) : base (permissions)
        {
        }
    }
}