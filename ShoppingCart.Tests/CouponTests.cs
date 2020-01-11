using FluentAssertions;
using NUnit.Framework;
using ShoppingCart.Abstractions;
using ShoppingCart.Domain;

namespace ShoppingCart.Tests
{
  /*
      Coupons exist for cart discounts.
      If Cart amount is less than minimum, discount is not applied.
   */

  public class CouponTests
  {
    [Test]
    public void It_should_create_coupon_with_rate_and_minimum_amount()
    {
      //Arrange
      double minimumCartAmount = 100;
      int rateOfDiscount = 10;

      //Act
      var discountType = DiscountType.Rate;
      var coupon = RateCoupon.Create(minimumCartAmount, rateOfDiscount);

      //Verify
      coupon.DiscountType.Should().Be(discountType);
    }

    [Test]
    public void It_should_create_coupon_with_amount_and_minimum_amount()
    {
      //Arrange
      double minimumCartAmount = 100;
      double amountOfDiscount = 10;

      //Act
      var discountType = DiscountType.Amount;
      var coupon = new AmountCoupon(minimumCartAmount, amountOfDiscount);

      //Verify
      coupon.DiscountType.Should().Be(discountType);
    }
  }
}