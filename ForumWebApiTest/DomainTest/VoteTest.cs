using ForumWebApi.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumWebApi.DomainTest
{
    [TestFixture]
    public class VoteTests
    {
        [Test]
        [Category("domain_vote")]
        public void VoteId_GetSet_ShouldWork()
        {
            // Arrange
            var vote = new Vote();

            // Act
            vote.VoteId = 1;
            var result = vote.VoteId;

            // Assert
            Assert.AreEqual(1, result);
        }

        [Test]
        [Category("domain_vote")]
        public void UpVote_GetSet_ShouldWork()
        {
            // Arrange
            var vote = new Vote();

            // Act
            vote.UpVote = true;
            var result = vote.UpVote;

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        [Category("domain_vote")]
        public void DateLiked_GetSet_ShouldWork()
        {
            // Arrange
            var vote = new Vote();
            var expectedDate = new DateTime(2023, 3, 7);

            // Act
            vote.DateLiked = expectedDate;
            var result = vote.DateLiked;

            // Assert
            Assert.AreEqual(expectedDate, result);
        }

        [Test]
        [Category("domain_vote")]
        public void UserId_GetSet_ShouldWork()
        {
            // Arrange
            var vote = new Vote();

            // Act
            vote.UserId = 1;
            var result = vote.UserId;

            // Assert
            Assert.AreEqual(1, result);
        }

        [Test]
        [Category("domain_vote")]
        public void User_GetSet_ShouldWork()
        {
            // Arrange
            var vote = new Vote();
            var user = new User { UserId = 1 };

            // Act
            vote.User = user;
            var result = vote.User;

            // Assert
            Assert.AreEqual(user, result);
        }

        [Test]
        [Category("domain_vote")]
        public void PostId_GetSet_ShouldWork()
        {
            // Arrange
            var vote = new Vote();

            // Act
            vote.PostId = 1;
            var result = vote.PostId;

            // Assert
            Assert.AreEqual(1, result);
        }

        [Test]
        [Category("domain_vote")]
        public void Post_GetSet_ShouldWork()
        {
            // Arrange
            var vote = new Vote();
            var post = new Post { PostId = 1 };

            // Act
            vote.Post = post;
            var result = vote.Post;

            // Assert
            Assert.AreEqual(post, result);
        }
    }

}
