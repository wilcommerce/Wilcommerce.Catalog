using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Category
{
    /// <summary>
    /// Category info updated
    /// </summary>
    public class CategoryInfoUpdatedEvent : DomainEvent
    {
        /// <summary>
        /// Get the category id
        /// </summary>
        public Guid CategoryId { get; private set; }

        /// <summary>
        /// Get the category code
        /// </summary>
        public string Code { get; private set; }

        /// <summary>
        /// Get the category name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Get the category url
        /// </summary>
        public string Url { get; private set; }

        /// <summary>
        /// Get the category description
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Whether the category is visible
        /// </summary>
        public bool IsVisible { get; private set; }

        /// <summary>
        /// Get the date and time from when the category is visible
        /// </summary>
        public DateTime? VisibleFrom { get; private set; }

        /// <summary>
        /// Get the date and time till when the category is visible
        /// </summary>
        public DateTime? VisibleTo { get; private set; }

        /// <summary>
        /// Construct the event
        /// </summary>
        /// <param name="categoryId">The category id</param>
        /// <param name="code">The category code</param>
        /// <param name="name">The category name</param>
        /// <param name="url">The category url</param>
        /// <param name="description">The category description</param>
        /// <param name="isVisible">Whether the category is visible</param>
        /// <param name="visibleFrom">The date and time from when the category is visible</param>
        /// <param name="visibleTo">The date and time till when the category is visible</param>
        public CategoryInfoUpdatedEvent(Guid categoryId, string code, string name, string url, string description, bool isVisible, DateTime? visibleFrom, DateTime? visibleTo)
            : base(categoryId, typeof(Models.Category))
        {
            CategoryId = categoryId;
            Code = code;
            Name = name;
            Url = url;
            Description = description;
            IsVisible = isVisible;
            VisibleFrom = visibleFrom;
            VisibleTo = visibleTo;
        }
    }
}
