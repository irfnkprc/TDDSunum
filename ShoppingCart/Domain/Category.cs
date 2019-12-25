namespace ShoppingCart.Domain
{
  public class Category
  {
    public string Title { get; set; }
    public Category(string title)
    {
      Title = title;
    }
  }
}