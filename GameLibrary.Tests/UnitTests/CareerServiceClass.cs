using GameLibrary.Core.Contracts;
using GameLibrary.Core.Services;
using GameLibrary.Infrastructure.Data;
using GameLibrary.Infrastructure.Data.Common;
using GameLibrary.Infrastructure.Data.Entities;
using GameLibrary.Tests.Mocks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Tests.UnitTests
{
    [TestFixture]
    public class CareerServiceClass
    {
        private IRepository repo;
        private ICareerService careerService;
        private ApplicationDbContext context;

        [SetUp]
        public void Setup()
        {
            context = DatabaseMock.Data;
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            repo = new Repository(context);

            var cacheMock = new Mock<IMemoryCache>();
            var cache = cacheMock.Object;
            careerService = new CareerService(repo, cache);
        }

        [Test]
        public async Task ExistsById_AndHelperWithPhoneNumber_ShouldReturnTrueAndFalse()
        {
            //Arrange
            string userId1 = "1ca18236-2d0f-49ee-ab91-246c38c3c1d9";
            string userId2 = "1786b223-314a-40ef-92e4-f7204b772971";
            string phoneNumber = "+35914500233";
            string phoneNumber2 = "+35914500243";

            await repo.AddAsync(new Helper
            {
                PhoneNumber = phoneNumber,
                UserId = userId1
            });

            IEnumerable<User> users = new List<User>()
            {
                new User
                {
                    Id = userId1
                },
                new User
                {
                    Id = userId2
                }
            };
            await repo.AddRangeAsync(users);

            await repo.SaveChangesAsync();

            //Act
            var truetest = await careerService.ExistsById(userId1);
            var falsetest = await careerService.ExistsById(userId2);

            var truePhonetest = await careerService.HelperWithPhoneNumberExists(phoneNumber);
            var falsePhonetest = await careerService.HelperWithPhoneNumberExists(phoneNumber2);

            //Assert
            Assert.That(truetest, Is.True);
            Assert.That(truePhonetest, Is.True);

            Assert.That(falsetest, Is.False);
            Assert.That(falsePhonetest, Is.False);
        }


        [TearDown]
        public void TearDown()
        {
            this.context.Dispose();
        }
    }
}
