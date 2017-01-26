using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Category
{
    public class CategoryCreatedEvent : DomainEvent
    {
        public Guid CategoryId { get; }

        public string Name { get; }

        public string Code { get; }

        public CategoryCreatedEvent(Guid categoryId, string name, string code)
            : base()
        {
            CategoryId = categoryId;
            Name = name;
            Code = code;
        }

        public override string ToString()
        {
            return $"[{FiredOn.ToString()}] Category {Name} - {Code} [{CategoryId}] created successfully";
        }
    }
}
