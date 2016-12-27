using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Brand
{
    public class BrandNotCreatedEvent : DomainEvent
    {
        public string Name { get; }

        public string Error { get; }

        public BrandNotCreatedEvent(string name, string error)
            : base()
        {
            Name = name;
            Error = error;
        }

        public override string ToString()
        {
            return $"[{FiredOn.ToString()}] {Error} while creating brand {Name}";
        }
    }
}
