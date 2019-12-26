using ShoppingCart.abstractions;

namespace ShoppingCart.Domain
{
    public class Coupon : BaseBenefit
    {
        public int Rate { get; set; }
        public DiscountType DiscountType { get; set; }
        public double AmountOfDiscount { get; set; }

        public static Coupon RateCoupon(double minimumCartAmount, int rateOfDiscount)
        {
            return new Coupon(minimumCartAmount, rateOfDiscount, DiscountType.Rate);
        }

        public static Coupon AmountCoupon(double minimumCartAmount, double amountOfDiscount)
        {
            return new Coupon(minimumCartAmount, amountOfDiscount, DiscountType.Amount);
        }

        private Coupon(double minimumCartAmount, int rateOfDiscount, DiscountType rate)
        {
            MinimumAmount = minimumCartAmount;
            Rate = rateOfDiscount;
            DiscountType = rate;
        }

        private Coupon(double minimumCartAmount, double amountOfDiscount, DiscountType discountType)
        {
            MinimumAmount = minimumCartAmount;
            AmountOfDiscount = amountOfDiscount;
            DiscountType = discountType;
        }

        public override double CalculateDiscountFor(Cart cart)
        {
            var cartTotalAmountAfterDiscounts = cart.TotalAmountAfterDiscounts;
            if (cartTotalAmountAfterDiscounts >= MinimumAmount)
            {
                return 0;
            }

            // todo: implement
            return 0;
        }
    }
}