using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.CustomAttribute
{
    public class CustomAttributeCreatedEvent : DomainEvent
    {
        public Guid AttributeId { get; }

        public string Name { get; }

        public string Type { get; }

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
