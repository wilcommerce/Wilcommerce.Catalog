using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wilcommerce.Catalog.Repository;

namespace Wilcommerce.Catalog.Commands.Brand.Handlers
{
    public class ChangeBrandNameCommandHandler : Interfaces.IChangeBrandNameCommandHandler
    {
        public IRepository Repository { get; }

        public ChangeBrandNameCommandHandler(IRepository repository)
        {
            Repository = repository;
        }

        public async Task Handle(ChangeBrandNameCommand command)
        {
            try
            {
                var brand = await Repository.GetByKeyAsync<Models.Brand>(command.BrandId);
                brand.ChangeName(command.Name);

                await Repository.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
