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

public class PermissionTypeServiceTests
{
    [Test]
    public void AddPermissionType_PermissionTypeAlreadyExist_Should_ThrowsConflictException()
    {
        // Arrange
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var permissionTypeRepositoryMock = new Mock<IPermissionTypeRepository>();

        var service = new PermissionTypeService(unitOfWorkMock.Object, null);

        var permissionType = new PermissionTypeDto { };

        permissionTypeRepositoryMock.Setup(x => x.ExistAsync(It.IsAny<Expression<Func<PermissionType, bool>>>()))
            .ReturnsAsync(true);

        unitOfWorkMock.Setup(x => x.PermissionTypeRepository)
            .Returns(permissionTypeRepositoryMock.Object);

        // Act => Assert
        Assert.ThrowsAsync<ConflictException>(() => service.AddPermissionType(permissionType), "The permission type already exist.");
    }

    [Test]
    public async Task AddPermissionType_PermissionTypeDoesNotExist_Should_SavePermissionType()
    {
        // Arrange
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var permissionTypeRepositoryMock = new Mock<IPermissionTypeRepository>();
        var mapperMock = new Mock<IMapper>();

        var service = new PermissionTypeService(unitOfWorkMock.Object, mapperMock.Object);

        var permissionType = new PermissionTypeDto { };

        permissionTypeRepositoryMock.Setup(x => x.ExistAsync(It.IsAny<Expression<Func<PermissionType, bool>>>()))
            .ReturnsAsync(false);

        unitOfWorkMock.Setup(x => x.PermissionTypeRepository)
            .Returns(permissionTypeRepositoryMock.Object);

        // Act
        await service.AddPermissionType(permissionType);

        // Assert
        permissionTypeRepositoryMock.Verify(x => x.AddAsync(It.IsAny<PermissionType>()));
        unitOfWorkMock.Verify(x => x.SaveAsync());
    }

    [Test]
    public void UpdatePermissionType_PermissionTypeNotFound_Should_ThrowsConflictException()
    {
        // Arrange
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var permissionTypeRepositoryMock = new Mock<IPermissionTypeRepository>();

        var service = new PermissionTypeService(unitOfWorkMock.Object, null);

        permissionTypeRepositoryMock.Setup(v => v.ExistAsync(It.IsAny<Expression<Func<PermissionType, bool>>>()))
            .ReturnsAsync(false);

        unitOfWorkMock.Setup(v => v.PermissionTypeRepository)
            .Returns(permissionTypeRepositoryMock.Object);

        var permissionType = new PermissionTypeDto();

        // Act => Assert
        Assert.ThrowsAsync<ConflictException>(() => service.UpdatePermissionType(permissionType), "The permission type doesn't exist.");
    }

