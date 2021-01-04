using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Product
{
    /// <summary>
    /// Product vendor set
    /// </summary>
    public class ProductBrandSetEvent : DomainEvent
    {
        /// <summary>
        /// Get the product id
        /// </summary>
        public Guid ProductId { get; private set; }

        /// <summary>
        /// Get the brand id
        /// </summary>
        public Guid BrandId { get; private set; }

        /// <summary>
        /// Construct the event
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="brandId">The vendor id</param>
        /// <param name="userId">The user's id</param>
        public ProductBrandSetEvent(Guid productId, Guid brandId, string userId)
            : base(productId, typeof(Models.Product), userId)
        {
            ProductId = productId;
            BrandId = brandId;
        }

        /// <summary>
        /// Convert the event to string
        /// </summary>
        /// <returns>The converted string</returns>
        public override string ToString()
        {
            return $"Set vendor {BrandId} to product {ProductId}";
        }
    }
}
