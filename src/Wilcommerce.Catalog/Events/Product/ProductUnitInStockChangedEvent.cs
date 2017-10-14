using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Product
{
    public class ProductUnitInStockChangedEvent : DomainEvent
    {
        public Guid ProductId { get; private set; }

        public int UnitInStock { get; private set; }

        public ProductUnitInStockChangedEvent(Guid productId, int unitInStock)
            : base(productId, typeof(Models.Product))
        {
            ProductId = productId;
            UnitInStock = unitInStock;
        }

        public override string ToString()
        {
            return $"[{FiredOn.ToString()}] Product {ProductId} unit in stock changed to {UnitInStock}";
        }
    }
}
