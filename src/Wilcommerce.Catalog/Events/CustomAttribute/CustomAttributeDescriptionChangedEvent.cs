using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.CustomAttribute
{
    public class CustomAttributeDescriptionChangedEvent : DomainEvent
    {
        public Guid AttributeId { get; private set; }

        public string Description { get; private set; }

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
