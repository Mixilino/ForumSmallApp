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
    public class PostCategoryTests
    {
        [Test]
        [Category("Domain_PostCategory")]
        public void CategoryName_Valid()
        {
            // Arrange
            var postCategory = new PostCategory
            {
                PcId = 1,
                CategoryName = "Kategorija 1"
            };
            var validationContext = new ValidationContext(postCategory, null, null);

            // Act
            var validationResult = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(postCategory, validationContext, validationResult, true);

            // Assert
            Assert.IsTrue(isValid);
            Assert.AreEqual(0, validationResult.Count);
        }

        [Test]
        [Category("Domain_PostCategory")]
        public void CategoryName_EmptyShouldThrowValidationException()
        {
            // Arrange
            var postCategory = new PostCategory
            {
                PcId = 1,
                CategoryName = ""
            };
            var validationContext = new ValidationContext(postCategory, null, null);

            // Act
            var validationResult = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(postCategory, validationContext, validationResult, true);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual(1, validationResult.Count);
            Assert.AreEqual("The CategoryName field is required.", validationResult[0].ErrorMessage);
        }

        [Test]
        [Category("Domain_PostCategory")]
        public void CategoryName_LongShouldThrowValidationException()
        {
            // Arrange
            var postCategory = new PostCategory
            {
                PcId = 1,
                CategoryName = "Very Big Category Name, Very Big Category Name, Very Big Category Name, "
            };
            var validationContext = new ValidationContext(postCategory, null, null);

            // Act
            var validationResult = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(postCategory, validationContext, validationResult, true);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual(1, validationResult.Count);
            Assert.AreEqual("The field CategoryName must be a string with a maximum length of 32.", validationResult[0].ErrorMessage);
        }
    }

}
