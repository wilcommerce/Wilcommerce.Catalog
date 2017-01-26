using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Product
{
    public class ProductVendorSetEvent : DomainEvent
    {
        public Guid ProductId { get; }

        public Guid VendorId { get; }

        public ProductVendorSetEvent(Guid productId, Guid vendorId)
            : base()
        {
            ProductId = productId;
            VendorId = vendorId;
        }

        public override string ToString()
        {
            return $"[{FiredOn.ToString()}] Set vendor {VendorId} to product {ProductId}";
        }
    }
}
