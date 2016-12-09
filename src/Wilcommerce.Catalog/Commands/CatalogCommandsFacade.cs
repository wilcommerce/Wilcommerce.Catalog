using System;
using System.Threading.Tasks;
using Wilcommerce.Core.Common.Domain.Models;
using Wilcommerce.Catalog.Repository;
using Wilcommerce.Catalog.Models;
using System.Collections.Generic;
using System.Linq;

namespace Wilcommerce.Catalog.Commands
{
    public class CatalogCommandsFacade : ICatalogCommandsFacade
    {
        public IRepository Repository { get; }

        public CatalogCommandsFacade(IRepository repository)
        {
            Repository = repository;
        }

        #region Brand Commands
        public async Task ChangeBrandDescription(Guid brandId, string description)
        {
            try
            {
                var brand = await Repository.GetByKeyAsync<Brand>(brandId);
                brand.ChangeDescription(description);

                await Repository.SaveChangesAsync();
            }
            catch 
            {
                throw;
            }
        }

        public async Task ChangeBrandName(Guid brandId, string name)
        {
            try
            {
                var brand = await Repository.GetByKeyAsync<Brand>(brandId);
                brand.ChangeName(name);

                await Repository.SaveChangesAsync();
            }
            catch 
            {
                throw;
            }
        }

