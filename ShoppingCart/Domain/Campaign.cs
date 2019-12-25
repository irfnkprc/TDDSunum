namespace ShoppingCart.Domain
{
    public class Campaign
    {
        public static Campaign RateDiscount(Category category, int rate, double minimumAmount)
        {
            return new Campaign(category, rate, minimumAmount, DiscountType.Rate);
        }

        public static Campaign AmountDiscount(Category category, double amount, double minimumAmount)
        {
            return new Campaign(category, amount, minimumAmount, DiscountType.Amount);
        }

        private Campaign(Category category, int rate, double minimumAmount, DiscountType discountType)
        {
            ApplicableCategory = category;
            Rate = rate;
            MinimumAmount = minimumAmount;
            DiscountType = discountType;
        }

        private Campaign(Category category, double amount, double minimumAmount, DiscountType discountType)
        {
            ApplicableCategory = category;
            Amount = amount;
            MinimumAmount = minimumAmount;
            DiscountType = discountType;
        }

        public Category ApplicableCategory { get; set; }
        public double MinimumAmount { get; set; }
        public DiscountType DiscountType { get; set; }
        public double Rate { get; set; }
        public double Amount { get; set; }
    }
}