using Xunit;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Inventory.API.Data.Repositories;
using Inventory.API.Models;

namespace Inventory.API.Tests.Controllers.ProductController
{
    public class AddProduct
    {
        [Fact]
        public void Add_a_new_product()
        {
            //Given
            var testProduct = new Product() { Name = "Product A" };
            var newProduct = new Product(){ Name = "Product A"};
            var mockRepo = new Mock<IDataRepository<Product>>();

            mockRepo.Setup(repo => repo.Insert(newProduct)).Returns(testProduct);
            var controller = new Inventory.API.Controllers.ProductController(mockRepo.Object);

            //When
            var result = controller.AddProduct(newProduct);

            //Then
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var productResult = Assert.IsType<Product>(actionResult.Value);
            Assert.Equal(testProduct.Name, productResult.Name);
            mockRepo.Verify(repo => repo.Insert(newProduct), Times.Once);
        }
    }
}