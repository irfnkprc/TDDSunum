using System.Collections.Generic;
using System.Linq;

namespace ShoppingCart.Domain
{
  public class Cart
  {
    public List<ChartItem> Items { get; set; }

    public Cart()
    {
      Items = new List<ChartItem>();
    }

    // todo: add method for items in category

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
        Items.Add(new ChartItem
        {
          Count = quantityToBeAdded,
          Product = product
        });
      }
    }

    public double CampaignDiscount { get; private set; }
    public double CouponDiscount { get; private set; }

    public double RawTotal => Items.Sum(chartItem => chartItem.LineTotal);
    public double TotalAmountAfterDiscounts => RawTotal - CampaignDiscount - CouponDiscount;

    public void ApplyDiscounts(params Campaign[] campaigns)
    {
      CampaignDiscount = campaigns.Select(c => c.CalculateDiscountFor(this)).Max();
    }

    public void ApplyCoupon(Coupon coupon)
    {
      if (coupon.MinimumAmount < TotalAmountAfterDiscounts)
      {
        return;
      }

      if (coupon.DiscountType == DiscountType.Amount)
      {
        CouponDiscount = coupon.AmountOfDiscount;
      }
      else
      {
        CouponDiscount = TotalAmountAfterDiscounts * coupon.Rate / 100;
      }
    }
  }
}