using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Category
{
    /// <summary>
    /// New category created
    /// </summary>
    public class CategoryCreatedEvent : DomainEvent
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
        /// Get the category code
        /// </summary>
        public string Code { get; private set; }

        /// <summary>
        /// Construct the event
        /// </summary>
        /// <param name="categoryId">The category id</param>
        /// <param name="name">The category name</param>
        /// <param name="code">The category code</param>
        public CategoryCreatedEvent(Guid categoryId, string name, string code)
            : base(categoryId, typeof(Models.Category))
        {
            CategoryId = categoryId;
            Name = name;
            Code = code;
        }

        /// <summary>
        /// Convert the event to string
        /// </summary>
        /// <returns>The converted string</returns>
        public override string ToString()
        {
            return $"Category {Name} - {Code} [{CategoryId}] created successfully";
        }
    }
}
