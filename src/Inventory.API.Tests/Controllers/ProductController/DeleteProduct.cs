using Xunit;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Inventory.API.Data.Repositories;
using Inventory.API.Models;

namespace Inventory.API.Tests.Controllers.ProductController
{
    public class DeleteProduct
    {
        [Fact]
        public void Update_an_existing_product()
        {
            //Given
            var mockRepo = new Mock<IDataRepository<Product>>();

            mockRepo.Setup(repo => repo.Delete("1"));
            var controller = new Inventory.API.Controllers.ProductController(mockRepo.Object);

            //When
            var result = controller.DeleteProduct("1");

            //Then
            Assert.IsType<OkObjectResult>(result);
            mockRepo.Verify(repo => repo.Delete("1"), Times.Once);
        }
    }
}