using FluentAssertions;
using NUnit.Framework;
using ShoppingCart.abstractions;
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
            var chartItem = cart.Items.Find(ci => ci.Product.Title == product.Title);
            chartItem.Count.Should().Be(2);
        }

        [Test]
        public void It_should_apply_rate_discounts()
        {
            //Arrange
            Category category = new Category("food");
            var campaign1 = new RateCampaign(category, 40, 2);

            var cart = new Cart();
            Product apple = new Product("Elma", 20, category);
            cart.AddItem(apple, 5);

            //Act
            cart.ApplyDiscounts(campaign1);

            //Verify
            cart.RawTotal.Should().Be(100);
            cart.TotalAmountAfterDiscounts.Should().Be(60);
        }

        [Test]
        public void It_should_not_apply_rate_discounts_when_minimum_purchase_not_met()
        {
            //Arrange
            Category category = new Category("food");
            var campaign1 = new RateCampaign(category, 40, 101);

            var cart = new Cart();
            Product apple = new Product("Elma", 20, category);
            cart.AddItem(apple, 5);

            //Act
            cart.ApplyDiscounts(campaign1);

            //Verify
            cart.RawTotal.Should().Be(100);
            cart.TotalAmountAfterDiscounts.Should().Be(100);
        }

        [Test]
        public void It_should_apply_amount_discounts()
        {
            //Arrange
            double amountToBeDiscounted = 49;

            Category category = new Category("food");

            int minimumPurcahsedItemCount = 4;
            var campaign1 = new AmountCampaign(category, amountToBeDiscounted, minimumPurcahsedItemCount);

            var cart = new Cart();
            Product apple = new Product("Elma", 20, category);
            cart.AddItem(apple, 5);

            //Act
            cart.ApplyDiscounts(campaign1);

            //Verify
            cart.RawTotal.Should().Be(100);
            var expectedTotalAfterCampaignApplied = cart.RawTotal - amountToBeDiscounted;

            cart.TotalAmountAfterDiscounts.Should().Be(expectedTotalAfterCampaignApplied);
        }

        [Test]
        public void It_should_not_apply_amount_discounts_when_minimum_discount_not_met()
        {
            //Arrange
            double amountToBeDiscounted = 49;

            Category category = new Category("food");

            var campaign1 = new AmountCampaign(category, amountToBeDiscounted, 101);

            var cart = new Cart();
            Product apple = new Product("Elma", 20, category);
            cart.AddItem(apple, 5);

            //Act
            cart.ApplyDiscounts(campaign1);

            //Verify
            cart.RawTotal.Should().Be(100);
            cart.TotalAmountAfterDiscounts.Should().Be(cart.RawTotal);
        }

        [Test]
        public void It_should_apply_discounts()
        {
            //Arrange
            Category category = new Category("food");

            var campaign1 = new RateCampaign(category, 40, 1);
            var campaign2 = new AmountCampaign(category, 50.0, 1);
            var campaign3 = new AmountCampaign(category, 5.0, 1);

            var cart = new Cart();
            Product apple = new Product("Elma", 20, category);
            Product armut = new Product("Armut", 50, category);
            cart.AddItem(apple, 5);
            cart.AddItem(armut, 2);

            //Act
            cart.ApplyDiscounts(campaign1, campaign2, campaign3);

            //Verify
            cart.RawTotal.Should().Be(200);
            cart.TotalAmountAfterDiscounts.Should().Be(120);
        }

        [Test]
        public void It_should_apply_coupons()
        {
            //Arrange
            Category category = new Category("food");

            double minimumCartAmount = 100;
            int rateOfDiscount = 60;
            var coupon = RateCoupon.Create(minimumCartAmount, rateOfDiscount);

            var cart = new Cart();
            Product apple = new Product("Elma", 20, category);
            cart.AddItem(apple, 5);

            //Act
            cart.ApplyCoupon(coupon);

            //Verify
            cart.TotalAmountAfterDiscounts.Should().Be(40);
            cart.CouponDiscount.Should().Be(60);
        }

        [Test]
        public void It_should_not_apply_coupons_when_mimimum_amount_constraint_met()
        {
            //Arrange
            Category category = new Category("food");

            double minimumCartAmount = 200;
            int rateOfDiscount = 60;
            var coupon = RateCoupon.Create(minimumCartAmount, rateOfDiscount);

            var cart = new Cart();
            Product apple = new Product("Elma", 20, category);
            cart.AddItem(apple, 5);

            //Act
            cart.ApplyCoupon(coupon);

            //Verify
            cart.TotalAmountAfterDiscounts.Should().Be(100);
            cart.CouponDiscount.Should().Be(0);
        }

        // Campaign Discounts are applied first, Then Coupons.
        [Test]
        public void It_should_apply_discounts_first_then_coupons()
        {
            //Arrange
            Category category = new Category("food");

            var campaign1 = new RateCampaign(category, 40, 1);
            var campaign2 = new AmountCampaign(category, 50.0, 1);

            var coupon = new AmountCoupon(100, 60);

            var cart = new Cart();
            Product apple = new Product("Elma", 200, category);
            Product armut = new Product("Armut", 500, category);
            cart.AddItem(apple, 5);
            cart.AddItem(armut, 2);

            //Act
            cart.ApplyCoupon(coupon);
            cart.ApplyDiscounts(campaign1, campaign2);

            //Verify

            cart.RawTotal.Should().Be(2000);
            cart.CampaignDiscount.Should().Be(800);
            cart.TotalAmountAfterDiscounts.Should().Be(1140);
        }

        [Test]
        public void It_should_apply_discount_more_than_category_total_amount()
        {
            //Arrange
            Category category = new Category("food");
            Category electronics = new Category("electronics");

            var electronicsAmountCampaign = new AmountCampaign(electronics, 580.0, 1);
            var cart = new Cart();
            var apple = new Product("Elma", 200, category);
            var laptop = new Product("Laptop", 500, electronics);

            cart.AddItem(apple, 5);
            cart.AddItem(laptop, 1);

            //Act
            cart.ApplyDiscounts(electronicsAmountCampaign);

            //Verify

            cart.RawTotal.Should().Be(1500);
            cart.CampaignDiscount.Should().Be(500);
            cart.TotalAmountAfterDiscounts.Should().Be(1000);
        }
    }
}