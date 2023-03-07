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
    public class UserTests
    {
        [Test]
        [Category("Domain_User")]
        public void User_CanBeCreatedWithValidProperties()
        {
            // Arrange
            var userName = "testuser";
            var passwordHash = new byte[] { 0x01, 0x02, 0x03 };
            var passwordSalt = new byte[] { 0x04, 0x05, 0x06 };
            var role = UserRoles.Regular;

            // Act
            var user = new User
            {
                UserName = userName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                role = role
            };

            // Assert
            Assert.AreEqual(userName, user.UserName);
            Assert.AreEqual(passwordHash, user.PasswordHash);
            Assert.AreEqual(passwordSalt, user.PasswordSalt);
            Assert.AreEqual(role, user.role);
        }
    }
}
