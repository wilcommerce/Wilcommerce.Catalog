using System;
using System.Threading.Tasks;
using Wilcommerce.Core.Common.Models;

namespace Wilcommerce.Catalog.Commands
{
    /// <summary>
    /// Defines all the commands related to the Category aggregate
    /// </summary>
    public interface ICategoryCommands
    {
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
        /// <param name="userId">The user's id</param>
        /// <returns>The category id</returns>
        Task<Guid> CreateNewCategory(string code, string name, string url, string description, bool isVisible, DateTime? visibleFrom, DateTime? visibleTo, string userId);

        /// <summary>
        /// Update the category's info
        /// </summary>
        /// <param name="categoryId">The category id</param>
        /// <param name="code">The category code</param>
        /// <param name="name">The category name</param>
        /// <param name="url">The category url</param>
        /// <param name="description">The category description</param>
        /// <param name="isVisible">Whether the category is visible</param>
        /// <param name="visibleFrom">The date and time from when the category is visible</param>
        /// <param name="visibleTo">The date and time till when the category is visible</param>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        Task UpdateCategoryInfo(Guid categoryId, string code, string name, string url, string description, bool isVisible, DateTime? visibleFrom, DateTime? visibleTo, string userId);

        /// <summary>
        /// Add a child to the category
        /// </summary>
        /// <param name="categoryId">The category id</param>
        /// <param name="childId">The child id</param>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        Task AddCategoryChild(Guid categoryId, Guid childId, string userId);

        /// <summary>
        /// Set the parent for the specified category
        /// </summary>
        /// <param name="categoryId">The category id</param>
        /// <param name="parentId">The parent category id</param>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        Task SetParentForCategory(Guid categoryId, Guid parentId, string userId);

        /// <summary>
        /// Delete the category
        /// </summary>
        /// <param name="categoryId">The category id</param>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        Task DeleteCategory(Guid categoryId, string userId);

        /// <summary>
        /// Restore the category
        /// </summary>
        /// <param name="categoryId">The category id</param>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        Task RestoreCategory(Guid categoryId, string userId);

        /// <summary>
        /// Remove the specified child from the category
        /// </summary>
        /// <param name="categoryId">The category id</param>
        /// <param name="childId">The child to remove</param>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        Task RemoveChildForCategory(Guid categoryId, Guid childId, string userId);

        /// <summary>
        /// Remove the parent from the specified category
        /// </summary>
        /// <param name="categoryId">The category id</param>
        /// <param name="parentId">The category parent id</param>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        Task RemoveParentForCategory(Guid categoryId, Guid parentId, string userId);

        /// <summary>
        /// Set the seo information for the specified category
        /// </summary>
        /// <param name="categoryId">The category id</param>
        /// <param name="seo">The seo information</param>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        Task SetCategorySeoData(Guid categoryId, SeoData seo, string userId);
        #endregion
    }
}
