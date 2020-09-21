
using Xunit;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Inventory.API.Data.Repositories;
using Inventory.API.Models;

namespace Inventory.API.Tests.Controllers.ProductController
{
    public class Get
    {
        [Fact]
        public void Return_one_product_by_id()
        {
            //Given
            var product = new Product() { Name = "Product A" };
            var mockRepo = new Mock<IDataRepository<Product>>();

            mockRepo.Setup(repo => repo.Get("1")).Returns(product);
            var controller = new Inventory.API.Controllers.ProductController(mockRepo.Object);

            //When
            var result = controller.Get("1");

            //Then
            Assert.Equal(product.Id, result.Id);
            mockRepo.Verify(repo => repo.Get("1"), Times.Once);
        }
    }
}