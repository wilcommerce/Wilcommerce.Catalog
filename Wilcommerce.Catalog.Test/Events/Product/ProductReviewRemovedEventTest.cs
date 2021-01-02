using System;
using Wilcommerce.Catalog.Events.Product;
using Xunit;

namespace Wilcommerce.Catalog.Test.Events.Product
{
    public class ProductReviewRemovedEventTest
    {
        [Fact]
        public void ProductReviewRemovedEvent_Ctor_Should_Set_Arguments_Correctly()
        {
            Guid productId = Guid.NewGuid();
            Guid reviewId = Guid.NewGuid();
            string userId = Guid.NewGuid().ToString();

            var @event = new ProductReviewRemovedEvent(productId, reviewId, userId);

            Assert.Equal(productId, @event.ProductId);
            Assert.Equal(reviewId, @event.ReviewId);

            Assert.Equal(productId, @event.AggregateId);
            Assert.Equal(typeof(Catalog.Models.Product), @event.AggregateType);
            Assert.Equal(userId, @event.UserId);
        }
    }
}
