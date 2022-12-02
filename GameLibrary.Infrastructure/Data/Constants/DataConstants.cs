using GameLibrary.Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Infrastructure.Data.Constants
{
    public static class DataConstants
    {
        public static class Game
        {
            public const int MaxTitleLength = 50;
            public const int MinTitleLength = 3;

            public const int MaxImageLength = 300;

            public const int MaxDescriptionLength = 500;
            public const int MinDescriptionLength = 50;

            public const int Precision = 18;
            public const int Scale = 2;
        }

        public static class Comment
        {
            public const int MaxDescriptionLength = 250;
        }

        public static class Genre
        {
            public const int MaxGenreNameLength = 50;
        }

        public static class Theme
        {
            public const int MaxThemeNameLength = 35;
        }

        public static class RegisterViewModel
        {
            public const int UserNameMaxLength = 20;
            public const int UserNameMinLength = 5;

            public const int EmailMaxLength = 60;
            public const int EmailMinLength = 10;

            public const int PasswordMaxLength = 20;
            public const int PasswordMinLength = 5;
        }
    }
}
