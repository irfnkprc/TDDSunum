using FluentAssertions;
using NUnit.Framework;
using ShoppingCart.Domain;

namespace ShoppingCart.Tests
{
  public class CategoryTest
  {
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