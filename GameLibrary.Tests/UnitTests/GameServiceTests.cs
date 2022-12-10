using GameLibrary.Core.Contracts;
using GameLibrary.Core.Models.Game;
using GameLibrary.Core.Services;
using GameLibrary.Infrastructure.Data;
using GameLibrary.Infrastructure.Data.Common;
using GameLibrary.Infrastructure.Data.Entities;
using GameLibrary.Tests.Mocks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
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
        public async Task EditGameAsync_EditsAGame()
        {

            await repo.AddAsync(new Game()
            {
                Id = 1,
                ImageUrl = "",
                Title = "",
                Description = "",
                UserId = "23c512d2-b8d8-46ec-b15c-4442e4d4cfbe"
            });

            await repo.SaveChangesAsync();

            await gameService.EditGameAsync(new GameFormViewModel()
            {
                Id = 1,
                ImageUrl = "",
                Title = "",
                Description = "This game is edited",
                UserId = "23c512d2-b8d8-46ec-b15c-4442e4d4cfbe"
            });

            var dbHouse = await repo.GetByIdAsync<Game>(1);

            Assert.That(dbHouse.Description, Is.EqualTo("This game is edited"));
        }

        [Test]
        public async Task AddGameAsync_ReturnsGameAdded()
        {

            //Act and Arrange
            await gameService.AddGameAsync(new GameFormViewModel
            {
                Id = 1,
                Description = "",
                Title = "",
                ImageUrl = ""
            }, 
            "23c512d2-b8d8-46ec-b15c-4442e4d4cfbe");

            var game = await repo.GetByIdAsync<Game>(1);

            Assert.That(game.UserId, Is.EqualTo("23c512d2-b8d8-46ec-b15c-4442e4d4cfbe"));
        }

        [Test]
        public async Task GetAllThemes_ReturnsAllGenres()
        {
            await repo.AddRangeAsync(new List<Theme>()
            {
                new Theme() { Id = 1, ThemeName = "Cool" },
                new Theme() { Id = 3, ThemeName = "Epic" }
            });
            await repo.SaveChangesAsync();

            var genres = await gameService.GetAllThemes();

            Assert.That(genres.Count(), Is.EqualTo(2));
        }

        [TestCase("23c512d2-b8d8-46ec-b15c-4442e4d4cfbe", "23c512d2-b8d8-46ec-b15c-4442e4d4cfbe", 0)]
        [TestCase("23c512d2-b8d8-46ec-b15c-4442e4d4cfbe", "4df272e3-8ddb-4218-8da7-006e32f8433c", 1)]
        public async Task MarkGameAsFavourite_ReturnsUserAndGameNull(string userId, string secondUserId,int gameId)
        {
            await repo.AddAsync(new User()
            {
                Id = userId,
            });

            await repo.AddAsync(new Game()
            {
                Id = gameId,
                ImageUrl = "",
                Description = "",
                Title = "",
                UserId = secondUserId
            });
            await repo.SaveChangesAsync();

            Assert.ThrowsAsync<ArgumentException>(async () => await gameService.MarkGameAsFavourite(secondUserId, gameId));
        }

        [Test]
        public async Task MarkGameAsFavourite_ReturnsSuccess()
        {
            var userId = "23c512d2-b8d8-46ec-b15c-4442e4d4cfbe";

            await repo.AddAsync(new User()
            {
                Id = userId,
            });

            await repo.AddAsync(new Game()
            {
                Id = 1,
                ImageUrl = "",
                Description = "",
                Title = "",
                UserId = userId
            });
            await repo.SaveChangesAsync();

            //Act
            await gameService.MarkGameAsFavourite(userId, 1);

            var usergames = await repo.All<UserGame>().Where(x => x.UserId == userId).ToListAsync();
            //Assert
            Assert.That(usergames.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task ShowsAllFavourites_ReturnsUserNull()
        {
            string userId = "23c512d2-b8d8-46ec-b15c-4442e4d4cfbe";
            string secondUserId = "23c512d2-b8d8-46ec-b15c-4442e4d4cfb3";
            await repo.AddAsync(new User()
            {
                Id = userId,
            });

            Assert.ThrowsAsync<ArgumentException>(async () => await gameService.ShowAllFavourites(secondUserId));
        }

        [Test]
        public async Task ShowsAllFavourites_ReturnsAllFavourites()
        {
            string userId = "23c512d2-b8d8-46ec-b15c-4442e4d4cfbe";
            User user = new User()
            {
                Id = userId,
            };
            await repo.AddAsync(user);

            await repo.AddAsync(new Game()
            {
                Id = 1,
                ImageUrl = "",
                Description = "",
                Title = "",
                UserId = userId
            });

            await repo.AddAsync(new Genre()
            {
                Id = 1,
                GenreName = "Cool"
            });
            await repo.SaveChangesAsync();
            await gameService.MarkGameAsFavourite(userId, 1);

            var favourites = await gameService.ShowAllFavourites(userId);

            Assert.That(favourites.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task GetAllGenres_ReturnsAllGenres()
        {
            await repo.AddRangeAsync(new List<Genre>()
            {
                new Genre() { Id = 1, GenreName = "Cool" },
                new Genre() { Id = 3, GenreName = "Epic" }
            });
            await repo.SaveChangesAsync();

            var genres = await gameService.GetAllGenres();

            Assert.That(genres.Count(), Is.EqualTo(2));
        }

        //[Test]
        //public async Task GetGamesCreatedByUserId_ReturnsTheGamesByUserId()
        //{
        //    //Arrange

        //    string userId = "6c0e1cc2-e4d5-4b77-a9ae-c88a0d2bd184";
        //    await repo.AddAsync(new Game()
        //    {
        //        Id = 1,
        //        Description = "",
        //        ImageUrl = "",
        //        Rating = 10.00m,
        //        Title = "League",
        //        UserId = userId,
        //        DislikesCount = 0,
        //        LikesCount = 0
        //    });

        //    await repo.SaveChangesAsync();
        //    //Act
        //    var games = await gameService.GetGamesCreatedByUserId(userId);
        //    var game = games.FirstOrDefault(x => x.UserId == userId);

        //    Assert.That(games.Count(), Is.EqualTo(1));
        //    Assert.That(game.Title, Is.EqualTo("League"));
        //}

        [TearDown]
        public void TearDown()
        {
            this.context.Dispose();
        }
    }
}
