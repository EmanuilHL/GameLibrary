using GameLibrary.Core.Contracts;
using GameLibrary.Core.Models;
using GameLibrary.Core.Models.Game;
using GameLibrary.Infrastructure.Data.Common;
using GameLibrary.Infrastructure.Data.Entities;
using GameLibrary.Infrastructure.Data.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Core.Services
{
    public class GameService : IGameService
    {
        private readonly IRepository repo;

        public GameService(IRepository repo)
        {
            this.repo = repo;
        }

        /// <summary>
        /// Adds game to the database
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Savechanges</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task AddGameAsync(GameFormViewModel model, string id)
        {
            var entity = new Game()
            {
                Description = model.Description,
                ThemeId = model.ThemeId,
                GenreId = model.GenreId,
                ImageUrl = model.ImageUrl,
                Rating = model.Rating,
                Title = model.Title,
                UserId = id
            };

            var result = await repo.AllReadonly<Game>()
                .Where(x => x.ImageUrl == entity.ImageUrl || x.Title == entity.Title).FirstOrDefaultAsync();

            if (result == null)
            {
                await repo.AddAsync(entity);
                await repo.SaveChangesAsync();
            }
        }


        /// <summary>
        /// gets All the genres (more will be seeded soon)
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<IEnumerable<Genre>> GetAllGenres()
        {
            return await repo.AllReadonly<Genre>().ToListAsync();
        }

        /// <summary>
        /// gets all the themes (few more will be seeded soon)
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<IEnumerable<Theme>> GetAllThemes()
        {
            return await repo.AllReadonly<Theme>().ToListAsync();
        }


        /// <summary>
        /// Adds a certain game to a certain user's database. 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="gameId"></param>
        /// <returns>Savechanges to the user's database</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task MarkGameAsFavourite(string userId, int gameId)
        {
            var user = await repo.All<User>()
                .Where(x => x.Id == userId)
                .Include(c => c.UsersGames)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("Invalid User");
            }

            var game = await repo.All<Game>().FirstOrDefaultAsync(g => g.Id == gameId);

            if (game == null)
            {
                throw new ArgumentException("Invalid Game");
            }

            if (!user.UsersGames.Any(g => g.GameId == gameId))
            {
                user.UsersGames.Add(new UserGame()
                {
                    Game = game,
                    GameId = gameId,
                    UserId = userId,
                    User = user
                });


                await repo.SaveChangesAsync();
            }
        }


        /// <summary>
        /// Show all personally liked games by the user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<IEnumerable<GameViewModel>> ShowAllFavourites(string userId)
        {
            var user = await repo.AllReadonly<User>()
                .Where(u => u.Id == userId)
                .Include(u => u.UsersGames)
                .ThenInclude(um => um.Game)
                .ThenInclude(m => m.Genre)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("Invalid user ID");
            }

            return user.UsersGames
                .Select(x => new GameViewModel()
                {
                    Title = x.Game.Title,
                    ImageUrl = x.Game.ImageUrl,
                    Rating = x.Game.Rating,
                    ReviewType = getReviewType(x.Game.Rating).ToString(),
                    Genre = x.Game.Genre.GenreName,
                    Description = x.Game.Description,
                    Id = x.Game.Id
                });
        }

        /// <summary>
        /// Unfavour a favourite gamepost selected by the user.
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task MarkGameAsUnfavourite(int gameId, string userId)
        {
            var user = await repo.All<User>()
                .Where(u => u.Id == userId)
                .Include(u => u.UsersGames)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("Invalid user ID");
            }

            var game = user.UsersGames.FirstOrDefault(m => m.GameId == gameId);

            if (game != null)
            {
                user.UsersGames.Remove(game);

                await repo.SaveChangesAsync();
            }
        }


        /// <summary>
        /// Find a Game by Id
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<GameViewModel> FindHottestGame()
        {
            var game = await repo.AllReadonly<Game>()
                .OrderByDescending(x => x.LikesCount)
                .ThenBy(x => x.DislikesCount)
                .Select(x => new GameViewModel()
                {
                    Id = x.Id,
                    Title = x.Title,
                    ImageUrl = x.ImageUrl,
                    ReviewType = getReviewType(x.Rating).ToString(),
                    UserId = x.UserId,
                    LikesCount = x.LikesCount,
                    DislikesCount = x.DislikesCount
                })
                .FirstOrDefaultAsync();


            return game;
        }

        /// <summary>
        /// Finds the needed edit post
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<GameFormViewModel> FindEditPostById(int gameId)
        {
            var game = await repo.All<Game>().FirstOrDefaultAsync(x => x.Id == gameId);

            if (game == null)
            {
                throw new ArgumentException("Game not found");
            }

            var entity = new GameFormViewModel()
            {
                Description = game.Description,
                ThemeId = game.ThemeId,
                GenreId = game.GenreId,
                ImageUrl = game.ImageUrl,
                Rating = game.Rating,
                Title = game.Title,
                Id = gameId,
                UserId = game.UserId,
                Genres = await GetAllGenres(),
                Themes = await GetAllThemes(),
            };

            return entity;
        }

        /// <summary>
        /// Edits a game
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task EditGameAsync(GameFormViewModel model)
        {
            var entity = await repo.GetByIdAsync<Game>(model.Id);

            entity.Id = model.Id;
            entity.Description = model.Description;
            entity.GenreId = model.GenreId;
            entity.ThemeId = model.ThemeId;
            entity.ImageUrl = model.ImageUrl;
            entity.Rating = model.Rating;
            entity.Title = model.Title;
            entity.UserId = model.UserId;

            await repo.SaveChangesAsync();
        }

        /// <summary>
        /// Gets the delete model for the delete page.
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<GameViewModel> GetGameToDelete(int gameId)
        {
            var game = await repo.All<Game>().FirstOrDefaultAsync(x => x.Id == gameId);

            if (game == null)
            {
                throw new ArgumentException("Game not found");
            }

            var entity = new GameViewModel()
            {
                Description = game.Description,
                ImageUrl = game.ImageUrl,
                Rating = game.Rating,
                Title = game.Title,
                Id = gameId,
                UserId = game.UserId
            };

            return entity;
        }

        /// <summary>
        /// Checks if a games exists by Id.
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns></returns>
        public async Task<bool> CheckIfGameExistsById(int gameId)
        {
            return await repo.All<Game>().AnyAsync(x => x.Id == gameId);
        }

        /// <summary>
        /// Deletes a game post.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteGamePost(int id)
        {
            var game = await repo.All<Game>().FirstOrDefaultAsync(x => x.Id == id);

            if (game == null)
            {
                throw new ArgumentException("Game is not existend.");
            }

            game.Comments = new List<Comment>();


            await repo.DeleteAsync<Game>(id);
            await repo.SaveChangesAsync();
        }

        /// <summary>
        /// Searches and displays gameposts.
        /// </summary>
        /// <param name="theme"></param>
        /// <param name="searchTerm"></param>
        /// <param name="sorting"></param>
        /// <returns></returns>
        public async Task<GameQueryModel> Search(string? theme = null, string? searchTerm = null
            , RatingSorting sorting = RatingSorting.None)
        {


            var result = new GameQueryModel();
            var games = repo.All<Game>();

            

            if (string.IsNullOrEmpty(theme) == false)
            {
                games = games
                    .Where(h => h.Theme.ThemeName == theme)
                    .OrderBy(x => x.Title);
            }

            if (string.IsNullOrEmpty(searchTerm) == false)
            {
                searchTerm = $"%{searchTerm.ToLower()}%";

                games = games
                    .Where(h => EF.Functions.Like(h.Title.ToLower(), searchTerm))
                    .OrderByDescending(x => x.Rating);
            }

            

            games = sorting switch
            {
                RatingSorting.Ascending => games
                    .OrderBy(h => h.Rating),
                RatingSorting.Descending => games
                    .OrderByDescending(h => h.Rating),
                _ => games
            };

            result.Games = await games
                .Include(c => c.Genre)
                .Select(g => new GameViewModel()
                {
                    Id = g.Id,
                    Description = g.Description,
                    ImageUrl = g.ImageUrl,
                    Rating = g.Rating,
                    Title = g.Title,
                    UserId = g.UserId,
                    Genre = g.Genre.GenreName,
                    ReviewType = getReviewType(g.Rating).ToString()
                })
                .ToListAsync();

            return result;
        }

        private static ReviewType getReviewType(decimal rating)
        {
            int id = int.MinValue;

            if (0.0m <= rating && rating < 2.0m)
            {
                id = 4;
            }
            else if (2.0m <= rating && rating < 4.0m)
            {
                id = 3;
            }
            else if (4.0m <= rating && rating < 6.0m)
            {
                id = 2;
            }
            else if (6.0m <= rating && rating < 8.0m)
            {
                id = 1;
            }
            else if (8.0m <= rating && rating <= 10.0m)
            {
                id = 0;
            }

            return (ReviewType)id;
        }

        public async Task<CommentViewModel> ShowDetailsPage(int gameId)
        {
            var game = await repo.All<Game>()
                .Where(g => g.Id == gameId)
                .Select(x => new CommentViewModel()
                {
                    Title = x.Title,
                    Description = x.Description,
                    Rating = x.Rating,
                    ImageUrl = x.ImageUrl,
                    Genre = x.Genre.GenreName,
                    ReviewType = getReviewType(x.Rating).ToString(),
                    Id = gameId,
                    UserId = x.UserId,
                    LikesCount = x.LikesCount,
                    DislikesCount = x.DislikesCount,
                    Comments = x.Comments.Select(i => new CommentFormModel()
                    {
                        CommentDescription = i.Description,
                        CommentId = i.Id,
                        UserName = i.User.UserName,
                    }).ToList()
                }).FirstOrDefaultAsync();


            if (game == null)
            {
                throw new ArgumentException("Game could not be found!");
            }

            return game;
        }



        public async Task<CommentPostModel> GetCommentView(int gameId, string userId)
        {
            
            var game = await repo.All<Game>().FirstOrDefaultAsync(x => x.Id == gameId);
            var user = await repo.All<User>().FirstOrDefaultAsync(x => x.Id == userId);

            if (game == null)
            {
                throw new ArgumentException("Invalid Game");
            }

            if (user == null)
            {
                throw new ArgumentException("Invalid User");
            }

            return new CommentPostModel()
            {
                Id = gameId,
                UserName = user.UserName
            };
        }

        public async Task AddComment(CommentPostModel model, int gameId, string userId)
        {
            var game = await repo.All<Game>().FirstOrDefaultAsync(x => x.Id == gameId);
            var user = await repo.All<User>().FirstOrDefaultAsync(x => x.Id == userId);

            if (game == null)
            {
                throw new ArgumentException("Invalid Game");
            }

            if (user == null)
            {
                throw new ArgumentException("Invalid User");
            }

            var comment = new Comment()
            {
                Description = model.CommentDescription,
                Id = model.CommentId,
                UserId = userId,
                User = user
            };

            //await repo.AddAsync(comment);
            game.Comments.Add(comment);

            await repo.SaveChangesAsync();
        }

        /// <summary>
        /// Likes a gamepost
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns>SaveChanges</returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task LikePost(int gameId, string userId)
        {
            var game = await repo.All<Game>().FirstOrDefaultAsync(x => x.Id == gameId);

            if (game == null)
            {
                throw new ArgumentException("Game is not found at likePost.");
            }

            var user = await repo.All<User>().Where(x => x.Id == userId)
                .Include(c => c.UsersGamesForLike).FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("User is not found at likePost.");
            }

            ///Allows a duplicate // fix 
            if (!user.UsersGamesForLike.Any(g => g.GameId == gameId))
            {
                user.UsersGamesForLike.Add(new UserGameForLike()
                {
                    Game = game,
                    GameId = gameId,
                    UserId = userId, 
                    User = user
                });
            }

            bool didUserLikeGame = user.UsersGamesForLike.Any(g => g.GameId == gameId && g.Game.HasLiked);
            bool didUserDisLikeGame = user.UsersGamesForLike.Any(g => g.GameId == gameId && g.Game.HasDisliked);


            if (didUserDisLikeGame == false)
            {
                if (didUserLikeGame)
                {
                    game.HasLiked = false;
                    game.LikesCount--;
                }
                else
                {
                    game.HasLiked = true;
                    game.LikesCount++;
                }
            }

            //if (!game.IsFirst)
            //{
            //    game.BaseRating = game.Rating;
            //    game.IsFirst = true;
            //}

            //if (game.LikesCount == game.DislikesCount)
            //{
            //    game.Rating = game.BaseRating;
            //}
            //else
            //{
            //    decimal count = (game.LikesCount / (game.LikesCount + game.DislikesCount)) * 10;

            //    game.Rating = (game.Rating + count) / 2;

            //    if (game.Rating > 10.0m)
            //    {
            //        game.Rating = 10.0m;
            //    }
            //}

            await repo.SaveChangesAsync();
        }

        /// <summary>
        /// Dislikes a gamepost
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="userId"></param>
        /// <returns>Savechanges</returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task DislikePost(int gameId, string userId)
        {
            var game = await repo.All<Game>().FirstOrDefaultAsync(x => x.Id == gameId);

            if (game == null)
            {
                throw new ArgumentException("Game is not found at dislikePost.");
            }

            var user = await repo.All<User>().Where(x => x.Id == userId)
                .Include(c => c.UsersGamesForLike).FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("User is not found at dislikePOst.");
            }

            if (!user.UsersGamesForLike.Any(g => g.GameId == gameId))
            {
                user.UsersGamesForLike.Add(new UserGameForLike()
                {
                    Game = game,
                    GameId = gameId,
                    UserId = userId,
                    User = user
                });

            }

            bool didUserLikeGame = user.UsersGamesForLike.Any(g => g.GameId == gameId && g.Game.HasLiked);
            bool didUserDisLikeGame = user.UsersGamesForLike.Any(g => g.GameId == gameId && g.Game.HasDisliked);


            if (didUserLikeGame == false)
            {
                if (didUserDisLikeGame)
                {
                    game.HasDisliked = false;
                    game.DislikesCount--;
                }
                else
                {
                    game.HasDisliked = true;
                    game.DislikesCount++;
                }
            }

            //if (user.HasLiked == false)
            //{
            //    if (user.HasDisliked)
            //    {
            //        user.HasDisliked = false;
            //        game.DislikesCount--;
            //    }
            //    else
            //    {
            //        user.HasDisliked = true;
            //        game.DislikesCount++;
            //    }
            //}

            //if (!game.IsFirst)
            //{
            //    game.BaseRating = game.Rating;
            //    game.IsFirst = true;
            //}

            //if (game.LikesCount == game.DislikesCount)
            //{
            //    game.Rating = game.BaseRating;
            //}
            //else
            //{
            //    decimal count = (game.LikesCount / (game.LikesCount + game.DislikesCount)) * 10;

            //    game.Rating = Math.Round((game.Rating + count) / 2, 2);

            //    if (game.Rating > 10.0m)
            //    {
            //        game.Rating = 10.0m;
            //    }
            //}

            await repo.SaveChangesAsync();
        }


        /// <summary>
        /// Shows if a user has added atleast one post.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> IsUserDevelepor(string userId)
        { 
            return await repo.All<Game>().AnyAsync(x => x.UserId == userId);
        }

    }
}
