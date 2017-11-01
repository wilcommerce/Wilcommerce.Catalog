using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Category
{
    /// <summary>
    /// Category url changed
    /// </summary>
    public class CategoryUrlChangedEvent : DomainEvent
    {
        /// <summary>
        /// Get the category id
        /// </summary>
        public Guid CategoryId { get; private set; }

        /// <summary>
        /// Get the category url
        /// </summary>
        public string Url { get; private set; }

        /// <summary>
        /// Construct the event
        /// </summary>
        /// <param name="categoryId">The category id</param>
        /// <param name="url">The category url</param>
        public CategoryUrlChangedEvent(Guid categoryId, string url)
            : base(categoryId, typeof(Models.Category))
        {
            CategoryId = categoryId;
            Url = url;
        }
    }
}
