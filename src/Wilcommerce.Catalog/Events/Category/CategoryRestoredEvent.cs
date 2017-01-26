using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Category
{
    public class CategoryRestoredEvent : DomainEvent
    {
        public Guid CategoryId { get; }

        public CategoryRestoredEvent(Guid categoryId)
            : base()
        {
            CategoryId = categoryId;
        }

        public override string ToString()
        {
            return $"[{FiredOn.ToString()}] Category {CategoryId} restored";
        }
    }
}
