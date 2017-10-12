using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Category
{
    public class CategoryCreatedEvent : DomainEvent
    {
        public Guid CategoryId { get; private set; }

        public string Name { get; private set; }

        public string Code { get; private set; }

        public CategoryCreatedEvent(Guid categoryId, string name, string code)
            : base(categoryId, typeof(Models.Category))
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
