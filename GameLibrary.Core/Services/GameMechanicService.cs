using GameLibrary.Core.Contracts;
using GameLibrary.Core.Models.GameMechanic;
using GameLibrary.Infrastructure.Data.Common;
using GameLibrary.Infrastructure.Data.Entities;
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

        public async Task<IEnumerable<MechanicsViewModel>> All(string userId)
        {
            var gameMechanics = await repo.All<GameMechanic>().Where(x => x.UserId == userId).ToListAsync();

            return gameMechanics.Select(x => new MechanicsViewModel()
            {
                UserId = x.UserId,
                MechanicDescription = x.MechanicDescription,
                GameId = x.GameId,
                GameName = x.GameName
            });
        }

        public async Task CreateGameMechanic(MechanicsFormModel model, string userId)
        {
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
                MechanicDescription = model.MechanicDescription,
                GameName = model.GameName,
                GameId = game.Id,
                UserId = userId
            };
            
            await repo.AddAsync(entity);
            await repo.SaveChangesAsync();
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
