using ShoppingCart.Domain;

namespace ShoppingCart.abstractions
{
    public abstract class BaseBenefit
    {
        public DiscountType DiscountType;

        public double CalculateDiscount(Cart cart)
        {
            if (IsApplicableTo(cart))
            {
                var calculateDiscountFor = CalculateDiscountFor(cart);
                return calculateDiscountFor;
            }

            return 0;
        }

        protected abstract double CalculateDiscountFor(Cart cart);
        protected abstract bool IsApplicableTo(Cart cart);
    }
}