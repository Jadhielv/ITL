using FluentValidation;
using KCTest.Domain.DTOs;

namespace KCTest.Infrastructure.Validators
{
    public class PermissionTypeValidator : AbstractValidator<PermissionTypeDto>
    {
        public PermissionTypeValidator()
        {
            RuleFor(x => x.Description).NotNull();
        }
    }
}