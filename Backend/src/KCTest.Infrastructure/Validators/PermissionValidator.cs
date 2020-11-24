using FluentValidation;
using KCTest.Domain.DTOs;

namespace KCTest.Infrastructure.Validators
{
    public class PermissionValidator : AbstractValidator<PermissionDto>
    {
        public PermissionValidator(IValidator<PermissionTypeDto> permissionTypeValidator)
        {
            RuleFor(x => x.Name).NotNull();
            RuleFor(x => x.LastName).NotNull();
            RuleFor(x => x.PermissionType).NotNull();
            RuleFor(x => x.PermissionType).SetValidator(permissionTypeValidator);
            RuleFor(x => x.Date).NotNull();
        }
    }
}