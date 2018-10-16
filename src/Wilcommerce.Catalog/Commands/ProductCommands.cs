﻿using System;
using System.Collections.Generic;
using System.Text;
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

        public ProductCommands(Repository.IRepository repository, IEventBus eventBus)
        {
            Repository = repository ?? throw new ArgumentNullException(nameof(repository));
            EventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }

        #region Product Commands
        /// <summary>
        /// Implementation of <see cref="IProductCommands.CreateNewProduct(string, string, string, string, Currency, string, int, bool, DateTime?, DateTime?)"/>
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
        /// <returns>The product id</returns>
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

        /// <summary>
        /// Implementation of <see cref="IProductCommands.UpdateProductInfo(Guid, string, string, string, string, Currency, string, int, bool, DateTime?, DateTime?)"/>
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
        /// <returns></returns>
        public Task UpdateProductInfo(Guid productId, string ean, string sku, string name, string url, Currency price, string description, int unitInStock, bool isOnSale, DateTime? onSaleFrom, DateTime? onSaleTo)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Implementation of <see cref="IProductCommands.DeleteProduct(Guid)"/>
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <returns></returns>
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

        /// <summary>
        /// Implementation of <see cref="IProductCommands.RestoreProduct(Guid)"/>
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <returns></returns>
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

        /// <summary>
        /// Implementation of <see cref="IProductCommands.SetProductVendor(Guid, Guid)"/>
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="brandId">The vendor id</param>
        /// <returns></returns>
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

        /// <summary>
        /// Implementation of <see cref="IProductCommands.AddCategoryToProduct(Guid, Guid)"/>
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="categoryId">The category id</param>
        /// <returns></returns>
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

        /// <summary>
        /// Implementation of <see cref="IProductCommands.AddMainCategoryToProduct(Guid, Guid)"/>
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="categoryId">The category id</param>
        /// <returns></returns>
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

        /// <summary>
        /// Implementation of <see cref="IProductCommands.AddProductVariant(Guid, string, string, string, Currency)"/>
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="name">The variant name</param>
        /// <param name="ean">The variant EAN code</param>
        /// <param name="sku">The variant SKU code</param>
        /// <param name="price">The variant price</param>
        /// <returns></returns>
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

        /// <summary>
        /// Implementation of <see cref="IProductCommands.RemoveProductVariant(Guid, Guid)"/>
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="variantId">The variant id</param>
        /// <returns></returns>
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

        /// <summary>
        /// Implementation of <see cref="IProductCommands.AddAttributeToProduct(Guid, Guid, object)"/>
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="attributeId">The attribute id</param>
        /// <param name="value">The attribute value</param>
        /// <returns></returns>
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

        /// <summary>
        /// Implementation of <see cref="IProductCommands.RemoveProductAttribute(Guid, Guid)"/>
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="attributeId">The attribute id</param>
        /// <returns></returns>
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

        /// <summary>
        /// Implementation of <see cref="IProductCommands.AddProductTierPrice(Guid, int, int, Currency)"/>
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="fromQuantity">The start quantity</param>
        /// <param name="toQuantity">The end quantity</param>
        /// <param name="price">The price value</param>
        /// <returns></returns>
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

        /// <summary>
        /// Implementation of <see cref="IProductCommands.ChangeProductTierPrice(Guid, Guid, int, int, Currency)"/>
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="tierPriceId">The tier price id</param>
        /// <param name="fromQuantity">The new starting quantity</param>
        /// <param name="toQuantity">The new ending quantity</param>
        /// <param name="price">The new price</param>
        /// <returns></returns>
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

        /// <summary>
        /// Implementation of <see cref="IProductCommands.RemoveTierPriceFromProduct(Guid, Guid)"/>
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="tierPriceId">The tier price id</param>
        /// <returns></returns>
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

        /// <summary>
        /// Implementation of <see cref="IProductCommands.AddProductReview(Guid, string, int, string)"/>
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="name">The name of the user who leave the review</param>
        /// <param name="rating">The rate given</param>
        /// <param name="comment">The comment given</param>
        /// <returns></returns>
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

        /// <summary>
        /// Implementation of <see cref="IProductCommands.ApproveProductReview(Guid, Guid)"/>
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="reviewId">The review id</param>
        /// <returns></returns>
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

        /// <summary>
        /// Implementation of <see cref="IProductCommands.RemoveProductReview(Guid, Guid)"/>
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="reviewId">The review id</param>
        /// <returns></returns>
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

        /// <summary>
        /// Implementation of <see cref="IProductCommands.AddProductImage(Guid, string, string, string, bool, DateTime)"/>
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="path">The file path</param>
        /// <param name="name">The file name</param>
        /// <param name="originalName">The file original name</param>
        /// <param name="isMain">Whether is the main image for the product</param>
        /// <param name="uploadedOn">The date and time of when the image is uploaded</param>
        /// <returns></returns>
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

        /// <summary>
        /// Implementation of <see cref="IProductCommands.RemoveProductImage(Guid, Guid)"/>
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="imageId">The image id</param>
        /// <returns></returns>
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

        /// <summary>
        /// Implementation of <see cref="IProductCommands.SetProductSeo(Guid, SeoData)"/>
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="seo">The SEO information</param>
        /// <returns></returns>
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
        #endregion
    }
}
