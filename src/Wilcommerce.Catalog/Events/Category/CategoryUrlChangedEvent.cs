using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Category
{
    public class CategoryUrlChangedEvent : DomainEvent
    {
        public Guid CategoryId { get; private set; }

        public string Url { get; private set; }

        public CategoryUrlChangedEvent(Guid categoryId, string url)
            : base(categoryId, typeof(Models.Category))
        {
            CategoryId = categoryId;
            Url = url;
        }
    }
}
