using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wilcommerce.Catalog.Repository;

namespace Wilcommerce.Catalog.Commands.Brand.Handlers
{
    public class RestoreBrandCommandHandler : Interfaces.IRestoreBrandCommandHandler
    {
        public IRepository Repository { get; }

        public RestoreBrandCommandHandler(IRepository repository)
        {
            Repository = repository;
        }

        public async Task Handle(RestoreBrandCommand command)
        {
            try
            {
                var brand = await Repository.GetByKeyAsync<Models.Brand>(command.BrandId);
                brand.Restore();

                await Repository.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
