using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Category
{
    public class CategoryCodeChangedEvent : DomainEvent
    {
        public Guid CategoryId { get; private set; }

        public string Code { get; private set; }

        public CategoryCodeChangedEvent(Guid categoryId, string code)
            : base(categoryId, typeof(Models.Category))
        {
            CategoryId = categoryId;
            Code = code;
        }

        public override string ToString()
        {
            return $"[{FiredOn.ToString()}] Category {CategoryId} code changed to {Code}";
        }
    }
}
