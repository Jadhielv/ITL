using AutoMapper;
using ITL.Application.Services;
using ITL.Domain;
using ITL.Domain.DTOs;
using ITL.Domain.Entities;
using ITL.Domain.Exceptions;
using ITL.Domain.Repositories;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ITL.Tests.Services;

public class PermissionServiceTests
{
    [Test]
    public void UpdatePermission_PermissionNotFound_Should_ThrowsConflictException()
    {
        // Arrange
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var permissionRepositoryMock = new Mock<IPermissionRepository>();

        var permission = new PermissionDto
        {
            Id = 1,
            Name = "Permission 1"
        };

        var service = new PermissionService(unitOfWorkMock.Object, null);

        permissionRepositoryMock.Setup(v => v.ExistAsync(It.IsAny<Expression<Func<Permission, bool>>>()))
            .ReturnsAsync(false);

        unitOfWorkMock.Setup(v => v.PermissionRepository)
            .Returns(permissionRepositoryMock.Object);

        // Act => Assert
        Assert.ThrowsAsync<ConflictException>(() => service.UpdatePermission(permission), "The permission doesn't exist");
    }

    [Test]
    public void UpdatePermission_PermissionTypeNotFound_Should_ThrowsConflictException()
    {
        // Arrange
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var permissionRepositoryMock = new Mock<IPermissionRepository>();
        var permissionTypeRepositoryMock = new Mock<IPermissionTypeRepository>();

        var permission = new PermissionDto
        {
            Id = 1,
            Name = "Permission 1",
            PermissionType = new PermissionTypeDto
            {
                Id = 2,
                Description = "PermissionType 1"
            }
        };

        var service = new PermissionService(unitOfWorkMock.Object, null);

        permissionRepositoryMock.Setup(v => v.ExistAsync(It.IsAny<Expression<Func<Permission, bool>>>()))
            .ReturnsAsync(true);

        permissionTypeRepositoryMock.Setup(v => v.ExistAsync(It.IsAny<Expression<Func<PermissionType, bool>>>()))
            .ReturnsAsync(false);

        unitOfWorkMock.Setup(v => v.PermissionRepository)
            .Returns(permissionRepositoryMock.Object);

        unitOfWorkMock.Setup(v => v.PermissionTypeRepository)
            .Returns(permissionTypeRepositoryMock.Object);

        // Act => Assert
        Assert.ThrowsAsync<ConflictException>(() => service.UpdatePermission(permission), "The permission type doesn't exist.");
    }

    [Test]
    public async Task UpdatePermission_PermissionAndPermissionTypeFound_Should_UpdatePermission()
    {
        // Arrange
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var permissionRepositoryMock = new Mock<IPermissionRepository>();
        var permissionTypeRepositoryMock = new Mock<IPermissionTypeRepository>();
        var mapperMock = new Mock<IMapper>();

        var permission = new PermissionDto
        {
            Id = 1,
            Name = "Permission 1",
            PermissionType = new PermissionTypeDto
            {
                Id = 2,
                Description = "PermissionType 1"
            }
        };

        var service = new PermissionService(unitOfWorkMock.Object, mapperMock.Object);

        permissionRepositoryMock.Setup(v => v.ExistAsync(It.IsAny<Expression<Func<Permission, bool>>>()))
            .ReturnsAsync(true);

        permissionTypeRepositoryMock.Setup(v => v.ExistAsync(It.IsAny<Expression<Func<PermissionType, bool>>>()))
            .ReturnsAsync(true);

        unitOfWorkMock.Setup(v => v.PermissionRepository)
            .Returns(permissionRepositoryMock.Object);

        unitOfWorkMock.Setup(v => v.PermissionTypeRepository)
            .Returns(permissionTypeRepositoryMock.Object);

        // Act
        await service.UpdatePermission(permission);

        // Assert
        permissionRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Permission>()));
        unitOfWorkMock.Verify(x => x.SaveAsync());
    }

    [Test]
    public void GetPermission_PermissionNotFound_Should_ThrowsNotFoundException()
    {
        // Arrange
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var permissionRepositoryMock = new Mock<IPermissionRepository>();

        var service = new PermissionService(unitOfWorkMock.Object, null);

        permissionRepositoryMock.Setup(v => v.ExistAsync(It.IsAny<Expression<Func<Permission, bool>>>()))
            .ReturnsAsync(false);

        unitOfWorkMock.Setup(v => v.PermissionRepository)
            .Returns(permissionRepositoryMock.Object);

        // Act => Assert
        Assert.ThrowsAsync<NotFoundException>(() => service.GetPermission(1), "The permission doesn't exist");
    }

    [Test]
    public async Task GetPermission_PermissionFound_Should_ReturnsPermission()
    {
        // Arrange
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var permissionRepositoryMock = new Mock<IPermissionRepository>();
        var mapperMock = new Mock<IMapper>();
        const int permissionId = 1;

        var permission = new Permission
        {
            Id = permissionId,
            Name = "Permission1"
        };

        var permissionDto = new PermissionDto();

        var service = new PermissionService(unitOfWorkMock.Object, mapperMock.Object);

        permissionRepositoryMock.Setup(v => v.ExistAsync(It.IsAny<Expression<Func<Permission, bool>>>()))
            .ReturnsAsync(true);

        permissionRepositoryMock.Setup(x => x.GetByIdAsync(permissionId, It.IsAny<IEnumerable<string>>()))
            .ReturnsAsync(permission);

        mapperMock.Setup(x => x.Map<PermissionDto>(permission))
            .Returns(permissionDto);

        unitOfWorkMock.Setup(v => v.PermissionRepository)
            .Returns(permissionRepositoryMock.Object);

        // Act 
        var result = await service.GetPermission(permissionId);

        // Assert
        Assert.AreEqual(permissionDto, result);
    }

    [Test]
    public void DeletePermission_PermissionNotFound_Should_ThrowsNotFoundException()
    {
        // Arrange
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var permissionRepositoryMock = new Mock<IPermissionRepository>();

        var service = new PermissionService(unitOfWorkMock.Object, null);

        permissionRepositoryMock.Setup(v => v.ExistAsync(It.IsAny<Expression<Func<Permission, bool>>>()))
            .ReturnsAsync(false);

        unitOfWorkMock.Setup(v => v.PermissionRepository)
            .Returns(permissionRepositoryMock.Object);

        // Act => Assert
        Assert.ThrowsAsync<NotFoundException>(() => service.DeletePermission(1), "The permission doesn't exist");
    }

    [Test]
    public async Task DeletePermission_PermissionFound_Should_DeletePermission()
    {
        // Arrange
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var permissionRepositoryMock = new Mock<IPermissionRepository>();
        var mapperMock = new Mock<IMapper>();
        const int permissionId = 1;

        var permission = new Permission
        {
            Id = permissionId,
            Name = "Permission1"
        };

        var service = new PermissionService(unitOfWorkMock.Object, mapperMock.Object);

        permissionRepositoryMock.Setup(v => v.ExistAsync(It.IsAny<Expression<Func<Permission, bool>>>()))
            .ReturnsAsync(true);

        permissionRepositoryMock.Setup(x => x.GetByIdAsync(permissionId, It.IsAny<IEnumerable<string>>()))
            .ReturnsAsync(permission);

        unitOfWorkMock.Setup(v => v.PermissionRepository)
            .Returns(permissionRepositoryMock.Object);

        // Act 
        await service.DeletePermission(permissionId);

        // Assert
        permissionRepositoryMock.Verify(x => x.DeleteAsync(permission));
        unitOfWorkMock.Verify(x => x.SaveAsync());
    }
}
