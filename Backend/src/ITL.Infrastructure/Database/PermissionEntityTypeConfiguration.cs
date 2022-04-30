using ITL.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace ITL.Infrastructure.Database;

public class PermissionEntityTypeConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.Property(p => p.Name).IsRequired();
        builder.Property(p => p.LastName).IsRequired();
        builder.Property(p => p.Date).IsRequired();
        builder.Property(p => p.Date).HasDefaultValue(DateTime.Now);
    }
}
