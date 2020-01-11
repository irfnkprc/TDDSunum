using System.Collections.Generic;
using System.Linq;
using ShoppingCart.Abstractions;

namespace ShoppingCart.Domain
{
    public class Cart
    {
        public List<ChartItem> Items { get; set; }

        public Cart()
        {
            Items = new List<ChartItem>();
        }

        public double CategoryTotal(Category category)
        {
            return Items.Where(c => c.Product.CategoryTitle == category.Title).Sum(c => c.LineTotal);
        }

        private bool HasItem(Product product)
        {
            return Items.Any(x => x.Product.Title == product.Title);
        }

        public void AddItem(Product product, int quantityToBeAdded)
        {
            if (HasItem(product))
            {
                var chartItem = Items.Find(i => i.Product.Title == product.Title);
                chartItem.Count += quantityToBeAdded;
            }
            else
            {
                Items.Add(new ChartItem { Count = quantityToBeAdded, Product = product });
            }
        }

        public double CampaignDiscount { get; private set; }
        public double CouponDiscount { get; private set; }

        public double RawTotal => Items.Sum(chartItem => chartItem.LineTotal);

        public int ItemCount => Items.Sum(x => x.Count);

        public double TotalAmountAfterDiscounts => RawTotal - CampaignDiscount - CouponDiscount;

        public void ApplyDiscounts(params BaseBenefit[] campaigns)
        {
            CampaignDiscount = campaigns.Select(c => c.CalculateDiscount(this)).Max();
        }

        public void ApplyCoupon(BaseBenefit coupon)
        {
            CouponDiscount = coupon.CalculateDiscount(this);
        }
    }
}