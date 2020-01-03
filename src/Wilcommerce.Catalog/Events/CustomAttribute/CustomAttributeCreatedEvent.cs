using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.CustomAttribute
{
    /// <summary>
    /// New custom attribute created
    /// </summary>
    public class CustomAttributeCreatedEvent : DomainEvent
    {
        /// <summary>
        /// Get the attribute id
        /// </summary>
        public Guid AttributeId { get; private set; }

        /// <summary>
        /// Get the attribute name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Get the attribute type
        /// </summary>
        public string Type { get; private set; }

        /// <summary>
        /// Construct the event
        /// </summary>
        /// <param name="attributeId">The attribute id</param>
        /// <param name="name">The attribute name</param>
        /// <param name="type">The attribute type</param>
        public CustomAttributeCreatedEvent(Guid attributeId, string name, string type)
            : base(attributeId, typeof(Models.CustomAttribute))
        {
            AttributeId = attributeId;
            Name = name;
            Type = type;
        }

        /// <summary>
        /// Convert the event to string
        /// </summary>
        /// <returns>The converted string</returns>
        public override string ToString()
        {
            return $"Attribute {AttributeId} with name {Name} and type {Type} created";
        }
    }
}
