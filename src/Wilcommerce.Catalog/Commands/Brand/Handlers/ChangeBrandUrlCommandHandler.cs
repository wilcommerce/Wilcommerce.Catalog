using System;
using System.Threading.Tasks;
using Wilcommerce.Catalog.Repository;

namespace Wilcommerce.Catalog.Commands.Brand.Handlers
{
    public class ChangeBrandUrlCommandHandler : Interfaces.IChangeBrandUrlCommandHandler
    {
        public IRepository Repository { get; }

        public ChangeBrandUrlCommandHandler(IRepository repository)
        {
            Repository = repository;
        }

        public async Task Handle(ChangeBrandUrlCommand command)
        {
            try
            {
                var brand = await Repository.GetByKeyAsync<Models.Brand>(command.BrandId);
                brand.ChangeUrl(command.Url);

                await Repository.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
