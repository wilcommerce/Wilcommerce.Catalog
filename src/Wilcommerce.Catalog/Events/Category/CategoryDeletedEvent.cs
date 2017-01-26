using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Category
{
    public class CategoryDeletedEvent : DomainEvent
    {
        public Guid CategoryId { get; }

        public CategoryDeletedEvent(Guid categoryId)
            : base()
        {
            CategoryId = categoryId;
        }

        public override string ToString()
        {
            return $"[{FiredOn.ToString()}] Category {CategoryId} deleted";
        }
    }
}
