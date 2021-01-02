using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.CustomAttribute
{
    /// <summary>
    /// Attribute restored
    /// </summary>
    public class CustomAttributeRestoredEvent : DomainEvent
    {
        /// <summary>
        /// Get the attribute id
        /// </summary>
        public Guid AttributeId { get; private set; }

        /// <summary>
        /// Construct the event
        /// </summary>
        /// <param name="attributeId">The attribute id</param>
        /// <param name="userId">The user's id</param>
        public CustomAttributeRestoredEvent(Guid attributeId, string userId)
            : base(attributeId, typeof(Models.CustomAttribute), userId)
        {
            AttributeId = attributeId;
        }

        /// <summary>
        /// Convert the event to string
        /// </summary>
        /// <returns>The converted string</returns>
        public override string ToString()
        {
            return $"Attribute {AttributeId} restored";
        }
    }
}
