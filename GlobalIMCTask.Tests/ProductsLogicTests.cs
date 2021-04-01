using GlobalIMCTask.Common.Errors;
using GlobalIMCTask.Domain.Products;
using GlobalIMCTask.Tests.Mock.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GlobalIMCTask.Tests
{
    public class ProductsLogicTests
    {
        //string.Empty, "Mock desc", "https//www.youtube.com", 10, new int[] { 1, 2 }); ;
        [Theory]
        [InlineData("", "Mock desc", "https://www.youtube.com", 10, new int[] { 1, 2 }, "vendor 1")]
        [InlineData("Mock Title", "", "https://www.youtube.com", 10, new int[] { 1, 2 }, "vendor 1")]
        [InlineData("Mock Title", "Mock desc", "https//hello there", 10, new int[] { 1, 2 }, "vendor 1")]
        [InlineData("Mock Title", "Mock desc", "", 10, new int[] { 1, 2 }, "vendor 1")]
        [InlineData("Mock Title", "Mock desc", "https://www.youtube.com", -10, new int[] { 1, 2 }, "vendor 1")]
        [InlineData("Mock Title", "Mock desc", "https://www.youtube.com", 10, new int[] { }, "vendor 1")]
        [InlineData("Mock Title", "Mock desc", "https://www.youtube.com", 10, new int[] { 9999 }, "vendor 1")]
        [InlineData("Mock Title", "Mock desc", "https://www.youtube.com", 10, new int[] { 9999 }, "")]
        public void CreateProduct_ShouldThrowBadRequestException(string title, string descirption,
            string imageURL, double price, int[] dts, string vendorUID)
        {
            ProductsLogic logic = new ProductsLogic(new MockUnitOfWork());

            Assert.Throws<BadRequestException>(() => logic.CreateProduct(title, descirption, imageURL, price, dts, vendorUID));
        }

        [Theory]
        [InlineData("Mock Title", "Mock desc", "https://www.youtube.com", 10, new int[] { 1, 2 }, "vendor 1")]
        public void CreateProduct_ShouldSucceed(string title, string descirption,
            string imageURL, double price, int[] dts, string vendorUID)
        {
            ProductsLogic logic = new ProductsLogic(new MockUnitOfWork());
            logic.CreateProduct(title, descirption, imageURL, price, dts, vendorUID);
            Assert.True(true);
        }

        [Theory]
        [InlineData(-11, "Mock Title", "Mock desc", "https://www.youtube.com", 10, new int[] { 1, 2 }, "vendor 1")]
        [InlineData(1, "", "Mock desc", "https://www.youtube.com", 10, new int[] { 1, 2 }, "vendor 1")]
        [InlineData(1, "Mock Title", "", "https://www.youtube.com", 10, new int[] { 1, 2 }, "vendor 1")]
        [InlineData(1, "Mock Title", "Mock desc", "https//hello there", 10, new int[] { 1, 2 }, "vendor 1")]
        [InlineData(1, "Mock Title", "Mock desc", "", 10, new int[] { 1, 2 }, "vendor 1")]
        [InlineData(1, "Mock Title", "Mock desc", "https://www.youtube.com", -10, new int[] { 1, 2 }, "vendor 1")]
        [InlineData(1, "Mock Title", "Mock desc", "https://www.youtube.com", 10, new int[] { }, "vendor 1")]
        [InlineData(1, "Mock Title", "Mock desc", "https://www.youtube.com", 10, new int[] { 9999 }, "vendor 1")]
        [InlineData(1, "Mock Title", "Mock desc", "https://www.youtube.com", 10, new int[] { 9999 }, "")]
        public void UpdateProduct_ShouldThrowBadRequestException(int id, string title, string descirption,
            string imageURL, double price, int[] dts, string vendorUID)
        {
            ProductsLogic logic = new ProductsLogic(new MockUnitOfWork());

            Assert.Throws<BadRequestException>(() => logic.UpdateProduct(id, title, descirption, imageURL, price, dts, vendorUID));
        }

        [Theory]
        [InlineData(11111, "Mock Title", "Mock desc", "https://www.youtube.com", 10, new int[] { 1, 2 }, "vendor 1")]
        public void UpdateProduct_ShouldThrowNotFoundException(int id, string title, string descirption,
            string imageURL, double price, int[] dts, string vendorUID)
        {
            ProductsLogic logic = new ProductsLogic(new MockUnitOfWork());

            Assert.Throws<NotFoundException>(() => logic.UpdateProduct(id, title, descirption, imageURL, price, dts, vendorUID));
        }

        [Theory]
        [InlineData(-11)]
        public void GetProduct_ShouldThrowBadRequestException(int id)
        {
            ProductsLogic logic = new ProductsLogic(new MockUnitOfWork());
            Assert.Throws<BadRequestException>(() => logic.GetProduct(id));
        }

        [Theory]
        [InlineData(11111)]
        public void GetProduct_ShouldThrowNotFoundException(int id)
        {
            ProductsLogic logic = new ProductsLogic(new MockUnitOfWork());
            Assert.Throws<NotFoundException>(() => logic.GetProduct(id));
        }

        [Theory]
        [InlineData(1)]
        public void GetProduct_ShouldSucceed(int id)
        {
            ProductsLogic logic = new ProductsLogic(new MockUnitOfWork());
            var result = logic.GetProduct(id);
            Assert.Equal(result.Id, id);
        }

        [Theory]
        [InlineData(1, 2)]
        public void GetProducts_ShouldSucceed(int page, int pageSize)
        {
            ProductsLogic logic = new ProductsLogic(new MockUnitOfWork());
            var result = logic.GetProducts(page, pageSize);
            Assert.Equal(pageSize, result.Item1.Count);
        }

        [Theory]
        [InlineData(100, 2)]
        public void GetProducts_PageShouldSucceed(int page, int pageSize)
        {
            ProductsLogic logic = new ProductsLogic(new MockUnitOfWork());
            var result = logic.GetProducts(page, pageSize);
            Assert.Empty(result.Item1);
        }
    }
}
