using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wilcommerce.Catalog.Commands;
using Wilcommerce.Catalog.Models;
using Wilcommerce.Core.Common.Models;
using Xunit;

namespace Wilcommerce.Catalog.Test.Commands
{
    public class ProductCommandsTest
    {
        private readonly string userId = Guid.NewGuid().ToString();

        #region Ctor tests
        [Fact]
        public void Ctor_Should_Throw_ArgumentNullException_If_Repository_Is_Null()
        {
            Repository.IRepository repository = null;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            var ex = Assert.Throws<ArgumentNullException>(() => new ProductCommands(repository, eventBus));
            Assert.Equal(nameof(repository), ex.ParamName);
        }

        [Fact]
        public void Ctor_Should_Throw_ArgumentNullException_If_EventBus_Is_Null()
        {
            Repository.IRepository repository = new Mock<Repository.IRepository>().Object;
            Core.Infrastructure.IEventBus eventBus = null;

            var ex = Assert.Throws<ArgumentNullException>(() => new ProductCommands(repository, eventBus));
            Assert.Equal(nameof(eventBus), ex.ParamName);
        }
        #endregion

        #region Commands tests
        [Fact]
        public async Task CreateNewProduct_Should_Create_A_New_Product_And_Return_The_Created_Product_Id()
        {
            var fakeProductList = new List<Product>();
            var repositoryMock = new Mock<Repository.IRepository>();
            repositoryMock.Setup(r => r.Add(It.IsAny<Product>()))
                .Callback<Product>((product) => fakeProductList.Add(product));

            Repository.IRepository repository = repositoryMock.Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            string ean = "ean";
            string sku = "sku";
            string name = "name";
            string url = "url";
            Currency price = new Currency { Code = "EUR", Amount = 10 };
            string description = "description";
            int unitInStock = 10;
            bool isOnSale = true;
            DateTime? onSaleFrom = DateTime.Today;
            DateTime? onSaleTo = DateTime.Today.AddYears(1);

            var commands = new ProductCommands(repository, eventBus);
            var productId = await commands.CreateNewProduct(ean, sku, name, url, price, description, unitInStock, isOnSale, onSaleFrom, onSaleTo, userId);

            var createdProduct = fakeProductList.FirstOrDefault(p => p.Id == productId);

            Assert.NotNull(createdProduct);
            Assert.Equal(ean, createdProduct.EanCode);
            Assert.Equal(sku, createdProduct.Sku);
            Assert.Equal(name, createdProduct.Name);
            Assert.Equal(url, createdProduct.Url);
            Assert.Equal(price, createdProduct.Price);
            Assert.Equal(description, createdProduct.Description);
            Assert.Equal(unitInStock, createdProduct.UnitInStock);
            Assert.Equal(isOnSale, createdProduct.IsOnSale);
            Assert.Equal(onSaleFrom, createdProduct.OnSaleFrom);
            Assert.Equal(onSaleTo, createdProduct.OnSaleTo);
        }

        [Fact]
        public async Task UpdateProductInfo_Should_Throw_ArgumentException_If_ProductId_Is_Empty()
        {
            Repository.IRepository repository = new Mock<Repository.IRepository>().Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid productId = Guid.Empty;
            string ean = "ean";
            string sku = "sku";
            string name = "name";
            string url = "url";
            Currency price = new Currency { Code = "EUR", Amount = 10 };
            string description = "description";
            int unitInStock = 10;
            bool isOnSale = true;
            DateTime? onSaleFrom = DateTime.Today;
            DateTime? onSaleTo = DateTime.Today.AddYears(1);

            var commands = new ProductCommands(repository, eventBus);
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.UpdateProductInfo(productId, ean, sku, name, url, price, description, unitInStock, isOnSale, onSaleFrom, onSaleTo, userId));

            Assert.Equal(nameof(productId), ex.ParamName);
        }

        [Fact]
        public async Task UpdateProductInfo_Should_Update_Product_With_Specified_Values()
        {
            var product = Product.Create("ean01", "sku01", "product", "product");

            var repositoryMock = new Mock<Repository.IRepository>();
            repositoryMock.Setup(r => r.GetByKeyAsync<Product>(It.IsAny<Guid>()))
                .Returns(Task.FromResult(product));

            Repository.IRepository repository = repositoryMock.Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid productId = product.Id;
            string ean = "ean";
            string sku = "sku";
            string name = "name";
            string url = "url";
            Currency price = new Currency { Code = "EUR", Amount = 10 };
            string description = "description";
            int unitInStock = 10;
            bool isOnSale = true;
            DateTime? onSaleFrom = DateTime.Today;
            DateTime? onSaleTo = DateTime.Today.AddYears(1);

            var commands = new ProductCommands(repository, eventBus);
            await commands.UpdateProductInfo(productId, ean, sku, name, url, price, description, unitInStock, isOnSale, onSaleFrom, onSaleTo, userId);

            Assert.Equal(ean, product.EanCode);
            Assert.Equal(sku, product.Sku);
            Assert.Equal(name, product.Name);
            Assert.Equal(url, product.Url);
            Assert.Equal(price, product.Price);
            Assert.Equal(description, product.Description);
            Assert.Equal(unitInStock, product.UnitInStock);
            Assert.Equal(isOnSale, product.IsOnSale);
            Assert.Equal(onSaleFrom, product.OnSaleFrom);
            Assert.Equal(onSaleTo, product.OnSaleTo);
        }

