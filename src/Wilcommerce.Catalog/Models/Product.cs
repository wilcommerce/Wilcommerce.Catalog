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
        public Guid Id { get; protected set; }

        #region Protected fields
        /// <summary>
        /// The product's variants
        /// </summary>
        protected ICollection<Product> _variants;

        /// <summary>
        /// The product's categories
        /// </summary>
        protected ICollection<ProductCategory> _categories;

        /// <summary>
        /// The product's tier prices
        /// </summary>
        protected ICollection<TierPrice> _tierPrices;

        /// <summary>
        /// The product's attributes
        /// </summary>
        protected ICollection<ProductAttribute> _attributes;

        /// <summary>
        /// The product's reviews
        /// </summary>
        protected ICollection<ProductReview> _reviews;

        /// <summary>
        /// The product's images
        /// </summary>
        protected ICollection<ProductImage> _images;
        #endregion

        #region Constructor
        /// <summary>
        /// Construct the product
        /// </summary>
        protected Product()
        {
            _variants = new HashSet<Product>();
            _categories = new HashSet<ProductCategory>();
            _tierPrices = new HashSet<TierPrice>();
            _attributes = new HashSet<ProductAttribute>();
            _reviews = new HashSet<ProductReview>();
            _images = new HashSet<ProductImage>();
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
        /// Get the product's variants
        /// </summary>
        public IEnumerable<Product> Variants => _variants;

        /// <summary>
        /// Get or set the main product
        /// </summary>
        public virtual Product MainProduct { get; protected set; }

        /// <summary>
        /// Get or set the product's vendor
        /// </summary>
        public virtual Brand Vendor { get; protected set; }

        /// <summary>
        /// Get the associated category
        /// </summary>
        public IEnumerable<ProductCategory> ProductCategories => _categories;

        /// <summary>
        /// Get the product's category
        /// </summary>
        public IEnumerable<Category> Categories => _categories.Select(c => c.Category);

        /// <summary>
        /// Get the main category for the product
        /// </summary>
        public Category MainCategory => _categories.FirstOrDefault(c => c.IsMain)?.Category;

        /// <summary>
        /// Get the product's custom attributes
        /// </summary>
        public IEnumerable<ProductAttribute> Attributes => _attributes;

        /// <summary>
        /// Get or set whether the product is deleted
        /// </summary>
        public bool Deleted { get; protected set; }

        /// <summary>
        /// Get or set whether the tier prices are enabled for the product
        /// </summary>
        public bool TierPriceEnabled { get; protected set; }

        /// <summary>
        /// Get the product's tier prices
        /// </summary>
        public IEnumerable<TierPrice> TierPrices => _tierPrices;

        /// <summary>
        /// Get the product's reviews
        /// </summary>
        public IEnumerable<ProductReview> Reviews => _reviews;

        /// <summary>
        /// Get the product's images
        /// </summary>
        public IEnumerable<ProductImage> Images => _images;

        /// <summary>
        /// Get or set the SEO information for the product
        /// </summary>
        public SeoData Seo { get; protected set; }

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
            if (unitInStock < 0)
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
            if (onSaleFrom >= onSaleTo)
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

        /// <summary>
        /// Add a category to the product
        /// </summary>
        /// <param name="category">The category to add</param>
        public virtual void AddCategory(Category category)
        {
            AddCategory(category, false);
        }

        /// <summary>
        /// Add a category to the product
        /// </summary>
        /// <param name="category">The category to add</param>
        /// <param name="isMain">Whether the category is the main category</param>
        public virtual void AddCategory(Category category, bool isMain)
        {
            if (category == null)
            {
                throw new ArgumentNullException("category");
            }

            if (_categories.Any(c => c.CategoryId == category.Id))
            {
                throw new ArgumentException("The category is already in collection");
            }

            if (isMain && _categories.Any(c => c.IsMain))
            {
                throw new ArgumentException("There's already a main category");
            }

            _categories.Add(new ProductCategory
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
        /// <param name="name">The variant name</param>
        /// <param name="ean">The EAN code</param>
        /// <param name="sku">The SKU code</param>
        /// <param name="price">The variant price</param>
        public virtual void AddVariant(string name, string ean, string sku, Currency price)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }

            if (string.IsNullOrEmpty(ean))
            {
                throw new ArgumentNullException("ean");
            }

            if (string.IsNullOrEmpty(sku))
            {
                throw new ArgumentNullException("sku");
            }

            if (price == null)
            {
                throw new ArgumentNullException("price");
            }

            if (price.Amount < 0)
            {
                throw new ArgumentException("Price amount cannot be less than zero");
            }

            if (_variants.Any(v => v.Name == name && v.EanCode == ean && v.Sku == sku))
            {
                throw new InvalidOperationException("The variant is already in collection");
            }

            _variants.Add(new Product
            {
                Id = Guid.NewGuid(),
                Name = name,
                EanCode = ean,
                Sku = sku,
                Price = price
            });
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

            if (_tierPrices.Any(t => t.FromQuantity == fromQuantity && t.ToQuantity == toQuantity))
            {
                throw new ArgumentException("The tier price is already in collection");
            }

            _tierPrices.Add(new TierPrice
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
            if (attribute == null)
            {
                throw new ArgumentNullException("attribute");
            }

            if (_attributes.Any(a => a.Attribute == attribute))
            {
                throw new ArgumentException("The attribute is already in collection");
            }

            _attributes.Add(new ProductAttribute
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

            if (rating < 0)
            {
                throw new ArgumentException("rating could not be less than zero", "rating");
            }

            _reviews.Add(new ProductReview
            {
                Id = Guid.NewGuid(),
                Name = name,
                Rating = rating,
                Comment = comment,
                Approved = false
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

            _images.Add(new ProductImage
            {
                Id = Guid.NewGuid(),
                Path = path,
                Name = name,
                OriginalName = originalName,
                IsMain = isMain,
                UploadedOn = uploadedOn
            });
        }

        /// <summary>
        /// Approve the review
        /// </summary>
        /// <param name="reviewId">The id of the review to approve</param>
        public virtual void ApproveReview(Guid reviewId)
        {
            var review = _reviews.FirstOrDefault(r => r.Id == reviewId);
            if (review == null)
            {
                throw new InvalidOperationException("Review not found");
            }

            review.Approved = true;
            review.ApprovedOn = DateTime.Now;
        }

        /// <summary>
        /// Remove the approval for the review
        /// </summary>
        /// <param name="reviewId">The id of the review</param>
        public virtual void RemoveReviewApproval(Guid reviewId)
        {
            var review = _reviews.FirstOrDefault(r => r.Id == reviewId);
            if (review == null)
            {
                throw new InvalidOperationException("Review not found");
            }

            review.Approved = false;
            review.ApprovedOn = null;
        }

        /// <summary>
        /// Delete the review
        /// </summary>
        /// <param name="reviewId">The id of the review to delete</param>
        public virtual void DeleteReview(Guid reviewId)
        {
            var review = _reviews.FirstOrDefault(r => r.Id == reviewId);
            if (review == null)
            {
                throw new InvalidOperationException("Review not found");
            }

            if (!_reviews.Remove(review))
            {
                throw new InvalidOperationException("Review not removed");
            }
        }

        /// <summary>
        /// Delete the custom attribute
        /// </summary>
        /// <param name="attributeId">The id of the attribute to delete</param>
        public virtual void DeleteAttribute(Guid attributeId)
        {
            var attribute = _attributes.FirstOrDefault(a => a.Id == attributeId);
            if (attribute == null)
            {
                throw new InvalidOperationException("Attribute not found");
            }

            if (!_attributes.Remove(attribute))
            {
                throw new InvalidOperationException("Attribute not removed");
            }
        }

        /// <summary>
        /// Delete the image
        /// </summary>
        /// <param name="imageId">The id of the image to delete</param>
        public virtual void DeleteImage(Guid imageId)
        {
            var image = _images.FirstOrDefault(i => i.Id == imageId);
            if (image == null)
            {
                throw new InvalidOperationException("Image not found");
            }

            if (!_images.Remove(image))
            {
                throw new InvalidOperationException("Image not removed");
            }
        }

        /// <summary>
        /// Delete the tier price
        /// </summary>
        /// <param name="tierPriceId">The id of the tier price to delete</param>
        public virtual void DeleteTierPrice(Guid tierPriceId)
        {
            var tierPrice = _tierPrices.FirstOrDefault(t => t.Id == tierPriceId);
            if (tierPrice == null)
            {
                throw new InvalidOperationException("Tier price not found");
            }

            if (!_tierPrices.Remove(tierPrice))
            {
                throw new InvalidOperationException("Tier price not deleted");
            }
        }

        /// <summary>
        /// Remove the product variant
        /// </summary>
        /// <param name="variantId">The id of the variant to remove</param>
        public virtual void RemoveVariant(Guid variantId)
        {
            var variant = _variants.FirstOrDefault(v => v.Id == variantId);
            if (variant == null)
            {
                throw new InvalidOperationException("Variant not found");
            }

            if (!_variants.Remove(variant))
            {
                throw new InvalidOperationException("Variant not removed");
            }
        }

        /// <summary>
        /// Set the SEO information for the product
        /// </summary>
        /// <param name="seo">The SEO information</param>
        public virtual void SetSeoData(SeoData seo)
        {
            if (seo == null)
            {
                throw new ArgumentNullException("seo");
            }

            Seo = seo;
        }

        /// <summary>
        /// Change the tier price values
        /// </summary>
        /// <param name="tierPriceId">The tier price id</param>
        /// <param name="fromQuantity">The start quantity</param>
        /// <param name="toQuantity">The end quantity</param>
        /// <param name="price">The price amount</param>
        public virtual void ChangeTierPrice(Guid tierPriceId, int fromQuantity, int toQuantity, Currency price)
        {
            if (!TierPriceEnabled)
            {
                throw new InvalidOperationException("Tier price disabled");
            }

            var tierPrice = _tierPrices.FirstOrDefault(t => t.Id == tierPriceId);
            if (tierPrice == null)
            {
                throw new InvalidOperationException("Tier price not found");
            }

            if (tierPrice.FromQuantity != fromQuantity)
            {
                tierPrice.FromQuantity = fromQuantity;
            }

            if (tierPrice.ToQuantity != toQuantity)
            {
                tierPrice.ToQuantity = toQuantity;
            }

            if (tierPrice.Price != price)
            {
                tierPrice.Price = price;
            }
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
