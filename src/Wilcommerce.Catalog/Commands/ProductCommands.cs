using System;
using System.Threading.Tasks;
using Wilcommerce.Catalog.Events.Product;
using Wilcommerce.Catalog.Models;
using Wilcommerce.Core.Common.Models;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Commands
{
    /// <summary>
    /// Implementation of <see cref="IProductCommands"/>
    /// </summary>
    public class ProductCommands : IProductCommands
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
        /// Construct the product commands
        /// </summary>
        /// <param name="repository">The repository</param>
        /// <param name="eventBus">The event bus</param>
        public ProductCommands(Repository.IRepository repository, IEventBus eventBus)
        {
            Repository = repository ?? throw new ArgumentNullException(nameof(repository));
            EventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }

        #region Product Commands
        /// <summary>
        /// Implementation of <see cref="IProductCommands.CreateNewProduct(string, string, string, string, Currency, string, int, bool, DateTime?, DateTime?, string)"/>
        /// </summary>
        /// <param name="ean">The EAN code</param>
        /// <param name="sku">The SKU code</param>
        /// <param name="name">The product's name</param>
        /// <param name="url">The product's url</param>
        /// <param name="price">The product's price</param>
        /// <param name="description">The product's description</param>
        /// <param name="unitInStock">The product's unit in stock</param>
        /// <param name="isOnSale">Whether the product is on sale</param>
        /// <param name="onSaleFrom">The date and time of when the product starts to be on sale</param>
        /// <param name="onSaleTo">The date and time till when the product is on sale</param>
        /// <param name="userId">The user's id</param>
        /// <returns>The product id</returns>
        public virtual async Task<Guid> CreateNewProduct(string ean, string sku, string name, string url, Currency price, string description, int unitInStock, bool isOnSale, DateTime? onSaleFrom, DateTime? onSaleTo, string userId)
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
                    if (!onSaleFrom.HasValue && !onSaleTo.HasValue)
                    {
                        product.SetOnSale();
                    }
                    else
                    {
                        product.SetOnSale(onSaleFrom, onSaleTo);
                    }
                }

                Repository.Add(product);
                await Repository.SaveChangesAsync();

                var @event = new ProductCreatedEvent(product.Id, product.EanCode, product.Sku, product.Name, userId);
                EventBus.RaiseEvent(@event);

                return product.Id;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Implementation of <see cref="IProductCommands.UpdateProductInfo(Guid, string, string, string, string, Currency, string, int, bool, DateTime?, DateTime?, string)"/>
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="ean">The EAN code</param>
        /// <param name="sku">The SKU code</param>
        /// <param name="name">The product's name</param>
        /// <param name="url">The product's url</param>
        /// <param name="price">The product's price</param>
        /// <param name="description">The product's description</param>
        /// <param name="unitInStock">The product's unit in stock</param>
        /// <param name="isOnSale">Whether the product is on sale</param>
        /// <param name="onSaleFrom">The date and time of when the product starts to be on sale</param>
        /// <param name="onSaleTo">The date and time till when the product is on sale</param>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public virtual async Task UpdateProductInfo(Guid productId, string ean, string sku, string name, string url, Currency price, string description, int unitInStock, bool isOnSale, DateTime? onSaleFrom, DateTime? onSaleTo, string userId)
        {
            try
            {
                if (productId == Guid.Empty)
                {
                    throw new ArgumentException("value cannot be empty", nameof(productId));
                }

                var product = await Repository.GetByKeyAsync<Product>(productId);
                if (product.EanCode != ean)
                {
                    product.ChangeEanCode(ean);
                }

                if (product.Sku != sku)
                {
                    product.ChangeSku(sku);
                }

                if (product.Name != name)
                {
                    product.ChangeName(name);
                }

                if (product.Url != url)
                {
                    product.ChangeUrl(url);
                }

                if (product.Price != price)
                {
                    product.SetPrice(price);
                }

                if (product.Description != description)
                {
                    product.ChangeDescription(description);
                }

                if (product.UnitInStock != unitInStock)
                {
                    product.SetUnitInStock(unitInStock);
                }

                if (product.IsOnSale != isOnSale || product.OnSaleFrom != onSaleFrom || product.OnSaleTo != onSaleTo)
                {
                    if (isOnSale)
                    {
                        if (!onSaleFrom.HasValue && !onSaleTo.HasValue)
                        {
                            product.SetOnSale();
                        }
                        else
                        {
                            product.SetOnSale(onSaleFrom, onSaleTo);
                        }
                    }
                    else
                    {
                        product.RemoveFromSale();
                    }
                }

                await Repository.SaveChangesAsync();

                var @event = new ProductInfoUpdateEvent(productId, ean, sku, name, url, price, description, unitInStock, isOnSale, onSaleFrom, onSaleTo, userId);
                EventBus.RaiseEvent(@event);
            }
            catch 
            {
                throw;
            }
        }

        /// <summary>
        /// Implementation of <see cref="IProductCommands.DeleteProduct(Guid, string)"/>
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public virtual async Task DeleteProduct(Guid productId, string userId)
        {
            try
            {
                if (productId == Guid.Empty)
                {
                    throw new ArgumentException("value cannot be empty", nameof(productId));
                }

                var product = await Repository.GetByKeyAsync<Product>(productId);
                product.Delete();

                await Repository.SaveChangesAsync();

                var @event = new ProductDeletedEvent(productId, userId);
                EventBus.RaiseEvent(@event);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Implementation of <see cref="IProductCommands.RestoreProduct(Guid, string)"/>
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public virtual async Task RestoreProduct(Guid productId, string userId)
        {
            try
            {
                if (productId == Guid.Empty)
                {
                    throw new ArgumentException("value cannot be empty", nameof(productId));
                }

                var product = await Repository.GetByKeyAsync<Product>(productId);
                product.Restore();

                await Repository.SaveChangesAsync();

                var @event = new ProductRestoredEvent(productId, userId);
                EventBus.RaiseEvent(@event);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Implementation of <see cref="IProductCommands.SetProductVendor(Guid, Guid, string)"/>
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="brandId">The vendor id</param>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public virtual async Task SetProductVendor(Guid productId, Guid brandId, string userId)
        {
            try
            {
                if (productId == Guid.Empty)
                {
                    throw new ArgumentException("value cannot be empty", nameof(productId));
                }

                if (brandId == Guid.Empty)
                {
                    throw new ArgumentException("value cannot be empty", nameof(brandId));
                }

                var product = await Repository.GetByKeyAsync<Product>(productId);
                var brand = await Repository.GetByKeyAsync<Brand>(brandId);

                product.SetVendor(brand);

                await Repository.SaveChangesAsync();

                var @event = new ProductVendorSetEvent(productId, brandId, userId);
                EventBus.RaiseEvent(@event);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Implementation of <see cref="IProductCommands.AddCategoryToProduct(Guid, Guid, string)"/>
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="categoryId">The category id</param>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public virtual async Task AddCategoryToProduct(Guid productId, Guid categoryId, string userId)
        {
            try
            {
                if (productId == Guid.Empty)
                {
                    throw new ArgumentException("value cannot be empty", nameof(productId));
                }

                if (categoryId == Guid.Empty)
                {
                    throw new ArgumentException("value cannot be empty", nameof(categoryId));
                }

                var product = await Repository.GetByKeyAsync<Product>(productId);
                var category = await Repository.GetByKeyAsync<Category>(categoryId);

                product.AddCategory(category);

                await Repository.SaveChangesAsync();

                var @event = new ProductCategoryAddedEvent(productId, categoryId, false, userId);
                EventBus.RaiseEvent(@event);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Implementation of <see cref="IProductCommands.AddMainCategoryToProduct(Guid, Guid, string)"/>
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="categoryId">The category id</param>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public virtual async Task AddMainCategoryToProduct(Guid productId, Guid categoryId, string userId)
        {
            try
            {
                if (productId == Guid.Empty)
                {
                    throw new ArgumentException("value cannot be empty", nameof(productId));
                }

                if (categoryId == Guid.Empty)
                {
                    throw new ArgumentException("value cannot be empty", nameof(categoryId));
                }

                var product = await Repository.GetByKeyAsync<Product>(productId);
                var category = await Repository.GetByKeyAsync<Category>(categoryId);

                product.AddMainCategory(category);

                await Repository.SaveChangesAsync();

                var @event = new ProductCategoryAddedEvent(productId, categoryId, true, userId);
                EventBus.RaiseEvent(@event);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Implementation of <see cref="IProductCommands.AddProductVariant(Guid, string, string, string, Currency, string)"/>
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="name">The variant name</param>
        /// <param name="ean">The variant EAN code</param>
        /// <param name="sku">The variant SKU code</param>
        /// <param name="price">The variant price</param>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public virtual async Task AddProductVariant(Guid productId, string name, string ean, string sku, Currency price, string userId)
        {
            try
            {
                if (productId == Guid.Empty)
                {
                    throw new ArgumentException("value cannot be empty", nameof(productId));
                }

                var product = await Repository.GetByKeyAsync<Product>(productId);
                product.AddVariant(name, ean, sku, price);

                await Repository.SaveChangesAsync();

                var @event = new ProductVariantAddedEvent(productId, name, ean, sku, userId);
                EventBus.RaiseEvent(@event);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Implementation of <see cref="IProductCommands.RemoveProductVariant(Guid, Guid, string)"/>
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="variantId">The variant id</param>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public virtual async Task RemoveProductVariant(Guid productId, Guid variantId, string userId)
        {
            try
            {
                if (productId == Guid.Empty)
                {
                    throw new ArgumentException("value cannot be null", nameof(productId));
                }

                if (variantId == Guid.Empty)
                {
                    throw new ArgumentException("value cannot be empty", nameof(variantId));
                }

                var product = await Repository.GetByKeyAsync<Product>(productId);
                product.RemoveVariant(variantId);

                await Repository.SaveChangesAsync();

                var @event = new ProductVariantRemovedEvent(productId, variantId, userId);
                EventBus.RaiseEvent(@event);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Implementation of <see cref="IProductCommands.AddAttributeToProduct(Guid, Guid, object, string)"/>
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="attributeId">The attribute id</param>
        /// <param name="value">The attribute value</param>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public virtual async Task AddAttributeToProduct(Guid productId, Guid attributeId, object value, string userId)
        {
            try
            {
                if (productId == Guid.Empty)
                {
                    throw new ArgumentException("value cannot be empty", nameof(productId));
                }

                if (attributeId == Guid.Empty)
                {
                    throw new ArgumentException("value cannot be empty", nameof(attributeId));
                }

                var product = await Repository.GetByKeyAsync<Product>(productId);
                var attribute = await Repository.GetByKeyAsync<CustomAttribute>(attributeId);

                product.AddAttribute(attribute, value);

                await Repository.SaveChangesAsync();

                var @event = new ProductAttributeAddedEvent(productId, attributeId, value, userId);
                EventBus.RaiseEvent(@event);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Implementation of <see cref="IProductCommands.RemoveProductAttribute(Guid, Guid, string)"/>
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="attributeId">The attribute id</param>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public virtual async Task RemoveProductAttribute(Guid productId, Guid attributeId, string userId)
        {
            try
            {
                if (productId == Guid.Empty)
                {
                    throw new ArgumentException("value cannot be empty", nameof(productId));
                }

                if (attributeId == Guid.Empty)
                {
                    throw new ArgumentException("value cannot be empty", nameof(attributeId));
                }

                var product = await Repository.GetByKeyAsync<Product>(productId);
                var attribute = await Repository.GetByKeyAsync<CustomAttribute>(attributeId);

                product.DeleteAttribute(attribute);

                await Repository.SaveChangesAsync();

                var @event = new ProductAttributeRemovedEvent(productId, attributeId, userId);
                EventBus.RaiseEvent(@event);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Implementation of <see cref="IProductCommands.AddProductTierPrice(Guid, int, int, Currency, string)"/>
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="fromQuantity">The start quantity</param>
        /// <param name="toQuantity">The end quantity</param>
        /// <param name="price">The price value</param>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public virtual async Task AddProductTierPrice(Guid productId, int fromQuantity, int toQuantity, Currency price, string userId)
        {
            try
            {
                if (productId == Guid.Empty)
                {
                    throw new ArgumentException("value cannot be empty", nameof(productId));
                }

                var product = await Repository.GetByKeyAsync<Product>(productId);
                if (!product.TierPriceEnabled)
                {
                    product.EnableTierPrices();
                }

                product.AddTierPrice(fromQuantity, toQuantity, price);

                await Repository.SaveChangesAsync();

                var @event = new ProductTierPriceAddedEvent(productId, fromQuantity, toQuantity, price, userId);
                EventBus.RaiseEvent(@event);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Implementation of <see cref="IProductCommands.ChangeProductTierPrice(Guid, Guid, int, int, Currency, string)"/>
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="tierPriceId">The tier price id</param>
        /// <param name="fromQuantity">The new starting quantity</param>
        /// <param name="toQuantity">The new ending quantity</param>
        /// <param name="price">The new price</param>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public virtual async Task ChangeProductTierPrice(Guid productId, Guid tierPriceId, int fromQuantity, int toQuantity, Currency price, string userId)
        {
            try
            {
                if (productId == Guid.Empty)
                {
                    throw new ArgumentException("value cannot be empty", nameof(productId));
                }

                if (tierPriceId == Guid.Empty)
                {
                    throw new ArgumentException("value cannot be empty", nameof(tierPriceId));
                }

                var product = await Repository.GetByKeyAsync<Product>(productId);
                product.ChangeTierPrice(tierPriceId, fromQuantity, toQuantity, price);

                await Repository.SaveChangesAsync();

                var @event = new ProductTierPriceChangedEvent(productId, tierPriceId, fromQuantity, toQuantity, price, userId);
                EventBus.RaiseEvent(@event);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Implementation of <see cref="IProductCommands.RemoveTierPriceFromProduct(Guid, Guid, string)"/>
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="tierPriceId">The tier price id</param>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public virtual async Task RemoveTierPriceFromProduct(Guid productId, Guid tierPriceId, string userId)
        {
            try
            {
                if (productId == Guid.Empty)
                {
                    throw new ArgumentException("value cannot be empty", nameof(productId));
                }

                if (tierPriceId == Guid.Empty)
                {
                    throw new ArgumentException("value cannot be empty", nameof(tierPriceId));
                }

                var product = await Repository.GetByKeyAsync<Product>(productId);
                product.DeleteTierPrice(tierPriceId);

                await Repository.SaveChangesAsync();

                var @event = new ProductTierPriceRemovedEvent(productId, tierPriceId, userId);
                EventBus.RaiseEvent(@event);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Implementation of <see cref="IProductCommands.AddProductReview(Guid, string, int, string, string)"/>
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="name">The name of the user who leave the review</param>
        /// <param name="rating">The rate given</param>
        /// <param name="comment">The comment given</param>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public virtual async Task AddProductReview(Guid productId, string name, int rating, string comment, string userId)
        {
            try
            {
                if (productId == Guid.Empty)
                {
                    throw new ArgumentException("value cannot be empty", nameof(productId));
                }

                var product = await Repository.GetByKeyAsync<Product>(productId);
                product.AddReview(name, rating, comment);

                await Repository.SaveChangesAsync();

                var @event = new ProductReviewAddedEvent(productId, name, rating, comment, userId);
                EventBus.RaiseEvent(@event);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Implementation of <see cref="IProductCommands.ApproveProductReview(Guid, Guid, string)"/>
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="reviewId">The review id</param>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public virtual async Task ApproveProductReview(Guid productId, Guid reviewId, string userId)
        {
            try
            {
                if (productId == Guid.Empty)
                {
                    throw new ArgumentException("value cannot be empty", nameof(productId));
                }

                if (reviewId == Guid.Empty)
                {
                    throw new ArgumentException("value cannot be empty", nameof(reviewId));
                }

                var product = await Repository.GetByKeyAsync<Product>(productId);
                product.ApproveReview(reviewId);

                await Repository.SaveChangesAsync();

                var @event = new ProductReviewApprovedEvent(productId, reviewId, userId);
                EventBus.RaiseEvent(@event);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Implementation of <see cref="IProductCommands.RemoveProductReview(Guid, Guid, string)"/>
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="reviewId">The review id</param>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public virtual async Task RemoveProductReview(Guid productId, Guid reviewId, string userId)
        {
            try
            {
                if (productId == Guid.Empty)
                {
                    throw new ArgumentException("value cannot be empty", nameof(productId));
                }

                if (reviewId == Guid.Empty)
                {
                    throw new ArgumentException("value cannot be empty", nameof(reviewId));
                }

                var product = await Repository.GetByKeyAsync<Product>(productId);
                product.RemoveReviewApproval(reviewId);
                product.DeleteReview(reviewId);

                await Repository.SaveChangesAsync();

                var @event = new ProductReviewRemovedEvent(productId, reviewId, userId);
                EventBus.RaiseEvent(@event);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Implementation of <see cref="IProductCommands.AddProductImage(Guid, string, string, string, bool, DateTime, string)"/>
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="path">The file path</param>
        /// <param name="name">The file name</param>
        /// <param name="originalName">The file original name</param>
        /// <param name="isMain">Whether is the main image for the product</param>
        /// <param name="uploadedOn">The date and time of when the image is uploaded</param>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public virtual async Task AddProductImage(Guid productId, string path, string name, string originalName, bool isMain, DateTime uploadedOn, string userId)
        {
            try
            {
                if (productId == Guid.Empty)
                {
                    throw new ArgumentException("value cannot be empty", nameof(productId));
                }

                var product = await Repository.GetByKeyAsync<Product>(productId);
                product.AddImage(path, name, originalName, isMain, uploadedOn);

                await Repository.SaveChangesAsync();

                var @event = new ProductImageAddedEvent(productId, name, originalName, userId);
                EventBus.RaiseEvent(@event);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Implementation of <see cref="IProductCommands.RemoveProductImage(Guid, Guid, string)"/>
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="imageId">The image id</param>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public virtual async Task RemoveProductImage(Guid productId, Guid imageId, string userId)
        {
            try
            {
                if (productId == Guid.Empty)
                {
                    throw new ArgumentException("value cannot be empty", nameof(productId));
                }

                if (imageId == Guid.Empty)
                {
                    throw new ArgumentException("value cannot be empty", nameof(imageId));
                }

                var product = await Repository.GetByKeyAsync<Product>(productId);
                product.DeleteImage(imageId);

                await Repository.SaveChangesAsync();

                var @event = new ProductImageRemovedEvent(productId, imageId, userId);
                EventBus.RaiseEvent(@event);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Implementation of <see cref="IProductCommands.SetProductSeo(Guid, SeoData, string)"/>
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="seo">The SEO information</param>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public virtual async Task SetProductSeo(Guid productId, SeoData seo, string userId)
        {
            try
            {
                if (productId == Guid.Empty)
                {
                    throw new ArgumentException("value cannot be empty", nameof(productId));
                }

                var product = await Repository.GetByKeyAsync<Product>(productId);
                product.SetSeoData(seo);

                await Repository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Implementation of <see cref="IProductCommands.ChangeProductVariant(Guid, Guid, string, string, string, Currency, string)"/>
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="variantId">The variant id</param>
        /// <param name="name">The variant name</param>
        /// <param name="ean">The variant EAN code</param>
        /// <param name="sku">The variant SKU code</param>
        /// <param name="price">The variant price</param>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public async Task ChangeProductVariant(Guid productId, Guid variantId, string name, string ean, string sku, Currency price, string userId)
        {
            if (productId == Guid.Empty)
            {
                throw new ArgumentException("value cannot be empty", nameof(productId));
            }

            if (variantId == Guid.Empty)
            {
                throw new ArgumentException("value cannot be empty", nameof(variantId));
            }

            var product = await Repository.GetByKeyAsync<Product>(productId);
            product.ChangeVariant(variantId, name, ean, sku, price);

            var @event = new ProductVariantChangedEvent(productId, variantId, name, ean, sku, userId);
            EventBus.RaiseEvent(@event);

            await Repository.SaveChangesAsync();
        }

        /// <summary>
        /// Implementation of <see cref="IProductCommands.ChangeProductMainCategory(Guid, Guid, string)"/>
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="categoryId">The category id</param>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public async Task ChangeProductMainCategory(Guid productId, Guid categoryId, string userId)
        {
            try
            {
                if (productId == Guid.Empty)
                {
                    throw new ArgumentException("value cannot be empty", nameof(productId));
                }

                if (categoryId == Guid.Empty)
                {
                    throw new ArgumentException("value cannot be empty", nameof(categoryId));
                }

                var product = await Repository.GetByKeyAsync<Product>(productId);
                var category = await Repository.GetByKeyAsync<Category>(categoryId);

                var currentMainCategory = product.MainCategory;
                if (currentMainCategory != null)
                {
                    product.RemoveCategory(currentMainCategory);
                }

                product.AddMainCategory(category);

                var @event = new ProductMainCategoryChangedEvent(productId, categoryId, userId);
                EventBus.RaiseEvent(@event);

                await Repository.SaveChangesAsync();
            }
            catch 
            {
                throw;
            }
        }

        /// <summary>
        /// Implementation of <see cref="IProductCommands.RemoveProductCategory(Guid, Guid, string)"/>
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="categoryId">The category id</param>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public async Task RemoveProductCategory(Guid productId, Guid categoryId, string userId)
        {
            try
            {
                if (productId == Guid.Empty)
                {
                    throw new ArgumentException("value cannot be empty", nameof(productId));
                }

                if (categoryId == Guid.Empty)
                {
                    throw new ArgumentException("value cannot be empty", nameof(categoryId));
                }

                var product = await Repository.GetByKeyAsync<Product>(productId);
                var category = await Repository.GetByKeyAsync<Category>(categoryId);

                product.RemoveCategory(category);

                var @event = new ProductCategoryRemovedEvent(productId, categoryId, userId);
                EventBus.RaiseEvent(@event);

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
