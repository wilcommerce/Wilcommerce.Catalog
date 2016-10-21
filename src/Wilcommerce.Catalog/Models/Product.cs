using System;
using System.Collections.Generic;
using System.Linq;
using Wilcommerce.Core.Common.Domain.Models;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Models
{
    /// <summary>
    /// Represents a product
    /// </summary>
    public class Product : IAggregateRoot
    {
        /// <summary>
        /// Get or set the product's id
        /// </summary>
        public Guid Id { get; set; }

        #region Constructor
        protected Product()
        {
            _Variants = new HashSet<Product>();
            _Categories = new HashSet<ProductCategory>();
            _TierPrices = new HashSet<TierPrice>();
            _Attributes = new HashSet<ProductAttribute>();
            _Reviews = new HashSet<ProductReview>();
            _Images = new HashSet<ProductImage>();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Get or set the EAN code for the product
        /// </summary>
        public string EanCode { get; protected set; }

        /// <summary>
        /// Get or set the SKU code for the product
        /// </summary>
        public string Sku { get; protected set; }

        /// <summary>
        /// Get or set the product's name
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Get or set the product's url
        /// </summary>
        public string Url { get; protected set; }

        /// <summary>
        /// Get or set the product's description
        /// </summary>
        public string Description { get; protected set; }

        /// <summary>
        /// Get or set the product's price
        /// </summary>
        public Currency Price { get; protected set; }

        /// <summary>
        /// Get or set the number of units in stock
        /// </summary>
        public int UnitInStock { get; protected set; }

        /// <summary>
        /// Get or set whether the product is on sale
        /// </summary>
        public bool IsOnSale { get; protected set; }

        /// <summary>
        /// Get or set the date and time from when the product is on sale
        /// </summary>
        public DateTime? OnSaleFrom { get; protected set; }

        /// <summary>
        /// Get or set the date and time till when the product is on sale
        /// </summary>
        public DateTime? OnSaleTo { get; protected set; }

        /// <summary>
        /// Get or set the product's variants
        /// </summary>
        protected virtual ICollection<Product> _Variants { get; set; }

        /// <summary>
        /// Get the product's variants
        /// </summary>
        public IEnumerable<Product> Variants => _Variants;

        /// <summary>
        /// Get or set the main product
        /// </summary>
        public virtual Product MainProduct { get; protected set; }

        /// <summary>
        /// Get or set the product's vendor
        /// </summary>
        public virtual Brand Vendor { get; protected set; }

        /// <summary>
        /// Get or set the product's categories
        /// </summary>
        protected virtual ICollection<ProductCategory> _Categories { get; set; }

        /// <summary>
        /// Get the product's category
        /// </summary>
        public IEnumerable<Category> Categories => _Categories.Select(p => p.Category);

        /// <summary>
        /// Get the main category for the product
        /// </summary>
        public Category MainCategory => _Categories.FirstOrDefault(c => c.IsMain)?.Category;

        /// <summary>
        /// Get or set the product's custom attributes
        /// </summary>
        protected virtual ICollection<ProductAttribute> _Attributes { get; set; }

        /// <summary>
        /// Get the product's custom attributes
        /// </summary>
        public IEnumerable<ProductAttribute> Attributes => _Attributes;

        /// <summary>
        /// Get or set whether the product is deleted
        /// </summary>
        public bool Deleted { get; protected set; }

        /// <summary>
        /// Get or set whether the tier prices are enabled for the product
        /// </summary>
        public bool TierPriceEnabled { get; protected set; }

        /// <summary>
        /// Get or set the product's tier prices
        /// </summary>
        protected virtual ICollection<TierPrice> _TierPrices { get; set; }

        /// <summary>
        /// Get the product's tier prices
        /// </summary>
        public IEnumerable<TierPrice> TierPrices => _TierPrices;

        /// <summary>
        /// Get or set the product's reviews
        /// </summary>
        protected virtual ICollection<ProductReview> _Reviews { get; set; }

        /// <summary>
        /// Get the product's reviews
        /// </summary>
        public IEnumerable<ProductReview> Reviews => _Reviews;

        /// <summary>
        /// Get or set the product's images
        /// </summary>
        protected virtual ICollection<ProductImage> _Images { get; set; }

        /// <summary>
        /// Get the product's images
        /// </summary>
        public IEnumerable<ProductImage> Images => _Images;

        #endregion

        #region Behaviors
        /// <summary>
        /// Delete the product
        /// </summary>
        public virtual void Delete()
        {
            if (Deleted)
            {
                throw new InvalidOperationException("Product already deleted");
            }

            Deleted = true;
        }

        /// <summary>
        /// Restore the deleted protected
        /// </summary>
        public virtual void Restore()
        {
            if (!Deleted)
            {
                throw new InvalidOperationException("Product is not deleted");
            }

            Deleted = false;
        }

        /// <summary>
        /// Set the unit in stock for the product
        /// </summary>
        /// <param name="unitInStock">The product's unit in stock</param>
        public virtual void SetUnitInStock(int unitInStock)
        {
            if(unitInStock < 0)
            {
                throw new ArgumentException("Stock unit cannot be less than zero");
            }

            UnitInStock = unitInStock;
        }

        /// <summary>
        /// Add product unit to the stock
        /// </summary>
        /// <param name="unit">The number of unit to add</param>
        public virtual void AddUnitInStock(int unit)
        {
            if (unit < 0)
            {
                throw new ArgumentException("Stock unit cannot be less than zero");
            }

            SetUnitInStock(UnitInStock + unit);
        }

        /// <summary>
        /// Remove product unit from stock
        /// </summary>
        /// <param name="unit">The number of unit to remove</param>
        public virtual void RemoveUnitFromStock(int unit)
        {
            if (unit < 0)
            {
                throw new ArgumentException("Stock unit cannot be less than zero");
            }

            int newUnitInStock = UnitInStock - unit;
            if (newUnitInStock < 0)
            {
                throw new InvalidOperationException("Stock unit cannot be less than zero");
            }

            SetUnitInStock(newUnitInStock);
        }

        /// <summary>
        /// Change the product EAN code
        /// </summary>
        /// <param name="ean">The product's ean code</param>
        public virtual void ChangeEanCode(string ean)
        {
            if (string.IsNullOrEmpty(ean))
            {
                throw new ArgumentNullException("ean");
            }

            EanCode = ean;
        }

        /// <summary>
        /// Change the product SKU
        /// </summary>
        /// <param name="sku">The product's sku</param>
        public virtual void ChangeSku(string sku)
        {
            if (string.IsNullOrEmpty(sku))
            {
                throw new ArgumentNullException("sku");
            }

            Sku = sku;
        }

        /// <summary>
        /// Change the product name
        /// </summary>
        /// <param name="name">The product's name</param>
        public virtual void ChangeName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }

            Name = name;
        }

        /// <summary>
        /// Change the product description
        /// </summary>
        /// <param name="description">The product's description</param>
        public virtual void ChangeDescription(string description)
        {
            if (string.IsNullOrEmpty(description))
            {
                throw new ArgumentNullException("description");
            }

            Description = description;
        }

        /// <summary>
        /// Change the product url
        /// </summary>
        /// <param name="url">The product's url</param>
        public virtual void ChangeUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }

            Url = url;
        }

        /// <summary>
        /// Set the product price
        /// </summary>
        /// <param name="price">The new price</param>
        public virtual void SetPrice(Currency price)
        {
            if (price == null)
            {
                throw new ArgumentNullException("price");
            }

            if (price.Amount < 0)
            {
                throw new ArgumentException("Price amount cannot be less than zero");
            }

            Price = price;
        }

        /// <summary>
        /// Enables the tier prices
        /// </summary>
        public virtual void EnableTierPrices()
        {
            if (TierPriceEnabled)
            {
                throw new InvalidOperationException("Tier prices already enabled");
            }

            TierPriceEnabled = true;
        }

        /// <summary>
        /// Disable the tier prices
        /// </summary>
        public virtual void DisableTierPrices()
        {
            if (!TierPriceEnabled)
            {
                throw new InvalidOperationException("Tier prices already disabled");
            }

            TierPriceEnabled = false;
        }

        /// <summary>
        /// Set the product vendor
        /// </summary>
        /// <param name="vendor">The product's vendor</param>
        public virtual void SetVendor(Brand vendor)
        {
            if (vendor == null)
            {
                throw new ArgumentNullException("vendor");
            }

            Vendor = vendor;
        }

        /// <summary>
        /// Set the product on sale
        /// </summary>
        public virtual void SetOnSale()
        {
            SetOnSale(DateTime.Now);
        }

        /// <summary>
        /// Set the product on sale starting from the specified date and time
        /// </summary>
        /// <param name="onSaleFrom">The date and time from when the product will be on sale</param>
        public virtual void SetOnSale(DateTime onSaleFrom)
        {
            IsOnSale = true;
            OnSaleFrom = onSaleFrom;
        }

        /// <summary>
        /// Set the product on sale, starting from the specified start date and till the specified end date
        /// </summary>
        /// <param name="onSaleFrom">The date and time from when the product will be on sale</param>
        /// <param name="onSaleTo">The date and time till when the product will be on sale</param>
        public virtual void SetOnSale(DateTime onSaleFrom, DateTime onSaleTo)
        {
            if(onSaleFrom >= onSaleTo)
            {
                throw new ArgumentException("The sale's start date must be precedent to the end date");
            }

            SetOnSale(onSaleFrom);
            OnSaleTo = onSaleTo;
        }

        /// <summary>
        /// Remove the product from the sales
        /// </summary>
        public virtual void RemoveFromSale()
        {
            RemoveFromSale(DateTime.Now);
        }

        /// <summary>
        /// Remove the product from the sales, starting from the specified date
        /// </summary>
        /// <param name="endSaleDate">The date and time from when the product will not be on sale anymore</param>
        public virtual void RemoveFromSale(DateTime endSaleDate)
        {
            IsOnSale = false;
            OnSaleTo = endSaleDate;
        }

        public virtual void AddCategory(Category category)
        {
            AddCategory(category, false);
        }

        /// <summary>
        /// Add a category to the product
        /// </summary>
        /// <param name="category">The category to add</param>
        public virtual void AddCategory(Category category, bool isMain)
        {
            if (category == null)
            {
                throw new ArgumentNullException("category");
            }

            if (_Categories.Any(c => c.CategoryId == category.Id))
            {
                throw new ArgumentException("The category is already in collection");
            }

            if (isMain && _Categories.Any(c => c.IsMain))
            {
                throw new ArgumentException("There's already a main category");
            }

            _Categories.Add(new ProductCategory
            {
                CategoryId = category.Id,
                IsMain = isMain
            });
        }

        /// <summary>
        /// Add the specified category as main category for the product
        /// </summary>
        /// <param name="category">The category to add</param>
        public virtual void AddMainCategory(Category category)
        {
            AddCategory(category, true);
        }

        /// <summary>
        /// Add a product variant
        /// </summary>
        /// <param name="product">The variant to add</param>
        public virtual void AddVariant(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException("product");
            }

            if (_Variants.Contains(product))
            {
                throw new ArgumentException("The product is already in collection");
            }

            _Variants.Add(product);
        }

        /// <summary>
        /// Add a tier price to the product
        /// </summary>
        /// <param name="fromQuantity">The start quantity</param>
        /// <param name="toQuantity">The end quantity</param>
        /// <param name="price">The price amount</param>
        public virtual void AddTierPrice(int fromQuantity, int toQuantity, Currency price)
        {
            if (!TierPriceEnabled)
            {
                throw new InvalidOperationException("Tier prices not enabled");
            }

            if (price == null)
            {
                throw new ArgumentNullException("price");
            }

            if (_TierPrices.Any(t => t.FromQuantity == fromQuantity && t.ToQuantity == toQuantity))
            {
                throw new ArgumentException("The tier price is already in collection");
            }

            _TierPrices.Add(new TierPrice
            {
                Id = Guid.NewGuid(),
                FromQuantity = fromQuantity,
                ToQuantity = toQuantity,
                Price = price
            });
        }

        /// <summary>
        /// Add a new custom attribute to the product
        /// </summary>
        /// <param name="attribute">The attribute to add</param>
        /// <param name="value">The attribute's value</param>
        public virtual void AddAttribute(CustomAttribute attribute, object value)
        {
            if(attribute == null)
            {
                throw new ArgumentNullException("attribute");
            }

            if(_Attributes.Any(a => a.Attribute == attribute))
            {
                throw new ArgumentException("The attribute is already in collection");
            }

            _Attributes.Add(new ProductAttribute
            {
                Id = Guid.NewGuid(),
                Attribute = attribute,
                Value = value
            });
        }

        /// <summary>
        /// Add a product review
        /// </summary>
        /// <param name="name">The name of the user who add the review</param>
        /// <param name="rating">The rating given to the product</param>
        public virtual void AddReview(string name, int rating)
        {
            AddReview(name, rating, null);
        }

        /// <summary>
        /// Add a product review
        /// </summary>
        /// <param name="name">The name of the user who add the review</param>
        /// <param name="rating">The rating given to the product</param>
        /// <param name="comment">The comment given to the product</param>
        public virtual void AddReview(string name, int rating, string comment)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }

            if(rating < 0)
            {
                throw new ArgumentException("rating could not be less than zero", "rating");
            }

            _Reviews.Add(new ProductReview
            {
                Id = Guid.NewGuid(),
                Name = name,
                Rating = rating,
                Comment = comment
            });
        }

        /// <summary>
        /// Add an image to the product
        /// </summary>
        /// <param name="path">The image full path</param>
        /// <param name="name">The image's name (no extension)</param>
        /// <param name="originalName">The image's original name</param>
        /// <param name="isMain">Whether the image is the product's main image</param>
        /// <param name="uploadedOn">The date and time of when the image is uploaded</param>
        public virtual void AddImage(string path, string name, string originalName, bool isMain, DateTime uploadedOn)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("path");
            }

            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }

            if (string.IsNullOrEmpty(originalName))
            {
                throw new ArgumentNullException("originalName");
            }

            _Images.Add(new ProductImage
            {
                Id = Guid.NewGuid(),
                Path = path,
                Name = name,
                OriginalName = originalName,
                IsMain = isMain,
                UploadedOn = uploadedOn
            });
        }

        #endregion

        #region Factory Methods
        /// <summary>
        /// Create a new product
        /// </summary>
        /// <param name="ean">The EAN code</param>
        /// <param name="sku">The SKU code</param>
        /// <param name="name">The product name</param>
        /// <param name="url">The product url</param>
        /// <returns>The created product</returns>
        public static Product Create(string ean, string sku, string name, string url)
        {
            if (string.IsNullOrEmpty(ean))
            {
                throw new ArgumentNullException("ean");
            }

            if (string.IsNullOrEmpty(sku))
            {
                throw new ArgumentNullException("sku");
            }

            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }

            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }

            var product = new Product
            {
                Id = Guid.NewGuid(),
                EanCode = ean,
                Sku = sku,
                Name = name,
                Url = url,
                Deleted = false
            };

            return product;
        }

        #endregion
    }
}
