using GameLibrary.Core.Contracts;
using GameLibrary.Core.Services;
using GameLibrary.Infrastructure.Data;
using GameLibrary.Infrastructure.Data.Common;
using GameLibrary.Infrastructure.Data.Entities;
using GameLibrary.Tests.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Tests.UnitTests
{
    public class GameServiceTests
    {
        private IRepository repo;
        private IGameService gameService;
        private ApplicationDbContext context;

        [SetUp]
        public void Setup()
        {
            context = DatabaseMock.Data;

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();


            repo = new Repository(context);
            gameService = new GameService(repo);
        }

        [Test]
        public async Task IsUserDevelepor_ReturnsTrueAndFalseWithTwoUsers()
        {
            //Arrange
            string userId1 = "1ca18236-2d0f-49ee-ab91-246c38c3c1d9";
            string userId2 = "1786b223-314a-40ef-92e4-f7204b772971";

            await repo.AddAsync(new Game
            {
                Title = "",
                ImageUrl = "",
                Description = "",
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
            var truetest = await gameService.IsUserDevelepor(userId1);
            var falsetest = await gameService.IsUserDevelepor(userId2);

            //Assert
            Assert.That(truetest, Is.True);
            Assert.That(falsetest, Is.False);
        }

        [Test]
        public async Task GetGamesCreatedByUserId_ReturnsTheGamesByUserId()
        {
            //Arrange
            string userId = "6c0e1cc2-e4d5-4b77-a9ae-c88a0d2bd184";
            await repo.AddAsync(new Game()
            {
                Id = 1,
                Description = "",
                ImageUrl = "",
                Rating = 10.00m,
                Title = "League",
                UserId = userId,
                DislikesCount = 0,
                LikesCount = 0
            });

            await repo.SaveChangesAsync();
            //Act
            var games = await gameService.GetGamesCreatedByUserId(userId);
            var game = games.FirstOrDefault(x => x.UserId == userId);

            Assert.That(games.Count(), Is.EqualTo(1));
            Assert.That(game.Title, Is.EqualTo("League"));
        }

        [TearDown]
        public void TearDown()
        {
            this.context.Dispose();
        }
    }
}
