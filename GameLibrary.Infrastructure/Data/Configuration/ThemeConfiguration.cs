using GameLibrary.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Infrastructure.Data.Configuration
{
    public class ThemeConfiguration : IEntityTypeConfiguration<Theme>
    {
        public void Configure(EntityTypeBuilder<Theme> builder)
        {
            builder.HasData(SeedThemes());
        }

        private List<Theme> SeedThemes()
        {
            var themes = new List<Theme>()
            {
                new Theme()
                {
                    Id = 1,
                    ThemeName = "Action"
                },
                new Theme()
                {
                    Id = 2,
                    ThemeName = "Mystery"
                },
                new Theme()
                {
                    Id = 3,
                    ThemeName = "OpenWorld"
                },
                new Theme()
                {
                    Id = 4,
                    ThemeName = "Horror"
                },
                new Theme()
                {
                    Id = 5,
                    ThemeName = "Romantic"
                }
            };

            return themes;
        }
    }
}
