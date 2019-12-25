using FluentAssertions;
using NUnit.Framework;
using ShoppingCart.Domain;
using ShoppingCart.Tests.TestHelpers;

namespace ShoppingCart.Tests
{
    public class CategoryTest
    {
        [TestCase("Title")]
        public void It_should_has_property_with_given_name(string propertyName)
        {
            // Arrange

            // Act

            // Assert
            typeof(Category).HasProperty(propertyName).Should().BeTrue();
        }

        [Test]
        public void It_should_create_category_with_name()
        {
            //Arrange
            string title = "food";

            //Act
            var category = new Category(title);

            //Verify
            category.Title.Should().Be(title);
        }
    }
}