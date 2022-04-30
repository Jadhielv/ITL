using FluentValidation;
using ITL.Domain.DTOs;

namespace ITL.Infrastructure.Validators;

public class PermissionTypeValidator : AbstractValidator<PermissionTypeDto>
{
    public PermissionTypeValidator()
    {
        RuleFor(x => x.Description).NotNull();
    }
}
