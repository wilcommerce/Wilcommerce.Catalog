using System;
using System.Linq;
using Wilcommerce.Catalog.Models;

namespace Wilcommerce.Catalog.ReadModels.Extensions
{
    public static class ProductCategoryExtensions
    {
        public static IQueryable<Product> ProductsByCategory(this IQueryable<ProductCategory> productCategories, Guid categoryId)
        {
            return from c in productCategories
                   where c.CategoryId == categoryId
                   select c.Product;
        }

        public static IQueryable<ProductCategory> Mains(this IQueryable<ProductCategory> productCategories, Guid categoryId)
        {

            return from c in productCategories
                   where c.IsMain
                   select c;
        }
    }
}