        public async Task ChangeBrandUrl(Guid brandId, string url)
        {
            try
            {
                var brand = await Repository.GetByKeyAsync<Brand>(brandId);
                brand.ChangeUrl(url);

                await Repository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<Guid> CreateNewBrand(string name, string url, string description, Image logo)
        {
            try
            {
                var brand = Brand.Create(name, url);
                if (!string.IsNullOrEmpty(description))
                {
                    brand.ChangeDescription(description);
                }

                if (logo != null)
                {
                    brand.SetLogo(logo);
                }

                Repository.Add(brand);
                await Repository.SaveChangesAsync();

                return brand.Id;
            }
            catch
            {
                throw;
            }
        }

        public async Task DeleteBrand(Guid brandId)
        {
            try
            {
                var brand = await Repository.GetByKeyAsync<Brand>(brandId);
                brand.Delete();

                await Repository.SaveChangesAsync();
            }
            catch 
            {
                throw;
            }
        }

        public async Task RestoreBrand(Guid brandId)
        {
            try
            {
                var brand = await Repository.GetByKeyAsync<Brand>(brandId);
                brand.Restore();

                await Repository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task SetBrandLogo(Guid brandId, Image logo)
        {
            try
            {
                var brand = await Repository.GetByKeyAsync<Brand>(brandId);
                brand.SetLogo(logo);

                await Repository.SaveChangesAsync();
            }
            catch 
            {
                throw;
            }
        }

        #endregion

        #region Category Commands
        public async Task<Guid> CreateNewCategory(string code, string name, string url, string description, bool isVisible, DateTime? visibleFrom, DateTime? visibleTo)
        {
            try
            {
                var category = Category.Create(code, name, url);
                if (!string.IsNullOrEmpty(description))
                {
                    category.ChangeDescription(description);
                }

                if (isVisible)
                {
                    if (visibleFrom == null)
                    {
                        category.SetAsVisible();
                    }
                    else if (visibleTo == null)
                    {
                        category.SetAsVisible((DateTime)visibleFrom);
                    }
                    else
                    {
                        category.SetAsVisible((DateTime)visibleFrom, (DateTime)visibleTo);
                    }
                }

                Repository.Add(category);
                await Repository.SaveChangesAsync();

                return category.Id;
            }
            catch 
            {
                throw;
            }
        }

        public async Task SetCategoryAsVisible(Guid categoryId, DateTime? from, DateTime? to)
        {
            try
            {
                var category = await Repository.GetByKeyAsync<Category>(categoryId);
                if (from == null)
                {
                    category.SetAsVisible();
                }
                else if (to == null)
                {
                    category.SetAsVisible((DateTime)from);
                }
                else
                {
                    category.SetAsVisible((DateTime)from, (DateTime)to);
                }

                await Repository.SaveChangesAsync();
            }
            catch 
            {
                throw;
            }
        }

        public async Task AddCategoryChild(Guid categoryId, Guid childId)
        {
            try
            {
                var category = await Repository.GetByKeyAsync<Category>(categoryId);
                var child = await Repository.GetByKeyAsync<Category>(childId);

                category.AddChild(child);
                await Repository.SaveChangesAsync();
            }
            catch 
            {
                throw;
            }
        }

        public async Task ChangeCategoryName(Guid categoryId, string name)
        {
            try
            {
                var category = await Repository.GetByKeyAsync<Category>(categoryId);
                category.ChangeName(name);

                await Repository.SaveChangesAsync();
            }
            catch 
            {
                throw;
            }
        }

        public async Task ChangeCategoryCode(Guid categoryId, string code)
        {
            try
            {
                var category = await Repository.GetByKeyAsync<Category>(categoryId);
                category.ChangeCode(code);

                await Repository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task ChangeCategoryDescription(Guid categoryId, string description)
        {
            try
            {
                var category = await Repository.GetByKeyAsync<Category>(categoryId);
                category.ChangeDescription(description);

                await Repository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task ChangeCategoryUrl(Guid categoryId, string url)
        {
            try
            {
                var category = await Repository.GetByKeyAsync<Category>(categoryId);
                category.ChangeUrl(url);

                await Repository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task SetParentForCategory(Guid categoryId, Guid parentId)
        {
            try
            {
                var category = await Repository.GetByKeyAsync<Category>(categoryId);
                var parent = await Repository.GetByKeyAsync<Category>(parentId);
                category.SetParentCategory(parent);

                await Repository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task DeleteCategory(Guid categoryId)
        {
            try
            {
                var category = await Repository.GetByKeyAsync<Category>(categoryId);
                category.Delete();

                await Repository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task RestoreCategory(Guid categoryId)
        {
            try
            {
                var category = await Repository.GetByKeyAsync<Category>(categoryId);
                category.Restore();

                await Repository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task RemoveChildForCategory(Guid categoryId, Guid childId)
        {
            try
            {
                var category = await Repository.GetByKeyAsync<Category>(categoryId);
                category.RemoveChild(childId);

                await Repository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task RemoveParentForCategory(Guid categoryId)
        {
            try
            {
                var category = await Repository.GetByKeyAsync<Category>(categoryId);
                category.RemoveParent();

                await Repository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region CustomAttribute Commands
        public async Task<Guid> CreateNewCustomAttribute(string name, string type, string description, string unitOfMeasure, IEnumerable<object> values)
        {
            try
            {
                var attribute = CustomAttribute.Create(name, type);
                if (!string.IsNullOrEmpty(description))
                {
                    attribute.ChangeDescription(description);
                }

                if (!string.IsNullOrEmpty(unitOfMeasure))
                {
                    attribute.SetUnitOfMeasure(unitOfMeasure);
                }

                if(values != null && values.Count() > 0)
                {
                    values.ToList().ForEach(v => attribute.AddValue(v));
                }

                Repository.Add(attribute);
                await Repository.SaveChangesAsync();

                return attribute.Id;
            }
            catch 
            {
                throw;
            }
        }

        public async Task AddValueForAttribute(Guid attributeId, object value)
        {
            try
            {
                var attribute = await Repository.GetByKeyAsync<CustomAttribute>(attributeId);
                attribute.AddValue(value);

                await Repository.SaveChangesAsync();
            }
            catch 
            {
                throw;
            }
        }

        public async Task RemoveValueFromAttribute(Guid attributeId, object value)
        {
            try
            {
                var attribute = await Repository.GetByKeyAsync<CustomAttribute>(attributeId);
                attribute.RemoveValue(value);

                await Repository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task ChangeAttributeName(Guid attributeId, string name)
        {
            try
            {
                var attribute = await Repository.GetByKeyAsync<CustomAttribute>(attributeId);
                attribute.ChangeName(name);

                await Repository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task ChangeAttributeDescription(Guid attributeId, string description)
        {
            try
            {
                var attribute = await Repository.GetByKeyAsync<CustomAttribute>(attributeId);
                attribute.ChangeDescription(description);

                await Repository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task SetAttributeUnitOfMeasure(Guid attributeId, string unitOfMeasure)
        {
            try
            {
                var attribute = await Repository.GetByKeyAsync<CustomAttribute>(attributeId);
                attribute.SetUnitOfMeasure(unitOfMeasure);

                await Repository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task ChangeAttributeDataType(Guid attributeId, string dataType)
        {
            try
            {
                var attribute = await Repository.GetByKeyAsync<CustomAttribute>(attributeId);
                attribute.ChangeDataType(dataType);

                await Repository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task DeleteCustomAttribute(Guid attributeId)
        {
            try
            {
                var attribute = await Repository.GetByKeyAsync<CustomAttribute>(attributeId);
                attribute.Delete();

                await Repository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task RestoreCustomAttribute(Guid attributeId)
        {
            try
            {
                var attribute = await Repository.GetByKeyAsync<CustomAttribute>(attributeId);
                attribute.Restore();

                await Repository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region Product Commands
        public async Task<Guid> CreateNewProduct(string ean, string sku, string name, string url, Currency price, string description, int unitInStock, bool isOnSale, DateTime? onSaleFrom, DateTime? onSaleTo)
        {
            try
            {
                var product = Product.Create(ean, sku, name, url);
                if (price != null)
                {
                    product.SetPrice(price);
                }

                if (!string.IsNullOrEmpty(description))
                {
                    product.ChangeDescription(description);
                }

                if (unitInStock > 0)
                {
                    product.SetUnitInStock(unitInStock);
                }

                if (isOnSale)
                {
                    if (onSaleFrom == null)
                    {
                        product.SetOnSale();
                    }
                    else if (onSaleTo == null)
                    {
                        product.SetOnSale((DateTime)onSaleFrom);
                    }
                    else
                    {
                        product.SetOnSale((DateTime)onSaleFrom, (DateTime)onSaleTo);
                    }
                }

                Repository.Add(product);
                await Repository.SaveChangesAsync();

                return product.Id;
            }
            catch 
            {
                throw;
            }
        }

        public async Task DeleteProduct(Guid productId)
        {
            try
            {
                var product = await Repository.GetByKeyAsync<Product>(productId);
                product.Delete();

                await Repository.SaveChangesAsync();
            }
            catch 
            {
                throw;
            }
        }

        public async Task RestoreProduct(Guid productId)
        {
            try
            {
                var product = await Repository.GetByKeyAsync<Product>(productId);
                product.Restore();

                await Repository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task SetUnitInStockForProduct(Guid productId, int unitInStock)
        {
            try
            {
                var product = await Repository.GetByKeyAsync<Product>(productId);
                product.SetUnitInStock(unitInStock);

                await Repository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task AddUnitInStockToProduct(Guid productId, int unitToAdd)
        {
            try
            {
                var product = await Repository.GetByKeyAsync<Product>(productId);
                product.AddUnitInStock(unitToAdd);

                await Repository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task RemoveUnitInStockFromProduct(Guid productId, int unitToRemove)
        {
            try
            {
                var product = await Repository.GetByKeyAsync<Product>(productId);
                product.RemoveUnitFromStock(unitToRemove);

                await Repository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task ChangeProductEanCode(Guid productId, string ean)
        {
            try
            {
                var product = await Repository.GetByKeyAsync<Product>(productId);
                product.ChangeEanCode(ean);

                await Repository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task ChangeProductSku(Guid productId, string sku)
        {
            try
            {
                var product = await Repository.GetByKeyAsync<Product>(productId);
                product.ChangeSku(sku);

                await Repository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task ChangeProductName(Guid productId, string name)
        {
            try
            {
                var product = await Repository.GetByKeyAsync<Product>(productId);
                product.ChangeName(name);

                await Repository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task ChangeProductDescription(Guid productId, string description)
        {
            try
            {
                var product = await Repository.GetByKeyAsync<Product>(productId);
                product.ChangeDescription(description);

                await Repository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task ChangeProductUrl(Guid productId, string url)
        {
            try
            {
                var product = await Repository.GetByKeyAsync<Product>(productId);
                product.ChangeUrl(url);

                await Repository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task SetProductPrice(Guid productId, Currency price)
        {
            try
            {
                var product = await Repository.GetByKeyAsync<Product>(productId);
                product.SetPrice(price);

                await Repository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task SetProductOnSale(Guid productId, DateTime? from, DateTime? to)
        {
            try
            {
                var product = await Repository.GetByKeyAsync<Product>(productId);
                if (from == null)
                {
                    product.SetOnSale();
                }
                else if (to == null)
                {
                    product.SetOnSale((DateTime)from);
                }
                else
                {
                    product.SetOnSale((DateTime)from, (DateTime)to);
                }

                await Repository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task RemoveProductFromSale(Guid productId)
        {
            try
            {
                var product = await Repository.GetByKeyAsync<Product>(productId);
                product.RemoveFromSale();

                await Repository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task SetProductVendor(Guid productId, Guid brandId)
        {
            try
            {
                var product = await Repository.GetByKeyAsync<Product>(productId);
                var brand = await Repository.GetByKeyAsync<Brand>(brandId);

                product.SetVendor(brand);

                await Repository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task AddCategoryToProduct(Guid productId, Guid categoryId)
        {
            try
            {
                var product = await Repository.GetByKeyAsync<Product>(productId);
                var category = await Repository.GetByKeyAsync<Category>(categoryId);

                product.AddCategory(category);

                await Repository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task AddMainCategoryToProduct(Guid productId, Guid categoryId)
        {
            try
            {
                var product = await Repository.GetByKeyAsync<Product>(productId);
                var category = await Repository.GetByKeyAsync<Category>(categoryId);

                product.AddMainCategory(category);

                await Repository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task AddProductVariant(Guid productId, string name, string ean, string sku, Currency price)
        {
            try
            {
                var product = await Repository.GetByKeyAsync<Product>(productId);
                product.AddVariant(name, ean, sku, price);

                await Repository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task RemoveProductVariant(Guid productId, Guid variantId)
        {
            try
            {
                var product = await Repository.GetByKeyAsync<Product>(productId);
                product.RemoveVariant(variantId);

                await Repository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task AddAttributeToProduct(Guid productId, Guid attributeId, object value)
        {
            try
            {
                var product = await Repository.GetByKeyAsync<Product>(productId);
                var attribute = await Repository.GetByKeyAsync<CustomAttribute>(attributeId);

                product.AddAttribute(attribute, value);

                await Repository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task RemoveProductAttribute(Guid productId, Guid attributeId)
        {
            try
            {
                var product = await Repository.GetByKeyAsync<Product>(productId);
                product.DeleteAttribute(attributeId);

                await Repository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task AddProductTierPrice(Guid productId, int fromQuantity, int toQuantity, Currency price)
        {
            try
            {
                var product = await Repository.GetByKeyAsync<Product>(productId);
                if (!product.TierPriceEnabled)
                {
                    product.EnableTierPrices();
                }

                product.AddTierPrice(fromQuantity, toQuantity, price);

                await Repository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task RemoveTierPriceFromProduct(Guid productId, Guid tierPriceId)
        {
            try
            {
                var product = await Repository.GetByKeyAsync<Product>(productId);
                product.DeleteTierPrice(tierPriceId);

                await Repository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task AddProductReview(Guid productId, string name, int rating, string comment)
        {
            try
            {
                var product = await Repository.GetByKeyAsync<Product>(productId);
                product.AddReview(name, rating, comment);

                await Repository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task ApproveProductReview(Guid productId, Guid reviewId)
        {
            try
            {
                var product = await Repository.GetByKeyAsync<Product>(productId);
                product.ApproveReview(reviewId);

                await Repository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task RemoveProductReview(Guid productId, Guid reviewId)
        {
            try
            {
                var product = await Repository.GetByKeyAsync<Product>(productId);
                product.RemoveReviewApproval(reviewId);
                product.DeleteReview(reviewId);

                await Repository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task AddProductImage(Guid productId, string path, string name, string originalName, bool isMain, DateTime uploadedOn)
        {
            try
            {
                var product = await Repository.GetByKeyAsync<Product>(productId);
                product.AddImage(path, name, originalName, isMain, uploadedOn);

                await Repository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task RemoveProductImage(Guid productId, Guid imageId)
        {
            try
            {
                var product = await Repository.GetByKeyAsync<Product>(productId);
                product.DeleteImage(imageId);

                await Repository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region CatalogSettings Commands

        public async Task<Guid> CreateCatalogSettings(int categoriesPerPage, int productsPerPage, CatalogSettings.ViewType categoriesViewType, CatalogSettings.ViewType productsViewType, bool showPrices, bool allowReviews, int reviewsPerPage)
        {
            try
            {
                var settings = CatalogSettings.Create(categoriesPerPage, productsPerPage, categoriesViewType, productsViewType);
                settings.ShowPrices(showPrices);
                settings.AllowProductReviews(allowReviews);

                if (settings.ProductReviewsAllowed)
                {
                    settings.SetProductReviewsPerPage(reviewsPerPage);
                }

                Repository.Add(settings);
                await Repository.SaveChangesAsync();

                return settings.Id;
            }
            catch 
            {
                throw;
            }
        }

        public async Task ShowPrices(Guid settingsId, bool showPrices)
        {
            try
            {
                var settings = await Repository.GetByKeyAsync<CatalogSettings>(settingsId);
                settings.ShowPrices(showPrices);

                await Repository.SaveChangesAsync();
            }
            catch 
            {
                throw;
            }
        }

        public async Task AllowProductReviews(Guid settingsId, bool allowReviews)
        {
            try
            {
                var settings = await Repository.GetByKeyAsync<CatalogSettings>(settingsId);
                settings.AllowProductReviews(allowReviews);

                await Repository.SaveChangesAsync();
            }
            catch 
            {
                throw;
            }
        }

        public async Task SetProductReviewsPerPage(Guid settingsId, int reviewsPerPage)
        {
            try
            {
                var settings = await Repository.GetByKeyAsync<CatalogSettings>(settingsId);
                settings.SetProductReviewsPerPage(reviewsPerPage);

                await Repository.SaveChangesAsync();
            }
            catch 
            {
                throw;
            }
        }

        public async Task SetCategoriesView(Guid settingsId, CatalogSettings.ViewType viewType, int categoriesPerPage)
        {
            try
            {
                var settings = await Repository.GetByKeyAsync<CatalogSettings>(settingsId);
                settings.SetCategoriesViewType(viewType);
                settings.SetCategoriesPerPage(categoriesPerPage);

                await Repository.SaveChangesAsync();
            }
            catch 
            {
                throw;
            }
        }

        public async Task SetProductsView(Guid settingsId, CatalogSettings.ViewType viewType, int productsPerPage)
        {
            try
            {
                var settings = await Repository.GetByKeyAsync<CatalogSettings>(settingsId);
                settings.SetProductsViewType(viewType);
                settings.SetProductsPerPage(productsPerPage);

                await Repository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        #endregion
    }
}
