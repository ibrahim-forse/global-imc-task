using GlobalIMCTask.Common.Errors;
using GlobalIMCTask.Core.Models;
using GlobalIMCTask.Repositories.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalIMCTask.Domain.Products
{
    public class ProductsLogic
    {
        private readonly IUnitOfWork _uow;
        public ProductsLogic(IUnitOfWork uow)
        {
            _uow = uow;
        }

        private bool CheckURLValid(string url)
        {
            Uri validatedUri;

            if (Uri.TryCreate(url, UriKind.Absolute, out validatedUri)) //.NET URI validation.
            {
                //If true: validatedUri contains a valid Uri. Check for the scheme in addition.
                return (validatedUri.Scheme == Uri.UriSchemeHttp || validatedUri.Scheme == Uri.UriSchemeHttps);
            }
            return false;
        }

        private void validateProduct(string title, string description, string imageURL, double price, 
            int[] dietaryTypeIds, string vendorUID)
        {
            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(description) || string.IsNullOrEmpty(vendorUID)
                || price <= 0 || dietaryTypeIds.Length == 0 || string.IsNullOrEmpty(imageURL))
                throw new BadRequestException("Params", "One or more parameters is missing");

            if (!CheckURLValid(imageURL)) {
                System.Diagnostics.Debug.WriteLine("######## imageURL "+imageURL);
                throw new BadRequestException("Image URL", "Invalid image url");
            }
        }

        public void CreateProduct(string title, string description, string imageURL, 
            double price, int[] dietaryTypeIds, string vendorUID)
        {
            validateProduct(title, description, imageURL, price, dietaryTypeIds,vendorUID);

            var dietaryTypes = _uow.Products.GetDietaryTypes(dietaryTypeIds);

            if (dietaryTypes.Count == 0)
                throw new BadRequestException("Dietary Type Ids", "Invalid dietary type ids");

            Product product = new Product()
            {
                Code = Guid.NewGuid().ToString(),
                Description = description,
                ImageURL = imageURL,
                Price = price,
                Title = title,
                ViewCount = 0,
                DietaryTypes = dietaryTypes,
                VendorUID = vendorUID
            };

            _uow.Products.CreateProduct(product);
            _uow.Complete();
        }

        public void UpdateProduct(int id, string title, string description,
            string imageURL, double price, int[] dietaryTypeIds, string vendorUID)
        {
            if (id <= 0)
                throw new BadRequestException("Product Id", "Invalid product id");

            validateProduct(title, description, imageURL, price, dietaryTypeIds, vendorUID);

            Product product = _uow.Products.GetProduct(id);
            if (product == null)
                throw new NotFoundException("Product", "Product Not Found");

            var dietaryTypes = _uow.Products.GetDietaryTypes(dietaryTypeIds);

            if (dietaryTypes.Count == 0)
                throw new BadRequestException("Dietary Type Ids", "Invalid dietary type ids");

            product.Description = description;
            product.DietaryTypes = dietaryTypes;
            product.ImageURL = imageURL;
            product.Price = price;
            product.Title = title;
            product.VendorUID = vendorUID;

            _uow.Complete();
        }

        public Product GetProduct(int id)
        {
            if (id <= 0)
                throw new BadRequestException("Product Id", "Invalid product id");

            Product product = _uow.Products.GetProduct(id);
            if (product == null)
                throw new NotFoundException("Product", "Product Not Found");
            product.ViewCount++;
            _uow.Complete();
            return product;
        }

        public Tuple<List<Product>,int> GetProducts(int page, int pageSize)
        {
            var products = _uow.Products.GetProducts(page - 1, pageSize);
            return products;
        }

        public Tuple<List<Product>, int> FindProductByTitle(string title, int page, int pageSize)
        {
            if (string.IsNullOrEmpty(title))
                throw new BadRequestException("Title", "Title is missing");

            var products = _uow.Products.FindProductByTitle(title, page - 1, pageSize);
            return products;
        }

        public Tuple<List<Product>, int> FindProductByDescription(string description, int page, int pageSize)
        {
            if (string.IsNullOrEmpty(description))
                throw new BadRequestException("Description", "Description is missing");

            var products = _uow.Products.FindProductByDescription(description, page - 1, pageSize);
            return products;
        }

        public void DeleteProduct(int id)
        {
            if (id <= 0)
                throw new BadRequestException("Product Id", "Invalid product id");

            Product product = _uow.Products.GetProduct(id);
            if (product == null)
                throw new NotFoundException("Product", "Product Not Found");

            _uow.Products.DeleteProduct(product);
            _uow.Complete();
        }

        public List<DietaryType> GetDietaryTypes()
        {
            return _uow.Products.GetDietaryTypes();
        }
    }
}
