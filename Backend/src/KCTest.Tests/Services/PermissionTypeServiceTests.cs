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
    }
}