using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Product
{
    /// <summary>
    /// Product variant removed
    /// </summary>
    public class ProductVariantRemovedEvent : DomainEvent
    {
        /// <summary>
        /// Get the product id
        /// </summary>
        public Guid ProductId { get; private set; }

        /// <summary>
        /// Get the variant id
        /// </summary>
        public Guid VariantId { get; private set; }

        /// <summary>
        /// Construct the event
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="variantId">The variant id</param>
        public ProductVariantRemovedEvent(Guid productId, Guid variantId)
            : base(productId, typeof(Models.Product))
        {
            ProductId = productId;
            VariantId = variantId;
        }

        /// <summary>
        /// Convert the event to string
        /// </summary>
        /// <returns>The converted string</returns>
        public override string ToString()
        {
            return $"[{FiredOn.ToString()}] Variant {VariantId} removed from product {ProductId}";
        }
    }
}
