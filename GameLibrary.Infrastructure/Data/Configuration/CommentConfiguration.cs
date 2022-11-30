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
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasData(SeedComments());
        }

        private List<Comment> SeedComments()
        {
            var comments = new List<Comment>();

            var comment = new Comment()
            {
                Id = 1,
                Description = "Omg guys this guy's program is really impressive!",
                UserId = "dea12856-c198-4129-b3f3-b893d8395082"
            };

            comments.Add(comment);

            return comments;
        }
    }
}
