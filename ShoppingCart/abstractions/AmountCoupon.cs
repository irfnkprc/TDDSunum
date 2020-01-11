using ShoppingCart.Domain;

namespace ShoppingCart.Abstractions
{
    public class AmountCoupon : BaseBenefit
    {
        private readonly double _minimumCartAmount;
        private readonly double _amountOfDiscount;

        public AmountCoupon(double minimumCartAmount, double amountOfDiscount)
        {
            _minimumCartAmount = minimumCartAmount;
            _amountOfDiscount = amountOfDiscount;
            DiscountType = DiscountType.Amount;
        }

        protected override double CalculateDiscountFor(Cart cart)
        {
            return _amountOfDiscount;
        }

        protected override bool IsApplicableTo(Cart cart)
        {
            return cart.TotalAmountAfterDiscounts >= _minimumCartAmount;
        }
    }
}