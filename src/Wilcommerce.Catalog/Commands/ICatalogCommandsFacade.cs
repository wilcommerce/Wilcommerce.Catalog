using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wilcommerce.Catalog.Models;
using Wilcommerce.Core.Common.Domain.Models;

namespace Wilcommerce.Catalog.Commands
{
    public interface ICatalogCommandsFacade
    {
        #region Brand Commands
        /// <summary>
        /// Create a new brand
        /// </summary>
        /// <param name="name">The brand's name</param>
        /// <param name="url">The brand's unique url</param>
        /// <param name="description">The brand's description</param>
        /// <param name="logo">The brand's logo</param>
        /// <returns>The brand id</returns>
        Task<Guid> CreateNewBrand(string name, string url, string description, Image logo);

        /// <summary>
        /// Change the brand's name
        /// </summary>
        /// <param name="brandId">The brand's id</param>
        /// <param name="name">The brand's new name</param>
        /// <returns></returns>
        Task ChangeBrandName(Guid brandId, string name);

        /// <summary>
        /// Change the brand's url
        /// </summary>
        /// <param name="brandId">The brand's id</param>
        /// <param name="url">The brand's new url</param>
        /// <returns></returns>
        Task ChangeBrandUrl(Guid brandId, string url);

        /// <summary>
        /// Change the brand's description
        /// </summary>
        /// <param name="brandId">The brand's id</param>
        /// <param name="description">The brand's description</param>
        /// <returns></returns>
        Task ChangeBrandDescription(Guid brandId, string description);

        /// <summary>
        /// Set the brand's logo
        /// </summary>
        /// <param name="brandId">The brand's id</param>
        /// <param name="logo">The brand's logo</param>
        /// <returns></returns>
        Task SetBrandLogo(Guid brandId, Image logo);

        /// <summary>
        /// Delete the brand
        /// </summary>
        /// <param name="brandId">The brand's id</param>
        /// <returns></returns>
        Task DeleteBrand(Guid brandId);

        /// <summary>
        /// Restore the brand
        /// </summary>
        /// <param name="brandId">The brand's id</param>
        /// <returns></returns>
        Task RestoreBrand(Guid brandId);

        /// <summary>
        /// Set the brand seo data
        /// </summary>
        /// <param name="brandId">The brand's id</param>
        /// <param name="seo">The seo data</param>
        /// <returns></returns>
        Task SetBrandSeoData(Guid brandId, SeoData seo);
        #endregion

        #region Category Commands
        /// <summary>
        /// Create a new category
        /// </summary>
        /// <param name="code">The category code</param>
        /// <param name="name">The category name</param>
        /// <param name="url">The category url</param>
        /// <param name="description">The category description</param>
        /// <param name="isVisible">Whether the category is visible</param>
        /// <param name="visibleFrom">The date and time of when the category starts to be visible</param>
        /// <param name="visibleTo">The date and time till when the category is visible</param>
        /// <returns>The category id</returns>
        Task<Guid> CreateNewCategory(string code, string name, string url, string description, bool isVisible, DateTime? visibleFrom, DateTime? visibleTo);

        /// <summary>
        /// Set the category as visible
        /// </summary>
        /// <param name="categoryId">The category id</param>
        /// <param name="from">The date and time of when the category starts to be visible</param>
        /// <param name="to">The date and time till when the category is visible</param>
        /// <returns></returns>
        Task SetCategoryAsVisible(Guid categoryId, DateTime? from, DateTime? to);

        /// <summary>
        /// Hide the category
        /// </summary>
        /// <param name="categoryId">The category id</param>
        /// <returns></returns>
        Task HideCategory(Guid categoryId);

        /// <summary>
        /// Add a child to the category
        /// </summary>
        /// <param name="categoryId">The category id</param>
        /// <param name="childId">The child id</param>
        /// <returns></returns>
        Task AddCategoryChild(Guid categoryId, Guid childId);