        [Fact]
        public async Task DeleteProduct_Should_Throw_ArgumentException_If_ProductId_Is_Empty()
        {
            Repository.IRepository repository = new Mock<Repository.IRepository>().Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid productId = Guid.Empty;

            var commands = new ProductCommands(repository, eventBus);
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.DeleteProduct(productId, userId));

            Assert.Equal(nameof(productId), ex.ParamName);
        }

        [Fact]
        public async Task DeleteProduct_Should_Mark_Product_As_Deleted()
        {
            var product = Product.Create("ean01", "sku01", "product", "product");

            var repositoryMock = new Mock<Repository.IRepository>();
            repositoryMock.Setup(r => r.GetByKeyAsync<Product>(It.IsAny<Guid>()))
                .Returns(Task.FromResult(product));

            Repository.IRepository repository = repositoryMock.Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid productId = product.Id;
            
            var commands = new ProductCommands(repository, eventBus);
            await commands.DeleteProduct(productId, userId);

            Assert.True(product.Deleted);
        }

        [Fact]
        public async Task RestoreProduct_Should_Throw_ArgumentException_If_ProductId_Is_Empty()
        {
            Repository.IRepository repository = new Mock<Repository.IRepository>().Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid productId = Guid.Empty;

            var commands = new ProductCommands(repository, eventBus);
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.RestoreProduct(productId, userId));

            Assert.Equal(nameof(productId), ex.ParamName);
        }

        [Fact]
        public async Task RestoreProduct_Should_Mark_Product_As_Restored()
        {
            var product = Product.Create("ean01", "sku01", "product", "product");
            product.Delete();

            var repositoryMock = new Mock<Repository.IRepository>();
            repositoryMock.Setup(r => r.GetByKeyAsync<Product>(It.IsAny<Guid>()))
                .Returns(Task.FromResult(product));

            Repository.IRepository repository = repositoryMock.Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid productId = product.Id;

            var commands = new ProductCommands(repository, eventBus);
            await commands.RestoreProduct(productId, userId);

            Assert.False(product.Deleted);
        }

        [Fact]
        public async Task SetProductVendor_Should_Throw_ArgumentException_If_ProductId_Is_Empty()
        {
            Repository.IRepository repository = new Mock<Repository.IRepository>().Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid productId = Guid.Empty;
            Guid brandId = Guid.NewGuid();

            var commands = new ProductCommands(repository, eventBus);
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.SetProductVendor(productId, brandId, userId));

            Assert.Equal(nameof(productId), ex.ParamName);
        }

        [Fact]
        public async Task SetProductVendor_Should_Throw_ArgumentException_If_BrandId_Is_Empty()
        {
            Repository.IRepository repository = new Mock<Repository.IRepository>().Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid productId = Guid.NewGuid();
            Guid brandId = Guid.Empty;

            var commands = new ProductCommands(repository, eventBus);
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.SetProductVendor(productId, brandId, userId));

            Assert.Equal(nameof(brandId), ex.ParamName);
        }

        [Fact]
        public async Task SetProductVendor_Should_Set_Product_Vendor_With_The_Specified_Brand()
        {
            var product = Product.Create("ean01", "sku01", "product", "product");
            var brand = Brand.Create("brand", "brand");

            var repositoryMock = new Mock<Repository.IRepository>();
            repositoryMock.Setup(r => r.GetByKeyAsync<Product>(It.IsAny<Guid>()))
                .Returns(Task.FromResult(product));

            repositoryMock.Setup(r => r.GetByKeyAsync<Brand>(It.IsAny<Guid>()))
                .Returns(Task.FromResult(brand));

            Repository.IRepository repository = repositoryMock.Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid productId = product.Id;
            Guid brandId = brand.Id;

            var commands = new ProductCommands(repository, eventBus);
            await commands.SetProductVendor(productId, brandId, userId);

            Assert.Equal(brand, product.Vendor);
        }

        [Fact]
        public async Task AddCategoryToProduct_Should_Throw_ArgumentException_If_ProductId_Is_Empty()
        {
            Repository.IRepository repository = new Mock<Repository.IRepository>().Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid productId = Guid.Empty;
            Guid categoryId = Guid.NewGuid();

            var commands = new ProductCommands(repository, eventBus);
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.AddCategoryToProduct(productId, categoryId, userId));

            Assert.Equal(nameof(productId), ex.ParamName);
        }

        [Fact]
        public async Task AddCategoryToProduct_Should_Throw_ArgumentException_If_CategoryId_Is_Empty()
        {
            Repository.IRepository repository = new Mock<Repository.IRepository>().Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid productId = Guid.NewGuid();
            Guid categoryId = Guid.Empty;

            var commands = new ProductCommands(repository, eventBus);
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.AddCategoryToProduct(productId, categoryId, userId));

            Assert.Equal(nameof(categoryId), ex.ParamName);
        }

        [Fact]
        public async Task AddCategoryToProduct_Should_Add_The_Specified_Category_To_The_Product()
        {
            var product = Product.Create("ean01", "sku01", "product", "product");
            var category = Category.Create("cat", "category", "category");

            var repositoryMock = new Mock<Repository.IRepository>();
            repositoryMock.Setup(r => r.GetByKeyAsync<Product>(It.IsAny<Guid>()))
                .Returns(Task.FromResult(product));

            repositoryMock.Setup(r => r.GetByKeyAsync<Category>(It.IsAny<Guid>()))
                .Returns(Task.FromResult(category));

            Repository.IRepository repository = repositoryMock.Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid productId = product.Id;
            Guid categoryId = category.Id;

            var commands = new ProductCommands(repository, eventBus);
            await commands.AddCategoryToProduct(productId, categoryId, userId);

            Assert.Collection(product.ProductCategories.Select(c => c.CategoryId).ToArray(), (c) => Assert.Equal(categoryId, c));
        }

        [Fact]
        public async Task AddMainCategoryToProduct_Should_Throw_ArgumentException_If_ProductId_Is_Empty()
        {
            Repository.IRepository repository = new Mock<Repository.IRepository>().Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid productId = Guid.Empty;
            Guid categoryId = Guid.NewGuid();

            var commands = new ProductCommands(repository, eventBus);
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.AddMainCategoryToProduct(productId, categoryId, userId));

            Assert.Equal(nameof(productId), ex.ParamName);
        }

        [Fact]
        public async Task AddMainCategoryToProduct_Should_Throw_ArgumentException_If_CategoryId_Is_Empty()
        {
            Repository.IRepository repository = new Mock<Repository.IRepository>().Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid productId = Guid.NewGuid();
            Guid categoryId = Guid.Empty;

            var commands = new ProductCommands(repository, eventBus);
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.AddMainCategoryToProduct(productId, categoryId, userId));

            Assert.Equal(nameof(categoryId), ex.ParamName);
        }

        [Fact]
        public async Task AddMainCategoryToProduct_Should_Add_The_Specified_Category_To_The_Product()
        {
            var product = Product.Create("ean01", "sku01", "product", "product");
            var category = Category.Create("cat", "category", "category");

            var repositoryMock = new Mock<Repository.IRepository>();
            repositoryMock.Setup(r => r.GetByKeyAsync<Product>(It.IsAny<Guid>()))
                .Returns(Task.FromResult(product));

            repositoryMock.Setup(r => r.GetByKeyAsync<Category>(It.IsAny<Guid>()))
                .Returns(Task.FromResult(category));

            Repository.IRepository repository = repositoryMock.Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid productId = product.Id;
            Guid categoryId = category.Id;

            var commands = new ProductCommands(repository, eventBus);
            await commands.AddMainCategoryToProduct(productId, categoryId, userId);

            Assert.Collection(product.ProductCategories.Where(c => c.IsMain).Select(c => c.CategoryId).ToArray(), (c) => Assert.Equal(categoryId, c));
        }

        [Fact]
        public async Task AddProductVariant_Should_Throw_ArgumentException_If_ProductId_Is_Empty()
        {
            Repository.IRepository repository = new Mock<Repository.IRepository>().Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid productId = Guid.Empty;
            string name = "variant";
            string ean = "ean";
            string sku = "sku";
            Currency price = new Currency { Code = "EUR", Amount = 100 };

            var commands = new ProductCommands(repository, eventBus);
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.AddProductVariant(productId, name, ean, sku, price, userId));

            Assert.Equal(nameof(productId), ex.ParamName);
        }

        [Fact]
        public async Task AddProductVariant_Should_Add_A_Product_Variant_With_The_Specified_Values()
        {
            var product = Product.Create("ean01", "sku01", "product", "product");

            var repositoryMock = new Mock<Repository.IRepository>();
            repositoryMock.Setup(r => r.GetByKeyAsync<Product>(It.IsAny<Guid>()))
                .Returns(Task.FromResult(product));

            Repository.IRepository repository = repositoryMock.Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid productId = product.Id;
            string name = "variant";
            string ean = "ean";
            string sku = "sku";
            Currency price = new Currency { Code = "EUR", Amount = 100 };

            var commands = new ProductCommands(repository, eventBus);
            await commands.AddProductVariant(productId, name, ean, sku, price, userId);

            var variantAdded = product.Variants.SingleOrDefault(v => v.Name == name && v.EanCode == ean && v.Sku == sku);
            Assert.NotNull(variantAdded);
        }

        [Fact]
        public async Task RemoveProductVariant_Should_Throw_ArgumentException_If_ProductId_Is_Empty()
        {
            Repository.IRepository repository = new Mock<Repository.IRepository>().Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid productId = Guid.Empty;
            Guid variantId = Guid.NewGuid();

            var commands = new ProductCommands(repository, eventBus);
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.RemoveProductVariant(productId, variantId, userId));

            Assert.Equal(nameof(productId), ex.ParamName);
        }

        [Fact]
        public async Task RemoveProductVariant_Should_Throw_ArgumentException_If_VariantId_Is_Empty()
        {
            Repository.IRepository repository = new Mock<Repository.IRepository>().Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid productId = Guid.NewGuid();
            Guid variantId = Guid.Empty;

            var commands = new ProductCommands(repository, eventBus);
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.RemoveProductVariant(productId, variantId, userId));

            Assert.Equal(nameof(variantId), ex.ParamName);
        }

        [Fact]
        public async Task RemoveProductVariant_Should_Remove_The_Specified_Variant_From_The_Product()
        {
            var product = Product.Create("ean01", "sku01", "product", "product");
            product.AddVariant("variant", "ean", "sku", new Currency { Code = "EUR", Amount = 100 });

            var repositoryMock = new Mock<Repository.IRepository>();
            repositoryMock.Setup(r => r.GetByKeyAsync<Product>(It.IsAny<Guid>()))
                .Returns(Task.FromResult(product));

            Repository.IRepository repository = repositoryMock.Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid productId = product.Id;
            Guid variantId = product.Variants.First().Id;

            var commands = new ProductCommands(repository, eventBus);
            await commands.RemoveProductVariant(productId, variantId, userId);

            Assert.True(product.Variants.All(v => v.Id != variantId));
        }

        [Fact]
        public async Task AddAttributeToProduct_Should_Throw_ArgumentException_If_ProductId_Is_Empty()
        {
            Repository.IRepository repository = new Mock<Repository.IRepository>().Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid productId = Guid.Empty;
            Guid attributeId = Guid.NewGuid();
            object value = 123;

            var commands = new ProductCommands(repository, eventBus);
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.AddAttributeToProduct(productId, attributeId, value, userId));

            Assert.Equal(nameof(productId), ex.ParamName);
        }

        [Fact]
        public async Task AddAttributeToProduct_Should_Throw_ArgumentException_If_AttributeId_Is_Empty()
        {
            Repository.IRepository repository = new Mock<Repository.IRepository>().Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid productId = Guid.NewGuid();
            Guid attributeId = Guid.Empty;
            object value = 123;

            var commands = new ProductCommands(repository, eventBus);
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.AddAttributeToProduct(productId, attributeId, value, userId));

            Assert.Equal(nameof(attributeId), ex.ParamName);
        }

        [Fact]
        public async Task AddAttributeToProduct_Should_Add_The_Specified_Attribute_To_The_Product()
        {
            var product = Product.Create("ean01", "sku01", "product", "product");
            var attribute = CustomAttribute.Create("attribute", "string");

            var repositoryMock = new Mock<Repository.IRepository>();
            repositoryMock.Setup(r => r.GetByKeyAsync<Product>(It.IsAny<Guid>()))
                .Returns(Task.FromResult(product));

            repositoryMock.Setup(r => r.GetByKeyAsync<CustomAttribute>(It.IsAny<Guid>()))
                .Returns(Task.FromResult(attribute));

            Repository.IRepository repository = repositoryMock.Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid productId = product.Id;
            Guid attributeId = attribute.Id;
            object value = 123;

            var commands = new ProductCommands(repository, eventBus);
            await commands.AddAttributeToProduct(productId, attributeId, value, userId);

            Assert.Collection(product.Attributes.Select(a => a.Attribute).ToArray(), a => Assert.Equal(attribute, a));
        }

        [Fact]
        public async Task RemoveProductAttribute_Should_Throw_ArgumentException_If_ProductId_Is_Empty()
        {
            Repository.IRepository repository = new Mock<Repository.IRepository>().Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid productId = Guid.Empty;
            Guid attributeId = Guid.NewGuid();

            var commands = new ProductCommands(repository, eventBus);
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.RemoveProductAttribute(productId, attributeId, userId));

            Assert.Equal(nameof(productId), ex.ParamName);
        }

        [Fact]
        public async Task RemoveProductAttribute_Should_Throw_ArgumentException_If_AttributeId_Is_Empty()
        {
            Repository.IRepository repository = new Mock<Repository.IRepository>().Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid productId = Guid.NewGuid();
            Guid attributeId = Guid.Empty;

            var commands = new ProductCommands(repository, eventBus);
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.RemoveProductAttribute(productId, attributeId, userId));

            Assert.Equal(nameof(attributeId), ex.ParamName);
        }

        [Fact]
        public async Task RemoveProductAttribute_Should_Remove_Attribute_From_Product()
        {
            var product = Product.Create("ean01", "sku01", "product", "product");
            var attribute = CustomAttribute.Create("attribute", "string");

            product.AddAttribute(attribute, 123);

            var repositoryMock = new Mock<Repository.IRepository>();
            repositoryMock.Setup(r => r.GetByKeyAsync<Product>(It.IsAny<Guid>()))
                .Returns(Task.FromResult(product));

            repositoryMock.Setup(r => r.GetByKeyAsync<CustomAttribute>(It.IsAny<Guid>()))
                .Returns(Task.FromResult(attribute));

            Repository.IRepository repository = repositoryMock.Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid productId = product.Id;
            Guid attributeId = attribute.Id;

            var commands = new ProductCommands(repository, eventBus);
            await commands.RemoveProductAttribute(productId, attributeId, userId);

            Assert.True(product.Attributes.All(a => a.Attribute != attribute));
        }

        [Fact]
        public async Task AddProductTierPrice_Should_Throw_ArgumentException_If_ProductId_Is_Empty()
        {
            Repository.IRepository repository = new Mock<Repository.IRepository>().Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid productId = Guid.Empty;
            int fromQuantity = 1;
            int toQuantity = 5;
            Currency price = new Currency { Code = "EUR", Amount = 10 };

            var commands = new ProductCommands(repository, eventBus);
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.AddProductTierPrice(productId, fromQuantity, toQuantity, price, userId));

            Assert.Equal(nameof(productId), ex.ParamName);
        }

        [Fact]
        public async Task AddProductTierPrice_Should_Add_The_Tier_Price_To_The_Product_With_The_Specified_Values()
        {
            var product = Product.Create("ean01", "sku01", "product", "product");

            var repositoryMock = new Mock<Repository.IRepository>();
            repositoryMock.Setup(r => r.GetByKeyAsync<Product>(It.IsAny<Guid>()))
                .Returns(Task.FromResult(product));

            Repository.IRepository repository = repositoryMock.Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid productId = product.Id;
            int fromQuantity = 1;
            int toQuantity = 5;
            Currency price = new Currency { Code = "EUR", Amount = 10 };

            var commands = new ProductCommands(repository, eventBus);
            await commands.AddProductTierPrice(productId, fromQuantity, toQuantity, price, userId);

            var tierPrice = product.TierPrices.FirstOrDefault(t => t.FromQuantity == fromQuantity && t.ToQuantity == toQuantity && t.Price == price);

            Assert.True(product.TierPriceEnabled);
            Assert.NotNull(tierPrice);
        }

        [Fact]
        public async Task ChangeProductTierPrice_Should_Throw_ArgumentException_If_ProductId_Is_Empty()
        {
            Repository.IRepository repository = new Mock<Repository.IRepository>().Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid productId = Guid.Empty;
            Guid tierPriceId = Guid.NewGuid();
            int fromQuantity = 1;
            int toQuantity = 5;
            Currency price = new Currency { Code = "EUR", Amount = 10 };

            var commands = new ProductCommands(repository, eventBus);
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.ChangeProductTierPrice(productId, tierPriceId, fromQuantity, toQuantity, price, userId));

            Assert.Equal(nameof(productId), ex.ParamName);
        }

        [Fact]
        public async Task ChangeProductTierPrice_Should_Throw_ArgumentException_If_TierPriceId_Is_Empty()
        {
            Repository.IRepository repository = new Mock<Repository.IRepository>().Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid productId = Guid.NewGuid();
            Guid tierPriceId = Guid.Empty;
            int fromQuantity = 1;
            int toQuantity = 5;
            Currency price = new Currency { Code = "EUR", Amount = 10 };

            var commands = new ProductCommands(repository, eventBus);
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.ChangeProductTierPrice(productId, tierPriceId, fromQuantity, toQuantity, price, userId));

            Assert.Equal(nameof(tierPriceId), ex.ParamName);
        }

        [Fact]
        public async Task ChangeProductTierPrice_Should_Change_TierPrice_With_The_Specified_Values()
        {
            var product = Product.Create("ean", "sku", "name", "url");
            product.EnableTierPrices();
            product.AddTierPrice(0, 3, new Currency { Code = "EUR", Amount = 5 });

            var repositoryMock = new Mock<Repository.IRepository>();
            repositoryMock.Setup(r => r.GetByKeyAsync<Product>(It.IsAny<Guid>()))
                .Returns(Task.FromResult(product));

            Repository.IRepository repository = repositoryMock.Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid productId = product.Id;
            Guid tierPriceId = product.TierPrices.First().Id;
            int fromQuantity = 1;
            int toQuantity = 5;
            Currency price = new Currency { Code = "EUR", Amount = 10 };

            var commands = new ProductCommands(repository, eventBus);
            await commands.ChangeProductTierPrice(productId, tierPriceId, fromQuantity, toQuantity, price, userId);

            var tierPrice = product.TierPrices.FirstOrDefault(t => t.Id == tierPriceId);
            
            Assert.NotNull(tierPrice);
            Assert.Equal(fromQuantity, tierPrice.FromQuantity);
            Assert.Equal(toQuantity, tierPrice.ToQuantity);
            Assert.Equal(price, tierPrice.Price);
        }

        [Fact]
        public async Task RemoveTierPriceFromProduct_Should_Throw_ArgumentException_If_ProductId_Is_Empty()
        {
            Repository.IRepository repository = new Mock<Repository.IRepository>().Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid productId = Guid.Empty;
            Guid tierPriceId = Guid.NewGuid();

            var commands = new ProductCommands(repository, eventBus);
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.RemoveTierPriceFromProduct(productId, tierPriceId, userId));

            Assert.Equal(nameof(productId), ex.ParamName);
        }

        [Fact]
        public async Task RemoveTierPriceFromProduct_Should_Throw_ArgumentException_If_TierPriceId_Is_Empty()
        {
            Repository.IRepository repository = new Mock<Repository.IRepository>().Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid productId = Guid.NewGuid();
            Guid tierPriceId = Guid.Empty;

            var commands = new ProductCommands(repository, eventBus);
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.RemoveTierPriceFromProduct(productId, tierPriceId, userId));

            Assert.Equal(nameof(tierPriceId), ex.ParamName);
        }

        [Fact]
        public async Task RemoveTierPriceFromProduct_Should_Remove_Tier_Price_From_Product()
        {
            var product = Product.Create("ean", "sku", "name", "url");
            product.EnableTierPrices();
            product.AddTierPrice(0, 3, new Currency { Code = "EUR", Amount = 5 });

            var repositoryMock = new Mock<Repository.IRepository>();
            repositoryMock.Setup(r => r.GetByKeyAsync<Product>(It.IsAny<Guid>()))
                .Returns(Task.FromResult(product));

            Repository.IRepository repository = repositoryMock.Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid productId = product.Id;
            Guid tierPriceId = product.TierPrices.First().Id;

            var commands = new ProductCommands(repository, eventBus);
            await commands.RemoveTierPriceFromProduct(productId, tierPriceId, userId);

            Assert.True(product.TierPrices.All(t => t.Id != tierPriceId));
        }

        [Fact]
        public async Task AddProductReview_Should_Throw_ArgumentException_If_ProductId_Is_Empty()
        {
            Repository.IRepository repository = new Mock<Repository.IRepository>().Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid productId = Guid.Empty;
            string name = "name";
            int rating = 2;
            string comment = "comment";

            var commands = new ProductCommands(repository, eventBus);
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.AddProductReview(productId, name, rating, comment, userId));

            Assert.Equal(nameof(productId), ex.ParamName);
        }

        [Fact]
        public async Task AddProductReview_Should_Add_The_Review_To_The_Product_With_The_Specified_Values()
        {
            var product = Product.Create("ean", "sku", "name", "url");

            var repositoryMock = new Mock<Repository.IRepository>();
            repositoryMock.Setup(r => r.GetByKeyAsync<Product>(It.IsAny<Guid>()))
                .Returns(Task.FromResult(product));

            Repository.IRepository repository = repositoryMock.Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid productId = product.Id;
            string name = "name";
            int rating = 2;
            string comment = "comment";

            var commands = new ProductCommands(repository, eventBus);
            await commands.AddProductReview(productId, name, rating, comment, userId);

            var review = product.Reviews.FirstOrDefault(r => r.Name == name && r.Rating == rating && r.Comment == comment);
            Assert.NotNull(review);
        }

        [Fact]
        public async Task ApproveProductReview_Should_Throw_ArgumentException_If_ProductId_Is_Empty()
        {
            Repository.IRepository repository = new Mock<Repository.IRepository>().Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid productId = Guid.Empty;
            Guid reviewId = Guid.NewGuid();

            var commands = new ProductCommands(repository, eventBus);
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.ApproveProductReview(productId, reviewId, userId));

            Assert.Equal(nameof(productId), ex.ParamName);
        }

        [Fact]
        public async Task ApproveProductReview_Should_Throw_ArgumentException_If_ReviewId_Is_Empty()
        {
            Repository.IRepository repository = new Mock<Repository.IRepository>().Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid productId = Guid.NewGuid();
            Guid reviewId = Guid.Empty;

            var commands = new ProductCommands(repository, eventBus);
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.ApproveProductReview(productId, reviewId, userId));

            Assert.Equal(nameof(reviewId), ex.ParamName);
        }

        [Fact]
        public async Task ApproveProductReview_Should_Approve_The_Specified_Review()
        {
            var product = Product.Create("ean", "sku", "name", "url");
            product.AddReview("name", 3);

            var repositoryMock = new Mock<Repository.IRepository>();
            repositoryMock.Setup(r => r.GetByKeyAsync<Product>(It.IsAny<Guid>()))
                .Returns(Task.FromResult(product));

            Repository.IRepository repository = repositoryMock.Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid productId = product.Id;
            Guid reviewId = product.Reviews.First().Id;

            var commands = new ProductCommands(repository, eventBus);
            await commands.ApproveProductReview(productId, reviewId, userId);

            var review = product.Reviews.FirstOrDefault(r => r.Id == reviewId);
            Assert.NotNull(review);
            Assert.True(review.Approved);
            Assert.NotNull(review.ApprovedOn);
        }

        [Fact]
        public async Task RemoveProductReview_Should_Throw_ArgumentException_If_ProductId_Is_Empty()
        {
            Repository.IRepository repository = new Mock<Repository.IRepository>().Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid productId = Guid.Empty;
            Guid reviewId = Guid.NewGuid();

            var commands = new ProductCommands(repository, eventBus);
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.RemoveProductReview(productId, reviewId, userId));

            Assert.Equal(nameof(productId), ex.ParamName);
        }

        [Fact]
        public async Task RemoveProductReview_Should_Throw_ArgumentException_If_ReviewId_Is_Empty()
        {
            Repository.IRepository repository = new Mock<Repository.IRepository>().Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid productId = Guid.NewGuid();
            Guid reviewId = Guid.Empty;

            var commands = new ProductCommands(repository, eventBus);
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.RemoveProductReview(productId, reviewId, userId));

            Assert.Equal(nameof(reviewId), ex.ParamName);
        }

        [Fact]
        public async Task RemoveProductReview_Should_Remove_The_Specified_Review_From_The_Product()
        {
            var product = Product.Create("ean", "sku", "name", "url");
            product.AddReview("name", 3);

            var repositoryMock = new Mock<Repository.IRepository>();
            repositoryMock.Setup(r => r.GetByKeyAsync<Product>(It.IsAny<Guid>()))
                .Returns(Task.FromResult(product));

            Repository.IRepository repository = repositoryMock.Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid productId = product.Id;
            Guid reviewId = product.Reviews.First().Id;

            var commands = new ProductCommands(repository, eventBus);
            await commands.RemoveProductReview(productId, reviewId, userId);

            Assert.True(product.Reviews.All(r => r.Id != reviewId));
        }

        [Fact]
        public async Task AddProductImage_Should_Throw_ArgumentException_If_ProductId_Is_Empty()
        {
            Repository.IRepository repository = new Mock<Repository.IRepository>().Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid productId = Guid.Empty;
            string path = "path/to/img";
            string name = "img";
            string originalName = "original.jpg";
            bool isMain = true;
            DateTime uploadedOn = DateTime.Today;

            var commands = new ProductCommands(repository, eventBus);
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.AddProductImage(productId, path, name, originalName, isMain, uploadedOn, userId));

            Assert.Equal(nameof(productId), ex.ParamName);
        }

        [Fact]
        public async Task AddProductImage_Should_Add_The_Image_With_The_Specified_Values()
        {
            var product = Product.Create("ean", "sku", "name", "url");

            var repositoryMock = new Mock<Repository.IRepository>();
            repositoryMock.Setup(r => r.GetByKeyAsync<Product>(It.IsAny<Guid>()))
                .Returns(Task.FromResult(product));

            Repository.IRepository repository = repositoryMock.Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid productId = product.Id;
            string path = "path/to/img";
            string name = "img";
            string originalName = "original.jpg";
            bool isMain = true;
            DateTime uploadedOn = DateTime.Today;

            var commands = new ProductCommands(repository, eventBus);
            await commands.AddProductImage(productId, path, name, originalName, isMain, uploadedOn, userId);

            var image = product.Images.FirstOrDefault(i => i.Path == path && i.Name == name && i.OriginalName == originalName && i.IsMain == isMain && i.UploadedOn == uploadedOn);
            Assert.NotNull(image);
        }

        [Fact]
        public async Task RemoveProductImage_Should_Throw_ArgumentException_If_ProductId_Is_Empty()
        {
            Repository.IRepository repository = new Mock<Repository.IRepository>().Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid productId = Guid.Empty;
            Guid imageId = Guid.NewGuid();

            var commands = new ProductCommands(repository, eventBus);
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.RemoveProductImage(productId, imageId, userId));

            Assert.Equal(nameof(productId), ex.ParamName);
        }

        [Fact]
        public async Task RemoveProductImage_Should_Throw_ArgumentException_If_ImageId_Is_Empty()
        {
            Repository.IRepository repository = new Mock<Repository.IRepository>().Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid productId = Guid.NewGuid();
            Guid imageId = Guid.Empty;

            var commands = new ProductCommands(repository, eventBus);
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.RemoveProductImage(productId, imageId, userId));

            Assert.Equal(nameof(imageId), ex.ParamName);
        }

        [Fact]
        public async Task RemoveProductImage_Should_Remove_The_Image_From_The_Product()
        {
            var product = Product.Create("ean", "sku", "name", "url");
            product.AddImage("path", "name", "original", true, DateTime.Today);

            var repositoryMock = new Mock<Repository.IRepository>();
            repositoryMock.Setup(r => r.GetByKeyAsync<Product>(It.IsAny<Guid>()))
                .Returns(Task.FromResult(product));

            Repository.IRepository repository = repositoryMock.Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid productId = product.Id;
            Guid imageId = product.Images.First().Id;

            var commands = new ProductCommands(repository, eventBus);
            await commands.RemoveProductImage(productId, imageId, userId);

            Assert.True(product.Images.All(i => i.Id != imageId));
        }

        [Fact]
        public async Task SetProductSeo_Should_Throw_ArgumentException_If_ProductId_Is_Empty()
        {
            Repository.IRepository repository = new Mock<Repository.IRepository>().Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid productId = Guid.Empty;
            SeoData seo = new SeoData { };

            var commands = new ProductCommands(repository, eventBus);
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.SetProductSeo(productId, seo, userId));

            Assert.Equal(nameof(productId), ex.ParamName);
        }

        [Fact]
        public async Task SetProductSeo_Should_Set_SeoData_With_The_Specified_Values()
        {
            var product = Product.Create("ean", "sku", "name", "url");

            var repositoryMock = new Mock<Repository.IRepository>();
            repositoryMock.Setup(r => r.GetByKeyAsync<Product>(It.IsAny<Guid>()))
                .Returns(Task.FromResult(product));

            Repository.IRepository repository = repositoryMock.Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid productId = product.Id;
            SeoData seo = new SeoData { Title = "title", Description = "description" };

            var commands = new ProductCommands(repository, eventBus);
            await commands.SetProductSeo(productId, seo, userId);

            Assert.Equal(seo.Title, product.Seo.Title);
            Assert.Equal(seo.Description, product.Seo.Description);
        }

        [Fact]
        public async Task ChangeProductVariant_Should_Throw_ArgumentException_If_ProductId_Is_Empty()
        {
            Repository.IRepository repository = new Mock<Repository.IRepository>().Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid productId = Guid.Empty;
            Guid variantId = Guid.NewGuid();
            string name = "name";
            string ean = "ean";
            string sku = "sku";
            Currency price = new Currency { Code = "EUR", Amount = 10 };

            var commands = new ProductCommands(repository, eventBus);

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.ChangeProductVariant(productId, variantId, name, ean, sku, price, userId));
            Assert.Equal(nameof(productId), ex.ParamName);
        }

        [Fact]
        public async Task ChangeProductVariant_Should_Throw_ArgumentException_If_VariantId_Is_Empty()
        {
            Repository.IRepository repository = new Mock<Repository.IRepository>().Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid productId = Guid.NewGuid();
            Guid variantId = Guid.Empty;
            string name = "name";
            string ean = "ean";
            string sku = "sku";
            Currency price = new Currency { Code = "EUR", Amount = 10 };

            var commands = new ProductCommands(repository, eventBus);

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.ChangeProductVariant(productId, variantId, name, ean, sku, price, userId));
            Assert.Equal(nameof(variantId), ex.ParamName);
        }

        [Fact]
        public async Task ChangeProductVariant_Should_Change_The_Product_Variant_With_The_Specified_Values()
        {
            var product = Product.Create("ean", "sku", "name", "url");
            product.AddVariant("v", "v", "v", new Currency { Code = "EUR", Amount = 5 });

            var repositoryMock = new Mock<Repository.IRepository>();
            repositoryMock.Setup(r => r.GetByKeyAsync<Product>(It.IsAny<Guid>()))
                .Returns(Task.FromResult(product));

            Repository.IRepository repository = repositoryMock.Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid productId = product.Id;
            Guid variantId = product.Variants.First().Id;
            string name = "name";
            string ean = "ean";
            string sku = "sku";
            Currency price = new Currency { Code = "EUR", Amount = 10 };

            var commands = new ProductCommands(repository, eventBus);
            await commands.ChangeProductVariant(productId, variantId, name, ean, sku, price, userId);

            var variant = product.Variants.FirstOrDefault(v => v.Id == variantId);
            
            Assert.NotNull(variant);
            Assert.Equal(name, variant.Name);
            Assert.Equal(ean, variant.EanCode);
            Assert.Equal(sku, variant.Sku);
            Assert.Equal(price, variant.Price);
        }

        [Fact]
        public async Task ChangeProductMainCategory_Should_Throw_ArgumentException_If_ProductId_Is_Empty()
        {
            Repository.IRepository repository = new Mock<Repository.IRepository>().Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid productId = Guid.Empty;
            Guid categoryId = Guid.NewGuid();

            var commands = new ProductCommands(repository, eventBus);

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.ChangeProductMainCategory(productId, categoryId, userId));
            Assert.Equal(nameof(productId), ex.ParamName);
        }

        [Fact]
        public async Task ChangeProductMainCategory_Should_Throw_ArgumentException_If_CategoryId_Is_Empty()
        {
            Repository.IRepository repository = new Mock<Repository.IRepository>().Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid productId = Guid.NewGuid();
            Guid categoryId = Guid.Empty;

            var commands = new ProductCommands(repository, eventBus);

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.ChangeProductMainCategory(productId, categoryId, userId));
            Assert.Equal(nameof(categoryId), ex.ParamName);
        }

        [Fact]
        public async Task RemoveProductCategory_Should_Throw_ArgumentException_If_ProductId_Is_Empty()
        {
            Repository.IRepository repository = new Mock<Repository.IRepository>().Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid productId = Guid.Empty;
            Guid categoryId = Guid.NewGuid();

            var commands = new ProductCommands(repository, eventBus);

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.RemoveProductCategory(productId, categoryId, userId));
            Assert.Equal(nameof(productId), ex.ParamName);
        }

        [Fact]
        public async Task RemoveProductCategory_Should_Throw_ArgumentException_If_CategoryId_Is_Empty()
        {
            Repository.IRepository repository = new Mock<Repository.IRepository>().Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid productId = Guid.NewGuid();
            Guid categoryId = Guid.Empty;

            var commands = new ProductCommands(repository, eventBus);

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.RemoveProductCategory(productId, categoryId, userId));
            Assert.Equal(nameof(categoryId), ex.ParamName);
        }

        [Fact]
        public async Task RemoveProductCategory_Should_Remove_Category_From_The_Product_Categories()
        {
            var product = Product.Create("ean", "sku", "name", "url");
            var category = Category.Create("category", "category", "category");

            product.AddCategory(category);

            var repositoryMock = new Mock<Repository.IRepository>();
            repositoryMock.Setup(r => r.GetByKeyAsync<Product>(It.IsAny<Guid>()))
                .Returns(Task.FromResult(product));

            repositoryMock.Setup(r => r.GetByKeyAsync<Category>(It.IsAny<Guid>()))
                .Returns(Task.FromResult(category));

            Repository.IRepository repository = repositoryMock.Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid productId = product.Id;
            Guid categoryId = category.Id;

            var commands = new ProductCommands(repository, eventBus);

            await commands.RemoveProductCategory(productId, categoryId, userId);
            Assert.True(product.ProductCategories.All(c => c.CategoryId != category.Id));
        }

        [Fact]
        public async Task UpdateProductInfo_Should_Change_Sales_Dates_If_IsOnSale_Did_Not_Change_But_Dates_Are_Different()
        {
            var product = Product.Create("ean01", "sku01", "product", "product");
            product.SetOnSale();

            var repositoryMock = new Mock<Repository.IRepository>();
            repositoryMock.Setup(r => r.GetByKeyAsync<Product>(It.IsAny<Guid>()))
                .Returns(Task.FromResult(product));

            Repository.IRepository repository = repositoryMock.Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid productId = product.Id;
            string ean = "ean";
            string sku = "sku";
            string name = "name";
            string url = "url";
            Currency price = new Currency { Code = "EUR", Amount = 10 };
            string description = "description";
            int unitInStock = 10;
            bool isOnSale = true;
            DateTime? onSaleFrom = DateTime.Today.AddDays(1);
            DateTime? onSaleTo = DateTime.Today.AddYears(1);

            var commands = new ProductCommands(repository, eventBus);
            await commands.UpdateProductInfo(productId, ean, sku, name, url, price, description, unitInStock, isOnSale, onSaleFrom, onSaleTo, userId);

            Assert.Equal(product.OnSaleFrom, onSaleFrom);
            Assert.Equal(product.OnSaleTo, onSaleTo);
        }
        #endregion
    }
}
