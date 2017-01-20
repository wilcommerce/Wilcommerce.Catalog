using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Product
{
    public class ProductUnitInStockChangedEvent : DomainEvent
    {
        public Guid ProductId { get; }

        public int UnitInStock { get; }

        public ProductUnitInStockChangedEvent(Guid productId, int unitInStock)
            : base()
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
