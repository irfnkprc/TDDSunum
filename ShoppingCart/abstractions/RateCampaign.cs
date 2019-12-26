using ShoppingCart.Domain;

namespace ShoppingCart.abstractions
{
    public class RateCampaign : BaseBenefit
    {
        private readonly Category _category;
        private readonly double _discountRate;
        private readonly int _minimumPurcahsedItemCount;

        public RateCampaign(Category category, double discountRate, int minimumPurcahsedItemCount)
        {
            _category = category;
            _discountRate = discountRate;
            _minimumPurcahsedItemCount = minimumPurcahsedItemCount;
            DiscountType = DiscountType.Rate;
        }

        protected override double CalculateDiscountFor(Cart cart)
        {
            var total = cart.CategoryTotal(_category);
            return total * _discountRate / 100;
        }

        protected override bool IsApplicableTo(Cart cart)
        {
            return cart.ItemCount >= _minimumPurcahsedItemCount;
        }
    }
}