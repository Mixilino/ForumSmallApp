using ForumWebApi.Data.Interfaces;
using ForumWebApi.DataTransferObject.CommentDto;
using ForumWebApi.DataTransferObject.PostCategoryDto;
using ForumWebApi.DataTransferObject.UserDto;
using ForumWebApi.Models;
using ForumWebApi.services.CommentService;
using ForumWebApi.services.PostCategoryService;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumWebApi.ServiceTest
{
    [TestFixture]
    public class PostCategoryServiceTest
    {
        [Test]
        [Category("PostCategory_Create")]
        public void Create_Successful()
        {
            // Arrange
            var newCategoryName = "Kategorija 1";
            var newPostCategory = new PostCategory() { CategoryName = newCategoryName, PcId = 1 };
            var expectedPostCategoryDto = new PostCategoryReturnDto() { PcId = newPostCategory.PcId, CategoryName = newPostCategory.CategoryName };
            var mockRepo = new Mock<IUnitOfWork>();
            mockRepo = new Mock<IUnitOfWork>();
            mockRepo.Setup(repo => repo.PostCategoryRepository.Add(newCategoryName)).Returns(newPostCategory);
            mockRepo.Setup(repo => repo.Save()).Returns(1);
            PostCategoryService cs = new PostCategoryService(mockRepo.Object);
            ServiceResponse<PostCategoryReturnDto> expectedResponse = new ServiceResponse<PostCategoryReturnDto>()
            {
                Data = expectedPostCategoryDto,
                Succes = true,
                Message = "Category created",
            };
            // Act
            var result = cs.Create(newCategoryName);

            // Assert
            Assert.AreEqual(result, expectedResponse);
            Assert.AreEqual(result.Data, expectedPostCategoryDto);
        }
        [Test]
        [Category("PostCategory_Create")]
        public void Create_SavingThrowsError()
        {
            // Arrange
            var newCategoryName = "Kategorija 1";
            var newPostCategory = new PostCategory() { CategoryName = newCategoryName, PcId = 1 };
            var mockRepo = new Mock<IUnitOfWork>();
            mockRepo = new Mock<IUnitOfWork>();
            mockRepo.Setup(repo => repo.PostCategoryRepository.Add(newCategoryName)).Returns(newPostCategory);
            mockRepo.Setup(repo => repo.Save()).Throws(new NullReferenceException());
            PostCategoryService cs = new PostCategoryService(mockRepo.Object);
            ServiceResponse<PostCategoryReturnDto> expectedResponse = new ServiceResponse<PostCategoryReturnDto>()
            {
                Data = null,
                Succes = false,
                Message = "Category already exist.",
            };
            // Act
            var result = cs.Create(newCategoryName);

            // Assert
            Assert.AreEqual(result, expectedResponse);
        }

        [Test]
        [Category("PostCategory_GetAll")]
        public void GetAll_ReturnsListOfCategories()
        {
            var expectedCategoryList = new List<PostCategoryReturnDto> {
                new PostCategoryReturnDto { CategoryName = "Category 1", PcId = 1},
                new PostCategoryReturnDto { CategoryName = "Category 2", PcId = 2},
            };
            var actualCategoryList = new List<PostCategory> {
                new PostCategory { CategoryName = "Category 1", PcId = 1, Posts = new List<Post>{ } },
                new PostCategory { CategoryName = "Category 2", PcId = 2, Posts = new List<Post>{ }},
            };
            var mockRepo = new Mock<IUnitOfWork>();
            mockRepo = new Mock<IUnitOfWork>();
            mockRepo.Setup(repo => repo.PostCategoryRepository.GetAll()).Returns(actualCategoryList);
            PostCategoryService cs = new PostCategoryService(mockRepo.Object);
            ServiceResponse<List<PostCategoryReturnDto>> expectedResponse = new ServiceResponse<List<PostCategoryReturnDto>>()
            {
                Data = expectedCategoryList,
                Succes = true,
                Message = "Success",
            };
            // Act
            var result = cs.GetAll();

            // Assert
            Assert.AreEqual(expectedResponse, result);
            CollectionAssert.AreEqual(expectedCategoryList, result.Data);
        }

        [Test]
        [Category("PostCategory_Delete")]
        public void Delete_Successful()
        {
            // Arrange
            var postCategoryId = 1;
            var mockRepo = new Mock<IUnitOfWork>();
            mockRepo = new Mock<IUnitOfWork>();
            mockRepo.Setup(repo => repo.PostCategoryRepository.Delete(postCategoryId)).Returns(postCategoryId);
            mockRepo.Setup(repo => repo.Save()).Returns(postCategoryId);
            PostCategoryService cs = new PostCategoryService(mockRepo.Object);
            ServiceResponse<int?> expectedResponse = new ServiceResponse<int?>()
            {
                Data = postCategoryId,
                Succes = true,
                Message = "Succes",
            };
            // Act
            var result = cs.Delete(postCategoryId);

            // Assert
            Assert.AreEqual(result, expectedResponse);
        }

        [Test]
        [Category("PostCategory_Delete")]
        public void Delete_DeletingThrowsError()
        {
            // Arrange
            var postCategoryId = 1;
            var mockRepo = new Mock<IUnitOfWork>();
            mockRepo = new Mock<IUnitOfWork>();
            mockRepo.Setup(repo => repo.PostCategoryRepository.Delete(postCategoryId)).Returns(postCategoryId);
            mockRepo.Setup(repo => repo.Save()).Throws(new NullReferenceException());
            PostCategoryService cs = new PostCategoryService(mockRepo.Object);
            ServiceResponse<int?> expectedResponse = new ServiceResponse<int?>()
            {
                Data = null,
                Succes = false,
                Message = "Category does not exist",
            };
            // Act
            var result = cs.Delete(postCategoryId);

            // Assert
            Assert.AreEqual(result, expectedResponse);
        }

        [Test]
        [Category("PostCategory_Update")]
        public void Update_Succesful()
        {
            // Arrange
            var requestPostCategory = new PostCategoryReturnDto() { CategoryName = "test", PcId = 1 };
            var actualChangedCategory = new PostCategory() { PcId = 1, CategoryName = requestPostCategory.CategoryName };
            var expectedChangedCategory = new PostCategoryReturnDto() { CategoryName = "test", PcId = 1 };

            var mockRepo = new Mock<IUnitOfWork>();
            mockRepo = new Mock<IUnitOfWork>();
            mockRepo.Setup(repo => repo.PostCategoryRepository.Rename(requestPostCategory)).Returns(actualChangedCategory);
            mockRepo.Setup(repo => repo.Save()).Returns(1);
            PostCategoryService cs = new PostCategoryService(mockRepo.Object);
            ServiceResponse<PostCategoryReturnDto> expectedResponse = new ServiceResponse<PostCategoryReturnDto>()
            {
                Data = requestPostCategory,
                Succes = true,
                Message = "Category renamed",
            };

            // Act
            var result = cs.Update(requestPostCategory);

            // Assert
            Assert.AreEqual(expectedResponse, result);
            Assert.AreEqual(expectedChangedCategory, result.Data);

        }

        [Test]
        [Category("PostCategory_Update")]
        public void Update_SaveThrowsError()
        {
            // Arrange
            var requestPostCategory = new PostCategoryReturnDto() { CategoryName = "test", PcId = 1 };
            var actualChangedCategory = new PostCategory() { PcId = 1, CategoryName = requestPostCategory.CategoryName };
            var mockRepo = new Mock<IUnitOfWork>();
            mockRepo = new Mock<IUnitOfWork>();
            mockRepo.Setup(repo => repo.PostCategoryRepository.Rename(requestPostCategory)).Returns(actualChangedCategory);
            mockRepo.Setup(repo => repo.Save()).Throws(new Exception());
            PostCategoryService cs = new PostCategoryService(mockRepo.Object);
            ServiceResponse<PostCategoryReturnDto> expectedResponse = new ServiceResponse<PostCategoryReturnDto>()
            {
                Data = null,
                Succes = false,
                Message = "Error",
            };

            // Act
            var result = cs.Update(requestPostCategory);

            // Assert
            Assert.AreEqual(expectedResponse, result);

        }

        [Test]
        [Category("PostCategory_Update")]
        public void Update_RepoReturnsNull()
        {
            // Arrange
            var requestPostCategory = new PostCategoryReturnDto() { CategoryName = "test", PcId = 1 };
            var actualChangedCategory = new PostCategory() { PcId = 1, CategoryName = requestPostCategory.CategoryName };
            var mockRepo = new Mock<IUnitOfWork>();
            mockRepo = new Mock<IUnitOfWork>();
            mockRepo.Setup(repo => repo.PostCategoryRepository.Rename(requestPostCategory)).Returns((PostCategory)null);
            mockRepo.Setup(repo => repo.Save()).Returns(0);
            PostCategoryService cs = new PostCategoryService(mockRepo.Object);
            ServiceResponse<PostCategoryReturnDto> expectedResponse = new ServiceResponse<PostCategoryReturnDto>()
            {
                Data = null,
                Succes = false,
                Message = "Category not found",
            };

            // Act
            var result = cs.Update(requestPostCategory);

            // Assert
            Assert.AreEqual(expectedResponse, result);

        }
    }
}
