﻿using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Brand
{
    public class BrandUrlChangedEvent : DomainEvent
    {
        public Guid BrandId { get; private set; }

        public string Url { get; private set; }

        public BrandUrlChangedEvent(Guid brandId, string url)
            : base(brandId, typeof(Models.Brand))
        {
            BrandId = brandId;
            Url = url;
        }

        public override string ToString()
        {
            return $"[{FiredOn.ToString()}] {BrandId} change url to {Url}";
        }
    }
}
