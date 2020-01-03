using System;
using System.Linq;
using Wilcommerce.Catalog.Models;
using Wilcommerce.Catalog.ReadModels;
using Xunit;

namespace Wilcommerce.Catalog.Test.ReadModels
{
    public class ProductExtensionsTest
    {
        #region Active tests
        [Fact]
        public void Active_Should_Throw_ArgumentNullException_If_Products_Is_Null()
        {
            IQueryable<Product> products = null;

            var ex = Assert.Throws<ArgumentNullException>(() => ProductExtensions.Active(products));
            Assert.Equal(nameof(products), ex.ParamName);
        }

        [Fact]
        public void Active_Should_Return_Only_Products_Not_Deleted()
        {
            var p1 = Product.Create("ean", "sku", "name", "url");
            var p2 = Product.Create("ean", "sku", "name", "url");
            var p3 = Product.Create("ean", "sku", "name", "url");
            p3.Delete();

            IQueryable<Product> products = new Product[]
            {
                p1, p2, p3
            }.AsQueryable();

            var activeProducts = ProductExtensions.Active(products).ToArray();
            Assert.True(activeProducts.All(p => !p.Deleted));
        }
        #endregion

        #region MainProducts tests
        [Fact]
        public void MainProducts_Should_Throw_ArgumentNullException_If_Products_Is_Null()
        {
            IQueryable<Product> products = null;

            var ex = Assert.Throws<ArgumentNullException>(() => ProductExtensions.MainProducts(products));
            Assert.Equal(nameof(products), ex.ParamName);
        }

        [Fact]
        public void MainProducts_Should_Return_Only_Products_Without_A_Main_Product_Associated()
        {
            var p1 = Product.Create("ean", "sku", "name", "url");
            var p2 = Product.Create("ean", "sku", "name", "url");
            var p3 = Product.Create("ean", "sku", "name", "url");

            IQueryable<Product> products = new Product[]
            {
                p1, p2, p3
            }.AsQueryable();

            var mainProducts = ProductExtensions.MainProducts(products).ToArray();
            Assert.True(mainProducts.All(p => p.MainProduct == null));
        }
        #endregion

        #region VariantsOf tests
        [Fact]
        public void VariantsOf_Should_Throw_ArgumentNullException_If_Products_Is_Null()
        {
            IQueryable<Product> products = null;
            Guid productId = Guid.NewGuid();

            var ex = Assert.Throws<ArgumentNullException>(() => ProductExtensions.VariantsOf(products, productId));
            Assert.Equal(nameof(products), ex.ParamName);
        }

        [Fact]
        public void VariantsOf_Should_Throw_ArgumentException_If_ProductId_Is_Empty()
        {
            IQueryable<Product> products = new Product[0].AsQueryable();
            Guid productId = Guid.Empty;

            var ex = Assert.Throws<ArgumentException>(() => ProductExtensions.VariantsOf(products, productId));
            Assert.Equal(nameof(productId), ex.ParamName);
        }
        #endregion

        #region WithUnitInStock tests
        [Fact]
        public void WithUnitInStock_Should_Throw_ArgumentNullException_If_Products_Is_Null()
        {
            IQueryable<Product> products = null;
            int unitInStock = 0;

            var ex = Assert.Throws<ArgumentNullException>(() => ProductExtensions.WithUnitInStock(products, unitInStock));
            Assert.Equal(nameof(products), ex.ParamName);
        }

        [Fact]
        public void WithUnitInStock_Should_Throw_ArgumentException_If_UnitInStock_Is_Less_Than_Zero()
        {
            IQueryable<Product> products = new Product[0].AsQueryable();
            int unitInStock = -1;

            var ex = Assert.Throws<ArgumentException>(() => ProductExtensions.WithUnitInStock(products, unitInStock));
            Assert.Equal(nameof(unitInStock), ex.ParamName);
        }

        [Fact]
        public void WithUnitInStock_Should_Return_Only_Products_With_Units_In_Stock_Greater_Than_The_Specified_Quantity()
        {
            var p1 = Product.Create("ean", "sku", "name", "url");
            var p2 = Product.Create("ean", "sku", "name", "url");
            var p3 = Product.Create("ean", "sku", "name", "url");

            p1.SetUnitInStock(2);
            p2.SetUnitInStock(3);
            p3.SetUnitInStock(1);

            IQueryable<Product> products = new Product[]
            {
                p1, p2, p3
            }.AsQueryable();
            int unitInStock = 1;

            var productsWithUnitInStock = ProductExtensions.WithUnitInStock(products, unitInStock).ToArray();
            Assert.True(productsWithUnitInStock.All(p => p.UnitInStock > unitInStock));
        }
        #endregion

        #region OnSale tests
        [Fact]
        public void OnSale_Should_Throw_ArgumentNullException_If_Products_Is_Null()
        {
            IQueryable<Product> products = null;

            var ex = Assert.Throws<ArgumentNullException>(() => ProductExtensions.OnSale(products));
            Assert.Equal(nameof(products), ex.ParamName);
        }

        [Fact]
        public void OnSale_Should_Return_Only_Products_OnSale_On_The_Current_Date()
        {
            var p1 = Product.Create("ean", "sku", "name", "url");
            var p2 = Product.Create("ean", "sku", "name", "url");
            var p3 = Product.Create("ean", "sku", "name", "url");

            p1.SetOnSale();
            p2.SetOnSale(DateTime.Today.AddDays(-1), DateTime.Today.AddMonths(1));

            IQueryable<Product> products = new Product[]
            {
                p1, p2, p3
            }.AsQueryable();

            var today = DateTime.Now;

            var productsOnSale = ProductExtensions.OnSale(products).ToArray();
            Assert.True(productsOnSale.All(p => p.IsOnSale && p.OnSaleFrom <= today && p.OnSaleTo >= today));
        }
        #endregion

