﻿using System;
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
        public Guid Id { get; protected set; }

        #region Constructor
        /// <summary>
        /// Construct the custom attribute
        /// </summary>
        protected CustomAttribute() { }
        #endregion

        #region Properties
        /// <summary>
        /// Get or set the attribute's name
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Get or set a description for the attribute
        /// </summary>
        public string Description { get; protected set; }

        /// <summary>
        /// Get or set the unit of measure of the attribute
        /// </summary>
        public string UnitOfMeasure { get; protected set; }

        /// <summary>
        /// Get or set the attribute's data type
        /// </summary>
        public string DataType { get; protected set; }

        /// <summary>
        /// Get or set whether the custom attribute is deleted
        /// </summary>
        public bool Deleted { get; protected set; }

        /// <summary>
        /// Get the string for the available values
        /// </summary>
        public string _Values { get; protected set; }

        /// <summary>
        /// Get the list of available values for the attribute
        /// </summary>
        public IEnumerable<object> Values => _Values?.Split(",".ToCharArray()).AsEnumerable<object>();

        /// <summary>
        /// Get the creation date
        /// </summary>
        public DateTime CreationDate { get; protected set; }
        #endregion

        #region Behaviors
        /// <summary>
        /// Add a value to the available values
        /// </summary>
        /// <param name="value">The value to add</param>
        public virtual void AddValue(object value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            var valueList = string.IsNullOrWhiteSpace(_Values) ? (new List<object>()) : _Values.Split(",".ToCharArray()).ToList<object>();
            if (valueList.Contains(value))
            {
                throw new ArgumentException("The value list contains the value", nameof(value));
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
                throw new ArgumentNullException(nameof(value));
            }

            if (string.IsNullOrEmpty(_Values))
            {
                throw new InvalidOperationException("Cannot remove item from empty list");
            }

            var valueList = _Values.Split(",".ToCharArray()).ToList<object>();
            if (!valueList.Contains(value))
            {
                throw new ArgumentException("The value list does not contains the value", nameof(value));
            }

            if (!valueList.Remove(value))
            {
                throw new Exception("The value cannot be removed from the list");
            }

            _Values = string.Join(",", valueList);
        }

        /// <summary>
        /// Change the attribute name
        /// </summary>
        /// <param name="name">The new name</param>
        public virtual void ChangeName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            Name = name;
        }

        /// <summary>
        /// Change the attribute description
        /// </summary>
        /// <param name="description">The description</param>
        public virtual void ChangeDescription(string description)
        {
            Description = description;
        }

        /// <summary>
        /// Set the attribute unit of measure
        /// </summary>
        /// <param name="unitOfMeasure">The unit of measure</param>
        public virtual void SetUnitOfMeasure(string unitOfMeasure)
        {
            if (string.IsNullOrWhiteSpace(unitOfMeasure))
            {
                throw new ArgumentNullException(nameof(unitOfMeasure));
            }

            UnitOfMeasure = unitOfMeasure;
        }

        /// <summary>
        /// Change the attribute data type
        /// </summary>
        /// <param name="dataType">The data type</param>
        public virtual void ChangeDataType(string dataType)
        {
            if (string.IsNullOrWhiteSpace(dataType))
            {
                throw new ArgumentNullException(nameof(dataType));
            }

            DataType = dataType;
        }

        /// <summary>
        /// Delete the attribute
        /// </summary>
        public virtual void Delete()
        {
            if (Deleted)
            {
                throw new InvalidOperationException("The attribute is already deleted");
            }

            Deleted = true;
        }

        /// <summary>
        /// Restore the deleted attribute
        /// </summary>
        public virtual void Restore()
        {
            if (!Deleted)
            {
                throw new InvalidOperationException("The attribute is not deleted");
            }

            Deleted = false;
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
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (string.IsNullOrWhiteSpace(type))
            {
                throw new ArgumentNullException(nameof(type));
            }

            var attribute = new CustomAttribute
            {
                Id = Guid.NewGuid(),
                Name = name,
                DataType = type,
                CreationDate = DateTime.Now
            };

            return attribute;
        }

        #endregion
    }
}
