using System;
using System.Threading.Tasks;
using Wilcommerce.Core.Common.Domain.Models;
using Wilcommerce.Catalog.Models;
using System.Collections.Generic;
using System.Linq;
using Wilcommerce.Core.Infrastructure;
using Wilcommerce.Catalog.Events.Brand;
using Wilcommerce.Catalog.Events.Category;
using Wilcommerce.Catalog.Events.CustomAttribute;
using Wilcommerce.Catalog.Events.Product;

namespace Wilcommerce.Catalog.Commands
{
    /// <see cref="ICatalogCommandsFacade"/>
    public class CatalogCommandsFacade : ICatalogCommandsFacade
    {
        /// <summary>
        /// Get the repository
        /// </summary>
        public Repository.IRepository Repository { get; }

        /// <summary>
        /// Get the event bus
        /// </summary>
        public IEventBus EventBus { get; }

        /// <summary>
        /// Construct the command facade
        /// </summary>
        /// <param name="repository">The repository</param>
        /// <param name="eventBus">The event bus</param>
        public CatalogCommandsFacade(Repository.IRepository repository, IEventBus eventBus)
        {
            Repository = repository;
            EventBus = eventBus;
        }

        #region Brand Commands
        /// <see cref="ICatalogCommandsFacade.ChangeBrandDescription(Guid, string)"/>
        public async Task ChangeBrandDescription(Guid brandId, string description)
        {
            try
            {
                var brand = await Repository.GetByKeyAsync<Brand>(brandId);
                brand.ChangeDescription(description);

                await Repository.SaveChangesAsync();

                var @event = new BrandDescriptionChangedEvent(brandId, description);
                EventBus.RaiseEvent(@event);
            }
            catch 
            {
                throw;
            }
        }

        /// <see cref="ICatalogCommandsFacade.ChangeBrandName(Guid, string)"/>
        public async Task ChangeBrandName(Guid brandId, string name)
        {
            try
            {
                var brand = await Repository.GetByKeyAsync<Brand>(brandId);
                brand.ChangeName(name);

                await Repository.SaveChangesAsync();

                var @event = new BrandNameChangedEvent(brandId, name);
                EventBus.RaiseEvent(@event);
            }
            catch 
            {
                throw;
            }
        }

        /// <see cref="ICatalogCommandsFacade.ChangeBrandUrl(Guid, string)"/>
        public async Task ChangeBrandUrl(Guid brandId, string url)
        {
            try
            {
                var brand = await Repository.GetByKeyAsync<Brand>(brandId);
                brand.ChangeUrl(url);

                await Repository.SaveChangesAsync();

                var @event = new BrandUrlChangedEvent(brandId, url);
                EventBus.RaiseEvent(@event);
            }
            catch 
            {
                throw;
            }
        }

        /// <see cref="ICatalogCommandsFacade.CreateNewBrand(string, string, string, Image)"/>
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

                var @event = new BrandCreatedEvent(brand.Id, brand.Name);
                EventBus.RaiseEvent(@event);

