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
    public class ServiceResponseTests
    {
        [Test]
        [Category("Domain_ServiceResponse")]
        public void DataIsNullSuccessIsTrue()
        {
            // Arrange
            ServiceResponse<object> response = new ServiceResponse<object>();

            // Assert
            Assert.IsTrue(response.Succes);
        }

        [Test]
        [Category("Domain_ServiceResponse")]
        public void DataIsNotNullSuccessIsTrue()
        {
            // Arrange
            ServiceResponse<string> response = new ServiceResponse<string>();
            response.Data = "some data";

            // Assert
            Assert.IsTrue(response.Succes);
        }

        [Test]
        [Category("Domain_ServiceResponse")]
        public void SetMessageSuccess()
        {
            // Arrange
            ServiceResponse<object> response = new ServiceResponse<object>();
            string message = "Poruka";

            // Act
            response.Message = message;

            // Assert
            Assert.AreEqual(message, response.Message);
        }

        [Test]
        [Category("Domain_ServiceResponse")]
        public void Equals_ReturnsTrue()
        {
            // Arrange
            ServiceResponse<int> response1 = new ServiceResponse<int>() { Succes = true, Message = "success", Data = 1 };
            ServiceResponse<int> response2 = new ServiceResponse<int>() { Succes = true, Message = "success", Data = 1 };

            // Assert
            Assert.AreEqual(response1, response2);
        }

        [Test]
        [Category("Domain_ServiceResponse")]
        public void Equals_ReturnsFalse()
        {
            // Arrange
            ServiceResponse<int> response1 = new ServiceResponse<int>() { Succes = true, Message = "success", Data = 1 };
            ServiceResponse<int> response2 = new ServiceResponse<int>() { Succes = true, Message = "success", Data = 2 };

            // Assert
            Assert.AreNotEqual(response1, response2);
        }
    }

}
