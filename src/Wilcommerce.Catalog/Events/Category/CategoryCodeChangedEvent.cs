using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Category
{
    /// <summary>
    /// Category code changed
    /// </summary>
    public class CategoryCodeChangedEvent : DomainEvent
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
        /// Construct the event
        /// </summary>
        /// <param name="categoryId">The category id</param>
        /// <param name="code">The category code</param>
        public CategoryCodeChangedEvent(Guid categoryId, string code)
            : base(categoryId, typeof(Models.Category))
        {
            CategoryId = categoryId;
            Code = code;
        }

        /// <summary>
        /// Convert the event to string
        /// </summary>
        /// <returns>The converted string</returns>
        public override string ToString()
        {
            return $"[{FiredOn.ToString()}] Category {CategoryId} code changed to {Code}";
        }
    }
}
