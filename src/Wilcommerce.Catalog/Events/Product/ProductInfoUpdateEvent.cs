using System;
using Wilcommerce.Core.Common.Models;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Events.Product
{
    /// <summary>
    /// Product info updated
    /// </summary>
    public class ProductInfoUpdateEvent : DomainEvent
    {
        /// <summary>
        /// Get the product id
        /// </summary>
        public Guid ProductId { get; private set; }

        /// <summary>
        /// Get the product EAN code
        /// </summary>
        public string EanCode { get; private set; }

        /// <summary>
        /// Get the product SKU
        /// </summary>
        public string Sku { get; private set; }

        /// <summary>
        /// Get the product name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Get the product url
        /// </summary>
        public string Url { get; private set; }

        /// <summary>
        /// Get the product price
        /// </summary>
        public Currency Price { get; private set; }

        /// <summary>
        /// Get the product description
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Get the product unit in stock
        /// </summary>
        public int UnitInStock { get; private set; }

        /// <summary>
        /// Get whether the product is on sale
        /// </summary>
        public bool IsOnSale { get; private set; }

        /// <summary>
        /// Get the date and time from when the product is on sale
        /// </summary>
        public DateTime? OnSaleFrom { get; private set; }

        /// <summary>
        /// Get the date and time till when the product is on sale
        /// </summary>
        public DateTime? OnSaleTo { get; private set; }

        /// <summary>
        /// Construct the event
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="eanCode">The product EAN code</param>
        /// <param name="sku">The product SKU</param>
        /// <param name="name">The product name</param>
        /// <param name="url">The product url</param>
        /// <param name="price">The product price</param>
        /// <param name="description">The product description</param>
        /// <param name="unitInStock">The product unit in stock</param>
        /// <param name="isOnSale">Whether the product is on sale</param>
        /// <param name="onSaleFrom">The date and time from when the product is on sale</param>
        /// <param name="onSaleTo">The date and time till when the product is on sale</param>
        /// <param name="userId">The user's id</param>
        public ProductInfoUpdateEvent(Guid productId, string eanCode, string sku, string name, string url, Currency price, string description, int unitInStock, bool isOnSale, DateTime? onSaleFrom, DateTime? onSaleTo, string userId)
            : base(productId, typeof(Models.Product), userId)
        {
            ProductId = productId;
            EanCode = eanCode;
            Sku = sku;
            Name = name;
            Url = url;
            Price = price;
            Description = description;
            UnitInStock = unitInStock;
            IsOnSale = isOnSale;
            OnSaleFrom = onSaleFrom;
            OnSaleTo = onSaleTo;
        }

        /// <summary>
        /// Convert the event to string
        /// </summary>
        /// <returns>The converted string</returns>
        public override string ToString()
        {
            return $"Product {ProductId} updated. EAN: {EanCode}, SKU: {Sku}, Name: {Name}, Url: {Url}, Price: {Price.ToString()}, Description: {Description}, Unit in stock: {UnitInStock}";
        }
    }
}
