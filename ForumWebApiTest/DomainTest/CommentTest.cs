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
    public class CommentTests
    {
        [Test]
        [Category("Domain_Comment")]
        public void Comment_CanBeCreatedWithValidProperties()
        {
            // Arrange
            var commentId = 1;
            var commentText = "test comment";
            var dateCreated = DateTime.Now;
            var userId = 2;
            var user = new User { UserId = userId };
            var postId = 3;
            var post = new Post { PostId = postId };

            // Act
            var comment = new Comment(commentId, commentText, dateCreated, userId, user, postId, post);

            // Assert
            Assert.AreEqual(commentId, comment.CommentId);
            Assert.AreEqual(commentText, comment.CommentText);
            Assert.AreEqual(dateCreated, comment.DateCreated);
            Assert.AreEqual(userId, comment.UserId);
            Assert.AreEqual(user, comment.User);
            Assert.AreEqual(postId, comment.PostId);
            Assert.AreEqual(post, comment.Post);
        }

        [Test]
        [Category("Domain_Comment")]
        public void Comment_RequiresCommentText()
        {
            // Arrange
            var comment = new Comment { CommentId = 1, CommentText = "" };
            var validationContext = new ValidationContext(comment, null, null);
            var validationResults = new System.Collections.Generic.List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(comment, validationContext, validationResults, true);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual(1, validationResults.Count);
            Assert.AreEqual("The CommentText field is required.", validationResults[0].ErrorMessage);
        }
    }
}
