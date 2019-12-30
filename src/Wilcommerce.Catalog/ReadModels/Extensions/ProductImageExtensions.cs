using System;
using System.Linq;
using Wilcommerce.Catalog.Models;

namespace Wilcommerce.Catalog.ReadModels
{
    /// <summary>
    /// Defines the extension methods for the product image read model
    /// </summary>
    public static class ProductImageExtensions
    {
        /// <summary>
        /// Retrieve all the images filtered by the specified product
        /// </summary>
        /// <param name="images"></param>
        /// <param name="productId">The product id</param>
        /// <returns>A list of product images</returns>
        public static IQueryable<ProductImage> ByProduct(this IQueryable<ProductImage> images, Guid productId)
        {
            if (images == null)
            {
                throw new ArgumentNullException(nameof(images));
            }

            if (productId == Guid.Empty)
            {
                throw new ArgumentException("value cannot be empty", nameof(productId));
            }

            return from i in images
                   where i.Product != null && i.Product.Id == productId
                   select i;
        }
    }
}
