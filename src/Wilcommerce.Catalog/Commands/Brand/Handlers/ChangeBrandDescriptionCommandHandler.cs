using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wilcommerce.Catalog.Repository;

namespace Wilcommerce.Catalog.Commands.Brand.Handlers
{
    public class ChangeBrandDescriptionCommandHandler : Interfaces.IChangeBrandDescriptionCommandHandler
    {
        public IRepository Repository { get; }

        public ChangeBrandDescriptionCommandHandler(IRepository repository)
        {
            Repository = repository;
        }

        public async Task Handle(ChangeBrandDescriptionCommand command)
        {
            try
            {
                var brand = await Repository.GetByKeyAsync<Models.Brand>(command.BrandId);
                brand.ChangeDescription(command.Description);

                await Repository.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
