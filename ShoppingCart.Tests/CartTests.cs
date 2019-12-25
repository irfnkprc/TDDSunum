using FluentAssertions;
using NUnit.Framework;
using ShoppingCart.Domain;

namespace ShoppingCart.Tests
{
    public class CartTests
    {
        [Test]
        public void It_Should_Have_Cart_Entity()
        {
            //Arrange
            var cart = new Cart();

            //Act

            //Verify
            cart.Should().NotBeNull();
        }

        [Test]
        public void It_should_add_products_with_quantity_info()
        {
            //Arrange
            var cart = new Cart();
            Category category = new Category("food");
            var product = new Product("Apple", 10, category);

            int quantityToBeAdded = 1;

            //Act

            //Verify
            Assert.DoesNotThrow(() => { cart.AddItem(product, quantityToBeAdded); });
        }

        [Test]
        public void It_should_add_products_with_quantity_info2()
        {
            //Arrange
            var cart = new Cart();
            Category category = new Category("food");
            var product = new Product("Elma", 10.0, category);

            int quantityToBeAdded = 1;

            //Act
            cart.AddItem(product, quantityToBeAdded);
            cart.AddItem(product, quantityToBeAdded);

            //Verify
            var chartItem = cart.Items.Find(x => x.Product.Title == product.Title);
            chartItem.Count.Should().Be(2);
        }

        [Test]
        public void It_should_apply_discounts()
        {
            //Arrange
            Category category = new Category("food");

            Campaign campaign1 = Campaign.RateDiscount(category, 20, 3);
            Campaign campaign2 = Campaign.AmountDiscount(category, 50.0, 5);
            Campaign campaign3 = Campaign.AmountDiscount(category, 5.0, 5);

            var cart = new Cart();

            //Act
            cart.ApplyDiscounts(campaign1, campaign2, campaign3);

            //Verify
            double totalAmountAfterDiscounts = cart.GetTotalAmountAfterDiscounts();
            totalAmountAfterDiscounts.Should().Be(500);
        }
    }
}