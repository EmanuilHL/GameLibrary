using GameLibrary.Infrastructure.Data.Configuration;
using GameLibrary.Infrastructure.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace GameLibrary.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole, string>
    {
        private bool dbseed;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, bool seed = true)
            : base(options)
        {
            if (this.Database.IsRelational())
            {
                this.Database.Migrate();
            }
            else
            {
                this.Database.EnsureCreated();
            }

            dbseed = seed;
        }

        //public DbSet<Comment> Comments { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Theme> Themes { get; set; }
        public DbSet<Helper> Helpers { get; set; }
        public DbSet<GameMechanic> GameMechanics { get; set; }
        //public DbSet<Like> Likes { get; set; }

        /// <summary>
        /// Created the entity classes, now do the seed configuration (look at watchlist)
        /// and add dbset and do migration and update database
        /// Also go to appsetings.json and add your database
        /// IF it doesnt work look at alternatives for reviewtype
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserGame>().HasKey(p => new { p.UserId, p.GameId});
            builder.Entity<UserGameForLike>().HasKey(p => new { p.UserId, p.GameId});
            builder.Entity<DeveloperGame>().HasKey(p => new { p.UserId, p.GameId});

            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            //Look to test this
            builder.Entity<Comment>()
                .HasOne(d => d.Game)
                .WithMany(b => b.Comments)
                .HasForeignKey(w => w.GameId)
                .OnDelete(DeleteBehavior.Cascade);

            if (this.dbseed)
            {
                builder.ApplyConfiguration(new UserConfiguration());
                builder.ApplyConfiguration(new HelperConfiguration());
                builder.ApplyConfiguration(new GenreConfiguration());
                builder.ApplyConfiguration(new ThemeConfiguration());
            }
            //builder.ApplyConfiguration(new CommentConfiguration());

            base.OnModelCreating(builder);
        }
    }
}