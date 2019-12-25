using FluentAssertions;
using NUnit.Framework;
using ShoppingCart.Domain;
using ShoppingCart.Tests.TestHelpers;

namespace ShoppingCart.Tests
{
    /*
Coupons exist for cart discounts.
If Cart amount is less than minimum, discount is not applied.
     */

    public class CouponTests
    {
        [TestCase("MinimumCartAmount")]
        public void It_should_have_Property_with_given_name(string propertyName)
        {
            //Arrange

            //Act

            //Verify
            typeof(Coupon).HasProperty(propertyName).Should().BeTrue();
        }

        [Test]
        public void It_should_create_coupon_with_rate_and_minimum_amount()
        {
            //Arrange
            double minimumCartAmount = 100;
            int rateOfDiscount = 10;

            //Act
            var discountType = DiscountType.Rate;
            var coupon = Coupon.RateCoupon(minimumCartAmount, rateOfDiscount);

            //Verify
            coupon.MinimumCartAmount.Should().Be(minimumCartAmount);
            coupon.Rate.Should().Be(rateOfDiscount);
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
            var coupon = Coupon.AmountCoupon(minimumCartAmount, amountOfDiscount);

            //Verify
            coupon.MinimumCartAmount.Should().Be(minimumCartAmount);
            coupon.AmountOfDiscount.Should().Be(amountOfDiscount);
            coupon.DiscountType.Should().Be(discountType);
        }
    }
}