

using System.Reflection;

namespace E_Commerce.Infrastructure.Identity
{
    public class Permissions
    {
        // permission constants 
        public static class Products
        {
            public const string View = "Products.View";
            public const string Create = "Products.Create";
            public const string Update = "Products.Update";
            public const string Delete = "Products.Delete";
        }

        public static class Categories
        {
        
        }

        public static IReadOnlyList<string> GetAll()
        {
            return typeof(Permissions)
                .GetNestedTypes(BindingFlags.Public)
                .SelectMany(t => t.GetFields(BindingFlags.Public | BindingFlags.Static))
                .Where(f => f.IsLiteral && !f.IsInitOnly)
                .Select(f => (string)f.GetRawConstantValue()!)
                .ToList();
        }
    }
}
