using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.CustomAttribute
{
    public class CustomAttributeCreatedEvent : DomainEvent
    {
        public Guid AttributeId { get; private set; }

        public string Name { get; private set; }

        public string Type { get; private set; }

        public CustomAttributeCreatedEvent(Guid attributeId, string name, string type)
            : base(attributeId, typeof(Models.CustomAttribute))
        {
            AttributeId = attributeId;
            Name = name;
            Type = type;
        }

        public override string ToString()
        {
            return $"[{FiredOn.ToString()}] Attribute {AttributeId} with name {Name} and type {Type} created";
        }
    }
}
