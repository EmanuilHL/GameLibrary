using GameLibrary.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GameLibrary.Core.Extensions
{
    public static class ModelExtension
    {
        public static string GetInformation(this IGameModel game)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(game.Title.Replace(" ", "-"));
            sb.Append("-");
            sb.Append(GetDescription(game.Description));

            return sb.ToString();
        }

        private static string GetDescription(string description)
        {
            string result = string
                .Join("-", description.Split(" ", StringSplitOptions.RemoveEmptyEntries).Take(3));

            return Regex.Replace(result, @"[^a-zA-Z0-9\-]", string.Empty);
        }
    }
}
