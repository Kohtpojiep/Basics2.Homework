using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Basics2.Homework.BusinessLogic.Services;
using Basics2.Homework.Domain.Interfaces;
using Basics2.Homework.Domain.Models;
using Moq;
using NUnit.Framework;

namespace Basics2.Homework.BusinessLogic.Tests
{
    public class ShowcaseServiceTests
    {

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test_ShouldNotRemoveShowcaseWithProducts()
        {
            // arrange
            var inputShowcase = new Showcase
            {
                Id = 1,
                Name = "Dfybi",
                Volume = 15,
                CreateDate = DateTime.Now.Date
            };

            var showcaseRepositoryMock = new Mock<IShowcaseRepository>();
            showcaseRepositoryMock
                .Setup(x => x.Remove(It.Is<Showcase>(y => y == inputShowcase)))
                .Verifiable();

            Expression<Func<ShowcaseProduct, bool>> predicate = x => x.ShowcaseId == inputShowcase.Id;

            var showcaseProductRepositoryMock = new Mock<IShowcaseProductRepository>();
            showcaseProductRepositoryMock
                .Setup(x =>
                    x.Get(It.Is<Expression<Func<ShowcaseProduct, bool>>>(y => y == predicate)))
                .Returns(new ShowcaseProduct[] 
                    { new ShowcaseProduct { Id = 1, ShowcaseId = 1, ProductId = 1, ProductCost = 100, ProductCount = 123 } })
                .Verifiable();

            var service = new ShowcaseService(showcaseRepositoryMock.Object, showcaseProductRepositoryMock.Object);

            // act
            Assert.Throws<Exception>(() => service.RemoveShowcase(inputShowcase));

            //assert
            Mock.Verify(showcaseRepositoryMock);
            Mock.Verify(showcaseProductRepositoryMock);
        }
    }
}
