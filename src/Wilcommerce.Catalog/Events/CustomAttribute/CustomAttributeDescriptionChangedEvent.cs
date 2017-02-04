using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.CustomAttribute
{
    public class CustomAttributeDescriptionChangedEvent : DomainEvent
    {
        public Guid AttributeId { get; }

        public string Description { get; }

        public CustomAttributeDescriptionChangedEvent(Guid attributeId, string description)
            : base(attributeId, typeof(Models.CustomAttribute))
        {
            AttributeId = attributeId;
            Description = description;
        }

        public override string ToString()
        {
            return $"[{FiredOn.ToString()}] Attribute {AttributeId} description changed to {Description}";
        }
    }
}
