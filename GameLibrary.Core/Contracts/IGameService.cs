using GameLibrary.Core.Models;
using GameLibrary.Core.Models.Game;
using GameLibrary.Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Core.Contracts
{
    public interface IGameService
    {
        //Task<IEnumerable<GameViewModel>> GetAllAsync();

        Task<IEnumerable<Genre>> GetAllGenres();
        Task<IEnumerable<Theme>> GetAllThemes();

        Task AddGameAsync(GameFormViewModel model, string id);
        Task AddComment(CommentPostModel model, int gameId, string userId);
        Task DeleteGamePost(int id);
        Task EditGameAsync(GameFormViewModel model);
        Task<GameFormViewModel> FindEditPostById(int gameId);
        Task<GameViewModel> GetGameToDelete(int gameId);

        Task MarkGameAsFavourite(string userId, int gameId);

        Task<IEnumerable<GameViewModel>> ShowAllFavourites(string userId);

        Task MarkGameAsUnfavourite(int gameId, string userId);

        Task<GameViewModel> FindHottestGame();
        Task<CommentPostModel> GetCommentView(int gameId, string userId);
        Task<CommentViewModel> ShowDetailsPage(int gameId);

        Task<bool> CheckIfGameExistsById(int gameId);

        Task<GameQueryModel> Search(string? theme = null, string? searchTerm = null, RatingSorting sorting = RatingSorting.None);

        Task LikePost(int gameId, string userId);
        Task DislikePost(int gameId, string userId);

        Task<bool> IsUserDevelepor(string userId);

        Task<IEnumerable<GameViewModel>> GetGamesCreatedByUserId(string userId);

        Task<CommentViewModel> GetGameById(int gameId);

        Task RemoveGameCommentById(int commentId, int gameId);
    }
}
