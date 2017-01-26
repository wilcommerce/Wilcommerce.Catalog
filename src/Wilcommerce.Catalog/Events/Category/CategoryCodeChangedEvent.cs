using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Category
{
    public class CategoryCodeChangedEvent : DomainEvent
    {
        public Guid CategoryId { get; }

        public string Code { get; }

        public CategoryCodeChangedEvent(Guid categoryId, string code)
            : base()
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
