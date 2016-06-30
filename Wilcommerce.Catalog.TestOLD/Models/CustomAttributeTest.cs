using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    }
}
