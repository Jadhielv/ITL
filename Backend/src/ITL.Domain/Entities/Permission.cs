using System;

namespace ITL.Domain.Entities;

public class Permission : BaseEntity
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public PermissionType PermissionType { get; set; }
    public DateTime Date { get; set; }
}
