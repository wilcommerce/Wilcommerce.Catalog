using System;
using System.Linq;
using Wilcommerce.Catalog.Models;

namespace Wilcommerce.Catalog.ReadModels.Extensions
{
    public static class TierPriceExtensions
    {
        public static IQueryable<TierPrice> ByProduct(this IQueryable<TierPrice> tierPrices, Guid productId)
        {
            return from t in tierPrices
                   where t.Product.Id == productId
                   select t;
        }
    }
}
