using System;
using System.Threading.Tasks;
using Wilcommerce.Catalog.Events.Brand;
using Wilcommerce.Catalog.Models;
using Wilcommerce.Core.Common.Models;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Commands
{
    /// <summary>
    /// Implementation of <see cref="IBrandCommands"/>
    /// </summary>
    public class BrandCommands : IBrandCommands
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
        /// Construct the brand commands
        /// </summary>
        /// <param name="repository">The repository instance</param>
        /// <param name="eventBus">The event bus instance</param>
        public BrandCommands(Repository.IRepository repository, IEventBus eventBus)
        {
            Repository = repository ?? throw new ArgumentNullException(nameof(repository));
            EventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }

        #region Brand Commands
        /// <summary>
        /// Implementation of <see cref="IBrandCommands.CreateNewBrand(string, string, string, Image, string)"/>
        /// </summary>
        /// <param name="name">The brand's name</param>
        /// <param name="url">The brand's unique url</param>
        /// <param name="description">The brand's description</param>
        /// <param name="logo">The brand's logo</param>
        /// <param name="userId">The user's id</param>
        /// <returns>The brand id</returns>
        public virtual async Task<Guid> CreateNewBrand(string name, string url, string description, Image logo, string userId)
        {
            try
            {
                var brand = Brand.Create(name, url);
                if (!string.IsNullOrEmpty(description))
                {
                    brand.ChangeDescription(description);
                }

                if (logo is not null)
                {
                    brand.SetLogo(logo);
                }

                Repository.Add(brand);
                await Repository.SaveChangesAsync();

                var @event = new BrandCreatedEvent(brand.Id, brand.Name, userId);
                EventBus.RaiseEvent(@event);

                return brand.Id;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Implementation of <see cref="IBrandCommands.UpdateBrandInfo(Guid, string, string, string, Image, string)"/>
        /// </summary>
        /// <param name="brandId">The brand's id</param>
        /// <param name="name">The brand's new name</param>
        /// <param name="url">The brand's new url</param>
        /// <param name="description">The brand's new description</param>
        /// <param name="logo">The brand's new logo</param>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public virtual async Task UpdateBrandInfo(Guid brandId, string name, string url, string description, Image logo, string userId)
        {
            try
            {
                if (brandId == Guid.Empty)
                {
                    throw new ArgumentException("value cannot be empty", nameof(brandId));
                }

                var brand = await Repository.GetByKeyAsync<Brand>(brandId);
                if (brand.Name != name)
                {
                    brand.ChangeName(name);
                }

                if (brand.Url != url)
                {
                    brand.ChangeUrl(url);
                }

                if (brand.Description != description)
                {
                    brand.ChangeDescription(description);
                }

                if (brand.Logo != logo)
                {
                    brand.SetLogo(logo);
                }

                await Repository.SaveChangesAsync();

                var @event = new BrandInfoUpdatedEvent(brandId, name, url, description, logo, userId);
                EventBus.RaiseEvent(@event);
            }
            catch 
            {
                throw;
            }
        }

        /// <summary>
        /// Implementation of <see cref="IBrandCommands.SetBrandSeoData(Guid, SeoData, string)"/>
        /// </summary>
        /// <param name="brandId">The brand's id</param>
        /// <param name="seo">The seo data</param>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public virtual async Task SetBrandSeoData(Guid brandId, SeoData seo, string userId)
        {
            try
            {
                if (brandId == Guid.Empty)
                {
                    throw new ArgumentException("value cannot be empty", nameof(brandId));
                }

                var brand = await Repository.GetByKeyAsync<Brand>(brandId);
                brand.SetSeoData(seo);

                await Repository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Implementation of <see cref="IBrandCommands.DeleteBrand(Guid, string)"/>
        /// </summary>
        /// <param name="brandId">The brand's id</param>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public virtual async Task DeleteBrand(Guid brandId, string userId)
        {
            try
            {
                if (brandId == Guid.Empty)
                {
                    throw new ArgumentException("value cannot be empty", nameof(brandId));
                }

                var brand = await Repository.GetByKeyAsync<Brand>(brandId);
                brand.Delete();

                await Repository.SaveChangesAsync();

                var @event = new BrandDeletedEvent(brandId, userId);
                EventBus.RaiseEvent(@event);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Implementation of <see cref="IBrandCommands.RestoreBrand(Guid, string)"/>
        /// </summary>
        /// <param name="brandId">The brand's id</param>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public virtual async Task RestoreBrand(Guid brandId, string userId)
        {
            try
            {
                if (brandId == Guid.Empty)
                {
                    throw new ArgumentException("value cannot be empty", nameof(brandId));
                }

                var brand = await Repository.GetByKeyAsync<Brand>(brandId);
                brand.Restore();

                await Repository.SaveChangesAsync();

                var @event = new BrandRestoredEvent(brandId, userId);
                EventBus.RaiseEvent(@event);
            }
            catch
            {
                throw;
            }
        }
        #endregion
    }
}
