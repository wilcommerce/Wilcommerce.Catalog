using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.CustomAttribute
{
    /// <summary>
    /// Attribute name changed
    /// </summary>
    public class CustomAttributeNameChangedEvent : DomainEvent
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
        /// Construct the event
        /// </summary>
        /// <param name="attributeId">The attribute id</param>
        /// <param name="name">The attribute name</param>
        public CustomAttributeNameChangedEvent(Guid attributeId, string name)
            : base(attributeId, typeof(Models.CustomAttribute))
        {
            AttributeId = attributeId;
            Name = name;
        }

        /// <summary>
        /// Convert the event to string
        /// </summary>
        /// <returns>The converted string</returns>
        public override string ToString()
        {
            return $"[{FiredOn.ToString()}] Attribute {AttributeId} name changed to {Name}";
        }
    }
}
