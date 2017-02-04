using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Category
{
    public class CategoryNameChangedEvent : DomainEvent
    {
        public Guid CategoryId { get; }

        public string Name { get; }

        public CategoryNameChangedEvent(Guid categoryId, string name)
            : base(categoryId, typeof(Models.Category))
        {
            CategoryId = categoryId;
            Name = name;
        }

        public override string ToString()
        {
            return $"[{FiredOn.ToString()}] Category {CategoryId} name changed to {Name}";
        }
    }
}
