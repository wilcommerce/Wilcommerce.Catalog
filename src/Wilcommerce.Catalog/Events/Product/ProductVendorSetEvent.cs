using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Product
{
    /// <summary>
    /// Product vendor set
    /// </summary>
    public class ProductVendorSetEvent : DomainEvent
    {
        /// <summary>
        /// Get the product id
        /// </summary>
        public Guid ProductId { get; private set; }

        /// <summary>
        /// Get the vendor id
        /// </summary>
        public Guid VendorId { get; private set; }

        /// <summary>
        /// Construct the event
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="vendorId">The vendor id</param>
        public ProductVendorSetEvent(Guid productId, Guid vendorId)
            : base(productId, typeof(Models.Product))
        {
            ProductId = productId;
            VendorId = vendorId;
        }

        /// <summary>
        /// Convert the event to string
        /// </summary>
        /// <returns>The converted string</returns>
        public override string ToString()
        {
            return $"[{FiredOn.ToString()}] Set vendor {VendorId} to product {ProductId}";
        }
    }
}
