using System;
using Wilcommerce.Catalog.Events.Category;
using Xunit;

namespace Wilcommerce.Catalog.Test.Events.Category
{
    public class CategoryInfoUpdatedEventTest
    {
        [Fact]
        public void CategoryInfoUpdatedEvent_Ctor_Should_Set_Arguments_Correctly()
        {
            Guid categoryId = Guid.NewGuid();
            string code = "code";
            string name = "category";
            string url = "category";
            string description = "description";
            bool isVisible = true;
            DateTime? visibleFrom = DateTime.Today;
            DateTime? visibleTo = DateTime.Today.AddDays(2);

            var @event = new CategoryInfoUpdatedEvent(categoryId, code, name, url, description, isVisible, visibleFrom, visibleTo);

            Assert.Equal(categoryId, @event.CategoryId);
            Assert.Equal(code, @event.Code);
            Assert.Equal(name, @event.Name);
            Assert.Equal(url, @event.Url);
            Assert.Equal(description, @event.Description);
            Assert.Equal(isVisible, @event.IsVisible);
            Assert.Equal(visibleFrom, @event.VisibleFrom);
            Assert.Equal(visibleTo, @event.VisibleTo);

            Assert.Equal(categoryId, @event.AggregateId);
            Assert.Equal(typeof(Catalog.Models.Category), @event.AggregateType);
        }
    }
}
