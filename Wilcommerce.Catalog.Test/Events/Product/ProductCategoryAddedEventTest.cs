using System;
using Wilcommerce.Catalog.Events.Product;
using Xunit;

namespace Wilcommerce.Catalog.Test.Events.Product
{
    public class ProductCategoryAddedEventTest
    {
        [Fact]
        public void ProductCategoryAddedEvent_Ctor_Should_Set_Arguments_Correctly()
        {
            Guid productId = Guid.NewGuid();
            Guid categoryId = Guid.NewGuid();
            bool isMain = true;

            var @event = new ProductCategoryAddedEvent(productId, categoryId, isMain);

            Assert.Equal(productId, @event.ProductId);
            Assert.Equal(categoryId, @event.CategoryId);
            Assert.Equal(isMain, @event.IsMain);

            Assert.Equal(productId, @event.AggregateId);
            Assert.Equal(typeof(Catalog.Models.Product), @event.AggregateType);
        }
    }
}
