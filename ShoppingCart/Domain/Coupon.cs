namespace ShoppingCart.Domain
{
    public class Coupon
    {
        public double MinimumCartAmount { get; set; }
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
            MinimumCartAmount = minimumCartAmount;
            Rate = rateOfDiscount;
            DiscountType = rate;
        }

        private Coupon(double minimumCartAmount, double amountOfDiscount, DiscountType discountType)
        {
            MinimumCartAmount = minimumCartAmount;
            AmountOfDiscount = amountOfDiscount;
            DiscountType = discountType;
        }
    }
}