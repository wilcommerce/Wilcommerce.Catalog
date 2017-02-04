using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.CustomAttribute
{
    public class CustomAttributeRestoredEvent : DomainEvent
    {
        public Guid AttributeId { get; }

        public CustomAttributeRestoredEvent(Guid attributeId)
            : base(attributeId, typeof(Models.CustomAttribute))
        {
            AttributeId = attributeId;
        }

        public override string ToString()
        {
            return $"[{FiredOn.ToString()}] Attribute {AttributeId} restored";
        }
    }
}
