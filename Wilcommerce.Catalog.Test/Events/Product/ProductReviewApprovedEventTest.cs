using System;
using Wilcommerce.Catalog.Events.Product;
using Xunit;

namespace Wilcommerce.Catalog.Test.Events.Product
{
    public class ProductReviewApprovedEventTest
    {
        [Fact]
        public void ProductReviewApprovedEvent_Ctor_Should_Set_Arguments_Correctly()
        {
            Guid productId = Guid.NewGuid();
            Guid reviewId = Guid.NewGuid();

            var @event = new ProductReviewApprovedEvent(productId, reviewId);

            Assert.Equal(productId, @event.ProductId);
            Assert.Equal(reviewId, @event.ReviewId);

            Assert.Equal(productId, @event.AggregateId);
            Assert.Equal(typeof(Catalog.Models.Product), @event.AggregateType);
        }
    }
}
