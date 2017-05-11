using System;
using System.Linq;
using Wilcommerce.Catalog.Models;
using Xunit;

namespace Wilcommerce.Catalog.Test.Models
{
    public class CustomAttributeTest
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void CustomAttributeFactory_Should_Throw_ArgumentNullException_If_Name_IsEmpty(string value)
        {
            var ex = Assert.Throws<ArgumentNullException>(() => CustomAttribute.Create(
                value,
                "string"
                ));

            Assert.Equal("name", ex.ParamName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void CustomAttributeFactory_Should_Throw_ArgumentNullException_If_Type_IsEmpty(string value)
        {
            var ex = Assert.Throws<ArgumentNullException>(() => CustomAttribute.Create(
                "Attribute",
                value
                ));

            Assert.Equal("type", ex.ParamName);
        }

        [Fact]
        public void AddValue_Should_Throw_ArgumentNullException_If_Value_IsNull()
        {
            var attribute = CustomAttribute.Create(
                "Attribute",
                "string"
                );

            var ex = Assert.Throws<ArgumentNullException>(() => attribute.AddValue(null));
            Assert.Equal("value", ex.ParamName);
        }

        [Fact]
        public void AddValue_Should_Throw_ArgumentException_If_Value_Is_Already_In_Collection()
        {
            var attribute = CustomAttribute.Create(
                "Attribute",
                "string"
                );

            string value = "My value";
            attribute.AddValue(value);

            var ex = Assert.Throws<ArgumentException>(() => attribute.AddValue(value));
            Assert.Equal("value", ex.ParamName);
        }

        [Fact]
        public void AddValue_Should_Increment_Values_Number()
        {
            var attribute = CustomAttribute.Create(
                "Attribute",
                "string"
                );

            int valuesCount = attribute.Values == null ? 0 : attribute.Values.Count();

            string value = "My value";
            attribute.AddValue(value);

            Assert.Equal(valuesCount + 1, attribute.Values.Count());
        }

        [Fact]
        public void RemoveValue_Should_Throw_ArgumentNullException_If_Value_IsNull()
        {
            var attribute = CustomAttribute.Create(
                "Attribute",
                "string"
                );

            var ex = Assert.Throws<ArgumentNullException>(() => attribute.RemoveValue(null));
            Assert.Equal("value", ex.ParamName);
        }

        [Fact]
        public void RemoveValue_Should_Throw_InvalidOperationException_If_Values_IsNull()
        {
            var attribute = CustomAttribute.Create(
                "Attribute",
                "string"
                );

            string value = "My value";

            var ex = Assert.Throws<InvalidOperationException>(() => attribute.RemoveValue(value));
            Assert.Equal("Cannot remove item from empty list", ex.Message);
        }

        [Fact]
        public void RemoveValue_Should_Throw_ArgumentException_If_Value_Is_Not_In_Collection()
        {
            var attribute = CustomAttribute.Create(
                "Attribute",
                "string"
                );

            attribute.AddValue("first value");
            string value = "My value";

            var ex = Assert.Throws<ArgumentException>(() => attribute.RemoveValue(value));
            Assert.Equal("value", ex.ParamName);
        }

        [Fact]
        public void RemoveValue_Should_Decrement_Values_Number()
        {
            var attribute = CustomAttribute.Create(
                "Attribute",
                "string"
                );

            string value = "My value";
            attribute.AddValue(value);
            attribute.AddValue("second value");

            int valuesCount = attribute.Values == null ? 0 : attribute.Values.Count();
            attribute.RemoveValue(value);

            Assert.Equal(valuesCount - 1, attribute.Values.Count());
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ChangeName_Should_Throw_ArgumentNullException_If_Name_IsEmpty(string value)
        {
            var attribute = CustomAttribute.Create(
                "Attribute",
                "string"
                );

            var ex = Assert.Throws<ArgumentNullException>(() => attribute.ChangeName(value));
            Assert.Equal("name", ex.ParamName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ChangeDescription_Should_Throw_ArgumentNullException_If_Description_IsEmpty(string value)
        {
            var attribute = CustomAttribute.Create(
                "Attribute",
                "string"
                );

            var ex = Assert.Throws<ArgumentNullException>(() => attribute.ChangeDescription(value));
            Assert.Equal("description", ex.ParamName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void SetUnitOfMeasure_Should_Throw_ArgumentNullException_If_UnitOfMeasure_IsEmpty(string value)
        {
            var attribute = CustomAttribute.Create(
                "Attribute",
                "string"
                );

            var ex = Assert.Throws<ArgumentNullException>(() => attribute.SetUnitOfMeasure(value));
            Assert.Equal("unitOfMeasure", ex.ParamName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ChangeDataType_Should_Throw_ArgumentNullException_If_DataType_IsEmpty(string value)
        {
            var attribute = CustomAttribute.Create(
                "Attribute",
                "string"
                );

            var ex = Assert.Throws<ArgumentNullException>(() => attribute.ChangeDataType(value));
            Assert.Equal("dataType", ex.ParamName);
        }

        [Fact]
        public void Delete_Should_Throw_InvalidOperationException_If_Attribute_IsDeleted()
        {
            var attribute = CustomAttribute.Create(
                "Attribute",
                "string"
                );

            attribute.Delete();

            var ex = Assert.Throws<InvalidOperationException>(() => attribute.Delete());
            Assert.Equal("The attribute is already deleted", ex.Message);
        }

        [Fact]
        public void Restore_Should_Throw_InvalidOperationException_If_Attribute_IsNotDeleted()
        {
            var attribute = CustomAttribute.Create(
                "Attribute",
                "string"
                );

            var ex = Assert.Throws<InvalidOperationException>(() => attribute.Restore());
            Assert.Equal("The attribute is not deleted", ex.Message);
        }
    }
}
