using System;
using System.Linq;
using Wilcommerce.Catalog.Models;

namespace Wilcommerce.Catalog.ReadModels
{
    public static class ProductAttributeExtensions
    {
        public static IQueryable<ProductAttribute> ByProduct(this IQueryable<ProductAttribute> attributes, Guid productId)
        {
            return from a in attributes
                   where a.Product.Id == productId
                   select a;
        }
    }
}
