using ForumWebApi.Data.Interfaces;
using ForumWebApi.DataTransferObject.UserDto;
using ForumWebApi.Models;
using ForumWebApi.services.CommentService;
using ForumWebApi.services.UserService;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumWebApi.ServiceTest
{
    public class UserServiceTest
    {
        [Test]
        [Category("Users")]
        public void GetAll_ReturnsListOfUsers()
        {
            // Arrange
            var actualUsersList = new List<User> {
                new User { UserId=1, UserName= "Mixilino", role = UserRoles.Admin},
                new User { UserId=2, UserName= "Vasilev", role = UserRoles.Regular},
            };
            var expectedUsersList = new List<UserRoleResponse> {
                new UserRoleResponse { UserId=1, UserName= "Mixilino", role = UserRoles.Admin},
                new UserRoleResponse { UserId=2, UserName= "Vasilev", role = UserRoles.Regular},
            };
            var mockRepo = new Mock<IUnitOfWork>();
            mockRepo.Setup(repo => repo.UserRepository.GetAll()).Returns(actualUsersList);
            UserService us = new UserService(mockRepo.Object);

            ServiceResponse<List<UserRoleResponse>> expectedResponse = new ServiceResponse<List<UserRoleResponse>>()
            {
                Data = expectedUsersList,
                Succes = true,
                Message = "Success",
            };

            // Act
            var result = us.GetAll();

            // Assert
            Assert.AreEqual(expectedResponse, result);
            CollectionAssert.AreEqual(result.Data, expectedUsersList);

        }

        [Test]
        [Category("Users")]
        public void ChangeRole_Successful()
        {
            // Arrange
            var userChangeRequest = new UserChangeRoleRequest() { role = UserRoles.Regular, UserId = 2 };
            var userMocked = new User()
            {
                UserId = userChangeRequest.UserId,
                role = UserRoles.Regular,
                UserName = "Mixilino"
            };
            var expectedDataReturned = new UserRoleResponse()
            {
                UserName = userMocked.UserName,
                role = userMocked.role,
                UserId = userMocked.UserId
            };
            var mockRepo = new Mock<IUnitOfWork>();
            mockRepo.Setup(repo => repo.UserRepository.ChangeRole(userChangeRequest)).Returns(userMocked);
            mockRepo.Setup(repo => repo.Save()).Returns(1);
            UserService us = new UserService(mockRepo.Object);

            ServiceResponse<UserRoleResponse> expectedResponse = new ServiceResponse<UserRoleResponse>()
            {
                Data = expectedDataReturned,
                Succes = true,
                Message = "User role changed",
            };

            // Act
            var result = us.ChangeRole(userChangeRequest);

            // Assert
            Assert.AreEqual(expectedResponse, result);
            Assert.AreEqual(result.Data, expectedDataReturned);
        }

        [Test]
        [Category("Users")]
        public void ChangeRole_UserDoesNotExist()
        {
            // Arrange
            var userChangeRequest = new UserChangeRoleRequest() { role = UserRoles.Regular, UserId = 2 };

            var mockRepo = new Mock<IUnitOfWork>();
            mockRepo.Setup(repo => repo.UserRepository.ChangeRole(userChangeRequest)).Returns((User)null);
            mockRepo.Setup(repo => repo.Save()).Returns(0);
            UserService us = new UserService(mockRepo.Object);

            ServiceResponse<UserRoleResponse> expectedResponse = new ServiceResponse<UserRoleResponse>()
            {
                Data = null,
                Succes = false,
                Message = "User does not exist.",
            };

            // Act
            var result = us.ChangeRole(userChangeRequest);

            // Assert
            Assert.AreEqual(expectedResponse, result);
            Assert.IsNull(result.Data);
        }

        [Test]
        [Category("Users")]
        public void ChangeRole_ErrorSavingIntoDb()
        {
            // Arrange
            var userChangeRequest = new UserChangeRoleRequest() { role = UserRoles.Regular, UserId = 2 };
            var userMocked = new User()
            {
                UserId = userChangeRequest.UserId,
                role = UserRoles.Regular,
                UserName = "Mixilino"
            };
            var expectedDataReturned = new UserRoleResponse()
            {
                UserName = userMocked.UserName,
                role = userMocked.role,
                UserId = userMocked.UserId
            };
            var mockRepo = new Mock<IUnitOfWork>();
            mockRepo.Setup(repo => repo.UserRepository.ChangeRole(userChangeRequest)).Returns(userMocked);
            mockRepo.Setup(repo => repo.Save()).Throws(new NullReferenceException());
            UserService us = new UserService(mockRepo.Object);

            ServiceResponse<UserRoleResponse> expectedResponse = new ServiceResponse<UserRoleResponse>()
            {
                Data = null,
                Succes = false,
                Message = "User failed to update.",
            };

            // Act
            var result = us.ChangeRole(userChangeRequest);

            // Assert
            Assert.AreEqual(expectedResponse, result);
            Assert.IsNull(result.Data);
        }
    }
}
