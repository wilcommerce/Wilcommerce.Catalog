using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Product
{
    public class ProductVendorSetEvent : DomainEvent
    {
        public Guid ProductId { get; private set; }

        public Guid VendorId { get; private set; }

        public ProductVendorSetEvent(Guid productId, Guid vendorId)
            : base(productId, typeof(Models.Product))
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
