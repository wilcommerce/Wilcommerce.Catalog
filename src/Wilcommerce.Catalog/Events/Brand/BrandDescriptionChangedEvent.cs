﻿using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Brand
{
    public class BrandDescriptionChangedEvent : DomainEvent
    {
        public Guid BrandId { get; private set; }

        public string Description { get; private set; }

        public BrandDescriptionChangedEvent(Guid brandId, string description)
            : base(brandId, typeof(Models.Brand))
        {
            BrandId = brandId;
            Description = description;
        }

        public override string ToString()
        {
            return $"[{FiredOn.ToString()}] {BrandId} change description to {Description}";
        }
    }
}
