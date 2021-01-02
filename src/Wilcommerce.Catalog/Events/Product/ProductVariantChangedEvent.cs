using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Product
{
    /// <summary>
    /// Product variant changed
    /// </summary>
    public class ProductVariantChangedEvent : DomainEvent
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
        /// Get the variant name
        /// </summary>
        public string VariantName { get; private set; }

        /// <summary>
        /// Get the variant EAN code
        /// </summary>
        public string EanCode { get; private set; }

        /// <summary>
        /// Get the variant SKU code
        /// </summary>
        public string Sku { get; private set; }

        /// <summary>
        /// Construct the event
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="variantId">The variant id</param>
        /// <param name="name">The variant name</param>
        /// <param name="ean">The variant EAN code</param>
        /// <param name="sku">The variant SKU</param>
        /// <param name="userId">The user's id</param>
        public ProductVariantChangedEvent(Guid productId, Guid variantId, string name, string ean, string sku, string userId)
            : base(productId, typeof(Models.Product), userId)
        {
            ProductId = productId;
            VariantId = variantId;
            VariantName = name;
            EanCode = ean;
            Sku = sku;
        }

        /// <summary>
        /// Convert the event to string
        /// </summary>
        /// <returns>The converted string</returns>
        public override string ToString()
        {
            return $"Variant {VariantId} changed with values: {VariantName} (EAN: {EanCode} - SKU: {Sku})";
        }
    }
}
