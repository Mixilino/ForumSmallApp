using ForumWebApi.Data.Interfaces;
using ForumWebApi.DataTransferObject.UserDto;
using ForumWebApi.Models;
using ForumWebApi.services.PostService;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumWebApi.ServiceTest
{
    public class PostServiceTest
    {
        [Test]
        public void GetAll_ReturnsValidResponse()
        {
            // Arrange
            var userDto = new UserResponseDto { UserId = 1 };
            var commentList = new List<Comment> {
                new Comment {
                            CommentId = 1,
                            CommentText = "Comment 1",
                            User = new User { UserName = "User2", UserId = 2, role=UserRoles.Regular },
                            DateCreated = DateTime.Now
                        }
            };
            var postCategoryList = new List<PostCategory> {
                new PostCategory { PcId = 1, CategoryName = "Category1" },
                new PostCategory { PcId = 2, CategoryName = "Category2" }
            };
            var voteList = new List<Vote> {
                new Vote { User = new User { UserName = "User2", UserId = 2 }, UpVote = true },
                new Vote { User = new User { UserName = "User3", UserId = 3 }, UpVote = false }
            };
            var posts = new List<Post> {
                new Post {
                    UserId= 1,
                    PostTitle = "Test Post 1",
                    PostText = "Lorem ipsum dolor sit amet",
                    User = new User { UserName = "User1", UserId = 1, role= UserRoles.Admin },
                    PostCategories = postCategoryList,
                    Comments = commentList,
                    Votes = voteList,
                    PostId= 1,
                },
                new Post {
                    PostTitle = "Test Post 2",
                    PostText = "Post text 2",
                    User = new User { UserName = "User2", UserId = 2, role= UserRoles.Regular },
                    UserId = 2,
                },
                new Post {
                PostTitle = "Test Post 3",
                PostText = "Post text 3",
                    User = new User { UserName = "User3", UserId = 3, role= UserRoles.Banned },
                UserId = 3
            }
            };
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(uow => uow.PostRepository.GetAll()).Returns(posts);
            mockUnitOfWork.Setup(uow => uow.UserRepository.GetById(userDto.UserId)).Returns(new User { UserId = userDto.UserId, role = UserRoles.Admin });
            var service = new PostService(mockUnitOfWork.Object);

            // Act
            var response = service.GetAll(userDto);
            Console.WriteLine(response.Data);
            // Assert
            Assert.IsTrue(response.Succes);
            Assert.AreEqual("Success", response.Message);
            Assert.AreEqual(3, response.Data.Count);
            var postResponseDto = response.Data.First();
            Assert.AreEqual("Test Post 1", postResponseDto.PostTitle);
            Assert.AreEqual("Lorem ipsum dolor sit amet", postResponseDto.PostText);
            Assert.AreEqual("User1", postResponseDto.User.UserName);
            Assert.AreEqual(1, postResponseDto.User.UserId);
            Assert.AreEqual(1, postResponseDto.Comments.Count);

        }
    }
}
