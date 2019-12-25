namespace ShoppingCart.Domain
{
  public class ChartItem
  {
    public Product Product { get; set; }
    public int Count { get; set; }

    public double LineTotal => Product.Price * Count;
  }
}