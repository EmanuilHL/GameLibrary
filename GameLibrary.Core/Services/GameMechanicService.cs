using GameLibrary.Core.Contracts;
using GameLibrary.Core.Models.GameMechanic;
using GameLibrary.Infrastructure.Data.Common;
using GameLibrary.Infrastructure.Data.Entities;
using Ganss.Xss;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Core.Services
{
    public class GameMechanicService : IGameMechanicService
    {
        private readonly IRepository repo;

        public GameMechanicService(IRepository repo)
        {
            this.repo = repo;
        }

        /// <summary>
        /// Gets all the game mechanic reports for one developer
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<MechanicsViewModel>> All(string userId)
        {
            var gameMechanics = await repo.All<GameMechanic>().Where(x => x.UserId == userId).ToListAsync();

            return gameMechanics.Select(x => new MechanicsViewModel()
            {
                UserId = x.UserId,
                MechanicDescription = x.MechanicDescription,
                GameId = x.GameId,
                GameName = x.GameName,
                MechanicId = x.Id
            });
        }

        /// <summary>
        /// creates a mechanic
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task CreateGameMechanic(MechanicsFormModel model, string userId)
        {
            HtmlSanitizer sanitizer = new HtmlSanitizer();
            var game = await FindGameByName(model.GameName);

            if (game == null)
            {
                throw new ArgumentException("Game does not exist.");
            }

            if (game.Title != model.GameName)
            {
                throw new ArgumentException("GameName does not match.");
            }

            var entity = new GameMechanic()
            {
                MechanicDescription = sanitizer.Sanitize(model.MechanicDescription),
                GameName = sanitizer.Sanitize(model.GameName),
                GameId = game.Id,
                UserId = userId
            };
            
            await repo.AddAsync(entity);
            await repo.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes a gamemechanic record from the database
        /// </summary>
        /// <param name="mechanicId"></param>
        /// <returns></returns>
        public async Task RemoveMechanicReport(int mechanicId)
        {
            await repo.DeleteAsync<GameMechanic>(mechanicId);
            await repo.SaveChangesAsync();
        }

        /// <summary>
        /// Sends all the game mechanics reports to the admin
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<MechanicsViewModel>> Reports()
        {
            return await repo.AllReadonly<GameMechanic>()
                .Select(x => new MechanicsViewModel()
                {
                    MechanicDescription = x.MechanicDescription,
                    GameId = x.GameId,
                    GameName = x.GameName,
                    UserId = x.UserId
                })
                .ToListAsync();
        }

        /// <summary>
        /// Finds game by name
        /// </summary>
        /// <param name="gameName"></param>
        /// <returns>null or the game</returns>
        private async Task<Game> FindGameByName(string gameName)
        {
            return await repo.All<Game>().Where(x => x.Title == gameName).FirstOrDefaultAsync();
        }
    }
}
