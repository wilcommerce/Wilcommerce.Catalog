using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Category
{
    public class CategoryUrlChangedEvent : DomainEvent
    {
        public Guid CategoryId { get; }

        public string Url { get; }

        public CategoryUrlChangedEvent(Guid categoryId, string url)
            : base()
        {
            CategoryId = categoryId;
            Url = url;
        }
    }
}
