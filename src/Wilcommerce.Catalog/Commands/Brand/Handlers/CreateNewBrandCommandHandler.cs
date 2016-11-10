using System;
using System.Threading.Tasks;
using Wilcommerce.Catalog.Repository;

namespace Wilcommerce.Catalog.Commands.Brand.Handlers
{
    public class CreateNewBrandCommandHandler : Interfaces.ICreateNewBrandCommandHandler
    {
        public IRepository Repository { get; }

        public CreateNewBrandCommandHandler(IRepository repository)
        {
            Repository = repository;
        }

        public async Task Handle(CreateNewBrandCommand command)
        {
            try
            {
                var brand = Models.Brand.Create(command.Name, command.Url);
                if (!string.IsNullOrEmpty(command.Description))
                {
                    brand.ChangeDescription(command.Description);
                }

                if (command.Logo != null)
                {
                    brand.SetLogo(command.Logo);
                }

                Repository.Add(brand);
                await Repository.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
