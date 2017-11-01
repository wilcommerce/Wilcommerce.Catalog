using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.CustomAttribute
{
    /// <summary>
    /// Attribute unit of measure set
    /// </summary>
    public class CustomAttributeUnitOfMeasureSetEvent : DomainEvent
    {
        /// <summary>
        /// Get the attribute id
        /// </summary>
        public Guid AttributeId { get; private set; }

        /// <summary>
        /// Get the attribute unit of measure
        /// </summary>
        public string UnitOfMeasure { get; private set; }

        /// <summary>
        /// Construct the event
        /// </summary>
        /// <param name="attributeId">The attribute id</param>
        /// <param name="unitOfMeasure">The attribute unit of measure</param>
        public CustomAttributeUnitOfMeasureSetEvent(Guid attributeId, string unitOfMeasure)
            : base(attributeId, typeof(Models.CustomAttribute))
        {
            AttributeId = attributeId;
            UnitOfMeasure = unitOfMeasure;
        }

        /// <summary>
        /// Convert the event to string
        /// </summary>
        /// <returns>The converted string</returns>
        public override string ToString()
        {
            return $"[{FiredOn.ToString()}] Attribute {AttributeId} unit of measure set to {UnitOfMeasure}";
        }
    }
}
