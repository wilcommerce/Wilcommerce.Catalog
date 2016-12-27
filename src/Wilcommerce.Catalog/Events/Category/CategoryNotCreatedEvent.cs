using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Category
{
    public class CategoryNotCreatedEvent : DomainEvent
    {
        public string Name { get; }

        public string Code { get; }

        public string Error { get; }

        public CategoryNotCreatedEvent(string name, string code, string error)
            : base()
        {
            Name = name;
            Code = code;
            Error = error;
        }

        public override string ToString()
        {
            return $"[{FiredOn.ToString()}] {Error} while creating category {Name} - {Code}";
        }
    }
}
