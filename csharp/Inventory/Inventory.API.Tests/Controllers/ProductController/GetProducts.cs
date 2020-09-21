
using Xunit;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Inventory.API.Data.Repositories;
using Inventory.API.Models;

namespace Inventory.API.Tests.Controllers.ProductController
{
    public class GetProducts
    {
        [Fact]
        public void Return_a_list_of_all_products()
        {
            //Given
            var products = new List<Product>{
                new Product(){ Name = "Product A" },
                new Product(){Name = "Product B" }
            };
            var mockRepo = new Mock<IDataRepository<Product>>();

            mockRepo.Setup(repo => repo.Get()).Returns(products);
            var controller = new Inventory.API.Controllers.ProductController(mockRepo.Object);

            //When
            var result = controller.GetProducts();

            //Then
            Assert.Equal(2, result.ToList().Count());
            mockRepo.Verify(repo => repo.Get(), Times.Once);
        }
    }
}