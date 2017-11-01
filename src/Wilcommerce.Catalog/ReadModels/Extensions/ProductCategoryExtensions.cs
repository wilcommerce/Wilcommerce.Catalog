using System;
using System.Linq;
using Wilcommerce.Catalog.Models;

namespace Wilcommerce.Catalog.ReadModels.Extensions
{
    /// <summary>
    /// Defines the extension methods for the product category read model
    /// </summary>
    public static class ProductCategoryExtensions
    {
        /// <summary>
        /// Retrieve all the products filtered by the category
        /// </summary>
        /// <param name="productCategories"></param>
        /// <param name="categoryId">The category id</param>
        /// <returns>A list of products</returns>
        public static IQueryable<Product> ProductsByCategory(this IQueryable<ProductCategory> productCategories, Guid categoryId)
        {
            return from c in productCategories
                   where c.CategoryId == categoryId
                   select c.Product;
        }

        /// <summary>
        /// Retrieve all the product categories which are a main category
        /// </summary>
        /// <param name="productCategories"></param>
        /// <returns>A list of product categories</returns>
        public static IQueryable<ProductCategory> Mains(this IQueryable<ProductCategory> productCategories)
        {

            return from c in productCategories
                   where c.IsMain
                   select c;
        }
    }
}
