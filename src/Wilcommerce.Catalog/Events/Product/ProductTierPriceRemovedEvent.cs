using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Product
{
    /// <summary>
    /// Tier price removed from product
    /// </summary>
    public class ProductTierPriceRemovedEvent : DomainEvent
    {
        /// <summary>
        /// Get the product id
        /// </summary>
        public Guid ProductId { get; private set; }

        /// <summary>
        /// Get the tier price id
        /// </summary>
        public Guid TierPriceId { get; private set; }

        /// <summary>
        /// Construct the event
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="tierPriceId">The tier price id</param>
        public ProductTierPriceRemovedEvent(Guid productId, Guid tierPriceId)
            : base(productId, typeof(Models.Product))
        {
            ProductId = productId;
            TierPriceId = tierPriceId;
        }

        /// <summary>
        /// Convert the event to string
        /// </summary>
        /// <returns>The converted string</returns>
        public override string ToString()
        {
            return $"[{FiredOn.ToString()}] Tier price {TierPriceId} removed from product {ProductId}";
        }
    }
}
