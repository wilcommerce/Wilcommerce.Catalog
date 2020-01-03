using System;
using System.Linq;
using Wilcommerce.Catalog.Models;

namespace Wilcommerce.Catalog.ReadModels
{
    /// <summary>
    /// Defines the extension methods for the product attribute read model
    /// </summary>
    public static class ProductAttributeExtensions
    {
        /// <summary>
        /// Retrieve all the product attributes for the specified product
        /// </summary>
        /// <param name="attributes"></param>
        /// <param name="productId">The product id</param>
        /// <returns>A list of product attributes</returns>
        public static IQueryable<ProductAttribute> ByProduct(this IQueryable<ProductAttribute> attributes, Guid productId)
        {
            if (attributes == null)
            {
                throw new ArgumentNullException(nameof(attributes));
            }

            if (productId == Guid.Empty)
            {
                throw new ArgumentException("value cannot be empty", nameof(productId));
            }

            return from a in attributes
                   where a.Product.Id == productId
                   select a;
        }
    }
}
