using System;
using System.Linq;
using Wilcommerce.Catalog.Models;

namespace Wilcommerce.Catalog.ReadModels
{
    public static class ProductExtensions
    {
        public static IQueryable<Product> Active(this IQueryable<Product> products)
        {
            return from p in products
                   where !p.Deleted
                   select p;
        }

        public static IQueryable<Product> ByCategory(this IQueryable<Product> products, Guid categoryId)
        {
            return from p in products
                   where p.Categories.Any(c => c.Id == categoryId) || (p.MainCategory != null && p.MainCategory.Id == categoryId)
                   select p;
        }

        public static IQueryable<Product> MainProducts(this IQueryable<Product> products)
        {
            return from p in products
                   where p.MainProduct == null
                   select p;
        }

        public static IQueryable<Product> ByMainProduct(this IQueryable<Product> products, Guid productId)
        {
            return from p in products
                   where p.MainProduct.Id == productId
                   select p;
        }

        public static IQueryable<Product> WithUnitInStock(this IQueryable<Product> products)
        {
            return WithUnitInStock(products, 0);
        }

        public static IQueryable<Product> WithUnitInStock(this IQueryable<Product> products, int unitInStock)
        {
            return from p in products
                   where p.UnitInStock > unitInStock
                   select p;
        }

        public static IQueryable<Product> OnSale(this IQueryable<Product> products)
        {
            var today = DateTime.Now;

            return from p in products
                   where p.IsOnSale && (p.OnSaleFrom <= today && p.OnSaleTo >= today)
                   select p;
        }

        public static IQueryable<Product> Available(this IQueryable<Product> products)
        {
            return products.OnSale().WithUnitInStock();
        }

        public static IQueryable<Product> OnSaleFrom(this IQueryable<Product> products, DateTime fromDate)
        {
            return from p in products
                   where p.IsOnSale && p.OnSaleFrom >= fromDate
                   select p;
        }

        public static IQueryable<Product> OnSaleTill(this IQueryable<Product> products, DateTime tillDate)
        {
            return from p in products
                   where p.IsOnSale && p.OnSaleTo <= tillDate
                   select p;
        }
    }
}
