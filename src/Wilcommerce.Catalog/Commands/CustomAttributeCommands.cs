using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wilcommerce.Catalog.Events.CustomAttribute;
using Wilcommerce.Catalog.Models;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Commands
{
    /// <summary>
    /// Implementation of <see cref="ICustomAttributesCommands"/>
    /// </summary>
    public class CustomAttributeCommands : ICustomAttributesCommands
    {
        /// <summary>
        /// Get the repository
        /// </summary>
        public Repository.IRepository Repository { get; }

        /// <summary>
        /// Get the event bus
        /// </summary>
        public IEventBus EventBus { get; }

        /// <summary>
        /// Construct the custom attribute commands
        /// </summary>
        /// <param name="repository">The repository</param>
        /// <param name="eventBus">The event bus</param>
        public CustomAttributeCommands(Repository.IRepository repository, IEventBus eventBus)
        {
            Repository = repository ?? throw new ArgumentNullException(nameof(repository));
            EventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }

        #region CustomAttribute Commands
        /// <summary>
        /// Implementation of <see cref="ICustomAttributesCommands.CreateNewCustomAttribute(string, string, string, string, IEnumerable{object})"/>
        /// </summary>
        /// <param name="name">The custom attribute's name</param>
        /// <param name="type">The data type of the custom attribute</param>
        /// <param name="description">The custom attribute's description</param>
        /// <param name="unitOfMeasure">The unit of measure of the custom attribute</param>
        /// <param name="values">The available values for the custom attribute</param>
        /// <returns>The custom attribute id</returns>
        public async Task<Guid> CreateNewCustomAttribute(string name, string type, string description, string unitOfMeasure, IEnumerable<object> values)
        {
            try
            {
                var attribute = CustomAttribute.Create(name, type);
                if (!string.IsNullOrWhiteSpace(description))
                {
                    attribute.ChangeDescription(description);
                }

                if (!string.IsNullOrWhiteSpace(unitOfMeasure))
                {
                    attribute.SetUnitOfMeasure(unitOfMeasure);
                }

                if (values != null && values.Count() > 0)
                {
                    values.ToList().ForEach(v => attribute.AddValue(v));
                }

                Repository.Add(attribute);
                await Repository.SaveChangesAsync();

                var @event = new CustomAttributeCreatedEvent(attribute.Id, attribute.Name, attribute.DataType);
                EventBus.RaiseEvent(@event);

                return attribute.Id;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Implementation of <see cref="ICustomAttributesCommands.UpdateCustomAttribute(Guid, string, string, string, string, IEnumerable{object})"/>
        /// </summary>
        /// <param name="attributeId">The custom attribute id</param>
        /// <param name="name">The custom attribute name</param>
        /// <param name="type">The custom attribute data type</param>
        /// <param name="description">The custom attribute description</param>
        /// <param name="unitOfMeasure">The custom attribute unit of measure</param>
        /// <param name="values">The custom attribute available values</param>
        /// <returns></returns>
        public async Task UpdateCustomAttribute(Guid attributeId, string name, string type, string description, string unitOfMeasure, IEnumerable<object> values)
        {
            try
            {
                if (attributeId == Guid.Empty)
                {
                    throw new ArgumentException("value cannot be empty", nameof(attributeId));
                }

                var attribute = await Repository.GetByKeyAsync<CustomAttribute>(attributeId);
                if (attribute.Name != name)
                {
                    attribute.ChangeName(name);
                }

                if (attribute.DataType != type)
                {
                    attribute.ChangeDataType(type);
                }

                if (attribute.Description != description)
                {
                    attribute.ChangeDescription(description);
                }

                if (attribute.UnitOfMeasure != unitOfMeasure)
                {
                    attribute.SetUnitOfMeasure(unitOfMeasure);
                }

                UpdateCustomAttributeValues(attribute, values);

                await Repository.SaveChangesAsync();

                var @event = new CustomAttributeUpdatedEvent(attributeId, name, type, description, unitOfMeasure, values);
                EventBus.RaiseEvent(@event);
            }
            catch 
            {
                throw;
            }
        }

        /// <summary>
        /// Implementation of <see cref="ICustomAttributesCommands.DeleteCustomAttribute(Guid)"/>
        /// </summary>
        /// <param name="attributeId">The custom attribute id</param>
        /// <returns></returns>
        public async Task DeleteCustomAttribute(Guid attributeId)
        {
            try
            {
                if (attributeId == Guid.Empty)
                {
                    throw new ArgumentException("value cannot be empty", nameof(attributeId));
                }

                var attribute = await Repository.GetByKeyAsync<CustomAttribute>(attributeId);
                attribute.Delete();

                await Repository.SaveChangesAsync();

                var @event = new CustomAttributeDeletedEvent(attributeId);
                EventBus.RaiseEvent(@event);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Implementation of <see cref="ICustomAttributesCommands.RestoreCustomAttribute(Guid)"/>
        /// </summary>
        /// <param name="attributeId">The custom attribute id</param>
        /// <returns></returns>
        public async Task RestoreCustomAttribute(Guid attributeId)
        {
            try
            {
                if (attributeId == Guid.Empty)
                {
                    throw new ArgumentException("value cannot be empty", nameof(attributeId));
                }

                var attribute = await Repository.GetByKeyAsync<CustomAttribute>(attributeId);
                attribute.Restore();

                await Repository.SaveChangesAsync();

                var @event = new CustomAttributeRestoredEvent(attributeId);
                EventBus.RaiseEvent(@event);
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region Private methods
        private void UpdateCustomAttributeValues(CustomAttribute attribute, IEnumerable<object> values)
        {
            var attributeValues = attribute.Values ?? new object[0];

            var valuesToAdd = values.Where(v => !attributeValues.Contains(v)).ToArray();
            var valuesToRemove = attributeValues.Where(v => !values.Contains(v)).ToArray();

            if (valuesToRemove.Any())
            {
                valuesToRemove
                    .ToList()
                    .ForEach(v => attribute.RemoveValue(v));
            }

            if (valuesToAdd.Any())
            {
                valuesToAdd
                    .ToList()
                    .ForEach(v => attribute.AddValue(v));
            }
        }
        #endregion
    }
}
