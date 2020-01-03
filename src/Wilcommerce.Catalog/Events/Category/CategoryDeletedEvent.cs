using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Category
{
    /// <summary>
    /// Category deleted
    /// </summary>
    public class CategoryDeletedEvent : DomainEvent
    {
        /// <summary>
        /// Get the category id
        /// </summary>
        public Guid CategoryId { get; private set; }

        /// <summary>
        /// Construct the event
        /// </summary>
        /// <param name="categoryId">The category id</param>
        public CategoryDeletedEvent(Guid categoryId)
            : base(categoryId, typeof(Models.Category))
        {
            CategoryId = categoryId;
        }

        /// <summary>
        /// Convert the event to string
        /// </summary>
        /// <returns>The converted string</returns>
        public override string ToString()
        {
            return $"Category {CategoryId} deleted";
        }
    }
}