        /// <summary>
        /// Change the category name
        /// </summary>
        /// <param name="categoryId">The category id</param>
        /// <param name="name">The new category name</param>
        /// <returns></returns>
        Task ChangeCategoryName(Guid categoryId, string name);

        /// <summary>
        /// Change the category code
        /// </summary>
        /// <param name="categoryId">The category id</param>
        /// <param name="code">The new category code</param>
        /// <returns></returns>
        Task ChangeCategoryCode(Guid categoryId, string code);

        /// <summary>
        /// Change the category description
        /// </summary>
        /// <param name="categoryId">The category id</param>
        /// <param name="description">The new category description</param>
        /// <returns></returns>
        Task ChangeCategoryDescription(Guid categoryId, string description);

        /// <summary>
        /// Change the category url
        /// </summary>
        /// <param name="categoryId">The category id</param>
        /// <param name="url">The new category url</param>
        /// <returns></returns>
        Task ChangeCategoryUrl(Guid categoryId, string url);

        /// <summary>
        /// Set the parent for the specified category
        /// </summary>
        /// <param name="categoryId">The category id</param>
        /// <param name="parentId">The parent category id</param>
        /// <returns></returns>
        Task SetParentForCategory(Guid categoryId, Guid parentId);

        /// <summary>
        /// Delete the category
        /// </summary>
        /// <param name="categoryId">The category id</param>
        /// <returns></returns>
        Task DeleteCategory(Guid categoryId);

        /// <summary>
        /// Restore the category
        /// </summary>
        /// <param name="categoryId">The category id</param>
        /// <returns></returns>
        Task RestoreCategory(Guid categoryId);

        /// <summary>
        /// Remove the specified child from the category
        /// </summary>
        /// <param name="categoryId">The category id</param>
        /// <param name="childId">The child to remove</param>
        /// <returns></returns>
        Task RemoveChildForCategory(Guid categoryId, Guid childId);

        /// <summary>
        /// Remove the parent from the specified category
        /// </summary>
        /// <param name="categoryId">The category id</param>
        /// <returns></returns>
        Task RemoveParentForCategory(Guid categoryId);

        /// <summary>
        /// Set the seo information for the specified category
        /// </summary>
        /// <param name="categoryId">The category id</param>
        /// <param name="seo">The seo information</param>
        /// <returns></returns>
        Task SetCategorySeoData(Guid categoryId, SeoData seo);
        #endregion

        #region CustomAttribute Commands
        /// <summary>
        /// Create a new custom attribute
        /// </summary>
        /// <param name="name">The custom attribute's name</param>
        /// <param name="type">The data type of the custom attribute</param>
        /// <param name="description">The custom attribute's description</param>
        /// <param name="unitOfMeasure">The unit of measure of the custom attribute</param>
        /// <param name="values">The available values for the custom attribute</param>
        /// <returns>The custom attribute id</returns>
        Task<Guid> CreateNewCustomAttribute(string name, string type, string description, string unitOfMeasure, IEnumerable<object> values);

        /// <summary>
        /// Add the value for the custom attribute
        /// </summary>
        /// <param name="attributeId">The custom attribute id</param>
        /// <param name="value">The value to add</param>
        /// <returns></returns>
        Task AddValueForAttribute(Guid attributeId, object value);

        /// <summary>
        /// Remove the value from the custom attribute
        /// </summary>
        /// <param name="attributeId">The custom attribute id</param>
        /// <param name="value">The value to remove</param>
        /// <returns></returns>
        Task RemoveValueFromAttribute(Guid attributeId, object value);

        /// <summary>
        /// Change the attribute's name
        /// </summary>
        /// <param name="attributeId">The custom attribute id</param>
        /// <param name="name">The new name</param>
        /// <returns></returns>
        Task ChangeAttributeName(Guid attributeId, string name);

        /// <summary>
        /// Change the attribute description
        /// </summary>
        /// <param name="attributeId">The custom attribute id</param>
        /// <param name="description">The new description</param>
        /// <returns></returns>
        Task ChangeAttributeDescription(Guid attributeId, string description);

