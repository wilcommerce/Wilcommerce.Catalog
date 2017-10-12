﻿using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Product
{
    public class ProductSkuChangedEvent : DomainEvent
    {
        public Guid ProductId { get; private set; }

        public string Sku { get; private set; }

        public ProductSkuChangedEvent(Guid productId, string sku)
            : base(productId, typeof(Models.Product))
        {
            ProductId = productId;
            Sku = sku;
        }

        public override string ToString()
        {
            return $"[{FiredOn.ToString()}] Product {ProductId} SKU changed to {Sku}";
        }
    }
}
