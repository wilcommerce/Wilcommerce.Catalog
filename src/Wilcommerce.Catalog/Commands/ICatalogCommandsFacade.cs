﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        /// <returns></returns>
        Task CreateNewBrand(string name, string url, string description, Image logo);

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
        /// <returns></returns>
        Task CreateNewCategory(string code, string name, string url, string description, bool isVisible, DateTime? visibleFrom, DateTime? visibleTo);

        /// <summary>
        /// Set the category as visible
        /// </summary>
        /// <param name="categoryId">The category id</param>
        /// <param name="from">The date and time of when the category starts to be visible</param>
        /// <param name="to">The date and time till when the category is visible</param>
        /// <returns></returns>
        Task SetCategoryAsVisible(Guid categoryId, DateTime? from, DateTime? to);

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
        /// <returns></returns>
        Task CreateNewCustomAttribute(string name, string type, string description, string unitOfMeasure, IEnumerable<object> values);

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
        Task CreateNewProduct(string ean, string sku, string name, string url, Currency price, string description, int unitInStock, bool isOnSale, DateTime? onSaleFrom, DateTime? onSaleTo);

        Task DeleteProduct(Guid productId);

        Task RestoreProduct(Guid productId);

        Task SetUnitInStockForProduct(Guid productId, int unitInStock);

        Task AddUnitInStockToProduct(Guid productId, int unitToAdd);

        Task RemoveUnitInStockFromProduct(Guid productId, int unitToRemove);

        Task ChangeProductEanCode(Guid productId, string ean);

        Task ChangeProductSku(Guid productId, string sku);

        Task ChangeProductName(Guid productId, string name);

        Task ChangeProductDescription(Guid productId, string description);

        Task ChangeProductUrl(Guid productId, string url);

        Task SetProductPrice(Guid productId, Currency price);

        Task SetProductOnSale(Guid productId, DateTime? from, DateTime? to);

        Task RemoveProductFromSale(Guid productId);

        Task SetProductVendor(Guid productId, Guid brandId);

        Task AddCategoryToProduct(Guid productId, Guid categoryId);

        Task AddMainCategoryToProduct(Guid productId, Guid categoryId);

        Task AddProductVariant(Guid productId, Guid variantId);

        Task RemoveProductVariant(Guid productId, Guid variantId);

        Task AddAttributeToProduct(Guid productId, Guid attributeId, object value);

        Task RemoveProductAttribute(Guid productId, Guid attributeId);

        Task AddProductTierPrice(Guid productId, int fromQuantity, int toQuantity, Currency price);

        Task RemoveTierPriceFromProduct(Guid productId, Guid tierPriceId);

        Task AddProductReview(Guid productId, string name, int rating, string comment);

        Task ApproveProductReview(Guid productId, Guid reviewId);

        Task RemoveProductReview(Guid productId, Guid reviewId);

        Task AddProductImage(Guid productId, string path, string name, string originalName, bool isMain, DateTime uploadedOn);

        Task RemoveProductImage(Guid productId, Guid imageId);
        #endregion
    }
}
