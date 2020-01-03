using System;
using System.Linq;
using Wilcommerce.Catalog.Models;

namespace Wilcommerce.Catalog.ReadModels
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
            if (tierPrices == null)
            {
                throw new ArgumentNullException(nameof(tierPrices));
            }

            if (productId == Guid.Empty)
            {
                throw new ArgumentException("value cannot be empty", nameof(productId));
            }

            return from t in tierPrices
                   where t.Product != null && t.Product.Id == productId
                   select t;
        }
    }
}
