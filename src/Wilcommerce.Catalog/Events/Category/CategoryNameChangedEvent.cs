using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Category
{
    public class CategoryNameChangedEvent : DomainEvent
    {
        public Guid CategoryId { get; private set; }

        public string Name { get; private set; }

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
