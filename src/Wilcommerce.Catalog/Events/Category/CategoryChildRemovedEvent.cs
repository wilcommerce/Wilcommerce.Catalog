using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Category
{
    /// <summary>
    /// Category child removed
    /// </summary>
    public class CategoryChildRemovedEvent : DomainEvent
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
        public CategoryChildRemovedEvent(Guid categoryId, Guid childId)
            : base(categoryId, typeof(Models.Category))
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
            return $"[{FiredOn.ToString()}] Child {ChildId} removed from category {CategoryId}";
        }
    }
}
