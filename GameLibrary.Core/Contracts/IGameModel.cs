using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Core.Contracts
{
    public interface IGameModel
    {
        public string Title { get; }

        public string Description { get; }
    }
}
