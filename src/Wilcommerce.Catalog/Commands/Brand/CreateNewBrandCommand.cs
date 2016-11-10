using Wilcommerce.Core.Common.Domain.Models;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Commands.Brand
{
    public class CreateNewBrandCommand : ICommand
    {
        public string Name { get; }

        public string Url { get; }

        public string Description { get; }

        public Image Logo { get; }

        public CreateNewBrandCommand(string name, string url, string description, Image logo)
        {
            Name = name;
            Url = url;
            Description = description;
            Logo = logo;
        }
    }
}
