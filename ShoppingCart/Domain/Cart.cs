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
                    Count = 1,
                    Product = product
                });
            }
        }

        public void ApplyDiscounts(params Campaign[] campaigns)
        {
            //var finalDiscount = 0;
            //foreach (var campaign in campaigns)
            //{

            //}
        }

        public double GetTotalAmountAfterDiscounts()
        {
            return 0;
        }
    }
}