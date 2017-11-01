using System;
using System.Linq;
using Wilcommerce.Catalog.Models;

namespace Wilcommerce.Catalog.ReadModels
{
    /// <summary>
    /// Defines the extension methods for the product review read model
    /// </summary>
    public static class ProductReviewExtensions
    {
        /// <summary>
        /// Retrieve all the reviews filtered by the specified product
        /// </summary>
        /// <param name="reviews"></param>
        /// <param name="productId">The product id</param>
        /// <returns>A list of product reviews</returns>
        public static IQueryable<ProductReview> ByProduct(this IQueryable<ProductReview> reviews, Guid productId)
        {
            return from r in reviews
                   where r.Product.Id == productId
                   select r;
        }

        /// <summary>
        /// Retrieve all the approved reviews
        /// </summary>
        /// <param name="reviews"></param>
        /// <returns>A list of product reviews</returns>
        public static IQueryable<ProductReview> Approved(this IQueryable<ProductReview> reviews)
        {
            return from r in reviews
                   where r.Approved
                   select r;
        }
    }
}
