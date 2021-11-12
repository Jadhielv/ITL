using System;

namespace KCTest.Domain.DTOs
{
    public class PermissionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public PermissionTypeDto PermissionType { get; set; }
        public DateTime? Date { get; set; }
    }
}