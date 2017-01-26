using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Product
{
    public class ProductReviewAddedEvent : DomainEvent
    {
        public Guid ProductId { get; }

        public string Name { get; }

        public int Rating { get; }

        public string Comment { get; }

        public ProductReviewAddedEvent(Guid productId, string name, int rating, string comment)
            : base()
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
