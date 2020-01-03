using System;
using System.Linq;
using Wilcommerce.Catalog.Models;

namespace Wilcommerce.Catalog.ReadModels
{
    /// <summary>
    /// Defines the extension methods for the product read model
    /// </summary>
    public static class ProductExtensions
    {
        /// <summary>
        /// Retrieve all the products which are not deleted
        /// </summary>
        /// <param name="products"></param>
        /// <returns>A list of products</returns>
        public static IQueryable<Product> Active(this IQueryable<Product> products)
        {
            if (products == null)
            {
                throw new ArgumentNullException(nameof(products));
            }

            return from p in products
                   where !p.Deleted
                   select p;
        }

        /// <summary>
        /// Retrieve all the main products
        /// </summary>
        /// <param name="products"></param>
        /// <returns>A list of products</returns>
        public static IQueryable<Product> MainProducts(this IQueryable<Product> products)
        {
            if (products == null)
            {
                throw new ArgumentNullException(nameof(products));
            }

            return from p in products
                   where p.MainProduct == null
                   select p;
        }

        /// <summary>
        /// Retrieve all the variants of the specified product
        /// </summary>
        /// <param name="products"></param>
        /// <param name="productId">The main product id</param>
        /// <returns>A list of products</returns>
        public static IQueryable<Product> VariantsOf(this IQueryable<Product> products, Guid productId)
        {
            if (products == null)
            {
                throw new ArgumentNullException(nameof(products));
            }

            if (productId == Guid.Empty)
            {
                throw new ArgumentException("value cannot be empty", nameof(productId));
            }

            return from p in products
                   where p.MainProduct != null && p.MainProduct.Id == productId
                   select p;
        }

        /// <summary>
        /// Retrieve all the products which have some units in stock
        /// </summary>
        /// <param name="products"></param>
        /// <returns>A list of products</returns>
        public static IQueryable<Product> WithUnitInStock(this IQueryable<Product> products)
        {
            if (products == null)
            {
                throw new ArgumentNullException(nameof(products));
            }

            return products.WithUnitInStock(0);
        }

        /// <summary>
        /// Retrieve all the products which have more than the specified units in stock
        /// </summary>
        /// <param name="products"></param>
        /// <param name="unitInStock">The unit in stock number</param>
        /// <returns>A list of products</returns>
        public static IQueryable<Product> WithUnitInStock(this IQueryable<Product> products, int unitInStock)
        {
            if (products == null)
            {
                throw new ArgumentNullException(nameof(products));
            }

            if (unitInStock < 0)
            {
                throw new ArgumentException("value cannot be less than zero", nameof(unitInStock));
            }

            return from p in products
                   where p.UnitInStock > unitInStock
                   select p;
        }

        /// <summary>
        /// Retrieve all the products on sale
        /// </summary>
        /// <param name="products"></param>
        /// <returns>A list of products</returns>
        public static IQueryable<Product> OnSale(this IQueryable<Product> products)
        {
            if (products == null)
            {
                throw new ArgumentNullException(nameof(products));
            }

            var today = DateTime.Now;

            return from p in products
                   where p.IsOnSale && (p.OnSaleFrom <= today && p.OnSaleTo >= today)
                   select p;
        }

        /// <summary>
        /// Retrieve all the products on sale and with some units in stock
        /// </summary>
        /// <param name="products"></param>
        /// <returns>A list of products</returns>
        public static IQueryable<Product> Available(this IQueryable<Product> products)
        {
            return products.OnSale().WithUnitInStock();
        }

        /// <summary>
        /// Retrieve all the products on sale from the specified date
        /// </summary>
        /// <param name="products"></param>
        /// <param name="fromDate">The starting date</param>
        /// <returns>A list of products</returns>
        public static IQueryable<Product> OnSaleFrom(this IQueryable<Product> products, DateTime fromDate)
        {
            if (products == null)
            {
                throw new ArgumentNullException(nameof(products));
            }

            return from p in products
                   where p.IsOnSale && p.OnSaleFrom >= fromDate
                   select p;
        }

        /// <summary>
        /// Retrieve all the products on sale till the specified date
        /// </summary>
        /// <param name="products"></param>
        /// <param name="tillDate">The ending date</param>
        /// <returns>A list of products</returns>
        public static IQueryable<Product> OnSaleTill(this IQueryable<Product> products, DateTime tillDate)
        {
            if (products == null)
            {
                throw new ArgumentNullException(nameof(products));
            }

            return from p in products
                   where p.IsOnSale && p.OnSaleTo <= tillDate
                   select p;
        }

        /// <summary>
        /// Retrieve all the products filtered by the specified vendor
        /// </summary>
        /// <param name="products"></param>
        /// <param name="vendorId">The vendor id</param>
        /// <returns>A list of products</returns>
        public static IQueryable<Product> ByVendor(this IQueryable<Product> products, Guid vendorId)
        {
            if (products == null)
            {
                throw new ArgumentNullException(nameof(products));
            }

            if (vendorId == Guid.Empty)
            {
                throw new ArgumentException("value cannot be empty", nameof(vendorId));
            }

            return from p in products
                   where p.Vendor != null && p.Vendor.Id == vendorId
                   select p;
        }
    }
}
