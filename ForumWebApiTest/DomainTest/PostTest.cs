using ForumWebApi.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumWebApi.DomainTest
{

    [TestFixture]
    public class PostTests
    {
        [Test]
        [Category("Post")]
        public void ToString_ReturnsCorrectString()
        {
            // Arrange
            var post = new Post
            {
                PostId = 1,
                PostText = "Sample post",
                DatePosted = new DateTime(2022, 1, 1),
                UserId = 2
            };

            // Act
            var result = post.ToString();

            // Assert
            Assert.AreEqual("1 Sample post 1/1/2022 12:00:00 AM 2", result);
        }

        [Test]
        [Category("Post")]
        public void PostCategories_InitializedAsEmptyList()
        {
            // Arrange
            var post = new Post();

            // Assert
            Assert.IsNotNull(post.PostCategories);
            Assert.IsInstanceOf<List<PostCategory>>(post.PostCategories);
            Assert.AreEqual(0, post.PostCategories.Count);
        }

        [Test]
        [Category("Post")]
        public void Votes_InitializedAsEmptyList()
        {
            // Arrange
            var post = new Post();

            // Assert
            Assert.IsNotNull(post.Votes);
            Assert.IsInstanceOf<List<Vote>>(post.Votes);
            Assert.AreEqual(0, post.Votes.Count);
        }

        [Test]
        [Category("Post")]
        public void Comments_InitializedAsEmptyList()
        {
            // Arrange
            var post = new Post();

            // Assert
            Assert.IsNotNull(post.Comments);
            Assert.IsInstanceOf<List<Comment>>(post.Comments);
            Assert.AreEqual(0, post.Comments.Count);
        }

        [Test]
        [Category("Post")]
        public void PostState_DefaultValue_IsInVerification()
        {
            // Arrange
            var post = new Post();

            // Assert
            Assert.AreEqual(PostStateEnum.In_Verification, post.PostState);
        }

        [Test]
        [Category("Post")]
        public void PostTitle_CannotBeEmpty()
        {
            // Arrange
            var post = new Post { PostTitle = "", PostText = "dasd" };
            var validationContext = new ValidationContext(post, null, null);
            var validationResults = new System.Collections.Generic.List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(post, validationContext, validationResults, true);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual(1, validationResults.Count);
            Assert.AreEqual("The PostTitle field is required.", validationResults[0].ErrorMessage);
        }

        [Test]
        [Category("Post")]
        public void PostText_CannotBeEmpty()
        {
            // Arrange
            var post = new Post { PostTitle = "title", PostText = "" };
            var validationContext = new ValidationContext(post, null, null);
            var validationResults = new System.Collections.Generic.List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(post, validationContext, validationResults, true);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual(1, validationResults.Count);
            Assert.AreEqual("The PostText field is required.", validationResults[0].ErrorMessage);
        }
    }
}
