using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Category
{
    public class CategoryChildRemovedEvent : DomainEvent
    {
        public Guid CategoryId { get; private set; }

        public Guid ChildId { get; private set; }

        public CategoryChildRemovedEvent(Guid categoryId, Guid childId)
            : base(categoryId, typeof(Models.Category))
        {
            CategoryId = categoryId;
            ChildId = childId;
        }

        public override string ToString()
        {
            return $"[{FiredOn.ToString()}] Child {ChildId} removed from category {CategoryId}";
        }
    }
}
