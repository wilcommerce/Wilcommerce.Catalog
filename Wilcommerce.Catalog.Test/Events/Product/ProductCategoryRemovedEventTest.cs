using System;
using Wilcommerce.Catalog.Events.Product;
using Xunit;

namespace Wilcommerce.Catalog.Test.Events.Product
{
    public class ProductCategoryRemovedEventTest
    {
        [Fact]
        public void ProductCategoryRemovedEvent_Ctor_Should_Set_Arguments_Correctly()
        {
            Guid productId = Guid.NewGuid();
            Guid categoryId = Guid.NewGuid();

            var @event = new ProductCategoryRemovedEvent(productId, categoryId);

            Assert.Equal(productId, @event.ProductId);
            Assert.Equal(categoryId, @event.CategoryId);

            Assert.Equal(productId, @event.AggregateId);
            Assert.Equal(typeof(Catalog.Models.Product), @event.AggregateType);
        }
    }
}
