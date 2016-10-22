using System;
using System.Linq;
using Wilcommerce.Catalog.Models;

namespace Wilcommerce.Catalog.ReadModels
{
    public static class ProductReviewExtensions
    {
        public static IQueryable<ProductReview> ByProduct(this IQueryable<ProductReview> reviews, Guid productId)
        {
            return from r in reviews
                   where r.Product.Id == productId
                   select r;
        }

        public static IQueryable<ProductReview> Approved(this IQueryable<ProductReview> reviews)
        {
            return from r in reviews
                   where r.Approved
                   select r;
        }
    }
}
