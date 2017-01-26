using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.CustomAttribute
{
    public class CustomAttributeNameChangedEvent : DomainEvent
    {
        public Guid AttributeId { get; }

        public string Name { get; }

        public CustomAttributeNameChangedEvent(Guid attributeId, string name)
            : base()
        {
            AttributeId = attributeId;
            Name = name;
        }

        public override string ToString()
        {
            return $"[{FiredOn.ToString()}] Attribute {AttributeId} name changed to {Name}";
        }
    }
}
