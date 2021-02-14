using Basics2.Homework.BusinessLogic.Services;
using Basics2.Homework.Domain.Interfaces;
using Basics2.Homework.Domain.Models;
using Moq;
using NUnit.Framework;

namespace Basics2.Homework.BusinessLogic.Tests
{
    public class ProductServiceTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test_ShouldReturnEmployeeWithId()
        {
            // arrange
            var inputProduct = new Product
            {
                Name = "Dfybi",
                Volume = 15
            };
            var expectedProduct = inputProduct;
            expectedProduct.Id = 10;

            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock
                .Setup(x => x.Add(It.Is<Product>(y => y == inputProduct)))
                .Returns(() => expectedProduct)
                .Verifiable();
            
            var service = new ProductService(productRepositoryMock.Object);

            // act
            var result = service.CreateProduct(inputProduct);

            //assert
            Mock.Verify(productRepositoryMock);
            Assert.AreEqual(result, expectedProduct);
        }
    }
}