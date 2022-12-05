using GameLibrary.Core.Contracts;
using GameLibrary.Core.Services;
using GameLibrary.Infrastructure.Data.Common;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Tests.UnitTests
{
    [TestFixture]
    public class CareerServiceClass : UnitTestsBase
    {
        private ICareerService careerService;
        //private readonly IRepository repo;
        protected Mock<IRepository> repoMock;

        //[SetUp]
        //public void SetUp()
        //{
        //    repoMock = new Mock<IRepository>(data);
        //    this.careerService = new CareerService(repoMock);
        //}

        [Test]
        public async Task ExistsById_ShouldReturnTrue()
        {
            //var mockCreditDecisionService = new Mock<ICreditDecisionService>();
            //mockCreditDecisionService
            //    .Setup(p => p.GetDecision(100))
            //    .Returns("Declined");

            //var controller = new CreditDecision(mockCreditDecisionService.Object);
            //var result = controller.MakeCreditDecision(100);

            //Assert.That(result, Is.EqualTo("Declined"));

            //Arrange
            //var mockCareerService = new Mock<ICareerService>();
            //mockCareerService
            //    .Setup(p => p.ExistsById(this.User2.Id))
            //    .Returns(Task.FromResult(true));
            ////Act
            //var choice = await careerService.ExistsById(this.User2.Id);

            ////Assert
            //Assert.That(choice, Is.EqualTo(true));
        }
    }
}
