using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Category
{
    /// <summary>
    /// Category description changed
    /// </summary>
    public class CategoryDescriptionChangedEvent : DomainEvent
    {
        /// <summary>
        /// Get the category id
        /// </summary>
        public Guid CategoryId { get; private set; }

        /// <summary>
        /// Get the category description
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Construct the event
        /// </summary>
        /// <param name="categoryId">The category id</param>
        /// <param name="description">The category description</param>
        public CategoryDescriptionChangedEvent(Guid categoryId, string description)
            : base(categoryId, typeof(Models.Category))
        {
            CategoryId = categoryId;
            Description = description;
        }

        /// <summary>
        /// Convert the event to string
        /// </summary>
        /// <returns>The converted string</returns>
        public override string ToString()
        {
            return $"[{FiredOn.ToString()}] Category {CategoryId} description changed to {Description}";
        }
    }
}
