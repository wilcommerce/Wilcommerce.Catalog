using System;
using System.Threading.Tasks;
using Wilcommerce.Catalog.Repository;

namespace Wilcommerce.Catalog.Commands.Brand.Handlers
{
    public class DeleteBrandCommandHandler : Interfaces.IDeleteBrandCommandHandler
    {
        public IRepository Repository { get; }

        public DeleteBrandCommandHandler(IRepository repository)
        {
            Repository = repository;
        }

        public async Task Handle(DeleteBrandCommand command)
        {
            try
            {
                var brand = await Repository.GetByKeyAsync<Models.Brand>(command.BrandId);
                brand.Delete();

                await Repository.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