        /// <summary>
        /// Set the attribute unit of measure
        /// </summary>
        /// <param name="attributeId">The custom attribute id</param>
        /// <param name="unitOfMeasure">The attribute's unit of measure</param>
        /// <returns></returns>
        Task SetAttributeUnitOfMeasure(Guid attributeId, string unitOfMeasure);

        /// <summary>
        /// Change the attribute data type
        /// </summary>
        /// <param name="attributeId">The custom attribute id</param>
        /// <param name="dataType">The new data type</param>
        /// <returns></returns>
        Task ChangeAttributeDataType(Guid attributeId, string dataType);

        /// <summary>
        /// Delete the custom attribute
        /// </summary>
        /// <param name="attributeId">The custom attribute id</param>
        /// <returns></returns>
        Task DeleteCustomAttribute(Guid attributeId);

        /// <summary>
        /// Restore the custom attribute
        /// </summary>
        /// <param name="attributeId">The custom attribute id</param>
        /// <returns></returns>
        Task RestoreCustomAttribute(Guid attributeId);
        #endregion

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
        /// Set the unit in stock number for the product
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="unitInStock">The number of unit in stock</param>
        /// <returns></returns>
        Task SetUnitInStockForProduct(Guid productId, int unitInStock);

        /// <summary>
        /// Add the number of unit in stock to the product
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="unitToAdd">The number of unit to add</param>
        /// <returns></returns>
        Task AddUnitInStockToProduct(Guid productId, int unitToAdd);

        /// <summary>
        /// Remove the number of unit in stock from the product
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="unitToRemove">The number of unit to remove</param>
        /// <returns></returns>
        Task RemoveUnitInStockFromProduct(Guid productId, int unitToRemove);

        /// <summary>
        /// Change the product EAN code
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="ean">The new EAN code</param>
        /// <returns></returns>
        Task ChangeProductEanCode(Guid productId, string ean);

        /// <summary>
        /// Change the product SKU
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="sku">The new SKU</param>
        /// <returns></returns>
        Task ChangeProductSku(Guid productId, string sku);

        /// <summary>
        /// Change the product name
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="name">The new product name</param>
        /// <returns></returns>
        Task ChangeProductName(Guid productId, string name);

        /// <summary>
        /// Change the product description
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="description">The new product description</param>
        /// <returns></returns>
        Task ChangeProductDescription(Guid productId, string description);

        /// <summary>
        /// Change the product url
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="url">The new product url</param>
        /// <returns></returns>
        Task ChangeProductUrl(Guid productId, string url);

        /// <summary>
        /// Set the product price
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="price">The product price</param>
        /// <returns></returns>
        Task SetProductPrice(Guid productId, Currency price);

        /// <summary>
        /// Set the product as on sale
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="from">The date and time from when the product will be on sale</param>
        /// <param name="to">The date and time till when the product will be on sale</param>
        /// <returns></returns>
        Task SetProductOnSale(Guid productId, DateTime? from, DateTime? to);

        /// <summary>
        /// Remove the product from sale
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <returns></returns>
        Task RemoveProductFromSale(Guid productId);

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
        /// Set the SEO information for the procut
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="seo">The SEO information</param>
        /// <returns></returns>
        Task SetProductSeo(Guid productId, SeoData seo);
        #endregion

        #region CatalogSettings Commands
        Task<Guid> CreateCatalogSettings(int categoriesPerPage, int productsPerPage, CatalogSettings.ViewType categoriesViewType, CatalogSettings.ViewType productsViewType, bool showPrices, bool allowReviews, int reviewsPerPage);

        Task ShowPrices(Guid settingsId, bool showPrices);

        Task AllowProductReviews(Guid settingsId, bool allowReviews);

        Task SetProductReviewsPerPage(Guid settingsId, int reviewsPerPage);

        Task SetCategoriesView(Guid settingsId, CatalogSettings.ViewType viewType, int categoriesPerPage);

        Task SetProductsView(Guid settingsId, CatalogSettings.ViewType viewType, int productsPerPage);
        #endregion
    }
}
