using FluentAssertions;
using NUnit.Framework;
using ShoppingCart.Domain;
using ShoppingCart.Tests.TestHelpers;

namespace ShoppingCart.Tests
{
    /*
Campaigns exist for product price discounts.
Campaigns are applicable to a product category.
Campaign discount vary based on the number of products in the cart
Coupons exist for cart discounts.
Coupons have minimum cart amount constraint. If Cart amount is less than minimum, discount is not applied.
     */

    /*
//you can apply discounts to a category
//discount rules can be 20% on a category if bought more than 3 items
     */

    public class CampaignsTests
    {
        [TestCase("ApplicableCategory")]
        public void It_should_have_Applicable_Category(string propertyName)
        {
            //Arrange

            //Act

            //Verify
            typeof(Campaign).HasProperty(propertyName).Should().BeTrue();
        }

        [Test]
        public void It_should_be_Defined_With_Rate_and_minimum_amount_Type_Discount()
        {
            //Arrange
            int discountRate = 60;
            var minimumPurchaseCount = 5;
            var category = new Category("food");

            //Act
            var campaign = Campaign.RateDiscount(category, discountRate, minimumPurchaseCount);

            //Verify
            campaign.ApplicableCategory.Should().Be(category);
            campaign.MinimumAmount.Should().Be(minimumPurchaseCount);
            campaign.Rate.Should().Be(discountRate);

            campaign.Amount.Should().Be(0);
        }

        [Test]
        public void It_should_be_Defined_With_Amount_and_minimum_amount_Type_Discount()
        {
            //Arrange
            double amountOfDiscount = 60;
            var minimumPurchaseCount = 5;
            var category = new Category("food");

            //Act
            var campaign = Campaign.AmountDiscount(category, amountOfDiscount, minimumPurchaseCount);

            //Verify
            campaign.ApplicableCategory.Should().Be(category);
            campaign.MinimumAmount.Should().Be(minimumPurchaseCount);
            campaign.Amount.Should().Be(amountOfDiscount);

            campaign.Rate.Should().Be(0);
        }
    }
}