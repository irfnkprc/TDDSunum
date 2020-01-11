using ShoppingCart.Domain;

namespace ShoppingCart.Abstractions
{
    public class AmountCampaign : BaseBenefit
    {
        private readonly Category _category;
        private readonly double _amount;
        private readonly int _minimumPurcahsedItemCount;

        public AmountCampaign(Category category, double amount, int minimumPurcahsedItemCount)
        {
            _category = category;
            _amount = amount;
            _minimumPurcahsedItemCount = minimumPurcahsedItemCount;
            DiscountType = DiscountType.Amount;
        }

        protected override double CalculateDiscountFor(Cart cart)
        {
            var categoryTotal = cart.CategoryTotal(_category);

            return categoryTotal > _amount ? _amount : categoryTotal;
        }

        protected override bool IsApplicableTo(Cart cart)
        {
            return cart.ItemCount >= _minimumPurcahsedItemCount;
        }
    }
}