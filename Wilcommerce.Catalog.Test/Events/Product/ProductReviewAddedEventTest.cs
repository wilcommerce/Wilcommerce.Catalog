using System;
using Wilcommerce.Catalog.Events.Product;
using Xunit;

namespace Wilcommerce.Catalog.Test.Events.Product
{
    public class ProductReviewAddedEventTest
    {
        [Fact]
        public void ProductReviewAddedEvent_Ctor_Should_Set_Arguments_Correctly()
        {
            Guid productId = Guid.NewGuid();
            string name = "review";
            int rating = 2;
            string comment = "comment";
            string userId = Guid.NewGuid().ToString();

            var @event = new ProductReviewAddedEvent(productId, name, rating, comment, userId);

            Assert.Equal(productId, @event.ProductId);
            Assert.Equal(name, @event.Name);
            Assert.Equal(rating, @event.Rating);
            Assert.Equal(comment, @event.Comment);

            Assert.Equal(productId, @event.AggregateId);
            Assert.Equal(typeof(Catalog.Models.Product), @event.AggregateType);
            Assert.Equal(userId, @event.UserId);
        }
    }
}
