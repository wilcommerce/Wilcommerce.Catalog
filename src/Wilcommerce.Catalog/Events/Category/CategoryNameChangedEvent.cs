using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Category
{
    /// <summary>
    /// Category name changed
    /// </summary>
    public class CategoryNameChangedEvent : DomainEvent
    {
        /// <summary>
        /// Get the category id
        /// </summary>
        public Guid CategoryId { get; private set; }

        /// <summary>
        /// Get the category name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Construct the event
        /// </summary>
        /// <param name="categoryId">The category id</param>
        /// <param name="name">The category name</param>
        public CategoryNameChangedEvent(Guid categoryId, string name)
            : base(categoryId, typeof(Models.Category))
        {
            CategoryId = categoryId;
            Name = name;
        }

        /// <summary>
        /// Convert the event to string
        /// </summary>
        /// <returns>The converted string</returns>
        public override string ToString()
        {
            return $"[{FiredOn.ToString()}] Category {CategoryId} name changed to {Name}";
        }
    }
}
