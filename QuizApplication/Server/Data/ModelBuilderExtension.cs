using Microsoft.EntityFrameworkCore;
using QuizApplication.Server.Models.Domain;

namespace QuizApplication.Server.Data
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MediaType>().HasData(new[]
            {
                new MediaType{ MediaId = Guid.NewGuid(), Mediatype = "image"},
                new MediaType{ MediaId = Guid.NewGuid(), Mediatype = "video"}
            });
        }
    }
}

