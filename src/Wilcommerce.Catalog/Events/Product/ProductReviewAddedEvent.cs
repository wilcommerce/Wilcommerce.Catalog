using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Product
{
    public class ProductReviewAddedEvent : DomainEvent
    {
        public Guid ProductId { get; private set; }

        public string Name { get; private set; }

        public int Rating { get; private set; }

        public string Comment { get; private set; }

        public ProductReviewAddedEvent(Guid productId, string name, int rating, string comment)
            : base(productId, typeof(Models.Product))
        {
            ProductId = productId;
            Name = name;
            Rating = rating;
            Comment = comment; 
        }

        public override string ToString()
        {
            return $"[{FiredOn.ToString()}] {Name} added a review with rating {Rating} and comment {Comment} for product {ProductId}";
        }
    }
}
