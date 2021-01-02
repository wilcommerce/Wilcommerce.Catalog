using System;
using System.Collections.Generic;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.CustomAttribute
{
    /// <summary>
    /// Attribute updated
    /// </summary>
    public class CustomAttributeUpdatedEvent : DomainEvent
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
        /// Get the attribute description
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Get the attribute unit of measure
        /// </summary>
        public string UnitOfMeasure { get; private set; }
        
        /// <summary>
        /// Get the attribute values
        /// </summary>
        public IEnumerable<object> Values { get; private set; }

        /// <summary>
        /// Construct the event
        /// </summary>
        /// <param name="attributeId">The attribute id</param>
        /// <param name="name">The attribute name</param>
        /// <param name="type">The attribute type</param>
        /// <param name="description">The attribute description</param>
        /// <param name="unitOfMeasure">The attribute unit of measure</param>
        /// <param name="values">The attribute values</param>
        /// <param name="userId">The user's id</param>
        public CustomAttributeUpdatedEvent(Guid attributeId, string name, string type, string description, string unitOfMeasure, IEnumerable<object> values, string userId)
            : base(attributeId, typeof(Models.CustomAttribute), userId)
        {
            AttributeId = attributeId;
            Name = name;
            Type = type;
            Description = description;
            UnitOfMeasure = unitOfMeasure;
            Values = values;
        }

        /// <summary>
        /// Convert the event to string
        /// </summary>
        /// <returns>The converted string</returns>
        public override string ToString()
        {
            return $"Attribute {AttributeId} updated. Name: {Name}, Type: {Type}, Description: {Description}, Unit of measure: {UnitOfMeasure}";
        }
    }
}
