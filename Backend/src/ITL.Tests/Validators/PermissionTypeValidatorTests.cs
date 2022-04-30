using FluentValidation.TestHelper;
using ITL.Domain.DTOs;
using ITL.Infrastructure.Validators;
using NUnit.Framework;

namespace ITL.Tests.Validators;

[TestFixture]
public class PermissionTypeValidatorTests
{
    private PermissionTypeValidator validator;

    [SetUp]
    public void Setup()
    {
        validator = new PermissionTypeValidator();
    }

    [Test]
    public void Should_have_error_when_Description_is_null()
    {
        var model = new PermissionTypeDto { Description = null };
        var result = validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(pt => pt.Description);
    }

    [Test]
    public void Should_not_have_error_when_Description_is_specified()
    {
        var model = new PermissionTypeDto { Description = "Otros" };
        var result = validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(pt => pt.Description);
    }
}
