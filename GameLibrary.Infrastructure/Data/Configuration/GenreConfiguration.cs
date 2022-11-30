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
    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.HasData(SeedGenres());
        }

        private List<Genre> SeedGenres()
        {
            var genres = new List<Genre>()
            {
                new Genre()
                {
                    Id = 1,
                    GenreName = "First-Person Shooter"
                },
                new Genre()
                {
                    Id = 2,
                    GenreName = "Fighting and Martial Arts"
                },
                new Genre()
                {
                    Id = 3,
                    GenreName = "Adventure RPG"
                },
                new Genre()
                {
                    Id = 4,
                    GenreName = "Strategy RPG"
                },
                new Genre()
                {
                    Id = 5,
                    GenreName = "Dating"
                }
            };

            return genres;
        }
    }
}
