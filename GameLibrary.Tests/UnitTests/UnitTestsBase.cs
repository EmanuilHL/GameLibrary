using GameLibrary.Infrastructure.Data;
using GameLibrary.Infrastructure.Data.Entities;
using GameLibrary.Tests.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Tests.UnitTests
{
    public class UnitTestsBase
    {
        protected ApplicationDbContext data;

        [OneTimeSetUp]
        public void SetUpBase()
        {
            this.data = DatabaseMock.Data;
            this.SeedDatabase();
        }

        public User User1 { get; private set; }
        public User User2 { get; private set; }
        public Helper Helper { get; private set; }
        public Game Game { get; private set; }
        public GameMechanic GameMechanic { get; private set; }
        public Genre Genre { get; private set; }
        public Theme Theme { get; private set; }


        private void SeedDatabase()
        {
            this.User1 = new User()
            {
                Email = "user@abv.bg",
                Id = "1b69efc7-0728-4e84-9717-6e2da37d6eb1",
                UserName = "Userovic"
            };

            this.data.Users.Add(this.User1);

            this.User2 = new User()
            {
                Email = "user2@abv.bg",
                Id = "d30f841f-7ecd-4440-ade2-b507f74a9058",
                UserName = "Userovicc"
            };

            this.data.Users.Add(this.User2);

            this.Helper = new Helper()
            {
                Id = 1,
                PhoneNumber = "+35912345678",
                UserId = "d30f841f-7ecd-4440-ade2-b507f74a9058"
            };

            this.data.Helpers.Add(this.Helper);

            this.Genre = new Genre()
            {
                Id = 1,
                GenreName = "Epicness"
            };
            this.data.Genres.Add(this.Genre);

            this.Theme = new Theme()
            {
                Id = 1,
                ThemeName = "Adventure"
            };

            this.data.Themes.Add(this.Theme);

            this.Game = new Game()
            {
                Id = 2,
                Description = "This is a very good description about a decription in which there is a pretty description.!",
                DislikesCount = 3,
                HasDisliked = false,
                GenreId = 1,
                ThemeId = 1,
                LikesCount = 2,
                ImageUrl = "https://www.bhg.com/thmb/0Fg0imFSA6HVZMS2DFWPvjbYDoQ=/1500x0/filters:no_upscale():max_bytes(150000):strip_icc()/white-modern-house-curved-patio-archway-c0a4a3b3-aa51b24d14d0464ea15d36e05aa85ac9.jpg",
                HasLiked = false,
                Rating = 10.00m,
                Title = "House Simulator",
                UserId = "1b69efc7-0728-4e84-9717-6e2da37d6eb1",
                Comments = new List<Comment>()
                {
                    new Comment()
                    {
                        Description = "This game is truly cool!",
                        GameId = 2,
                        Id = 1,
                        UserId = "d30f841f-7ecd-4440-ade2-b507f74a9058"
                    }
                }
            };

            this.data.Games.Add(this.Game);

            this.GameMechanic = new GameMechanic()
            {
                MechanicDescription = "THis game is really boring and needs some challenges to distract the player",
                GameName = "House Simulator",
                GameId = 2,
                UserId = "d30f841f-7ecd-4440-ade2-b507f74a9058"
            };

            this.data.GameMechanics.Add(this.GameMechanic);

            this.data.SaveChanges();
        }

        [OneTimeTearDown]
        public void TearDownBase() => this.data.Dispose();
    }
}
