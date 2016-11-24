using System;
using System.Threading.Tasks;
using Wilcommerce.Core.Common.Domain.Models;
using Wilcommerce.Catalog.Repository;
using Wilcommerce.Catalog.Models;

namespace Wilcommerce.Catalog.Commands
{
    public class CatalogCommandsFacade : ICatalogCommandsFacade
    {
        public IRepository Repository { get; }

        public CatalogCommandsFacade(IRepository repository)
        {
            Repository = repository;
        }

        #region Brand Commands
        public async Task ChangeBrandDescription(Guid brandId, string description)
        {
            try
            {
                var brand = await Repository.GetByKeyAsync<Brand>(brandId);
                brand.ChangeDescription(description);

                await Repository.SaveChangesAsync();
            }
            catch 
            {
                throw;
            }
        }

        public async Task ChangeBrandName(Guid brandId, string name)
        {
            try
            {
                var brand = await Repository.GetByKeyAsync<Brand>(brandId);
                brand.ChangeName(name);

                await Repository.SaveChangesAsync();
            }
            catch 
            {
                throw;
            }
        }

        public async Task ChangeBrandUrl(Guid brandId, string url)
        {
            try
            {
                var brand = await Repository.GetByKeyAsync<Brand>(brandId);
                brand.ChangeUrl(url);

                await Repository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task CreateNewBrand(string name, string url, string description, Image logo)
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
            }
            catch
            {
                throw;
            }
        }

        public async Task DeleteBrand(Guid brandId)
        {
            try
            {
                var brand = await Repository.GetByKeyAsync<Brand>(brandId);
                brand.Delete();

                await Repository.SaveChangesAsync();
            }
            catch 
            {
                throw;
            }
        }

        public async Task RestoreBrand(Guid brandId)
        {
            try
            {
                var brand = await Repository.GetByKeyAsync<Brand>(brandId);
                brand.Restore();

                await Repository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

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
        #endregion
    }
}
