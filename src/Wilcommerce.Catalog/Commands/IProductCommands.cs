using System;
using System.Threading.Tasks;
using Wilcommerce.Core.Common.Models;

namespace Wilcommerce.Catalog.Commands
{
    /// <summary>
    /// Defines all the commands related to the Product aggregate
    /// </summary>
    public interface IProductCommands
    {
        #region Product Commands
        /// <summary>
        /// Create a new product
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
        Task<Guid> CreateNewProduct(string ean, string sku, string name, string url, Currency price, string description, int unitInStock, bool isOnSale, DateTime? onSaleFrom, DateTime? onSaleTo);

        /// <summary>
        /// Update the product general info
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
        Task UpdateProductInfo(Guid productId, string ean, string sku, string name, string url, Currency price, string description, int unitInStock, bool isOnSale, DateTime? onSaleFrom, DateTime? onSaleTo);

        /// <summary>
        /// Delete the product
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <returns></returns>
        Task DeleteProduct(Guid productId);

        /// <summary>
        /// Restore the product
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <returns></returns>
        Task RestoreProduct(Guid productId);

        /// <summary>
        /// Set the product vendor
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="brandId">The vendor id</param>
        /// <returns></returns>
        Task SetProductVendor(Guid productId, Guid brandId);

        /// <summary>
        /// Add the category to the product
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="categoryId">The category id</param>
        /// <returns></returns>
        Task AddCategoryToProduct(Guid productId, Guid categoryId);

        /// <summary>
        /// Add the main category to the product
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="categoryId">The category id</param>
        /// <returns></returns>
        Task AddMainCategoryToProduct(Guid productId, Guid categoryId);

        /// <summary>
        /// Add a variant to the product
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="name">The variant name</param>
        /// <param name="ean">The variant EAN code</param>
        /// <param name="sku">The variant SKU code</param>
        /// <param name="price">The variant price</param>
        /// <returns></returns>
        Task AddProductVariant(Guid productId, string name, string ean, string sku, Currency price);

        /// <summary>
        /// Remove the variant from the product
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="variantId">The variant id</param>
        /// <returns></returns>
        Task RemoveProductVariant(Guid productId, Guid variantId);

        /// <summary>
        /// Add the attribute to the product
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="attributeId">The attribute id</param>
        /// <param name="value">The attribute value</param>
        /// <returns></returns>
        Task AddAttributeToProduct(Guid productId, Guid attributeId, object value);

        /// <summary>
        /// Remove the attribute from the product
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="attributeId">The attribute id</param>
        /// <returns></returns>
        Task RemoveProductAttribute(Guid productId, Guid attributeId);

        /// <summary>
        /// Add a tier price to the product
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="fromQuantity">The start quantity</param>
        /// <param name="toQuantity">The end quantity</param>
        /// <param name="price">The price value</param>
        /// <returns></returns>
        Task AddProductTierPrice(Guid productId, int fromQuantity, int toQuantity, Currency price);

        /// <summary>
        /// Change the tier price for the product
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="tierPriceId">The tier price id</param>
        /// <param name="fromQuantity">The new starting quantity</param>
        /// <param name="toQuantity">The new ending quantity</param>
        /// <param name="price">The new price</param>
        /// <returns></returns>
        Task ChangeProductTierPrice(Guid productId, Guid tierPriceId, int fromQuantity, int toQuantity, Currency price);

        /// <summary>
        /// Remove the tier price from the product
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="tierPriceId">The tier price id</param>
        /// <returns></returns>
        Task RemoveTierPriceFromProduct(Guid productId, Guid tierPriceId);

        /// <summary>
        /// Add a review to the product
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="name">The name of the user who leave the review</param>
        /// <param name="rating">The rate given</param>
        /// <param name="comment">The comment given</param>
        /// <returns></returns>
        Task AddProductReview(Guid productId, string name, int rating, string comment);

        /// <summary>
        /// Approve the product review
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="reviewId">The review id</param>
        /// <returns></returns>
        Task ApproveProductReview(Guid productId, Guid reviewId);

        /// <summary>
        /// Remove the review from the product
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="reviewId">The review id</param>
        /// <returns></returns>
        Task RemoveProductReview(Guid productId, Guid reviewId);

        /// <summary>
        /// Add an image to the product
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="path">The file path</param>
        /// <param name="name">The file name</param>
        /// <param name="originalName">The file original name</param>
        /// <param name="isMain">Whether is the main image for the product</param>
        /// <param name="uploadedOn">The date and time of when the image is uploaded</param>
        /// <returns></returns>
        Task AddProductImage(Guid productId, string path, string name, string originalName, bool isMain, DateTime uploadedOn);

        /// <summary>
        /// Remove the image from the product
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="imageId">The image id</param>
        /// <returns></returns>
        Task RemoveProductImage(Guid productId, Guid imageId);

        /// <summary>
        /// Set the SEO information for the product
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="seo">The SEO information</param>
        /// <returns></returns>
        Task SetProductSeo(Guid productId, SeoData seo);

        /// <summary>
        /// Change the product's variant information
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="variantId">The variant id</param>
        /// <param name="name">The variant name</param>
        /// <param name="ean">The variant EAN code</param>
        /// <param name="sku">The variant SKU code</param>
        /// <param name="price">The variant price</param>
        /// <returns></returns>
        Task ChangeProductVariant(Guid productId, Guid variantId, string name, string ean, string sku, Currency price);

        /// <summary>
        /// Change the product main category
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="categoryId">The new category id</param>
        /// <returns></returns>
        Task ChangeProductMainCategory(Guid productId, Guid categoryId);

        /// <summary>
        /// Remove the category from the product categories
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="categoryId">The category id</param>
        /// <returns></returns>
        Task RemoveProductCategory(Guid productId, Guid categoryId);
        #endregion
    }
}
