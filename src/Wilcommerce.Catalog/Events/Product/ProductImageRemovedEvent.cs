﻿using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Product
{
    public class ProductImageRemovedEvent : DomainEvent
    {
        public Guid ProductId { get; private set; }

        public Guid ImageId { get; private set; }

        public ProductImageRemovedEvent(Guid productId, Guid imageId)
            : base(productId, typeof(Models.Product))
        {
            ProductId = productId;
            ImageId = imageId;
        }

        public override string ToString()
        {
            return $"[{FiredOn.ToString()}] Image {ImageId} removed from product {ProductId}";
        }
    }
}
