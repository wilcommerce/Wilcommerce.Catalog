using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wilcommerce.Catalog.Commands;
using Wilcommerce.Catalog.Models;
using Xunit;

namespace Wilcommerce.Catalog.Test.Commands
{
    public class CustomAttributeCommandsTest
    {
        private readonly string userId = Guid.NewGuid().ToString();

        #region Ctor tests
        [Fact]
        public void Ctor_Should_Throw_ArgumentNullException_If_Repository_Is_Null()
        {
            Repository.IRepository repository = null;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            var ex = Assert.Throws<ArgumentNullException>(() => new CustomAttributeCommands(repository, eventBus));
            Assert.Equal(nameof(repository), ex.ParamName);
        }

        [Fact]
        public void Ctor_Should_Throw_ArgumentNullException_If_EventBus_Is_Null()
        {
            Repository.IRepository repository = new Mock<Repository.IRepository>().Object;
            Core.Infrastructure.IEventBus eventBus = null;

            var ex = Assert.Throws<ArgumentNullException>(() => new CustomAttributeCommands(repository, eventBus));
            Assert.Equal(nameof(eventBus), ex.ParamName);
        }
        #endregion

        #region Commands tests
        [Fact]
        public async Task CreateNewCustomAttribute_Should_Create_A_New_Custom_Attribute_And_Return_The_Created_Attribute_Id()
        {
            var fakeCustomAttributeList = new List<CustomAttribute>();
            var repositoryMock = new Mock<Repository.IRepository>();
            repositoryMock.Setup(r => r.Add(It.IsAny<CustomAttribute>()))
                .Callback<CustomAttribute>((attribute) => fakeCustomAttributeList.Add(attribute));

            Repository.IRepository repository = repositoryMock.Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            string name = "Size";
            string type = "string";
            string description = "size description";
            string unitOfMeasure = "unit";
            IEnumerable<object> values = new[] { "XS", "S", "M", "L", "XL" };

            var commands = new CustomAttributeCommands(repository, eventBus);
            var attributeId = await commands.CreateNewCustomAttribute(name, type, description, unitOfMeasure, values, userId);

            var createdAttribute = fakeCustomAttributeList.FirstOrDefault(a => a.Id == attributeId);

            Assert.Equal(name, createdAttribute.Name);
            Assert.Equal(type, createdAttribute.DataType);
            Assert.Equal(description, createdAttribute.Description);
            Assert.Equal(unitOfMeasure, createdAttribute.UnitOfMeasure);
            Assert.Equal(values, createdAttribute.Values);
        }

        [Fact]
        public async Task UpdateCustomAttribute_Should_Throw_ArgumentException_If_AttributeId_Is_Empty()
        {
            Repository.IRepository repository = new Mock<Repository.IRepository>().Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid attributeId = Guid.Empty;
            string name = "Size";
            string type = "string";
            string description = "size description";
            string unitOfMeasure = "unit";
            IEnumerable<object> values = new[] { "XS", "S", "M", "L", "XL" };

            var commands = new CustomAttributeCommands(repository, eventBus);

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.UpdateCustomAttribute(attributeId, name, type, description, unitOfMeasure, values, userId));
            Assert.Equal(nameof(attributeId), ex.ParamName);
        }

        [Fact]
        public async Task UpdateCustomAttribute_Should_Update_Custom_Attribute_With_Specified_Values()
        {
            var attribute = CustomAttribute.Create("attribute", "number");
            var repositoryMock = new Mock<Repository.IRepository>();
            repositoryMock.Setup(r => r.GetByKeyAsync<CustomAttribute>(It.IsAny<Guid>()))
                .Returns(Task.FromResult(attribute));

            Repository.IRepository repository = repositoryMock.Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid attributeId = attribute.Id;
            string name = "Size";
            string type = "string";
            string description = "size description";
            string unitOfMeasure = "unit";
            IEnumerable<object> values = new[] { "XS", "S", "M", "L", "XL" };

            var commands = new CustomAttributeCommands(repository, eventBus);
            await commands.UpdateCustomAttribute(attributeId, name, type, description, unitOfMeasure, values, userId);

            Assert.Equal(name, attribute.Name);
            Assert.Equal(type, attribute.DataType);
            Assert.Equal(description, attribute.Description);
            Assert.Equal(unitOfMeasure, attribute.UnitOfMeasure);
            Assert.Equal(values, attribute.Values);
        }

        [Fact]
        public async Task DeleteCustomAttribute_Should_Throw_ArgumentException_If_AttributeId_Is_Empty()
        {
            Repository.IRepository repository = new Mock<Repository.IRepository>().Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid attributeId = Guid.Empty;

            var commands = new CustomAttributeCommands(repository, eventBus);

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.DeleteCustomAttribute(attributeId, userId));
            Assert.Equal(nameof(attributeId), ex.ParamName);
        }

        [Fact]
        public async Task DeleteCustomAttribute_Should_Mark_CustomAttribute_As_Deleted()
        {
            var attribute = CustomAttribute.Create("attribute", "number");
            var repositoryMock = new Mock<Repository.IRepository>();
            repositoryMock.Setup(r => r.GetByKeyAsync<CustomAttribute>(It.IsAny<Guid>()))
                .Returns(Task.FromResult(attribute));

            Repository.IRepository repository = repositoryMock.Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid attributeId = attribute.Id;

            var commands = new CustomAttributeCommands(repository, eventBus);
            await commands.DeleteCustomAttribute(attributeId, userId);

            Assert.True(attribute.Deleted);
        }

        [Fact]
        public async Task RestoreCustomAttribute_Should_Throw_ArgumentException_If_AttributeId_Is_Empty()
        {
            Repository.IRepository repository = new Mock<Repository.IRepository>().Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid attributeId = Guid.Empty;

            var commands = new CustomAttributeCommands(repository, eventBus);

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commands.RestoreCustomAttribute(attributeId, userId));
            Assert.Equal(nameof(attributeId), ex.ParamName);
        }

        [Fact]
        public async Task RestoreCustomAttribute_Should_Mark_CustomAttribute_As_Restored()
        {
            var attribute = CustomAttribute.Create("attribute", "number");
            attribute.Delete();

            var repositoryMock = new Mock<Repository.IRepository>();
            repositoryMock.Setup(r => r.GetByKeyAsync<CustomAttribute>(It.IsAny<Guid>()))
                .Returns(Task.FromResult(attribute));

            Repository.IRepository repository = repositoryMock.Object;
            Core.Infrastructure.IEventBus eventBus = new Mock<Core.Infrastructure.IEventBus>().Object;

            Guid attributeId = attribute.Id;

            var commands = new CustomAttributeCommands(repository, eventBus);
            await commands.RestoreCustomAttribute(attributeId, userId);

            Assert.False(attribute.Deleted);
        }
        #endregion
    }
}
