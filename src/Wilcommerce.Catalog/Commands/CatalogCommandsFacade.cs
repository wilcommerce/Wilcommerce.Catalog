using System;
using System.Threading.Tasks;
using Wilcommerce.Core.Common.Domain.Models;
using Wilcommerce.Catalog.Repository;
using Wilcommerce.Catalog.Models;
using System.Collections.Generic;
using System.Linq;

namespace Wilcommerce.Catalog.Commands
{
    public class CatalogCommandsFacade : ICatalogCommandsFacade
    {
        public IRepository Repository { get; }

        public CatalogCommandsFacade(IRepository repository)
        {
            Repository = repository;
        }

        #region Brand Commands
        public async Task ChangeBrandDescription(Guid brandId, string description)
        {
            try
            {
                var brand = await Repository.GetByKeyAsync<Brand>(brandId);
                brand.ChangeDescription(description);

                await Repository.SaveChangesAsync();
            }
            catch 
            {
                throw;
            }
        }

        public async Task ChangeBrandName(Guid brandId, string name)
        {
            try
            {
                var brand = await Repository.GetByKeyAsync<Brand>(brandId);
                brand.ChangeName(name);

                await Repository.SaveChangesAsync();
            }
            catch 
            {
                throw;
            }
        }

        public async Task ChangeBrandUrl(Guid brandId, string url)
        {
            try
            {
                var brand = await Repository.GetByKeyAsync<Brand>(brandId);
                brand.ChangeUrl(url);

                await Repository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task CreateNewBrand(string name, string url, string description, Image logo)
        {
            try
            {
                var brand = Brand.Create(name, url);
                if (!string.IsNullOrEmpty(description))
                {
                    brand.ChangeDescription(description);
                }

                if (logo != null)
                {
                    brand.SetLogo(logo);
                }

                Repository.Add(brand);
                await Repository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task DeleteBrand(Guid brandId)
        {
            try
            {
                var brand = await Repository.GetByKeyAsync<Brand>(brandId);
                brand.Delete();

                await Repository.SaveChangesAsync();
            }
            catch 
            {
                throw;
            }
        }

        public async Task RestoreBrand(Guid brandId)
        {
            try
            {
                var brand = await Repository.GetByKeyAsync<Brand>(brandId);
                brand.Restore();

                await Repository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task SetBrandLogo(Guid brandId, Image logo)
        {
            try
            {
                var brand = await Repository.GetByKeyAsync<Brand>(brandId);
                brand.SetLogo(logo);

                await Repository.SaveChangesAsync();
            }
            catch 
            {
                throw;
            }
        }

        #endregion

        #region Category Commands
        public async Task CreateNewCategory(string code, string name, string url, string description, bool isVisible, DateTime? visibleFrom, DateTime? visibleTo)
        {
            try
            {
                var category = Category.Create(code, name, url);
                if (!string.IsNullOrEmpty(description))
                {
                    category.ChangeDescription(description);
                }

                if (isVisible)
                {
                    if (visibleFrom == null)
                    {
                        category.SetAsVisible();
                    }
                    else if (visibleTo == null)
                    {
                        category.SetAsVisible((DateTime)visibleFrom);
                    }
                    else
                    {
                        category.SetAsVisible((DateTime)visibleFrom, (DateTime)visibleTo);
                    }
                }

                Repository.Add(category);
                await Repository.SaveChangesAsync();
            }
            catch 
            {
                throw;
            }
        }

        public async Task SetCategoryAsVisible(Guid categoryId, DateTime? from, DateTime? to)
        {
            try
            {
                var category = await Repository.GetByKeyAsync<Category>(categoryId);
                if (from == null)
                {
                    category.SetAsVisible();
                }
                else if (to == null)
                {
                    category.SetAsVisible((DateTime)from);
                }
                else
                {
                    category.SetAsVisible((DateTime)from, (DateTime)to);
                }

                await Repository.SaveChangesAsync();
            }
            catch 
            {
                throw;
            }
        }

        public async Task AddCategoryChild(Guid categoryId, Guid childId)
        {
            try
            {
                var category = await Repository.GetByKeyAsync<Category>(categoryId);
                var child = await Repository.GetByKeyAsync<Category>(childId);

                category.AddChild(child);
                await Repository.SaveChangesAsync();
            }
            catch 
            {
                throw;
            }
        }

        public async Task ChangeCategoryName(Guid categoryId, string name)
        {
            try
            {
                var category = await Repository.GetByKeyAsync<Category>(categoryId);
                category.ChangeName(name);

                await Repository.SaveChangesAsync();
            }
            catch 
            {
                throw;
            }
        }

        public async Task ChangeCategoryCode(Guid categoryId, string code)
        {
            try
            {
                var category = await Repository.GetByKeyAsync<Category>(categoryId);
                category.ChangeCode(code);

                await Repository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task ChangeCategoryDescription(Guid categoryId, string description)
        {
            try
            {
                var category = await Repository.GetByKeyAsync<Category>(categoryId);
                category.ChangeDescription(description);

                await Repository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task ChangeCategoryUrl(Guid categoryId, string url)
        {
            try
            {
                var category = await Repository.GetByKeyAsync<Category>(categoryId);
                category.ChangeUrl(url);

                await Repository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task SetParentForCategory(Guid categoryId, Guid parentId)
        {
            try
            {
                var category = await Repository.GetByKeyAsync<Category>(categoryId);
                var parent = await Repository.GetByKeyAsync<Category>(parentId);
                category.SetParentCategory(parent);

                await Repository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task DeleteCategory(Guid categoryId)
        {
            try
            {
                var category = await Repository.GetByKeyAsync<Category>(categoryId);
                category.Delete();

                await Repository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task RestoreCategory(Guid categoryId)
        {
            try
            {
                var category = await Repository.GetByKeyAsync<Category>(categoryId);
                category.Restore();

                await Repository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task RemoveChildForCategory(Guid categoryId, Guid childId)
        {
            try
            {
                var category = await Repository.GetByKeyAsync<Category>(categoryId);
                category.RemoveChild(childId);

                await Repository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task RemoveParentForCategory(Guid categoryId)
        {
            try
            {
                var category = await Repository.GetByKeyAsync<Category>(categoryId);
                category.RemoveParent();

                await Repository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region CustomAttribute Commands
        public async Task CreateNewCustomAttribute(string name, string type, string description, string unitOfMeasure, IEnumerable<object> values)
        {
            try
            {
                var attribute = CustomAttribute.Create(name, type);
                if (!string.IsNullOrEmpty(description))
                {
                    attribute.ChangeDescription(description);
                }

                if (!string.IsNullOrEmpty(unitOfMeasure))
                {
                    attribute.SetUnitOfMeasure(unitOfMeasure);
                }

                if(values != null && values.Count() > 0)
                {
                    values.ToList().ForEach(v => attribute.AddValue(v));
                }

                Repository.Add(attribute);
                await Repository.SaveChangesAsync();
            }
            catch 
            {
                throw;
            }
        }

        public async Task AddValueForAttribute(Guid attributeId, object value)
        {
            try
            {
                var attribute = await Repository.GetByKeyAsync<CustomAttribute>(attributeId);
                attribute.AddValue(value);

                await Repository.SaveChangesAsync();
            }
            catch 
            {
                throw;
            }
        }

        public async Task RemoveValueFromAttribute(Guid attributeId, object value)
        {
            try
            {
                var attribute = await Repository.GetByKeyAsync<CustomAttribute>(attributeId);
                attribute.RemoveValue(value);

                await Repository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task ChangeAttributeName(Guid attributeId, string name)
        {
            try
            {
                var attribute = await Repository.GetByKeyAsync<CustomAttribute>(attributeId);
                attribute.ChangeName(name);

                await Repository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task ChangeAttributeDescription(Guid attributeId, string description)
        {
            try
            {
                var attribute = await Repository.GetByKeyAsync<CustomAttribute>(attributeId);
                attribute.ChangeDescription(description);

                await Repository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task SetAttributeUnitOfMeasure(Guid attributeId, string unitOfMeasure)
        {
            try
            {
                var attribute = await Repository.GetByKeyAsync<CustomAttribute>(attributeId);
                attribute.SetUnitOfMeasure(unitOfMeasure);

                await Repository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task ChangeAttributeDataType(Guid attributeId, string dataType)
        {
            try
            {
                var attribute = await Repository.GetByKeyAsync<CustomAttribute>(attributeId);
                attribute.ChangeDataType(dataType);

                await Repository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task DeleteCustomAttribute(Guid attributeId)
        {
            try
            {
                var attribute = await Repository.GetByKeyAsync<CustomAttribute>(attributeId);
                attribute.Delete();

                await Repository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task RestoreCustomAttribute(Guid attributeId)
        {
            try
            {
                var attribute = await Repository.GetByKeyAsync<CustomAttribute>(attributeId);
                attribute.Restore();

                await Repository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }
        #endregion
    }
}
