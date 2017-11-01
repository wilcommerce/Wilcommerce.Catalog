using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.CustomAttribute
{
    /// <summary>
    /// Attribute value removed
    /// </summary>
    public class CustomAttributeValueRemovedEvent : DomainEvent
    {
        /// <summary>
        /// Get the attribute id
        /// </summary>
        public Guid AttributeId { get; private set; }

        /// <summary>
        /// Get the removed value
        /// </summary>
        public object Value { get; private set; }

        /// <summary>
        /// Construct the event
        /// </summary>
        /// <param name="attributeId">The attribute id</param>
        /// <param name="value">The removed value</param>
        public CustomAttributeValueRemovedEvent(Guid attributeId, object value)
            : base(attributeId, typeof(Models.CustomAttribute))
        {
            AttributeId = attributeId;
            Value = value;
        }

        /// <summary>
        /// Convert the event to string
        /// </summary>
        /// <returns>The converted string</returns>
        public override string ToString()
        {
            return $"[{FiredOn.ToString()}] {Value} removed from attribute {AttributeId}";
        }
    }
}