    [Test]
    public async Task UpdatePermissionType_PermissionTypeFound_Should_UpdateAndSaveChanges()
    {
        // Arrange
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var permissionTypeRepositoryMock = new Mock<IPermissionTypeRepository>();
        var mapperMock = new Mock<IMapper>();

        var service = new PermissionTypeService(unitOfWorkMock.Object, mapperMock.Object);

        permissionTypeRepositoryMock.Setup(v => v.ExistAsync(It.IsAny<Expression<Func<PermissionType, bool>>>()))
            .ReturnsAsync(true);

        unitOfWorkMock.Setup(v => v.PermissionTypeRepository)
            .Returns(permissionTypeRepositoryMock.Object);

        var permissionType = new PermissionTypeDto();

        // Act
        await service.UpdatePermissionType(permissionType);

        // Assert
        permissionTypeRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<PermissionType>()));
        unitOfWorkMock.Verify(x => x.SaveAsync());
    }

    [Test]
    public void DeletePermissionType_PermissionTypeNotFound_Should_ThrowsNotFoundException()
    {
        // Arrange
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var permissionTypeRepositoryMock = new Mock<IPermissionTypeRepository>();

        var service = new PermissionTypeService(unitOfWorkMock.Object, null);

        permissionTypeRepositoryMock.Setup(v => v.ExistAsync(It.IsAny<Expression<Func<PermissionType, bool>>>()))
            .ReturnsAsync(false);

        unitOfWorkMock.Setup(v => v.PermissionTypeRepository)
            .Returns(permissionTypeRepositoryMock.Object);

        // Act => Assert
        Assert.ThrowsAsync<NotFoundException>(() => service.DeletePermissionType(1), "The permission type doesn't exist.");
    }

    [Test]
    public async Task DeletePermissionType_PermissionTypeFound_Should_DeletePermissionType()
    {
        // Arrange
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var permissionTypeRepositoryMock = new Mock<IPermissionTypeRepository>();
        var mapperMock = new Mock<IMapper>();
        const int permissionTypeId = 1;

        var permissionType = new PermissionType();

        var service = new PermissionTypeService(unitOfWorkMock.Object, mapperMock.Object);

        permissionTypeRepositoryMock.Setup(v => v.ExistAsync(It.IsAny<Expression<Func<PermissionType, bool>>>()))
            .ReturnsAsync(true);

        permissionTypeRepositoryMock.Setup(x => x.GetByIdAsync(permissionTypeId, It.IsAny<IEnumerable<string>>()))
            .ReturnsAsync(permissionType);

        unitOfWorkMock.Setup(v => v.PermissionTypeRepository)
            .Returns(permissionTypeRepositoryMock.Object);

        // Act 
        await service.DeletePermissionType(permissionTypeId);

        // Assert
        permissionTypeRepositoryMock.Verify(x => x.DeleteAsync(permissionType));
        unitOfWorkMock.Verify(x => x.SaveAsync());
    }

    [Test]
    public void GetPermissionType_PermissionTypeNotFound_Should_ThrowsNotFoundException()
    {
        // Arrange
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var permissionTypeRepositoryMock = new Mock<IPermissionTypeRepository>();

        var service = new PermissionTypeService(unitOfWorkMock.Object, null);

        permissionTypeRepositoryMock.Setup(v => v.ExistAsync(It.IsAny<Expression<Func<PermissionType, bool>>>()))
            .ReturnsAsync(false);

        unitOfWorkMock.Setup(v => v.PermissionTypeRepository)
            .Returns(permissionTypeRepositoryMock.Object);

        // Act => Assert
        Assert.ThrowsAsync<NotFoundException>(() => service.GetPermissionType(1), "The permission type doesn't exist.");
    }

    [Test]
    public async Task GetPermissionType_PermissionTypeFound_Should_ReturnsPermissionType()
    {
        // Arrange
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var permissionRepositoryMock = new Mock<IPermissionTypeRepository>();
        var mapperMock = new Mock<IMapper>();
        const int permissionTypeId = 1;

        var permission = new PermissionType
        {
            Id = permissionTypeId,
            Description = "PermissionType1"
        };

        var permissionTypeDto = new PermissionTypeDto();

        var service = new PermissionTypeService(unitOfWorkMock.Object, mapperMock.Object);

        permissionRepositoryMock.Setup(v => v.ExistAsync(It.IsAny<Expression<Func<PermissionType, bool>>>()))
            .ReturnsAsync(true);

        permissionRepositoryMock.Setup(x => x.GetByIdAsync(permissionTypeId, It.IsAny<IEnumerable<string>>()))
            .ReturnsAsync(permission);

        mapperMock.Setup(x => x.Map<PermissionTypeDto>(permission))
            .Returns(permissionTypeDto);

        unitOfWorkMock.Setup(v => v.PermissionTypeRepository)
            .Returns(permissionRepositoryMock.Object);

        // Act 
        var result = await service.GetPermissionType(permissionTypeId);

        // Assert
        Assert.AreEqual(permissionTypeDto, result);
    }
}
