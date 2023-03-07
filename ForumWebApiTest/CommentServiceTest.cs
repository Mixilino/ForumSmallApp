using Castle.Core.Resource;
using ForumWebApi.Data.Interfaces;
using ForumWebApi.Data.UnitOfWork;
using ForumWebApi.DataTransferObject.CommentDto;
using ForumWebApi.DataTransferObject.UserDto;
using ForumWebApi.Models;
using ForumWebApi.services.CommentService;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumWebApi
{
    [TestFixture]
    public class CommentServiceTest
    {
        [Test]
        [Category("Change")]
        public void Change_UnSuccessfulCommentIsNull()
        {
            // Arrange

            var mockRepo = new Mock<IUnitOfWork>();
            var commentDto = new CommentChangeDto(1, "Novi tekst");
            var userResponseDto = new UserResponseDto(1, "mixilino");
            mockRepo = new Mock<IUnitOfWork>();
            mockRepo.Setup(repo => repo.CommentRepository.Change(commentDto, userResponseDto)).Returns((Comment)null);
            mockRepo.Setup(repo => repo.Save()).Returns(0);
            CommentService cs = new CommentService(mockRepo.Object);
            ServiceResponse<CommentResponseDto> expectedResponse = new ServiceResponse<CommentResponseDto>()
            {
                Data = null,
                Succes = false,
                Message = "Comment does not exist.",
            };
            // Act
            var result = cs.Change(commentDto, userResponseDto);

            // Assert
            Assert.AreEqual(result, expectedResponse);
        }

        [Test]
        [Category("Change")]
        public void Change_Successful()
        {
            // Arrange
            var commentDto = new CommentChangeDto(1, "Novi tekst");
            var userResponseDto = new UserResponseDto(2, "mixilino");
            var timeNow = new DateTime();
            var com = new Comment
            {
                PostId = 1,
                UserId = userResponseDto.UserId,
                CommentId = commentDto.CommentId,
                CommentText = commentDto.CommentText,
                DateCreated = timeNow,
                User = new User { UserName = userResponseDto.UserName, UserId = userResponseDto.UserId}
            };
            var mockRepo = new Mock<IUnitOfWork>();
            mockRepo.Setup(repo => repo.CommentRepository.Change(commentDto, userResponseDto))
                .Returns(com);
            mockRepo.Setup(repo => repo.Save()).Returns(1);
            CommentService cs = new CommentService(mockRepo.Object);

            ServiceResponse<CommentResponseDto> expectedResponse = new ServiceResponse<CommentResponseDto>()
            {
                Data = new CommentResponseDto { 
                    CommentId = com.CommentId, 
                    DateCreated = com.DateCreated, 
                    PostId = com.PostId, 
                    CommentText = com.CommentText, 
                    UserId = com.UserId 
                },
                Succes = true,
                Message = "Comment updated succesfully",
            };


            // Act
            var result = cs.Change(commentDto, userResponseDto);
           

            // Assert
            Assert.AreEqual(expectedResponse, result);
        }

        [Test]
        [Category("Change")]
        public void Change_FailedError()
        {
            // Arrange
            var commentDto = new CommentChangeDto(1, "Novi tekst");
            var userResponseDto = new UserResponseDto(2, "mixilino");
            var timeNow = new DateTime();
            var com = new Comment
            {
                PostId = 1,
                UserId = userResponseDto.UserId,
                CommentId = commentDto.CommentId,
                CommentText = commentDto.CommentText,
                DateCreated = timeNow,
                };
            var mockRepo = new Mock<IUnitOfWork>();
            mockRepo.Setup(repo => repo.CommentRepository.Change(commentDto, userResponseDto))
                .Returns(com);
            mockRepo.Setup(repo => repo.Save()).Returns(1);
            CommentService cs = new CommentService(mockRepo.Object);

            ServiceResponse<CommentResponseDto> expectedResponse = new ServiceResponse<CommentResponseDto>()
            {
                Data = null,
                Succes = false,
                Message = "Comment failed to update.",
            };


            // Act
            var result = cs.Change(commentDto, userResponseDto);


            // Assert
            Assert.AreEqual(expectedResponse, result);
        }

        [Test]
        [Category("Change")]
        public void Change_UnsuccesfullInvalidUser()
        {
            // Arrange
            var commentDto = new CommentChangeDto(1, "Novi tekst");
            var userResponseDto = new UserResponseDto(2, "mixilino");
            var timeNow = new DateTime();
            var com = new Comment
            {
                PostId = 1,
                UserId = 100,
                CommentId = commentDto.CommentId,
                CommentText = commentDto.CommentText,
                DateCreated = timeNow,
                User = new User { UserName = "Invalid username", UserId = 100 }
            };
            var mockRepo = new Mock<IUnitOfWork>();
            mockRepo.Setup(repo => repo.CommentRepository.Change(commentDto, userResponseDto))
                .Returns(com);
            mockRepo.Setup(repo => repo.Save()).Returns(1);
            CommentService cs = new CommentService(mockRepo.Object);

            ServiceResponse<CommentResponseDto> expectedResponse = new ServiceResponse<CommentResponseDto>()
            {
                Data = null,
                Succes = false,
                Message = "Invalid user.",
            };


            // Act
            var result = cs.Change(commentDto, userResponseDto);


            // Assert
            Assert.AreEqual(expectedResponse, result);
        }

        [Test]
        [Category("Create")]
        public void Create_Successful()
        {
            // Arrange
            var commentDto = new CommentCreateDto() {CommentText = "Some comment", PostId = 1 };
            var userResponseDto = new UserResponseDto(2, "mixilino");
            var timeNow = new DateTime();
            var com = new Comment
            {
                PostId = commentDto.PostId,
                UserId = userResponseDto.UserId,
                CommentId = 2,
                CommentText = commentDto.CommentText,
                DateCreated = timeNow,
                User = new User { UserName = userResponseDto.UserName, UserId = userResponseDto.UserId }
            };
            var mockRepo = new Mock<IUnitOfWork>();
            mockRepo.Setup(repo => repo.CommentRepository.Add(commentDto, userResponseDto))
                .Returns(com);
            mockRepo.Setup(repo => repo.Save()).Returns(1);
            CommentService cs = new CommentService(mockRepo.Object);

            ServiceResponse<CommentResponseDto> expectedResponse = new ServiceResponse<CommentResponseDto>()
            {
                Data = new CommentResponseDto
                {
                    CommentId = com.CommentId,
                    DateCreated = com.DateCreated,
                    PostId = com.PostId,
                    CommentText = com.CommentText,
                    UserId = com.UserId
                },
                Succes = true,
                Message = "Comment created succesfully",
            };


            // Act
            var result = cs.Create(commentDto, userResponseDto);


            // Assert
            Assert.AreEqual(expectedResponse, result);
        }

        [Test]
        [Category("Create")]
        public void Create_Unuccessful()
        {
            // Arrange
            var commentDto = new CommentCreateDto() { CommentText = "Some comment", PostId = 1 };
            var userResponseDto = new UserResponseDto(2, "mixilino");
            var timeNow = new DateTime();
            var com = new Comment
            {
                PostId = commentDto.PostId,
                UserId = userResponseDto.UserId,
                CommentId = 2,
                CommentText = commentDto.CommentText,
                DateCreated = timeNow,
                User = new User { UserName = userResponseDto.UserName, UserId = userResponseDto.UserId }
            };
            var mockRepo = new Mock<IUnitOfWork>();
            mockRepo.Setup(repo => repo.CommentRepository.Add(commentDto, userResponseDto))
                .Returns(com);
            mockRepo.Setup(repo => repo.Save()).Throws(new NullReferenceException());
            CommentService cs = new CommentService(mockRepo.Object);

            ServiceResponse<CommentResponseDto> expectedResponse = new ServiceResponse<CommentResponseDto>()
            {
                Data = null,
                Succes = false,
                Message = "Error when creating comment.",
            };


            // Act
            var result = cs.Create(commentDto, userResponseDto);


            // Assert
            Assert.AreEqual(expectedResponse, result);
        }

        [Test]
        [Category("Delete")]
        public void Delete_Successful()
        {
            // Arrange
            int commentId = 2;
            var userResponseDto = new UserResponseDto(2, "mixilino");
            var timeNow = new DateTime();
            var com = new Comment
            {
                PostId = commentId,
                UserId = userResponseDto.UserId,
                CommentId = 2,
                DateCreated = timeNow,
                User = new User { UserName = userResponseDto.UserName, UserId = userResponseDto.UserId }
            };
            var mockRepo = new Mock<IUnitOfWork>();
            mockRepo.Setup(repo => repo.CommentRepository.Delete(commentId, userResponseDto))
               .Verifiable();
            mockRepo.Setup(repo => repo.Save()).Returns(1);
            CommentService cs = new CommentService(mockRepo.Object);

            ServiceResponse<bool> expectedResponse = new ServiceResponse<bool>()
            {
                Data = true,
                Succes = true,
                Message = "Comment deleted succesfully",
            };


            // Act
            var result = cs.Delete(commentId, userResponseDto);


            // Assert
            Assert.AreEqual(expectedResponse, result);
        }

        [Test]
        [Category("Delete")]
        public void Delete_CommentDoesNotExist()
        {
            // Arrange
            int commentId = 2;
            var userResponseDto = new UserResponseDto(2, "mixilino");
            var timeNow = new DateTime();
            var com = new Comment
            {
                PostId = commentId,
                UserId = userResponseDto.UserId,
                CommentId = 2,
                DateCreated = timeNow,
                User = new User { UserName = userResponseDto.UserName, UserId = userResponseDto.UserId }
            };
            var mockRepo = new Mock<IUnitOfWork>();
            mockRepo.Setup(repo => repo.CommentRepository.Delete(commentId, userResponseDto))
               .Verifiable();
            mockRepo.Setup(repo => repo.Save()).Returns(0);
            CommentService cs = new CommentService(mockRepo.Object);

            ServiceResponse<bool> expectedResponse = new ServiceResponse<bool>()
            {
                Data = false,
                Succes = false,
                Message = "Comment does not exist.",
            };


            // Act
            var result = cs.Delete(commentId, userResponseDto);


            // Assert
            Assert.AreEqual(expectedResponse, result);
        }

        [Test]
        [Category("Delete")]
        public void Delete_CommentFailedToUpdate()
        {
            // Arrange
            int commentId = 2;
            var userResponseDto = new UserResponseDto(2, "mixilino");
            var timeNow = new DateTime();
            var com = new Comment
            {
                PostId = commentId,
                UserId = userResponseDto.UserId,
                CommentId = 2,
                DateCreated = timeNow,
                User = new User { UserName = userResponseDto.UserName, UserId = userResponseDto.UserId }
            };
            var mockRepo = new Mock<IUnitOfWork>();
            mockRepo.Setup(repo => repo.CommentRepository.Delete(commentId, userResponseDto))
               .Verifiable();
            mockRepo.Setup(repo => repo.Save()).Throws(new NullReferenceException());
            CommentService cs = new CommentService(mockRepo.Object);

            ServiceResponse<bool> expectedResponse = new ServiceResponse<bool>()
            {
                Data = false,
                Succes = false,
                Message = "Comment failed to delete.",
            };


            // Act
            var result = cs.Delete(commentId, userResponseDto);


            // Assert
            Assert.AreEqual(expectedResponse, result);
        }

    }
}
