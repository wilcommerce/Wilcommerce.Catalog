using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Wilcommerce.Catalog.Commands
{
    /// <summary>
    /// Defines all the commands related to the CustomAttribute aggregate
    /// </summary>
    public interface ICustomAttributesCommands
    {
        #region CustomAttribute Commands
        /// <summary>
        /// Create a new custom attribute
        /// </summary>
        /// <param name="name">The custom attribute's name</param>
        /// <param name="type">The data type of the custom attribute</param>
        /// <param name="description">The custom attribute's description</param>
        /// <param name="unitOfMeasure">The unit of measure of the custom attribute</param>
        /// <param name="values">The available values for the custom attribute</param>
        /// <returns>The custom attribute id</returns>
        Task<Guid> CreateNewCustomAttribute(string name, string type, string description, string unitOfMeasure, IEnumerable<object> values);

        /// <summary>
        /// Update the custom attribute's info
        /// </summary>
        /// <param name="attributeId">The custom attribute id</param>
        /// <param name="name">The custom attribute name</param>
        /// <param name="type">The custom attribute data type</param>
        /// <param name="description">The custom attribute description</param>
        /// <param name="unitOfMeasure">The custom attribute unit of measure</param>
        /// <param name="values">The custom attribute available values</param>
        /// <returns></returns>
        Task UpdateCustomAttribute(Guid attributeId, string name, string type, string description, string unitOfMeasure, IEnumerable<object> values);

        /// <summary>
        /// Delete the custom attribute
        /// </summary>
        /// <param name="attributeId">The custom attribute id</param>
        /// <returns></returns>
        Task DeleteCustomAttribute(Guid attributeId);

        /// <summary>
        /// Restore the custom attribute
        /// </summary>
        /// <param name="attributeId">The custom attribute id</param>
        /// <returns></returns>
        Task RestoreCustomAttribute(Guid attributeId);
        #endregion
    }
}
