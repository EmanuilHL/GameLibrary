using GameLibrary.Core.Contracts;
using GameLibrary.Core.Services;
using GameLibrary.Infrastructure.Data.Common;
using GameLibrary.Infrastructure.Data;
using GameLibrary.Tests.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLibrary.Infrastructure.Data.Entities;
using GameLibrary.Core.Models.GameMechanic;

namespace GameLibrary.Tests.UnitTests
{
    [TestFixture]
    public class GameMechanicServiceTests
    {
        private IRepository repo;
        private IGameMechanicService gameMechanicService;
        private ApplicationDbContext context;

        [SetUp]
        public void Setup()
        {
            context = DatabaseMock.Data;

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();


            repo = new Repository(context);
            gameMechanicService = new GameMechanicService(repo);
        }

        [Test]
        public async Task All_Success()
        {
            await repo.AddAsync(new GameMechanic()
            {
                Id = 1,
                MechanicDescription = "",
                GameId = 1,
                GameName = "",
                UserId = "1"
            });
            await repo.SaveChangesAsync();

            var all = await gameMechanicService.All("1");

            Assert.AreEqual(1, all.Count());
        }

        [TestCase("1", "0")]
        [TestCase("0", "1")]
        public async Task CreateGameMechanic_Exceptions(string modelGameName, string title)
        {
            await repo.AddAsync(new Game()
            {
                Id = 1,
                Title = title,
                ImageUrl = "",
                Description = "",
                UserId = "1"
            });

            await repo.AddAsync(new GameMechanic()
            {
                Id = 1,
                MechanicDescription = "",
                GameId = 1,
                GameName = "",
                UserId = "1"
            });
            await repo.SaveChangesAsync();

            var model = new MechanicsFormModel()
            {
                GameName = modelGameName
            };

            Assert.ThrowsAsync<ArgumentException>(async () => await gameMechanicService.CreateGameMechanic(model, "1"));
        }

        [TearDown]
        public void TearDown()
        {
            this.context.Dispose();
        }
    }
}
