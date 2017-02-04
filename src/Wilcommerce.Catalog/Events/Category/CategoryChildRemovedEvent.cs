using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Category
{
    public class CategoryChildRemovedEvent : DomainEvent
    {
        public Guid CategoryId { get; }

        public Guid ChildId { get; }

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
