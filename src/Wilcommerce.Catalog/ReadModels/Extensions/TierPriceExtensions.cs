using System;
using System.Linq;
using Wilcommerce.Catalog.Models;

namespace Wilcommerce.Catalog.ReadModels.Extensions
{
    /// <summary>
    /// Defines the extension methods for the tier price read model
    /// </summary>
    public static class TierPriceExtensions
    {
        /// <summary>
        /// Retrieve all the tier prices filtered by the specified product
        /// </summary>
        /// <param name="tierPrices"></param>
        /// <param name="productId">The product id</param>
        /// <returns>A list of tier prices</returns>
        public static IQueryable<TierPrice> ByProduct(this IQueryable<TierPrice> tierPrices, Guid productId)
        {
            return from t in tierPrices
                   where t.Product.Id == productId
                   select t;
        }
    }
}
