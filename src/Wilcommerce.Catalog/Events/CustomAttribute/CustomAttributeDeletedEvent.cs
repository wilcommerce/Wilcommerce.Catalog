﻿using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.CustomAttribute
{
    public class CustomAttributeDeletedEvent : DomainEvent
    {
        public Guid AttributeId { get; }

        public CustomAttributeDeletedEvent(Guid attributeId)
            : base()
        {
            AttributeId = attributeId;
        }

        public override string ToString()
        {
            return $"[{FiredOn.ToString()}] Attribute {AttributeId} deleted";
        }
    }
}
