using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wilcommerce.Catalog.Commands;
using Wilcommerce.Catalog.Models;
using Wilcommerce.Core.Common.Models;
using Xunit;

namespace Wilcommerce.Catalog.Test.Commands
{
    public class BrandCommandsTest
    {
        private readonly string userId = Guid.NewGuid().ToString();

        #region Ctor tests
        [Fact]
        public void Ctor_Should_Throw_ArgumentNullException_If_Repository_Is_Null()
        {
            Repository.IRepository repository = null;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            var ex = Assert.Throws<ArgumentNullException>(() => new BrandCommands(repository, eventBus));
            Assert.Equal(nameof(repository), ex.ParamName);
        }

        [Fact]
        public void Ctor_Should_Throw_ArgumentNullException_If_EventBus_Is_Null()
        {
            Repository.IRepository repository = new Mock<Repository.IRepository>().Object;
            Core.Infrastructure.IEventBus eventBus = null;

            var ex = Assert.Throws<ArgumentNullException>(() => new BrandCommands(repository, eventBus));
            Assert.Equal(nameof(eventBus), ex.ParamName);
        }
        #endregion

        #region Commands tests
        [Fact]
        public async Task CreateNewBrand_Should_Create_A_New_Brand_And_Return_The_Created_Brand_Id()
        {
            var fakeBrandList = new List<Brand>();
            var repositoryMock = new Mock<Repository.IRepository>();
            repositoryMock.Setup(r => r.Add(It.IsAny<Brand>()))
                .Callback<Brand>((brand) => fakeBrandList.Add(brand));

            Repository.IRepository repository = repositoryMock.Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            string name = "brand";
            string url = "url";
            string description = "description";
            Image logo = new Image { MimeType = "image/jpeg", Data = new byte[0] };

            var commands = new BrandCommands(repository, eventBus);
            var brandId = await commands.CreateNewBrand(name, url, description, logo, userId);

            var createdBrand = fakeBrandList.FirstOrDefault(b => b.Id == brandId);
            
            Assert.NotNull(createdBrand);
            Assert.Equal(name, createdBrand.Name);
            Assert.Equal(url, createdBrand.Url);
            Assert.Equal(description, createdBrand.Description);
            Assert.Equal(logo, createdBrand.Logo);
        }

        [Fact]
        public async Task UpdateBrandInfo_Should_Throw_ArgumentException_If_BrandId_Is_Empty()
        {
            Repository.IRepository repository = new Mock<Repository.IRepository>().Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid brandId = Guid.Empty;
            string name = "brand";
            string url = "url";
            string description = "description";
            Image logo = new Image { MimeType = "image/jpeg", Data = new byte[0] };

            var commands = new BrandCommands(repository, eventBus);
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.UpdateBrandInfo(brandId, name, url, description, logo, userId));

            Assert.Equal(nameof(brandId), ex.ParamName);
        }

        [Fact]
        public async Task UpdateBrandInfo_Should_Update_Brand_With_Specified_Values()
        {
            var brand = Brand.Create("brand to edit", "brand-to-edit");
            
            var repositoryMock = new Mock<Repository.IRepository>();
            repositoryMock.Setup(r => r.GetByKeyAsync<Brand>(It.IsAny<Guid>()))
                .Returns(Task.FromResult(brand));

            Repository.IRepository repository = repositoryMock.Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid brandId = brand.Id;
            string name = "brand";
            string url = "url";
            string description = "description";
            Image logo = new Image { MimeType = "image/jpeg", Data = new byte[0] };

            var commands = new BrandCommands(repository, eventBus);
            await commands.UpdateBrandInfo(brandId, name, url, description, logo, userId);

            Assert.Equal(name, brand.Name);
            Assert.Equal(url, brand.Url);
            Assert.Equal(description, brand.Description);
            Assert.Equal(logo, brand.Logo);
        }

        [Fact]
        public async Task SetBrandSeoData_Should_Throw_ArgumentException_If_BrandId_Is_Empty()
        {
            Repository.IRepository repository = new Mock<Repository.IRepository>().Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid brandId = Guid.Empty;
            SeoData seo = new SeoData { Title = "title", Description = "description" };

            var commands = new BrandCommands(repository, eventBus);
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.SetBrandSeoData(brandId, seo, userId));

            Assert.Equal(nameof(brandId), ex.ParamName);
        }

        [Fact]
        public async Task SetBrandSeoData_Should_Set_SeoData_With_Specified_Values()
        {
            var brand = Brand.Create("brand to edit", "brand-to-edit");

            var repositoryMock = new Mock<Repository.IRepository>();
            repositoryMock.Setup(r => r.GetByKeyAsync<Brand>(It.IsAny<Guid>()))
                .Returns(Task.FromResult(brand));

            Repository.IRepository repository = repositoryMock.Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid brandId = brand.Id;
            SeoData seo = new SeoData { Title = "title", Description = "description" };

            var commands = new BrandCommands(repository, eventBus);
            await commands.SetBrandSeoData(brandId, seo, userId);

            Assert.NotNull(brand.Seo);
            Assert.Equal(seo.Title, brand.Seo.Title);
            Assert.Equal(seo.Description, brand.Seo.Description);
        }

        [Fact]
        public async Task DeleteBrand_Should_Throw_ArgumentException_If_BrandId_Is_Empty()
        {
            Repository.IRepository repository = new Mock<Repository.IRepository>().Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid brandId = Guid.Empty;

            var commands = new BrandCommands(repository, eventBus);
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.DeleteBrand(brandId, userId));

            Assert.Equal(nameof(brandId), ex.ParamName);
        }

        [Fact]
        public async Task DeleteBrand_Should_Mark_Brand_As_Deleted()
        {
            var brand = Brand.Create("brand to edit", "brand-to-edit");

            var repositoryMock = new Mock<Repository.IRepository>();
            repositoryMock.Setup(r => r.GetByKeyAsync<Brand>(It.IsAny<Guid>()))
                .Returns(Task.FromResult(brand));

            Repository.IRepository repository = repositoryMock.Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid brandId = brand.Id;

            var commands = new BrandCommands(repository, eventBus);
            await commands.DeleteBrand(brandId, userId);

            Assert.True(brand.Deleted);
        }

        [Fact]
        public async Task RestoreBrand_Should_Throw_ArgumentException_If_BrandId_Is_Empty()
        {
            Repository.IRepository repository = new Mock<Repository.IRepository>().Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid brandId = Guid.Empty;

            var commands = new BrandCommands(repository, eventBus);
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.RestoreBrand(brandId, userId));

            Assert.Equal(nameof(brandId), ex.ParamName);
        }

        [Fact]
        public async Task RestoreBrand_Should_Mark_Brand_As_Restored()
        {
            var brand = Brand.Create("brand to edit", "brand-to-edit");
            brand.Delete();

            var repositoryMock = new Mock<Repository.IRepository>();
            repositoryMock.Setup(r => r.GetByKeyAsync<Brand>(It.IsAny<Guid>()))
                .Returns(Task.FromResult(brand));

            Repository.IRepository repository = repositoryMock.Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid brandId = brand.Id;

            var commands = new BrandCommands(repository, eventBus);
            await commands.RestoreBrand(brandId, userId);

            Assert.False(brand.Deleted);
        }
        #endregion
    }
}
