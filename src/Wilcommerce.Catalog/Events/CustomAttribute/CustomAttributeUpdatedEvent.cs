using System;
using System.Collections.Generic;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.CustomAttribute
{
    public class CustomAttributeUpdatedEvent : DomainEvent
    {
        public Guid AttributeId { get; private set; }
        public string Name { get; private set; }
        public string Type { get; private set; }
        public string Description { get; private set; }
        public string UnitOfMeasure { get; private set; }
        public IEnumerable<object> Values { get; private set; }

        public CustomAttributeUpdatedEvent(Guid attributeId, string name, string type, string description, string unitOfMeasure, IEnumerable<object> values)
            : base(attributeId, typeof(Models.CustomAttribute))
        {
            AttributeId = attributeId;
            Name = name;
            Type = type;
            Description = description;
            UnitOfMeasure = unitOfMeasure;
            Values = values;
        }
    }
}
