using System;
using Wilcommerce.Catalog.Events.Category;
using Xunit;

namespace Wilcommerce.Catalog.Test.Events.Category
{
    public class CategoryCreatedEventTest
    {
        [Fact]
        public void CategoryCreatedEvent_Ctor_Should_Set_Arguments_Correctly()
        {
            Guid categoryId = Guid.NewGuid();
            string name = "category";
            string code = "code";
            string userId = Guid.NewGuid().ToString();

            var @event = new CategoryCreatedEvent(categoryId, name, code, userId);

            Assert.Equal(categoryId, @event.CategoryId);
            Assert.Equal(name, @event.Name);
            Assert.Equal(code, @event.Code);

            Assert.Equal(categoryId, @event.AggregateId);
            Assert.Equal(typeof(Catalog.Models.Category), @event.AggregateType);
            Assert.Equal(userId, @event.UserId);
        }
    }
}
