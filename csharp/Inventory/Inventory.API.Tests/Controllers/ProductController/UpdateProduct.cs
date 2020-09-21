using Xunit;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Inventory.API.Data.Repositories;
using Inventory.API.Models;

namespace Inventory.API.Tests.Controllers.ProductController
{
    public class UpdateProduct
    {
        [Fact]
        public void Update_an_existing_product()
        {
            //Given
            var testProduct = new Product() { Name = "Product A" };
            var newProduct = new Product(){ Name = "Product A"};
            var mockRepo = new Mock<IDataRepository<Product>>();

            mockRepo.Setup(repo => repo.Update("1", newProduct)).Returns(testProduct);
            var controller = new Inventory.API.Controllers.ProductController(mockRepo.Object);

            //When
            var result = controller.UpdateProduct("1", newProduct);

            //Then
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var productResult = Assert.IsType<Product>(actionResult.Value);
            Assert.Equal(testProduct.Name, productResult.Name);
            mockRepo.Verify(repo => repo.Update("1", newProduct), Times.Once);
        }
    }
}