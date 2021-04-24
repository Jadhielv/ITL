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
    }
}