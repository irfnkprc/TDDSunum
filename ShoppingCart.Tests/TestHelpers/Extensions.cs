using System;

namespace ShoppingCart.Tests.TestHelpers
{
    public static class Extensions
    {
        public static bool HasProperty(this Type obj, string propertyName)
        {
            return obj.GetProperty(propertyName) != null;
        }
    }
}