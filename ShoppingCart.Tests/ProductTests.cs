using FluentAssertions;
using NUnit.Framework;
using ShoppingCart.Domain;

namespace ShoppingCart.Tests
{
  public class ProductTests
  {
    [Test]
    public void It_should_not_create_product_with_Title_price_and_category()
    {
      // Arrange
      Category category = new Category("food");

      // Act
      var productName = "Apple";
      var sut = new Product(productName, 150.0, category);

      // Assert
      sut.Title.Should().Be(productName);
    }
  }
}