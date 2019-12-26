using ShoppingCart.Domain;

namespace ShoppingCart.abstractions
{
    public abstract class BaseBenefit
    {
       public double MinimumAmount { get; set; }
       public abstract double CalculateDiscountFor(Cart cart);
    }
}