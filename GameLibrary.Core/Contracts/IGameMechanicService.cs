using GameLibrary.Core.Models.GameMechanic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Core.Contracts
{
    public interface IGameMechanicService
    {
        Task CreateGameMechanic(MechanicsFormModel model, string userId);

        Task<IEnumerable<MechanicsViewModel>> All(string userId);

        Task<IEnumerable<MechanicsViewModel>> Reports();
        Task RemoveMechanicReport(int mechanicId);
    }
}
