using FluentAssertions;
using NUnit.Framework;
using ShoppingCart.Domain;
using ShoppingCart.Tests.TestHelpers;

namespace ShoppingCart.Tests
{
    public class ProductTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase("Title")]
        [TestCase("Price")]
        [TestCase("Category")]
        public void It_should_has_property_with_given_name(string propertyName)
        {
            // Arrange

            // Act

            // Assert
            typeof(Product).HasProperty(propertyName).Should().BeTrue();
        }

        [Test]
        public void It_should_not_create_product_with_Title_price_and_category(string propertyName)
        {
            // Arrange
            Category category = new Category("food");
            
            var sut = new Product("Apple", 150.0, category);
            // Act



            // Assert
        }
    }
}