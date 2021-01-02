using System;
using Wilcommerce.Catalog.Events.Product;
using Wilcommerce.Core.Common.Models;
using Xunit;

namespace Wilcommerce.Catalog.Test.Events.Product
{
    public class ProductInfoUpdateEventTest
    {
        [Fact]
        public void ProductInfoUpdateEvent_Ctor_Should_Set_Arguments_Correctly()
        {
            Guid productId = Guid.NewGuid();
            string eanCode = "ean";
            string sku = "sku";
            string name = "name";
            string url = "url";
            Currency price = new Currency { Amount = 10, Code = "EUR" };
            string description = "description";
            int unitInStock = 1;
            bool isOnSale = true;
            DateTime? onSaleFrom = DateTime.Today;
            DateTime? onSaleTo = DateTime.Today.AddMonths(1);
            string userId = Guid.NewGuid().ToString();

            var @event = new ProductInfoUpdateEvent(productId, eanCode, sku, name, url, price, description, unitInStock, isOnSale, onSaleFrom, onSaleTo, userId);

            Assert.Equal(productId, @event.ProductId);
            Assert.Equal(eanCode, @event.EanCode);
            Assert.Equal(sku, @event.Sku);
            Assert.Equal(name, @event.Name);
            Assert.Equal(url, @event.Url);
            Assert.Equal(price, @event.Price);
            Assert.Equal(description, @event.Description);
            Assert.Equal(unitInStock, @event.UnitInStock);
            Assert.Equal(isOnSale, @event.IsOnSale);
            Assert.Equal(onSaleFrom, @event.OnSaleFrom);
            Assert.Equal(onSaleTo, @event.OnSaleTo);

            Assert.Equal(productId, @event.AggregateId);
            Assert.Equal(typeof(Catalog.Models.Product), @event.AggregateType);
            Assert.Equal(userId, @event.UserId);
        }
    }
}
