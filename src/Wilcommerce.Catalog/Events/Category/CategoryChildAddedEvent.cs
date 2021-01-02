using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Category
{
    /// <summary>
    /// Category child added
    /// </summary>
    public class CategoryChildAddedEvent : DomainEvent
    {
        /// <summary>
        /// Get the category id
        /// </summary>
        public Guid CategoryId { get; private set; }

        /// <summary>
        /// Get the child id
        /// </summary>
        public Guid ChildId { get; private set; }

        /// <summary>
        /// Construct the event
        /// </summary>
        /// <param name="categoryId">The category id</param>
        /// <param name="childId">The child id</param>
        /// <param name="userId">The user's id</param>
        public CategoryChildAddedEvent(Guid categoryId, Guid childId, string userId)
            : base(categoryId, typeof(Models.Category), userId)
        {
            CategoryId = categoryId;
            ChildId = childId;
        }

        /// <summary>
        /// Convert the event to string
        /// </summary>
        /// <returns>The converted string</returns>
        public override string ToString()
        {
            return $"Child {ChildId} added to category {CategoryId}";
        }
    }
}
