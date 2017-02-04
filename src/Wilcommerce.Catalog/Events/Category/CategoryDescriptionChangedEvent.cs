using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Category
{
    public class CategoryDescriptionChangedEvent : DomainEvent
    {
        public Guid CategoryId { get; }

        public string Description { get; }

        public CategoryDescriptionChangedEvent(Guid categoryId, string description)
            : base(categoryId, typeof(Models.Category))
        {
            CategoryId = categoryId;
            Description = description;
        }

        public override string ToString()
        {
            return $"[{FiredOn.ToString()}] Category {CategoryId} description changed to {Description}";
        }
    }
}
