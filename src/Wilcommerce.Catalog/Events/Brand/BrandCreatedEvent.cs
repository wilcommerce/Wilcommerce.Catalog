﻿using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Brand
{
    public class BrandCreatedEvent : DomainEvent
    {
        public Guid BrandId { get; private set; }

        public string Name { get; private set; }

        public BrandCreatedEvent(Guid brandId, string name)
            : base(brandId, typeof(Models.Brand))
        {
            BrandId = brandId;
            Name = name;
        }

        public override string ToString()
        {
            return $"[{FiredOn.ToString()}] Brand {Name} [{BrandId}] created successfully";
        }
    }
}
