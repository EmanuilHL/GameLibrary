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
        //22
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
        public async Task FindEditPostById_ReturnsPost()
        {
            await repo.AddAsync(new Game
            {
                Id = 1,
                Title = "1",
                ImageUrl = "2",
                Description = "3",
                UserId = "1"
            });
            await repo.SaveChangesAsync();

            var game = await gameService.FindEditPostById(1);

            Assert.That(game.Title, Is.EqualTo("1"));
            Assert.That(game.ImageUrl, Is.EqualTo("2"));
            Assert.That(game.Description, Is.EqualTo("3"));
        }

        [Test]
        public async Task FindEditPostById_ReturnsNull()
        {
            await repo.AddAsync(new Game
            {
                Id = 1,
                Title = "",
                ImageUrl = "",
                Description = "",
                UserId = "1"
            });
            await repo.SaveChangesAsync();

            Assert.ThrowsAsync<ArgumentException>(async () => await gameService.FindEditPostById(2));
        }

        [Test]
        public async Task GetGameToDelete_ReturnsPost()
        {
            await repo.AddAsync(new Game
            {
                Id = 1,
                Title = "1",
                ImageUrl = "2",
                Description = "3",
                UserId = "1"
            });
            await repo.SaveChangesAsync();

            var game = await gameService.GetGameToDelete(1);

            Assert.That(game.Title, Is.EqualTo("1"));
            Assert.That(game.ImageUrl, Is.EqualTo("2"));
            Assert.That(game.Description, Is.EqualTo("3"));
        }

        [Test]
        public async Task GetGameToDelete_ReturnsNull()
        {
            await repo.AddAsync(new Game
            {
                Id = 1,
                Title = "",
                ImageUrl = "",
                Description = "",
                UserId = "1"
            });
            await repo.SaveChangesAsync();

            Assert.ThrowsAsync<ArgumentException>(async () => await gameService.GetGameToDelete(2));
        }

        [Test]
        public async Task DeleteGamePost_Null()
        {
            await repo.AddAsync(new Game
            {
                Id = 1,
                Title = "",
                ImageUrl = "",
                Description = "",
                UserId = "1"
            });
            await repo.SaveChangesAsync();

            Assert.ThrowsAsync<ArgumentException>(async () => await gameService.DeleteGamePost(2));
        }

        [Test]
        public async Task DeleteGamePost_Deletes()
        {
            await repo.AddAsync(new Game
            {
                Id = 1,
                Title = "",
                ImageUrl = "",
                Description = "",
                UserId = "1"
            });
            await repo.SaveChangesAsync();

            await gameService.DeleteGamePost(1);

            var games = await repo.All<Game>().ToListAsync();
            Assert.That(games.Count, Is.EqualTo(0));
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
            Genre genre = new Genre()
            {
                Id = 1,
                GenreName = "Cool"
            };
            await repo.AddAsync(new Game()
            {
                Id = 1,
                ImageUrl = "",
                Description = "",
                Title = "",
                UserId = userId,
                Rating = 10.00m,
                Genre = genre,
                GenreId = 1
            });

            await repo.AddAsync(genre);
            await repo.SaveChangesAsync();
            await gameService.MarkGameAsFavourite(userId, 1);

            var favourites = await gameService.ShowAllFavourites(userId);

            Assert.That(favourites.Count(), Is.EqualTo(1));
        }

        [TestCase("23c512d2-b8d8-46ec-b15c-4442e4d4cfbe", "23c512d2-b8d8-46ec-b15c-4442e4d4cfbe", 0)]
        [TestCase("23c512d2-b8d8-46ec-b15c-4442e4d4cfbe", "4df272e3-8ddb-4218-8da7-006e32f8433c", 1)]
        public async Task MarkGameAsUnFavourite_ReturnsUserAndGameNull(string userId, string secondUserId, int gameId)
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

            Assert.ThrowsAsync<ArgumentException>(async () => await gameService.MarkGameAsUnfavourite(gameId, secondUserId));
        }

        [Test]
        public async Task CheckIfGameExistsById_FalseAndTrue()
        {
            await repo.AddAsync(new Game()
            {
                Id = 1,
                ImageUrl = "",
                Description = "",
                Title = "",
                UserId = "1"
            });
            await repo.SaveChangesAsync();

            bool test = await gameService.CheckIfGameExistsById(1);
            bool test2 = await gameService.CheckIfGameExistsById(2);

            Assert.That(test, Is.True);
            Assert.That(test2, Is.False);
        }

        [Test]
        public async Task MarkGameAsUnFavourite_ReturnsSuccess()
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
            await gameService.MarkGameAsUnfavourite(1, userId);
            var usergames = await repo.All<UserGame>().Where(x => x.UserId == userId).ToListAsync();
            //Assert
            Assert.That(usergames.Count, Is.EqualTo(0));
        }

        [Test]
        public async Task FindHottestGame_ReturnsHottestGame()
        {
            await repo.AddRangeAsync(new List<Game>()
            {
                new Game
                {
                    Id = 1,
                    Description = "asd",
                    ImageUrl = "asd",
                    Title = "asd",
                    DislikesCount = 0,
                    LikesCount = 0,
                    UserId = "1"
                },
                new Game
                {
                    Id = 2,
                    Description = "dsa",
                    ImageUrl = "dsa",
                    Title = "dsa",
                    DislikesCount = 1,
                    LikesCount = 1,
                    UserId = "2"
                },
                new Game
                {
                    Id = 3,
                    Description = "sad",
                    ImageUrl = "sad",
                    Title = "sad",
                    DislikesCount = 1,
                    LikesCount = 2,
                    UserId = "3"
                },
                new Game
                {
                    Id = 4,
                    Description = "sad",
                    ImageUrl = "sad",
                    Title = "sad",
                    DislikesCount = 0,
                    LikesCount = 2,
                    UserId = "3"
                }
            });
            await repo.SaveChangesAsync();

            var game = await gameService.FindHottestGame();

            Assert.That(game.LikesCount, Is.EqualTo(2));
            Assert.That(game.DislikesCount, Is.EqualTo(0));
        }

        [Test]
        public void FindHottestGame_ReturnsNull()
        {
            Assert.ThrowsAsync<ArgumentException>( async () => await gameService.FindHottestGame());
        }

        [Test]
        public async Task Search_AllOptions()
        {
            string theme = "Epic";
            string searchTerm = "League";
            RatingSorting sorting = RatingSorting.Descending;

            Theme themde = new Theme()
            {
                Id = 1,
                ThemeName = "Epic"
            };
            Genre genre = new Genre()
            {
                Id = 1,
                GenreName = "Awesome"
            };
            await repo.AddAsync(themde);
            await repo.AddAsync(genre);
            await repo.AddRangeAsync(new List<Game>()
            {
                new Game
                {
                    Id = 1,
                    Description = "asd",
                    ImageUrl = "asd",
                    Title = "League",
                    DislikesCount = 0,
                    Rating = 10.00m,
                    LikesCount = 0,
                    UserId = "1",
                    Theme = themde,
                    Genre = genre
                },
                new Game
                {
                    Id = 2,
                    Description = "dsa",
                    ImageUrl = "dsa",
                    Title = "League of",
                    DislikesCount = 1,
                    Rating = 9.00m,
                    LikesCount = 1,
                    UserId = "2",
                    Theme = themde,
                    Genre = genre
                },
                new Game
                {
                    Id = 3,
                    Description = "sad",
                    ImageUrl = "sad",
                    Title = "sad",
                    DislikesCount = 1,
                    Rating = 8.00m,
                    LikesCount = 2,
                    UserId = "3",
                    Genre = genre
                },
                new Game
                {
                    Id = 4,
                    Description = "sad",
                    ImageUrl = "sad",
                    Title = "sad",
                    DislikesCount = 0,
                    Rating = 7.00m,
                    LikesCount = 2,
                    UserId = "3",
                    Genre = genre
                }
            });
            await repo.SaveChangesAsync();

            var ersteFall = await gameService.Search();
            var zweiteFall = await gameService.Search(theme);
            var dritteFall = await gameService.Search(theme, searchTerm);
            var vierteFall = await gameService.Search(theme, searchTerm, sorting);

            var test = vierteFall.Games.FirstOrDefault();

            Assert.That(ersteFall.Games.Count(), Is.EqualTo(4));
            Assert.That(zweiteFall.Games.Count(), Is.EqualTo(2));
            Assert.That(dritteFall.Games.Count(), Is.EqualTo(2));
            Assert.That(test.Id, Is.EqualTo(1));
        }

        [Test]
        public async Task ShowDetailsPage_ReturnsNull()
        {
            await repo.AddAsync(new Game
            {
                Id = 1,
                Description = "asd",
                ImageUrl = "asd",
                Title = "League",
                DislikesCount = 0,
                Rating = 10.00m,
                LikesCount = 0,
                UserId = "1"
            });
            await repo.SaveChangesAsync();

            Assert.ThrowsAsync<ArgumentException>(async () => await gameService.ShowDetailsPage(2));
        }

        [Test]
        public async Task ShowDetailsPage_ReturnsDetailsPage()
        {
            var user = new User()
            {
                Id = "1"
            };
            var genre = new Genre()
            {
                Id = 1,
                GenreName = "Epic"
            };
            await repo.AddAsync(genre);
            await repo.AddAsync(user);
            await repo.AddAsync(new Game()
            {
                Title = "",
                Description = "",
                Rating = 5.0m,
                ImageUrl = "",
                Genre = genre,
                Id = 1,
                UserId = "1",
                LikesCount = 0,
                DislikesCount = 0,
                Comments = new List<Comment>()
                {
                    new Comment()
                    {
                        Id = 1,
                        Description = "wow",
                        User = user
                    }
                }
            });
            await repo.SaveChangesAsync();

            var detailsPage = await gameService.ShowDetailsPage(1);

            Assert.That(detailsPage.Id, Is.EqualTo(1));
            Assert.That(detailsPage.Rating, Is.EqualTo(5.0m));
            Assert.That(detailsPage.ReviewType, Is.EqualTo("OK"));
            Assert.IsNotNull(detailsPage.Comments);
        }

        [TestCase("23c512d2-b8d8-46ec-b15c-4442e4d4cfbe", "23c512d2-b8d8-46ec-b15c-4442e4d4cfbe", 0)]
        [TestCase("23c512d2-b8d8-46ec-b15c-4442e4d4cfbe", "4df272e3-8ddb-4218-8da7-006e32f8433c", 1)]
        public async Task GetCommentView_Exceptions(string userId, string secondUserId, int gameId)
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

            Assert.ThrowsAsync<ArgumentException>(async () => await gameService.GetCommentView(gameId, secondUserId));
        }

        [Test]
        public async Task GetCommentView_Success()
        {
            var userId = "23c512d2-b8d8-46ec-b15c-4442e4d4cfbe";

            await repo.AddAsync(new User()
            {
                Id = userId,
                UserName = "D"
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
            var post = await gameService.GetCommentView(1, userId);
            //Assert
            Assert.That(post.Id, Is.EqualTo(1));
            Assert.That(post.UserName, Is.EqualTo("D"));
        }

        [TestCase(null, "23c512d2-b8d8-46ec-b15c-4442e4d4cfbe", "23c512d2-b8d8-46ec-b15c-4442e4d4cfbe", 0)]
        [TestCase(null, "23c512d2-b8d8-46ec-b15c-4442e4d4cfbe", "4df272e3-8ddb-4218-8da7-006e32f8433c", 1)]
        public async Task AddComment_Exceptions(CommentPostModel model,string userId, string secondUserId, int gameId)
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

            Assert.ThrowsAsync<ArgumentException>(async () => await gameService.AddComment(model, gameId, secondUserId));
        }

        //[Test]
        //public async Task AddComment_Success()
        //{
        //    var userId = "23c512d2-b8d8-46ec-b15c-4442e4d4cfbe";

        //    await repo.AddAsync(new User()
        //    {
        //        Id = userId,
        //        UserName = "D"
        //    });
        //    var game = new Game()
        //    {
        //        Id = 1,
        //        ImageUrl = "",
        //        Description = "",
        //        Title = "",
        //        UserId = userId,
        //        Comments = new List<Comment>()
        //    };
        //    await repo.AddAsync(game);
        //    await repo.SaveChangesAsync();

        //    var commentPost = new CommentPostModel()
        //    {
        //        CommentId = 1,
        //        CommentDescription = "Buzz"
        //    };

        //    //Act
        //    await gameService.AddComment(commentPost, 1, userId);
        //    //Assert
        //    Assert.That(game.Comments.Count(), Is.EqualTo(1));
        //}

        [TestCase("23c512d2-b8d8-46ec-b15c-4442e4d4cfbe", "23c512d2-b8d8-46ec-b15c-4442e4d4cfbe", 0)]
        [TestCase("23c512d2-b8d8-46ec-b15c-4442e4d4cfbe", "4df272e3-8ddb-4218-8da7-006e32f8433c", 1)]
        public async Task LikePost_Exceptions(string userId, string secondUserId, int gameId)
        {
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
                UserId = secondUserId
            });
            await repo.SaveChangesAsync();

            Assert.ThrowsAsync<ArgumentException>(async () => await gameService.LikePost(gameId, secondUserId));
        }

        [Test]
        public async Task LikePost_Success()
        {
            await repo.AddAsync(new User()
            {
                Id = "1",
                UsersGamesForLike = new List<UserGameForLike>()
            });
            var game = new Game()
            {
                Id = 1,
                ImageUrl = "",
                Description = "",
                Title = "",
                UserId = "2",
                HasDisliked = false,
                HasLiked = false,
                LikesCount = 0,
                DislikesCount = 0,
                UsersGamesForLike = new List<UserGameForLike>()
            };
            await repo.AddAsync(new User()
            {
                Id = "2",
                UsersGamesForLike = new List<UserGameForLike>()
            });
            await repo.AddAsync(new User()
            {
                Id = "3",
                UsersGamesForLike = new List<UserGameForLike>()
            });
            var secondgame = new Game()
            {
                Id = 2,
                ImageUrl = "",
                Description = "",
                Title = "SD",
                UserId = "3",
                HasDisliked = false,
                HasLiked = true,
                LikesCount = 1,
                DislikesCount = 0,
                UsersGamesForLike = new List<UserGameForLike>()
                {
                    new UserGameForLike()
                    {
                        GameId = 2,
                        UserId = "2"
                    }
                }
            };
            var thirdgame = new Game()
            {
                Id = 3,
                ImageUrl = "",
                Description = "",
                Title = "SD",
                UserId = "4",
                HasDisliked = true,
                HasLiked = false,
                LikesCount = 1,
                DislikesCount = 0,
                UsersGamesForLike = new List<UserGameForLike>()
                {
                    new UserGameForLike()
                    {
                        GameId = 3,
                        UserId = "3"
                    }
                }
            };
            await repo.AddAsync(game);
            await repo.AddAsync(secondgame);
            await repo.AddAsync(thirdgame);
            await repo.SaveChangesAsync();

            await gameService.LikePost(1, "1");
            await gameService.LikePost(2, "2");
            await gameService.LikePost(3, "3");

            Assert.That(game.LikesCount, Is.EqualTo(1));
            Assert.That(game.HasLiked == true);

            Assert.That(secondgame.LikesCount, Is.EqualTo(0));
            Assert.That(secondgame.HasLiked == false);

            Assert.That(thirdgame.LikesCount, Is.EqualTo(1));
            Assert.That(thirdgame.HasLiked == false);
            Assert.That(thirdgame.HasDisliked == true);
        }

        [TestCase("23c512d2-b8d8-46ec-b15c-4442e4d4cfbe", "23c512d2-b8d8-46ec-b15c-4442e4d4cfbe", 0)]
        [TestCase("23c512d2-b8d8-46ec-b15c-4442e4d4cfbe", "4df272e3-8ddb-4218-8da7-006e32f8433c", 1)]
        public async Task DislikePost_Exceptions(string userId, string secondUserId, int gameId)
        {
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
                UserId = secondUserId
            });
            await repo.SaveChangesAsync();

            Assert.ThrowsAsync<ArgumentException>(async () => await gameService.DislikePost(gameId, secondUserId));
        }

        [Test]
        public async Task DislikePost_Success()
        {
            await repo.AddAsync(new User()
            {
                Id = "1",
                UsersGamesForLike = new List<UserGameForLike>()
            });
            var game = new Game()
            {
                Id = 1,
                ImageUrl = "",
                Description = "",
                Title = "",
                UserId = "2",
                HasDisliked = false,
                HasLiked = false,
                LikesCount = 0,
                DislikesCount = 0,
                UsersGamesForLike = new List<UserGameForLike>()
            };
            await repo.AddAsync(new User()
            {
                Id = "2",
                UsersGamesForLike = new List<UserGameForLike>()
            });
            await repo.AddAsync(new User()
            {
                Id = "3",
                UsersGamesForLike = new List<UserGameForLike>()
            });
            var secondgame = new Game()
            {
                Id = 2,
                ImageUrl = "",
                Description = "",
                Title = "SD",
                UserId = "3",
                HasDisliked = true,
                HasLiked = false,
                LikesCount = 1,
                DislikesCount = 1,
                UsersGamesForLike = new List<UserGameForLike>()
                {
                    new UserGameForLike()
                    {
                        GameId = 2,
                        UserId = "2"
                    }
                }
            };
            var thirdgame = new Game()
            {
                Id = 3,
                ImageUrl = "",
                Description = "",
                Title = "SD",
                UserId = "4",
                HasDisliked = false,
                HasLiked = true,
                LikesCount = 0,
                DislikesCount = 1,
                UsersGamesForLike = new List<UserGameForLike>()
                {
                    new UserGameForLike()
                    {
                        GameId = 3,
                        UserId = "3"
                    }
                }
            };
            await repo.AddAsync(game);
            await repo.AddAsync(secondgame);
            await repo.AddAsync(thirdgame);
            await repo.SaveChangesAsync();

            await gameService.DislikePost(1, "1");
            await gameService.DislikePost(2, "2");
            await gameService.DislikePost(3, "3");

            Assert.That(game.DislikesCount, Is.EqualTo(1));
            Assert.That(game.HasDisliked == true);

            Assert.That(secondgame.DislikesCount, Is.EqualTo(0));
            Assert.That(secondgame.HasDisliked == false);

            Assert.That(thirdgame.DislikesCount, Is.EqualTo(1));
            Assert.That(thirdgame.HasLiked == true);
            Assert.That(thirdgame.HasDisliked == false);
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

        [Test]
        public async Task GetGamesCreatedByUserId_Success()
        {
            //arrange
            var genre = new Genre()
            {
                Id = 1,
                GenreName = "Epic"
            };
            await repo.AddAsync(genre);
            string userid = "6c0e1cc2-e4d5-4b77-a9ae-c88a0d2bd184";
            await repo.AddAsync(new Game()
            {
                Id = 1,
                Description = "",
                ImageUrl = "",
                Rating = 10.00m,
                Title = "League",
                UserId = userid,
                DislikesCount = 0,
                LikesCount = 0,
                Genre = genre
            });

            await repo.SaveChangesAsync();
            //act
            var games = await gameService.GetGamesCreatedByUserId(userid);
            var game = games.FirstOrDefault(x => x.UserId == userid);

            Assert.That(games.Count(), Is.EqualTo(1));
            Assert.That(game.Title, Is.EqualTo("League"));
        }

        [Test]
        public async Task GetGameById_Null()
        {
            await repo.AddAsync(new Game()
            {
                Id = 1,
                Description = "",
                ImageUrl = "",
                Rating = 10.00m,
                Title = "League",
                UserId = "1",
                DislikesCount = 0,
                LikesCount = 0
            });
            await repo.SaveChangesAsync();

            Assert.ThrowsAsync<ArgumentException>(async () => await gameService.GetGameById(2));
        }

        [Test]
        public async Task GetGameById_GetsGameById()
        {
            await repo.AddAsync(new Game()
            {
                Id = 1,
                Description = "",
                ImageUrl = "",
                Rating = 10.00m,
                Title = "League",
                UserId = "1",
                DislikesCount = 0,
                LikesCount = 0
            });
            await repo.SaveChangesAsync();

            var game = await gameService.GetGameById(1);

            Assert.That(game.Title, Is.EqualTo("League"));
            Assert.That(game.Description, Is.EqualTo(""));
        }

        [TestCase(1, 0)]
        [TestCase(0, 1)]
        public async Task RemoveComment_Exceptions(int commentId, int gameId)
        {
            await repo.AddAsync(new Game()
            {
                Id = 1,
                ImageUrl = "",
                Description = "",
                Title = "",
                UserId = "1",
                Comments = new List<Comment>()
                {
                    new Comment()
                    {
                        Id = 1,
                        Description = "",
                        UserId = "1"
                    }
                }
            });
            await repo.SaveChangesAsync();

            Assert.ThrowsAsync<ArgumentException>(async () => await gameService.RemoveGameCommentById(commentId, gameId));
        }

        [Test]
        public async Task RemoveComment_Success()
        {
            var game = new Game()
            {
                Id = 1,
                ImageUrl = "",
                Description = "",
                Title = "",
                UserId = "1",
                Comments = new List<Comment>()
                {
                    new Comment()
                    {
                        Id = 1,
                        Description = "",
                        UserId = "1"
                    }
                }
            };
            await repo.AddAsync(game);
            await repo.SaveChangesAsync();

            await gameService.RemoveGameCommentById(1, 1);

            Assert.That(game.Comments.Count, Is.EqualTo(0));
        }

        [TearDown]
        public void TearDown()
        {
            this.context.Dispose();
        }
    }
}
