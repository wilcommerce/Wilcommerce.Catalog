using System;
using System.Linq;
using Wilcommerce.Catalog.Models;

namespace Wilcommerce.Catalog.ReadModels
{
    public static class ProductImageExtensions
    {
        public static IQueryable<ProductImage> ByProduct(this IQueryable<ProductImage> images, Guid productId)
        {
            return from i in images
                   where i.Product.Id == productId
                   select i;
        }
    }
}