                return brand.Id;
            }
            catch 
            {
                throw;
            }
        }

        /// <see cref="ICatalogCommandsFacade.DeleteBrand(Guid)"/>
        public async Task DeleteBrand(Guid brandId)
        {
            try
            {
                var brand = await Repository.GetByKeyAsync<Brand>(brandId);
                brand.Delete();

                await Repository.SaveChangesAsync();

                var @event = new BrandDeletedEvent(brandId);
                EventBus.RaiseEvent(@event);
            }
            catch 
            {
                throw;
            }
        }

        /// <see cref="ICatalogCommandsFacade.RestoreBrand(Guid)"/>
        public async Task RestoreBrand(Guid brandId)
        {
            try
            {
                var brand = await Repository.GetByKeyAsync<Brand>(brandId);
                brand.Restore();

                await Repository.SaveChangesAsync();

                var @event = new BrandRestoredEvent(brandId);
                EventBus.RaiseEvent(@event);
            }
            catch
            {
                throw;
            }
        }

        /// <see cref="ICatalogCommandsFacade.SetBrandLogo(Guid, Image)"/>
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

        /// <see cref="ICatalogCommandsFacade.SetBrandSeoData(Guid, SeoData)"/>
        public async Task SetBrandSeoData(Guid brandId, SeoData seo)
        {
            try
            {
                var brand = await Repository.GetByKeyAsync<Brand>(brandId);
                brand.SetSeoData(seo);

                await Repository.SaveChangesAsync();
            }
            catch 
            {
                throw;
            }
        }

        #endregion

        #region Category Commands
        /// <see cref="ICatalogCommandsFacade.CreateNewCategory(string, string, string, string, bool, DateTime?, DateTime?)"/>
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

                var @event = new CategoryCreatedEvent(category.Id, name, code);
                EventBus.RaiseEvent(@event);

                return category.Id;
            }
            catch 
            {
                throw;
            }
        }

        /// <see cref="ICatalogCommandsFacade.SetCategoryAsVisible(Guid, DateTime?, DateTime?)"/>
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

        /// <see cref="ICatalogCommandsFacade.HideCategory(Guid)"/>
        public async Task HideCategory(Guid categoryId)
        {
            try
            {
                var category = await Repository.GetByKeyAsync<Category>(categoryId);
                category.Hide();

                await Repository.SaveChangesAsync();
            }
            catch 
            {
                throw;
            }
        }

        /// <see cref="ICatalogCommandsFacade.AddCategoryChild(Guid, Guid)"/>
        public async Task AddCategoryChild(Guid categoryId, Guid childId)
        {
            try
            {
                var category = await Repository.GetByKeyAsync<Category>(categoryId);
                var child = await Repository.GetByKeyAsync<Category>(childId);

                category.AddChild(child);
                await Repository.SaveChangesAsync();

                var @event = new CategoryChildAddedEvent(categoryId, childId);
                EventBus.RaiseEvent(@event);
            }
            catch 
            {
                throw;
            }
        }

        /// <see cref="ICatalogCommandsFacade.ChangeCategoryName(Guid, string)"/>
        public async Task ChangeCategoryName(Guid categoryId, string name)
        {
            try
            {
                var category = await Repository.GetByKeyAsync<Category>(categoryId);
                category.ChangeName(name);

                await Repository.SaveChangesAsync();

                var @event = new CategoryNameChangedEvent(categoryId, name);
                EventBus.RaiseEvent(@event);
            }
            catch 
            {
                throw;
            }
        }

        /// <see cref="ICatalogCommandsFacade.ChangeCategoryCode(Guid, string)"/>
        public async Task ChangeCategoryCode(Guid categoryId, string code)
        {
            try
            {
                var category = await Repository.GetByKeyAsync<Category>(categoryId);
                category.ChangeCode(code);

                await Repository.SaveChangesAsync();

                var @event = new CategoryCodeChangedEvent(categoryId, code);
                EventBus.RaiseEvent(@event);
            }
            catch
            {
                throw;
            }
        }

        /// <see cref="ICatalogCommandsFacade.ChangeCategoryDescription(Guid, string)"/>
        public async Task ChangeCategoryDescription(Guid categoryId, string description)
        {
            try
            {
                var category = await Repository.GetByKeyAsync<Category>(categoryId);
                category.ChangeDescription(description);

                await Repository.SaveChangesAsync();

                var @event = new CategoryDescriptionChangedEvent(categoryId, description);
                EventBus.RaiseEvent(@event);
            }
            catch
            {
                throw;
            }
        }

        /// <see cref="ICatalogCommandsFacade.ChangeCategoryUrl(Guid, string)"/>
        public async Task ChangeCategoryUrl(Guid categoryId, string url)
        {
            try
            {
                var category = await Repository.GetByKeyAsync<Category>(categoryId);
                category.ChangeUrl(url);

                await Repository.SaveChangesAsync();

                var @event = new CategoryUrlChangedEvent(categoryId, url);
                EventBus.RaiseEvent(@event);
            }
            catch
            {
                throw;
            }
        }

        /// <see cref="ICatalogCommandsFacade.SetParentForCategory(Guid, Guid)"/>
        public async Task SetParentForCategory(Guid categoryId, Guid parentId)
        {
            try
            {
                var category = await Repository.GetByKeyAsync<Category>(categoryId);
                var parent = await Repository.GetByKeyAsync<Category>(parentId);
                category.SetParentCategory(parent);

                await Repository.SaveChangesAsync();

                var @event = new CategoryChildAddedEvent(parentId, categoryId);
                EventBus.RaiseEvent(@event);
            }
            catch
            {
                throw;
            }
        }

        /// <see cref="ICatalogCommandsFacade.DeleteCategory(Guid)"/>
        public async Task DeleteCategory(Guid categoryId)
        {
            try
            {
                var category = await Repository.GetByKeyAsync<Category>(categoryId);
                category.Delete();

                await Repository.SaveChangesAsync();

                var @event = new CategoryDeletedEvent(categoryId);
                EventBus.RaiseEvent(@event);
            }
            catch
            {
                throw;
            }
        }

        /// <see cref="ICatalogCommandsFacade.RestoreCategory(Guid)"/>
        public async Task RestoreCategory(Guid categoryId)
        {
            try
            {
                var category = await Repository.GetByKeyAsync<Category>(categoryId);
                category.Restore();

                await Repository.SaveChangesAsync();

                var @event = new CategoryRestoredEvent(categoryId);
                EventBus.RaiseEvent(@event);
            }
            catch
            {
                throw;
            }
        }

        /// <see cref="ICatalogCommandsFacade.RemoveChildForCategory(Guid, Guid)"/>
        public async Task RemoveChildForCategory(Guid categoryId, Guid childId)
        {
            try
            {
                var category = await Repository.GetByKeyAsync<Category>(categoryId);
                category.RemoveChild(childId);

                await Repository.SaveChangesAsync();

                var @event = new CategoryChildRemovedEvent(categoryId, childId);
                EventBus.RaiseEvent(@event);
            }
            catch
            {
                throw;
            }
        }

        /// <see cref="ICatalogCommandsFacade.RemoveParentForCategory(Guid)"/>
        public async Task RemoveParentForCategory(Guid categoryId)
        {
            try
            {
                var category = await Repository.GetByKeyAsync<Category>(categoryId);
                var parentId = category.Parent?.Id;

                category.RemoveParent();

                await Repository.SaveChangesAsync();

                if (parentId != null)
                {
                    var @event = new CategoryChildRemovedEvent((Guid)parentId, categoryId);
                    EventBus.RaiseEvent(@event);
                }
            }
            catch
            {
                throw;
            }
        }

        /// <see cref="ICatalogCommandsFacade.SetCategorySeoData(Guid, SeoData)"/>
        public async Task SetCategorySeoData(Guid categoryId, SeoData seo)
        {
            try
            {
                var category = await Repository.GetByKeyAsync<Category>(categoryId);
                category.SetSeoData(seo);

                await Repository.SaveChangesAsync();
            }
            catch 
            {
                throw;
            }
        }
        #endregion

        #region CustomAttribute Commands
        /// <see cref="ICatalogCommandsFacade.CreateNewCustomAttribute(string, string, string, string, IEnumerable{object})"/>
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

                var @event = new CustomAttributeCreatedEvent(attribute.Id, attribute.Name, attribute.DataType);
                EventBus.RaiseEvent(@event);

                return attribute.Id;
            }
            catch 
            {
                throw;
            }
        }

        /// <see cref="ICatalogCommandsFacade.AddValueForAttribute(Guid, object)"/>
        public async Task AddValueForAttribute(Guid attributeId, object value)
        {
            try
            {
                var attribute = await Repository.GetByKeyAsync<CustomAttribute>(attributeId);
                attribute.AddValue(value);

                await Repository.SaveChangesAsync();

                var @event = new CustomAttributeValueAddedEvent(attributeId, value);
                EventBus.RaiseEvent(@event);
            }
            catch 
            {
                throw;
            }
        }

        /// <see cref="ICatalogCommandsFacade.RemoveValueFromAttribute(Guid, object)"/>
        public async Task RemoveValueFromAttribute(Guid attributeId, object value)
        {
            try
            {
                var attribute = await Repository.GetByKeyAsync<CustomAttribute>(attributeId);
                attribute.RemoveValue(value);

                await Repository.SaveChangesAsync();

                var @event = new CustomAttributeValueRemovedEvent(attributeId, value);
                EventBus.RaiseEvent(@event);
            }
            catch
            {
                throw;
            }
        }

        /// <see cref="ICatalogCommandsFacade.ChangeAttributeName(Guid, string)"/>
        public async Task ChangeAttributeName(Guid attributeId, string name)
        {
            try
            {
                var attribute = await Repository.GetByKeyAsync<CustomAttribute>(attributeId);
                attribute.ChangeName(name);

                await Repository.SaveChangesAsync();

                var @event = new CustomAttributeNameChangedEvent(attributeId, name);
                EventBus.RaiseEvent(@event);
            }
            catch
            {
                throw;
            }
        }

        /// <see cref="ICatalogCommandsFacade.ChangeAttributeDescription(Guid, string)"/>
        public async Task ChangeAttributeDescription(Guid attributeId, string description)
        {
            try
            {
                var attribute = await Repository.GetByKeyAsync<CustomAttribute>(attributeId);
                attribute.ChangeDescription(description);

                await Repository.SaveChangesAsync();

                var @event = new CustomAttributeDescriptionChangedEvent(attributeId, description);
                EventBus.RaiseEvent(@event);
            }
            catch
            {
                throw;
            }
        }

        /// <see cref="ICatalogCommandsFacade.SetAttributeUnitOfMeasure(Guid, string)"/>
        public async Task SetAttributeUnitOfMeasure(Guid attributeId, string unitOfMeasure)
        {
            try
            {
                var attribute = await Repository.GetByKeyAsync<CustomAttribute>(attributeId);
                attribute.SetUnitOfMeasure(unitOfMeasure);

                await Repository.SaveChangesAsync();

                var @event = new CustomAttributeUnitOfMeasureSetEvent(attributeId, unitOfMeasure);
                EventBus.RaiseEvent(@event);
            }
            catch
            {
                throw;
            }
        }

        /// <see cref="ICatalogCommandsFacade.ChangeAttributeDataType(Guid, string)"/>
        public async Task ChangeAttributeDataType(Guid attributeId, string dataType)
        {
            try
            {
                var attribute = await Repository.GetByKeyAsync<CustomAttribute>(attributeId);
                attribute.ChangeDataType(dataType);

                await Repository.SaveChangesAsync();

                var @event = new CustomAttributeDataTypeChangedEvent(attributeId, dataType);
                EventBus.RaiseEvent(@event);
            }
            catch
            {
                throw;
            }
        }

        /// <see cref="ICatalogCommandsFacade.DeleteCustomAttribute(Guid)"/>
        public async Task DeleteCustomAttribute(Guid attributeId)
        {
            try
            {
                var attribute = await Repository.GetByKeyAsync<CustomAttribute>(attributeId);
                attribute.Delete();

                await Repository.SaveChangesAsync();

                var @event = new CustomAttributeDeletedEvent(attributeId);
                EventBus.RaiseEvent(@event);
            }
            catch
            {
                throw;
            }
        }

        /// <see cref="ICatalogCommandsFacade.RestoreCustomAttribute(Guid)"/>
        public async Task RestoreCustomAttribute(Guid attributeId)
        {
            try
            {
                var attribute = await Repository.GetByKeyAsync<CustomAttribute>(attributeId);
                attribute.Restore();

                await Repository.SaveChangesAsync();

                var @event = new CustomAttributeRestoredEvent(attributeId);
                EventBus.RaiseEvent(@event);
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region Product Commands
        /// <see cref="ICatalogCommandsFacade.CreateNewProduct(string, string, string, string, Currency, string, int, bool, DateTime?, DateTime?)"/>
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

                var @event = new ProductCreatedEvent(product.Id, product.EanCode, product.Sku, product.Name);
                EventBus.RaiseEvent(@event);

                return product.Id;
            }
            catch 
            {
                throw;
            }
        }

        /// <see cref="ICatalogCommandsFacade.DeleteProduct(Guid)"/>
        public async Task DeleteProduct(Guid productId)
        {
            try
            {
                var product = await Repository.GetByKeyAsync<Product>(productId);
                product.Delete();

                await Repository.SaveChangesAsync();

                var @event = new ProductDeletedEvent(productId);
                EventBus.RaiseEvent(@event);
            }
            catch 
            {
                throw;
            }
        }

        /// <see cref="ICatalogCommandsFacade.RestoreProduct(Guid)"/>
        public async Task RestoreProduct(Guid productId)
        {
            try
            {
                var product = await Repository.GetByKeyAsync<Product>(productId);
                product.Restore();

                await Repository.SaveChangesAsync();

                var @event = new ProductRestoredEvent(productId);
                EventBus.RaiseEvent(@event);
            }
            catch
            {
                throw;
            }
        }

        /// <see cref="ICatalogCommandsFacade.SetUnitInStockForProduct(Guid, int)"/>
        public async Task SetUnitInStockForProduct(Guid productId, int unitInStock)
        {
            try
            {
                var product = await Repository.GetByKeyAsync<Product>(productId);
                product.SetUnitInStock(unitInStock);

                await Repository.SaveChangesAsync();

                var @event = new ProductUnitInStockChangedEvent(productId, product.UnitInStock);
                EventBus.RaiseEvent(@event);
            }
            catch
            {
                throw;
            }
        }

        /// <see cref="ICatalogCommandsFacade.AddUnitInStockToProduct(Guid, int)"/>
        public async Task AddUnitInStockToProduct(Guid productId, int unitToAdd)
        {
            try
            {
                var product = await Repository.GetByKeyAsync<Product>(productId);
                product.AddUnitInStock(unitToAdd);

                await Repository.SaveChangesAsync();

                var @event = new ProductUnitInStockChangedEvent(productId, product.UnitInStock);
                EventBus.RaiseEvent(@event);
            }
            catch
            {
                throw;
            }
        }

        /// <see cref="ICatalogCommandsFacade.RemoveUnitInStockFromProduct(Guid, int)"/>
        public async Task RemoveUnitInStockFromProduct(Guid productId, int unitToRemove)
        {
            try
            {
                var product = await Repository.GetByKeyAsync<Product>(productId);
                product.RemoveUnitFromStock(unitToRemove);

                await Repository.SaveChangesAsync();

                var @event = new ProductUnitInStockChangedEvent(productId, product.UnitInStock);
                EventBus.RaiseEvent(@event);
            }
            catch
            {
                throw;
            }
        }

        /// <see cref="ICatalogCommandsFacade.ChangeProductEanCode(Guid, string)"/>
        public async Task ChangeProductEanCode(Guid productId, string ean)
        {
            try
            {
                var product = await Repository.GetByKeyAsync<Product>(productId);
                product.ChangeEanCode(ean);

                await Repository.SaveChangesAsync();

                var @event = new ProductEanCodeChangedEvent(productId, ean);
                EventBus.RaiseEvent(@event);
            }
            catch
            {
                throw;
            }
        }

        /// <see cref="ICatalogCommandsFacade.ChangeProductSku(Guid, string)"/>
        public async Task ChangeProductSku(Guid productId, string sku)
        {
            try
            {
                var product = await Repository.GetByKeyAsync<Product>(productId);
                product.ChangeSku(sku);

                await Repository.SaveChangesAsync();

                var @event = new ProductSkuChangedEvent(productId, sku);
                EventBus.RaiseEvent(@event);
            }
            catch
            {
                throw;
            }
        }

        /// <see cref="ICatalogCommandsFacade.ChangeProductName(Guid, string)"/>
        public async Task ChangeProductName(Guid productId, string name)
        {
            try
            {
                var product = await Repository.GetByKeyAsync<Product>(productId);
                product.ChangeName(name);

                await Repository.SaveChangesAsync();

                var @event = new ProductNameChangedEvent(productId, name);
                EventBus.RaiseEvent(@event);
            }
            catch
            {
                throw;
            }
        }

        /// <see cref="ICatalogCommandsFacade.ChangeProductDescription(Guid, string)"/>
        public async Task ChangeProductDescription(Guid productId, string description)
        {
            try
            {
                var product = await Repository.GetByKeyAsync<Product>(productId);
                product.ChangeDescription(description);

                await Repository.SaveChangesAsync();

                var @event = new ProductDescriptionChangedEvent(productId, description);
                EventBus.RaiseEvent(@event);
            }
            catch
            {
                throw;
            }
        }

        /// <see cref="ICatalogCommandsFacade.ChangeProductUrl(Guid, string)"/>
        public async Task ChangeProductUrl(Guid productId, string url)
        {
            try
            {
                var product = await Repository.GetByKeyAsync<Product>(productId);
                product.ChangeUrl(url);

                await Repository.SaveChangesAsync();

                var @event = new ProductUrlChangedEvent(productId, url);
                EventBus.RaiseEvent(@event);
            }
            catch
            {
                throw;
            }
        }

        /// <see cref="ICatalogCommandsFacade.SetProductPrice(Guid, Currency)"/>
        public async Task SetProductPrice(Guid productId, Currency price)
        {
            try
            {
                var product = await Repository.GetByKeyAsync<Product>(productId);
                product.SetPrice(price);

                await Repository.SaveChangesAsync();

                var @event = new ProductPriceSetEvent(productId, price);
                EventBus.RaiseEvent(@event);
            }
            catch
            {
                throw;
            }
        }

        /// <see cref="ICatalogCommandsFacade.SetProductOnSale(Guid, DateTime?, DateTime?)"/>
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

                var @event = new ProductSetOnSaleEvent(productId);
                EventBus.RaiseEvent(@event);
            }
            catch
            {
                throw;
            }
        }

        /// <see cref="ICatalogCommandsFacade.RemoveProductFromSale(Guid)"/>
        public async Task RemoveProductFromSale(Guid productId)
        {
            try
            {
                var product = await Repository.GetByKeyAsync<Product>(productId);
                product.RemoveFromSale();

                await Repository.SaveChangesAsync();

                var @event = new ProductRemovedFromSaleEvent(productId);
                EventBus.RaiseEvent(@event);
            }
            catch
            {
                throw;
            }
        }

        /// <see cref="ICatalogCommandsFacade.SetProductVendor(Guid, Guid)"/>
        public async Task SetProductVendor(Guid productId, Guid brandId)
        {
            try
            {
                var product = await Repository.GetByKeyAsync<Product>(productId);
                var brand = await Repository.GetByKeyAsync<Brand>(brandId);

                product.SetVendor(brand);

                await Repository.SaveChangesAsync();

                var @event = new ProductVendorSetEvent(productId, brandId);
                EventBus.RaiseEvent(@event);
            }
            catch
            {
                throw;
            }
        }

        /// <see cref="ICatalogCommandsFacade.AddCategoryToProduct(Guid, Guid)"/>
        public async Task AddCategoryToProduct(Guid productId, Guid categoryId)
        {
            try
            {
                var product = await Repository.GetByKeyAsync<Product>(productId);
                var category = await Repository.GetByKeyAsync<Category>(categoryId);

                product.AddCategory(category);

                await Repository.SaveChangesAsync();

                var @event = new ProductCategoryAddedEvent(productId, categoryId, false);
                EventBus.RaiseEvent(@event);
            }
            catch
            {
                throw;
            }
        }

        /// <see cref="ICatalogCommandsFacade.AddMainCategoryToProduct(Guid, Guid)"/>
        public async Task AddMainCategoryToProduct(Guid productId, Guid categoryId)
        {
            try
            {
                var product = await Repository.GetByKeyAsync<Product>(productId);
                var category = await Repository.GetByKeyAsync<Category>(categoryId);

                product.AddMainCategory(category);

                await Repository.SaveChangesAsync();

                var @event = new ProductCategoryAddedEvent(productId, categoryId, true);
                EventBus.RaiseEvent(@event);
            }
            catch
            {
                throw;
            }
        }

        /// <see cref="ICatalogCommandsFacade.AddProductVariant(Guid, string, string, string, Currency)"/>
        public async Task AddProductVariant(Guid productId, string name, string ean, string sku, Currency price)
        {
            try
            {
                var product = await Repository.GetByKeyAsync<Product>(productId);
                product.AddVariant(name, ean, sku, price);

                await Repository.SaveChangesAsync();

                var @event = new ProductVariantAddedEvent(productId, name, ean, sku);
                EventBus.RaiseEvent(@event);
            }
            catch
            {
                throw;
            }
        }

        /// <see cref="ICatalogCommandsFacade.RemoveProductVariant(Guid, Guid)"/>
        public async Task RemoveProductVariant(Guid productId, Guid variantId)
        {
            try
            {
                var product = await Repository.GetByKeyAsync<Product>(productId);
                product.RemoveVariant(variantId);

                await Repository.SaveChangesAsync();

                var @event = new ProductVariantRemovedEvent(productId, variantId);
                EventBus.RaiseEvent(@event);
            }
            catch
            {
                throw;
            }
        }

        /// <see cref="ICatalogCommandsFacade.AddAttributeToProduct(Guid, Guid, object)"/>
        public async Task AddAttributeToProduct(Guid productId, Guid attributeId, object value)
        {
            try
            {
                var product = await Repository.GetByKeyAsync<Product>(productId);
                var attribute = await Repository.GetByKeyAsync<CustomAttribute>(attributeId);

                product.AddAttribute(attribute, value);

                await Repository.SaveChangesAsync();

                var @event = new ProductAttributeAddedEvent(productId, attributeId, value);
                EventBus.RaiseEvent(@event);
            }
            catch
            {
                throw;
            }
        }

        /// <see cref="ICatalogCommandsFacade.RemoveProductAttribute(Guid, Guid)"/>
        public async Task RemoveProductAttribute(Guid productId, Guid attributeId)
        {
            try
            {
                var product = await Repository.GetByKeyAsync<Product>(productId);
                product.DeleteAttribute(attributeId);

                await Repository.SaveChangesAsync();

                var @event = new ProductAttributeRemovedEvent(productId, attributeId);
                EventBus.RaiseEvent(@event);
            }
            catch
            {
                throw;
            }
        }

        /// <see cref="ICatalogCommandsFacade.AddProductTierPrice(Guid, int, int, Currency)"/>
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

                var @event = new ProductTierPriceAddedEvent(productId, fromQuantity, toQuantity, price);
                EventBus.RaiseEvent(@event);
            }
            catch
            {
                throw;
            }
        }

        /// <see cref="ICatalogCommandsFacade.RemoveTierPriceFromProduct(Guid, Guid)"/>
        public async Task RemoveTierPriceFromProduct(Guid productId, Guid tierPriceId)
        {
            try
            {
                var product = await Repository.GetByKeyAsync<Product>(productId);
                product.DeleteTierPrice(tierPriceId);

                await Repository.SaveChangesAsync();

                var @event = new ProductTierPriceRemovedEvent(productId, tierPriceId);
                EventBus.RaiseEvent(@event);
            }
            catch
            {
                throw;
            }
        }

        /// <see cref="ICatalogCommandsFacade.AddProductReview(Guid, string, int, string)"/>
        public async Task AddProductReview(Guid productId, string name, int rating, string comment)
        {
            try
            {
                var product = await Repository.GetByKeyAsync<Product>(productId);
                product.AddReview(name, rating, comment);

                await Repository.SaveChangesAsync();

                var @event = new ProductReviewAddedEvent(productId, name, rating, comment);
                EventBus.RaiseEvent(@event);
            }
            catch
            {
                throw;
            }
        }

        /// <see cref="ICatalogCommandsFacade.ApproveProductReview(Guid, Guid)"/>
        public async Task ApproveProductReview(Guid productId, Guid reviewId)
        {
            try
            {
                var product = await Repository.GetByKeyAsync<Product>(productId);
                product.ApproveReview(reviewId);

                await Repository.SaveChangesAsync();

                var @event = new ProductReviewApprovedEvent(productId, reviewId);
                EventBus.RaiseEvent(@event);
            }
            catch
            {
                throw;
            }
        }

        /// <see cref="ICatalogCommandsFacade.RemoveProductReview(Guid, Guid)"/>
        public async Task RemoveProductReview(Guid productId, Guid reviewId)
        {
            try
            {
                var product = await Repository.GetByKeyAsync<Product>(productId);
                product.RemoveReviewApproval(reviewId);
                product.DeleteReview(reviewId);

                await Repository.SaveChangesAsync();

                var @event = new ProductReviewRemovedEvent(productId, reviewId);
                EventBus.RaiseEvent(@event);
            }
            catch
            {
                throw;
            }
        }

        /// <see cref="ICatalogCommandsFacade.AddProductImage(Guid, string, string, string, bool, DateTime)"/>
        public async Task AddProductImage(Guid productId, string path, string name, string originalName, bool isMain, DateTime uploadedOn)
        {
            try
            {
                var product = await Repository.GetByKeyAsync<Product>(productId);
                product.AddImage(path, name, originalName, isMain, uploadedOn);

                await Repository.SaveChangesAsync();

                var @event = new ProductImageAddedEvent(productId, name, originalName);
                EventBus.RaiseEvent(@event);
            }
            catch
            {
                throw;
            }
        }

        /// <see cref="ICatalogCommandsFacade.RemoveProductImage(Guid, Guid)"/>
        public async Task RemoveProductImage(Guid productId, Guid imageId)
        {
            try
            {
                var product = await Repository.GetByKeyAsync<Product>(productId);
                product.DeleteImage(imageId);

                await Repository.SaveChangesAsync();

                var @event = new ProductImageRemovedEvent(productId, imageId);
                EventBus.RaiseEvent(@event);
            }
            catch
            {
                throw;
            }
        }

        /// <see cref="ICatalogCommandsFacade.SetProductSeo(Guid, SeoData)"/>
        public async Task SetProductSeo(Guid productId, SeoData seo)
        {
            try
            {
                var product = await Repository.GetByKeyAsync<Product>(productId);
                product.SetSeoData(seo);

                await Repository.SaveChangesAsync();
            }
            catch 
            {
                throw;
            }
        }

        /// <see cref="ICatalogCommandsFacade.ChangeProductTierPrice(Guid, Guid, int, int, Currency)"/>
        public async Task ChangeProductTierPrice(Guid productId, Guid tierPriceId, int fromQuantity, int toQuantity, Currency price)
        {
            try
            {
                var product = await Repository.GetByKeyAsync<Product>(productId);
                product.ChangeTierPrice(tierPriceId, fromQuantity, toQuantity, price);

                await Repository.SaveChangesAsync();

                var @event = new ProductTierPriceChangedEvent(productId, tierPriceId, fromQuantity, toQuantity, price);
                EventBus.RaiseEvent(@event);
            }
            catch 
            {
                throw;
            }
        }

        #endregion

        #region CatalogSettings Commands
        /// <see cref="ICatalogCommandsFacade.CreateCatalogSettings(int, int, CatalogSettings.ViewType, CatalogSettings.ViewType, bool, bool, int)"/>
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

        /// <see cref="ICatalogCommandsFacade.ShowPrices(Guid, bool)"/>
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

        /// <see cref="ICatalogCommandsFacade.AllowProductReviews(Guid, bool)"/>
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

        /// <see cref="ICatalogCommandsFacade.SetProductReviewsPerPage(Guid, int)"/>
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

        /// <see cref="ICatalogCommandsFacade.SetCategoriesView(Guid, CatalogSettings.ViewType, int)"/>
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

        /// <see cref="ICatalogCommandsFacade.SetProductsView(Guid, CatalogSettings.ViewType, int)"/>
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