        #region Available tests
        [Fact]
        public void Available_Should_Throw_ArgumentNullException_If_Products_Is_Null()
        {
            IQueryable<Product> products = null;

            var ex = Assert.Throws<ArgumentNullException>(() => ProductExtensions.Available(products));
            Assert.Equal(nameof(products), ex.ParamName);
        }

        [Fact]
        public void Available_Should_Return_Only_Products_On_Sale_And_With_Units_In_Stock()
        {
            var p1 = Product.Create("ean", "sku", "name", "url");
            var p2 = Product.Create("ean", "sku", "name", "url");
            var p3 = Product.Create("ean", "sku", "name", "url");

            p1.SetOnSale();
            p1.SetUnitInStock(1);
            p2.SetOnSale(DateTime.Today.AddDays(-1), DateTime.Today.AddMonths(1));
            p2.SetUnitInStock(2);

            IQueryable<Product> products = new Product[]
            {
                p1, p2, p3
            }.AsQueryable();

            var today = DateTime.Now;

            var productsAvailable = ProductExtensions.Available(products).ToArray();
            Assert.True(productsAvailable.All(p => p.IsOnSale && p.OnSaleFrom <= today && p.OnSaleTo >= today && p.UnitInStock > 0));
        }
        #endregion

        #region OnSaleFrom tests
        [Fact]
        public void OnSaleFrom_Should_Throw_ArgumentNullException_If_Products_Is_Null()
        {
            IQueryable<Product> products = null;
            DateTime fromDate = DateTime.Today;

            var ex = Assert.Throws<ArgumentNullException>(() => ProductExtensions.OnSaleFrom(products, fromDate));
            Assert.Equal(nameof(products), ex.ParamName);
        }

        [Fact]
        public void OnSaleFrom_Should_Return_Only_Products_On_Sale_With_OnSaleFrom_Date_Greater_Than_The_Specified_Date()
        {
            var p1 = Product.Create("ean", "sku", "name", "url");
            var p2 = Product.Create("ean", "sku", "name", "url");
            var p3 = Product.Create("ean", "sku", "name", "url");

            p1.SetOnSale();
            p2.SetOnSale(DateTime.Today, null);

            IQueryable<Product> products = new Product[]
            {
                p1, p2, p3
            }.AsQueryable();
            DateTime fromDate = DateTime.Today;

            var productsOnSale = ProductExtensions.OnSaleFrom(products, fromDate).ToArray();
            Assert.True(productsOnSale.All(p => p.IsOnSale && p.OnSaleFrom >= fromDate));
        }
        #endregion

        #region OnSaleTill tests
        [Fact]
        public void OnSaleTill_Should_Throw_ArgumentNullException_If_Products_Is_Null()
        {
            IQueryable<Product> products = null;
            DateTime tillDate = DateTime.Today;

            var ex = Assert.Throws<ArgumentNullException>(() => ProductExtensions.OnSaleTill(products, tillDate));
            Assert.Equal(nameof(products), ex.ParamName);
        }

        [Fact]
        public void OnSaleTill_Should_Return_Only_Products_OnSale_And_With_OnSaleTo_Date_Previous_Than_The_Specified_Date() 
        {
            var p1 = Product.Create("ean", "sku", "name", "url");
            var p2 = Product.Create("ean", "sku", "name", "url");
            var p3 = Product.Create("ean", "sku", "name", "url");

            p1.SetOnSale(DateTime.Today.AddDays(-5), DateTime.Today.AddDays(-1));
            p2.SetOnSale(DateTime.Today.AddDays(-3), DateTime.Today);

            IQueryable<Product> products = new Product[]
            {
                p1, p2, p3
            }.AsQueryable();
            DateTime tillDate = DateTime.Today;

            var productsOnSale = ProductExtensions.OnSaleTill(products, tillDate).ToArray();
            Assert.True(productsOnSale.All(p => p.IsOnSale && p.OnSaleTo <= tillDate));
        }
        #endregion

        #region ByVendor tests
        [Fact]
        public void ByVendor_Should_Throw_ArgumentNullException_If_Products_Is_Null()
        {
            IQueryable<Product> products = null;
            Guid vendorId = Guid.NewGuid();

            var ex = Assert.Throws<ArgumentNullException>(() => ProductExtensions.ByVendor(products, vendorId));
            Assert.Equal(nameof(products), ex.ParamName);
        }

        [Fact]
        public void ByVendor_Should_Throw_ArgumentException_If_VendorId_Is_Empty()
        {
            IQueryable<Product> products = new Product[0].AsQueryable();
            Guid vendorId = Guid.Empty;

            var ex = Assert.Throws<ArgumentException>(() => ProductExtensions.ByVendor(products, vendorId));
            Assert.Equal(nameof(vendorId), ex.ParamName);
        }

        [Fact]
        public void ByVendor_Should_Return_Only_Products_With_The_Specified_Vendor()
        {
            var vendor = Brand.Create("brand", "brand");

            var p1 = Product.Create("ean", "sku", "name", "url");
            var p2 = Product.Create("ean", "sku", "name", "url");
            var p3 = Product.Create("ean", "sku", "name", "url");

            p1.SetVendor(vendor);
            p2.SetVendor(vendor);

            IQueryable<Product> products = new Product[]
            {
                p1, p2, p3
            }.AsQueryable();
            Guid vendorId = vendor.Id;

            var productsByVendor = ProductExtensions.ByVendor(products, vendorId).ToArray();
            Assert.True(productsByVendor.All(p => p.Vendor != null && p.Vendor.Id == vendorId));
        }
        #endregion
    }
}
