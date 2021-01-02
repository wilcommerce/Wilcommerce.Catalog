using System;
using System.Threading.Tasks;
using Wilcommerce.Core.Common.Models;

namespace Wilcommerce.Catalog.Commands
{
    /// <summary>
    /// Defines all the commands related to the Brand aggregate
    /// </summary>
    public interface IBrandCommands
    {
        #region Brand Commands
        /// <summary>
        /// Create a new brand
        /// </summary>
        /// <param name="name">The brand's name</param>
        /// <param name="url">The brand's unique url</param>
        /// <param name="description">The brand's description</param>
        /// <param name="logo">The brand's logo</param>
        /// <param name="userId">The user's id</param>
        /// <returns>The brand id</returns>
        Task<Guid> CreateNewBrand(string name, string url, string description, Image logo, string userId);

        /// <summary>
        /// Updates the brand's info
        /// </summary>
        /// <param name="brandId">The brand's id</param>
        /// <param name="name">The brand's new name</param>
        /// <param name="url">The brand's new url</param>
        /// <param name="description">The brand's new description</param>
        /// <param name="logo">The brand's new logo</param>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        Task UpdateBrandInfo(Guid brandId, string name, string url, string description, Image logo, string userId);

        /// <summary>
        /// Set the brand seo data
        /// </summary>
        /// <param name="brandId">The brand's id</param>
        /// <param name="seo">The seo data</param>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        Task SetBrandSeoData(Guid brandId, SeoData seo, string userId);

        /// <summary>
        /// Delete the brand
        /// </summary>
        /// <param name="brandId">The brand's id</param>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        Task DeleteBrand(Guid brandId, string userId);

        /// <summary>
        /// Restore the brand
        /// </summary>
        /// <param name="brandId">The brand's id</param>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        Task RestoreBrand(Guid brandId, string userId);
        #endregion
    }
}
