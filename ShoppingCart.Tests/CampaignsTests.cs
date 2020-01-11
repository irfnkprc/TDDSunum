using NUnit.Framework;
using ShoppingCart.Abstractions;
using ShoppingCart.Domain;

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
        [Test]
        public void It_should_be_Defined_With_Rate_and_minimum_amount_Type_Discount()
        {
            //Arrange
            int discountRate = 60;
            var minimumPurchaseCount = 5;
            var category = new Category("food");

            //Act
            var campaign = new RateCampaign(category, discountRate, minimumPurchaseCount);

            //Verify
            // todo: verify
        }

        [Test]
        public void It_should_be_Defined_With_Amount_and_minimum_amount_Type_Discount()
        {
            //Arrange
            double amountOfDiscount = 60;
            var minimumPurchaseCount = 5;
            var category = new Category("food");

            //Act
            var campaign = new AmountCampaign(category, amountOfDiscount, minimumPurchaseCount);

            //Verify
            // todo: verify
        }
    }
}