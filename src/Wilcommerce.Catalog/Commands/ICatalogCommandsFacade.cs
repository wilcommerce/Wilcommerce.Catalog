using System;
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
        Task CreateNewCategory(string code, string name, string url, string description, bool isVisible, DateTime? visibleFrom, DateTime? visibleTo);

        Task SetCategoryAsVisible(Guid categoryId, DateTime? from, DateTime? to);

        Task AddCategoryChild(Guid categoryId, Guid childId);

        Task ChangeCategoryName(Guid categoryId, string name);

        Task ChangeCategoryCode(Guid categoryId, string code);

        Task ChangeCategoryDescription(Guid categoryId, string description);

        Task ChangeCategoryUrl(Guid categoryId, string url);

        Task SetParentForCategory(Guid categoryId, Guid parentId);

        Task DeleteCategory(Guid categoryId);

        Task RestoreCategory(Guid categoryId);

        Task RemoveChildForCategory(Guid categoryId, Guid childId);

        Task RemoveParentForCategory(Guid categoryId);
        #endregion
    }
}
