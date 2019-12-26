using ShoppingCart.Domain;

namespace ShoppingCart.abstractions
{
    public class RateCoupon : BaseBenefit
    {
        private readonly double _minimumCartAmount;
        private readonly double _rateOfDiscount;

        public static BaseBenefit Create(double minimumCartAmount, double rateOfDiscount)
        {
            return new RateCoupon(minimumCartAmount, rateOfDiscount);
        }

        private RateCoupon(double minimumCartAmount, double rateOfDiscount)
        {
            _minimumCartAmount = minimumCartAmount;
            _rateOfDiscount = rateOfDiscount;

            DiscountType = DiscountType.Rate;
        }

        protected override double CalculateDiscountFor(Cart cart)
        {
            return cart.TotalAmountAfterDiscounts * _rateOfDiscount / 100;
        }

        protected override bool IsApplicableTo(Cart cart)
        {
            // after discounts applied ?
            return cart.TotalAmountAfterDiscounts >= _minimumCartAmount;
        }
    }
}