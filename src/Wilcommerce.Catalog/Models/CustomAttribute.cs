using System;
using System.Collections.Generic;
using System.Linq;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Models
{
    /// <summary>
    /// Represents a custom attribute
    /// </summary>
    public class CustomAttribute : IAggregateRoot
    {
        /// <summary>
        /// Get or set the Custom attribute id
        /// </summary>
        public Guid Id { get; set; }

        #region Constructor
        protected CustomAttribute() { }
        #endregion

        #region Properties
        /// <summary>
        /// Get or set the attribute's name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Get or set a description for the attribute
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Get or set the unit of measure of the attribute
        /// </summary>
        public string UnitOfMeasure { get; set; }

        /// <summary>
        /// Get or set the attribute's data type
        /// </summary>
        public string DataType { get; set; }

        /// <summary>
        /// Get or set whether the custom attribute is deleted
        /// </summary>
        public bool Deleted { get; set; }

        /// <summary>
        /// Get or set the available values for the attribute
        /// </summary>
        protected virtual string _Values { get; set; }

        /// <summary>
        /// Get the list of available values for the attribute
        /// </summary>
        public IEnumerable<object> Values => _Values?.Split(",".ToCharArray()).AsEnumerable<object>();

        #endregion

        #region Behaviors
        /// <summary>
        /// Add a value to the available values
        /// </summary>
        /// <param name="value">The value to add</param>
        public virtual void AddValue(object value)
        {
            if(value == null)
            {
                throw new ArgumentNullException("value");
            }

            var valueList = string.IsNullOrEmpty(_Values) ? (new List<object>()) : _Values.Split(",".ToCharArray()).ToList<object>();
            if (valueList.Contains(value))
            {
                throw new ArgumentException("The value list contains the value", "value");
            }

            valueList.Add(value);
            _Values = string.Join(",", valueList);
        }

        /// <summary>
        /// Remove the value from the available values
        /// </summary>
        /// <param name="value">The value to remove</param>
        public virtual void RemoveValue(object value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            if (string.IsNullOrEmpty(_Values))
            {
                throw new InvalidOperationException("Cannot remove item from empty list");
            }

            var valueList = _Values.Split(",".ToCharArray()).ToList<object>();
            if (!valueList.Contains(value))
            {
                throw new ArgumentException("The value list does not contains the value", "value");
            }

            if (!valueList.Remove(value))
            {
                throw new Exception("The value cannot be removed from the list");
            }

            _Values = string.Join(",", valueList);
        }

        #endregion

        #region Factory Methods
        /// <summary>
        /// Create a new custom attribute
        /// </summary>
        /// <param name="name">The attribute's name</param>
        /// <param name="type">The attribute's data type</param>
        /// <returns>The created attribute</returns>
        public static CustomAttribute Create(string name, string type)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }

            if (string.IsNullOrEmpty(type))
            {
                throw new ArgumentNullException("type");
            }

            var attribute = new CustomAttribute
            {
                Id = Guid.NewGuid(),
                Name = name,
                DataType = type
            };

            return attribute;
        }

        #endregion
    }
}
