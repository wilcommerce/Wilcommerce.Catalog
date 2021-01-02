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
    public class CategoryCommandsTest
    {
        private readonly string userId = Guid.NewGuid().ToString();

        #region Ctor tests
        [Fact]
        public void Ctor_Should_Throw_ArgumentNullException_If_Repository_Is_Null()
        {
            Repository.IRepository repository = null;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            var ex = Assert.Throws<ArgumentNullException>(() => new CategoryCommands(repository, eventBus));
            Assert.Equal(nameof(repository), ex.ParamName);
        }

        [Fact]
        public void Ctor_Should_Throw_ArgumentNullException_If_EventBus_Is_Null()
        {
            Repository.IRepository repository = new Mock<Repository.IRepository>().Object;
            Core.Infrastructure.IEventBus eventBus = null;

            var ex = Assert.Throws<ArgumentNullException>(() => new CategoryCommands(repository, eventBus));
            Assert.Equal(nameof(eventBus), ex.ParamName);
        }
        #endregion

        #region Commands tests
        [Fact]
        public async Task CreateNewCategory_Should_Create_A_New_Category_And_Return_The_Created_Category_Id()
        {
            var fakeCategoryList = new List<Category>();
            var repositoryMock = new Mock<Repository.IRepository>();
            repositoryMock.Setup(r => r.Add(It.IsAny<Category>()))
                .Callback<Category>((category) => fakeCategoryList.Add(category));

            Repository.IRepository repository = repositoryMock.Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            string code = "code";
            string name = "name";
            string url = "url";
            string description = "description";
            bool isVisible = true;
            DateTime? visibleFrom = DateTime.Today;
            DateTime? visibleTo = DateTime.Today.AddYears(1);

            var commands = new CategoryCommands(repository, eventBus);
            var categoryId = await commands.CreateNewCategory(code, name, url, description, isVisible, visibleFrom, visibleTo, userId);

            var createdCategory = fakeCategoryList.FirstOrDefault(c => c.Id == categoryId);

            Assert.NotNull(createdCategory);
            Assert.Equal(code, createdCategory.Code);
            Assert.Equal(name, createdCategory.Name);
            Assert.Equal(url, createdCategory.Url);
            Assert.Equal(description, createdCategory.Description);
            Assert.Equal(isVisible, createdCategory.IsVisible);
            Assert.Equal(visibleFrom, createdCategory.VisibleFrom);
            Assert.Equal(visibleTo, createdCategory.VisibleTo);
        }

        [Fact]
        public async Task UpdateCategoryInfo_Should_Throw_ArgumentException_If_CategoryId_Is_Empty()
        {
            Repository.IRepository repository = new Mock<Repository.IRepository>().Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid categoryId = Guid.Empty;
            string code = "code";
            string name = "name";
            string url = "url";
            string description = "description";
            bool isVisible = true;
            DateTime? visibleFrom = DateTime.Today;
            DateTime? visibleTo = DateTime.Today.AddYears(1);

            var commands = new CategoryCommands(repository, eventBus);
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.UpdateCategoryInfo(categoryId, code, name, url, description, isVisible, visibleFrom, visibleTo, userId));

            Assert.Equal(nameof(categoryId), ex.ParamName);
        }

        [Fact]
        public async Task UpdateCategoryInfo_Should_Update_Category_With_Specified_Values()
        {
            var category = Category.Create("mycode", "Category name", "category-url");

            var repositoryMock = new Mock<Repository.IRepository>();
            repositoryMock.Setup(r => r.GetByKeyAsync<Category>(It.IsAny<Guid>()))
                .Returns(Task.FromResult(category));

            Repository.IRepository repository = repositoryMock.Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid categoryId = category.Id;
            string code = "code";
            string name = "name";
            string url = "url";
            string description = "description";
            bool isVisible = true;
            DateTime? visibleFrom = DateTime.Today;
            DateTime? visibleTo = DateTime.Today.AddYears(1);

            var commands = new CategoryCommands(repository, eventBus);
            await commands.UpdateCategoryInfo(categoryId, code, name, url, description, isVisible, visibleFrom, visibleTo, userId);

            Assert.Equal(code, category.Code);
            Assert.Equal(name, category.Name);
            Assert.Equal(url, category.Url);
            Assert.Equal(description, category.Description);
            Assert.Equal(isVisible, category.IsVisible);
            Assert.Equal(visibleFrom, category.VisibleFrom);
            Assert.Equal(visibleTo, category.VisibleTo);
        }

        [Fact]
        public async Task AddCategoryChild_Should_Throw_ArgumentException_If_CategoryId_Is_Empty()
        {
            Repository.IRepository repository = new Mock<Repository.IRepository>().Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid categoryId = Guid.Empty;
            Guid childId = Guid.NewGuid();

            var commands = new CategoryCommands(repository, eventBus);
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.AddCategoryChild(categoryId, childId, userId));

            Assert.Equal(nameof(categoryId), ex.ParamName);
        }

        [Fact]
        public async Task AddCategoryChild_Should_Throw_ArgumentException_If_ChildId_Is_Empty()
        {
            Repository.IRepository repository = new Mock<Repository.IRepository>().Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid categoryId = Guid.NewGuid();
            Guid childId = Guid.Empty;

            var commands = new CategoryCommands(repository, eventBus);
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.AddCategoryChild(categoryId, childId, userId));

            Assert.Equal(nameof(childId), ex.ParamName);
        }

        [Fact]
        public async Task AddCategoryChild_Should_Add_The_Specified_Child_To_Category()
        {
            var category = Category.Create("mycode", "Category name", "category-url");
            var child = Category.Create("childcode", "Child", "child");

            var repositoryMock = new Mock<Repository.IRepository>();
            repositoryMock.Setup(r => r.GetByKeyAsync<Category>(category.Id))
                .Returns(Task.FromResult(category));

            repositoryMock.Setup(r => r.GetByKeyAsync<Category>(child.Id))
                .Returns(Task.FromResult(child));

            Repository.IRepository repository = repositoryMock.Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid categoryId = category.Id;
            Guid childId = child.Id;

            var commands = new CategoryCommands(repository, eventBus);
            await commands.AddCategoryChild(categoryId, childId, userId);

            Assert.True(category.Children.Contains(child));
        }

        [Fact]
        public async Task SetParentForCategory_Should_Throw_ArgumentException_If_CategoryId_Is_Empty()
        {
            Repository.IRepository repository = new Mock<Repository.IRepository>().Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid categoryId = Guid.Empty;
            Guid parentId = Guid.NewGuid();

            var commands = new CategoryCommands(repository, eventBus);
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.SetParentForCategory(categoryId, parentId, userId));

            Assert.Equal(nameof(categoryId), ex.ParamName);
        }

        [Fact]
        public async Task SetParentForCategory_Should_Throw_ArgumentException_If_ParentId_Is_Empty()
        {
            Repository.IRepository repository = new Mock<Repository.IRepository>().Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid categoryId = Guid.NewGuid();
            Guid parentId = Guid.Empty;

            var commands = new CategoryCommands(repository, eventBus);
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.SetParentForCategory(categoryId, parentId, userId));

            Assert.Equal(nameof(parentId), ex.ParamName);
        }

        [Fact]
        public async Task SetParentForCategory_Should_Set_The_Parent_With_The_Specified_Value()
        {
            var category = Category.Create("code", "name", "url");
            var parent = Category.Create("parent", "parent", "parent");

            var repositoryMock = new Mock<Repository.IRepository>();
            repositoryMock.Setup(r => r.GetByKeyAsync<Category>(category.Id))
                .Returns(Task.FromResult(category));

            repositoryMock.Setup(r => r.GetByKeyAsync<Category>(parent.Id))
                .Returns(Task.FromResult(parent));

            Repository.IRepository repository = repositoryMock.Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid categoryId = category.Id;
            Guid parentId = parent.Id;

            var commands = new CategoryCommands(repository, eventBus);
            await commands.SetParentForCategory(categoryId, parentId, userId);

            Assert.Equal(parent, category.Parent);
        }

        [Fact]
        public async Task DeleteCategory_Should_Throw_ArgumentException_If_CategoryId_Is_Empty()
        {
            Repository.IRepository repository = new Mock<Repository.IRepository>().Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid categoryId = Guid.Empty;

            var commands = new CategoryCommands(repository, eventBus);
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.DeleteCategory(categoryId, userId));

            Assert.Equal(nameof(categoryId), ex.ParamName);
        }

        [Fact]
        public async Task DeleteCategory_Should_Mark_Category_As_Deleted()
        {
            var category = Category.Create("code", "name", "url");

            var repositoryMock = new Mock<Repository.IRepository>();
            repositoryMock.Setup(r => r.GetByKeyAsync<Category>(It.IsAny<Guid>()))
                .Returns(Task.FromResult(category));

            Repository.IRepository repository = repositoryMock.Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid categoryId = category.Id;

            var commands = new CategoryCommands(repository, eventBus);
            await commands.DeleteCategory(categoryId, userId);

            Assert.True(category.Deleted);
        }

        [Fact]
        public async Task RestoreCategory_Should_Return_ArgumentException_If_CategoryId_Is_Empty()
        {
            Repository.IRepository repository = new Mock<Repository.IRepository>().Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid categoryId = Guid.Empty;

            var commands = new CategoryCommands(repository, eventBus);
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.RestoreCategory(categoryId, userId));

            Assert.Equal(nameof(categoryId), ex.ParamName);
        }

        [Fact]
        public async Task RestoreCategory_Should_Mark_Category_As_Restored()
        {
            var category = Category.Create("code", "name", "url");
            category.Delete();

            var repositoryMock = new Mock<Repository.IRepository>();
            repositoryMock.Setup(r => r.GetByKeyAsync<Category>(It.IsAny<Guid>()))
                .Returns(Task.FromResult(category));

            Repository.IRepository repository = repositoryMock.Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid categoryId = category.Id;

            var commands = new CategoryCommands(repository, eventBus);
            await commands.RestoreCategory(categoryId, userId);

            Assert.False(category.Deleted);
        }

        [Fact]
        public async Task RemoveChildForCategory_Should_Throw_ArgumentException_If_CategoryId_Is_Empty()
        {
            Repository.IRepository repository = new Mock<Repository.IRepository>().Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid categoryId = Guid.Empty;
            Guid childId = Guid.NewGuid();

            var commands = new CategoryCommands(repository, eventBus);
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.RemoveChildForCategory(categoryId, childId, userId));

            Assert.Equal(nameof(categoryId), ex.ParamName);
        }

        [Fact]
        public async Task RemoveChildForCategory_Should_Throw_ArgumentException_If_ChildId_Is_Empty()
        {
            Repository.IRepository repository = new Mock<Repository.IRepository>().Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid categoryId = Guid.NewGuid();
            Guid childId = Guid.Empty;

            var commands = new CategoryCommands(repository, eventBus);
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.RemoveChildForCategory(categoryId, childId, userId));

            Assert.Equal(nameof(childId), ex.ParamName);
        }

        [Fact]
        public async Task RemoveChildForCategory_Should_Remove_Child_From_The_Category_Children()
        {
            var category = Category.Create("mycode", "Category name", "category-url");
            var child = Category.Create("childcode", "Child", "child");

            category.AddChild(child);

            var repositoryMock = new Mock<Repository.IRepository>();
            repositoryMock.Setup(r => r.GetByKeyAsync<Category>(category.Id))
                .Returns(Task.FromResult(category));

            repositoryMock.Setup(r => r.GetByKeyAsync<Category>(child.Id))
                .Returns(Task.FromResult(child));

            Repository.IRepository repository = repositoryMock.Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid categoryId = category.Id;
            Guid childId = child.Id;

            var commands = new CategoryCommands(repository, eventBus);
            await commands.RemoveChildForCategory(categoryId, childId, userId);

            Assert.False(category.Children.Contains(child));
        }

        [Fact]
        public async Task RemoveParentForCategory_Should_Throw_ArgumentException_If_CategoryId_Is_Empty()
        {
            Repository.IRepository repository = new Mock<Repository.IRepository>().Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid categoryId = Guid.Empty;
            Guid parentId = Guid.NewGuid();

            var commands = new CategoryCommands(repository, eventBus);
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.RemoveParentForCategory(categoryId, parentId, userId));

            Assert.Equal(nameof(categoryId), ex.ParamName);
        }

        [Fact]
        public async Task RemoveParentForCategory_Should_Remove_The_Category_Parent()
        {
            var category = Category.Create("code", "name", "url");
            var parent = Category.Create("parent", "parent", "parent");

            category.SetParentCategory(parent);

            var repositoryMock = new Mock<Repository.IRepository>();
            repositoryMock.Setup(r => r.GetByKeyAsync<Category>(category.Id))
                .Returns(Task.FromResult(category));

            repositoryMock.Setup(r => r.GetByKeyAsync<Category>(parent.Id))
                .Returns(Task.FromResult(parent));

            Repository.IRepository repository = repositoryMock.Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid categoryId = category.Id;
            Guid parentId = parent.Id;

            var commands = new CategoryCommands(repository, eventBus);
            await commands.RemoveParentForCategory(categoryId, parentId, userId);

            Assert.Null(category.Parent);
        }

        [Fact]
        public async Task SetCategorySeoData_Should_Throw_ArgumentException_If_CategoryId_Is_Empty()
        {
            Repository.IRepository repository = new Mock<Repository.IRepository>().Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid categoryId = Guid.Empty;
            SeoData seo = new SeoData { Title = "title", Description = "description" };

            var commands = new CategoryCommands(repository, eventBus);
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.SetCategorySeoData(categoryId, seo, userId));

            Assert.Equal(nameof(categoryId), ex.ParamName);
        }

        [Fact]
        public async Task SetCategorySeoData_Should_Set_The_Seo_Data_With_The_Specified_Values()
        {
            var category = Category.Create("code", "name", "url");

            var repositoryMock = new Mock<Repository.IRepository>();
            repositoryMock.Setup(r => r.GetByKeyAsync<Category>(It.IsAny<Guid>()))
                .Returns(Task.FromResult(category));

            Repository.IRepository repository = repositoryMock.Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid categoryId = category.Id;
            SeoData seo = new SeoData { Title = "title", Description = "description" };

            var commands = new CategoryCommands(repository, eventBus);
            await commands.SetCategorySeoData(categoryId, seo, userId);

            Assert.Equal(seo.Title, category.Seo.Title);
            Assert.Equal(seo.Description, category.Seo.Description);
        }
        #endregion
    }
}
