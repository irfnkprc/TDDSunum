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
      var chartItem = cart.Items.Find(ci => ci.Product.Title == product.Title);
      chartItem.Count.Should().Be(2);
    }

    [Test]
    public void It_should_apply_rate_discounts()
    {
      //Arrange
      Category category = new Category("food");
      Campaign campaign1 = Campaign.RateDiscount(category, 40, 100);

      // 300
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
      Campaign campaign1 = Campaign.RateDiscount(category, 40, 101);

      // 300
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

      Campaign campaign1 = Campaign.AmountDiscount(category, amountToBeDiscounted, 100);

      // 300
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

      Campaign campaign1 = Campaign.AmountDiscount(category, amountToBeDiscounted, 101);

      // 300
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

      Campaign campaign1 = Campaign.RateDiscount(category, 40, 100);
      Campaign campaign2 = Campaign.AmountDiscount(category, 50.0, 100);
      Campaign campaign3 = Campaign.AmountDiscount(category, 5.0, 100);

      // 300
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

      double minimumCartAmount = 200;
      int rateOfDiscount = 40;
      Coupon coupon = Coupon.RateCoupon(minimumCartAmount, rateOfDiscount);

      // 300
      var cart = new Cart();
      Product apple = new Product("Elma", 20, category);
      cart.AddItem(apple, 5);

      //Act
      cart.ApplyCoupon(coupon);

      //Verify
      cart.RawTotal.Should().Be(100);
      cart.CouponDiscount.Should().Be(40);
    }

    // Campaign Discounts are applied first, Then Coupons.
    [Test]
    public void It_should_apply_discounts_first_then_coupons()
    {

    }
  }
}