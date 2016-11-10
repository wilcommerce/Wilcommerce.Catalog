using System;
using System.Threading.Tasks;
using Wilcommerce.Core.Common.Domain.Models;
using Wilcommerce.Catalog.Commands.Brand;
using Wilcommerce.Catalog.Commands.Brand.Handlers.Interfaces;

namespace Wilcommerce.Catalog.Commands
{
    public class CatalogCommandsFacade : ICatalogCommandsFacade
    {
        #region Brand Handlers
        public ICreateNewBrandCommandHandler CreateBrandHandler { get; }

        public IChangeBrandNameCommandHandler ChangeBrandNameHandler { get; }

        public IChangeBrandUrlCommandHandler ChangeBrandUrlHandler { get; }

        public IChangeBrandDescriptionCommandHandler ChangeBrandDescriptionHandler { get; }

        public ISetBrandLogoCommandHandler SetBrandLogoHandler { get; }

        public IDeleteBrandCommandHandler DeleteBrandHandler { get; }

        public IRestoreBrandCommandHandler RestoreBrandHandler { get; }
        #endregion

        public CatalogCommandsFacade(
            ICreateNewBrandCommandHandler createBrandHandler,
            IChangeBrandNameCommandHandler changeBrandNameHandler,
            IChangeBrandUrlCommandHandler changeBrandUrlHandler,
            IChangeBrandDescriptionCommandHandler changeBrandDescriptionHandler,
            ISetBrandLogoCommandHandler setBrandLogoHandler,
            IDeleteBrandCommandHandler deleteBrandHandler,
            IRestoreBrandCommandHandler restoreBrandHandler)
        {
            CreateBrandHandler = createBrandHandler;
            ChangeBrandNameHandler = changeBrandNameHandler;
            ChangeBrandUrlHandler = changeBrandUrlHandler;
            ChangeBrandDescriptionHandler = changeBrandDescriptionHandler;
            SetBrandLogoHandler = setBrandLogoHandler;
            DeleteBrandHandler = deleteBrandHandler;
            RestoreBrandHandler = restoreBrandHandler;
        }

        #region Brand Commands
        public async Task ChangeBrandDescription(Guid brandId, string description)
        {
            try
            {
                var command = new ChangeBrandDescriptionCommand(brandId, description);
                await ChangeBrandDescriptionHandler.Handle(command);
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
                var command = new ChangeBrandNameCommand(brandId, name);
                await ChangeBrandNameHandler.Handle(command);
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
                var command = new ChangeBrandUrlCommand(brandId, url);
                await ChangeBrandUrlHandler.Handle(command);
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
                var command = new CreateNewBrandCommand(name, url, description, logo);
                await CreateBrandHandler.Handle(command);
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
                var command = new DeleteBrandCommand(brandId);
                await DeleteBrandHandler.Handle(command);
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
                var command = new RestoreBrandCommand(brandId);
                await RestoreBrandHandler.Handle(command);
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
                var command = new SetBrandLogoCommand(brandId, logo);
                await SetBrandLogoHandler.Handle(command);
            }
            catch 
            {
                throw;
            }
        }
        #endregion
    }
}
