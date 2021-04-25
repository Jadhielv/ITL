using AutoMapper;
using KCTest.Application.Services;
using KCTest.Domain;
using KCTest.Domain.DTOs;
using KCTest.Domain.Entities;
using KCTest.Domain.Exceptions;
using KCTest.Domain.Repositories;
using Moq;
using NUnit.Framework;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace KCTest.Tests.Services
{
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
    }
}