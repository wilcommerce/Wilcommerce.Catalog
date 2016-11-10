using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wilcommerce.Catalog.Repository;

namespace Wilcommerce.Catalog.Commands.Brand.Handlers
{
    public class SetBrandLogoCommandHandler : Interfaces.ISetBrandLogoCommandHandler
    {
        public IRepository Repository { get; }

        public SetBrandLogoCommandHandler(IRepository repository)
        {
            Repository = repository;
        }

        public async Task Handle(SetBrandLogoCommand command)
        {
            try
            {
                var brand = await Repository.GetByKeyAsync<Models.Brand>(command.BrandId);
                brand.SetLogo(command.Logo);

                await Repository.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
