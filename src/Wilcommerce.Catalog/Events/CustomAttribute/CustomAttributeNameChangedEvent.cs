using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.CustomAttribute
{
    public class CustomAttributeNameChangedEvent : DomainEvent
    {
        public Guid AttributeId { get; private set; }

        public string Name { get; private set; }

        public CustomAttributeNameChangedEvent(Guid attributeId, string name)
            : base(attributeId, typeof(Models.CustomAttribute))
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
