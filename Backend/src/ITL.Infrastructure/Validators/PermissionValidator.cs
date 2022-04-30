using FluentValidation;
using ITL.Domain.DTOs;

namespace ITL.Infrastructure.Validators;

public class PermissionValidator : AbstractValidator<PermissionDto>
{
    public PermissionValidator()
    {
        RuleFor(x => x.Name).NotNull();
        RuleFor(x => x.LastName).NotNull();
        RuleFor(x => x.PermissionType).NotNull();
        RuleFor(x => x.PermissionType).SetValidator(new PermissionTypeValidator());
        RuleFor(x => x.Date).NotNull();
    }
}
