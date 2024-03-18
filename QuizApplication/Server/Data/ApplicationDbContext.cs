
using Microsoft.EntityFrameworkCore;
using QuizApplication.Server.Models.Domain;
using System.Drawing.Imaging;

namespace QuizApplication.Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<MediaType> MediaType { get; set; } 
        public DbSet<MediaFile>? MediaFiles { get; set; }
        public DbSet<Question>? Questions { get; set; }
        public DbSet<Answer>? Answers { get; set; }
        public DbSet<QuizItem>? QuizItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MediaType>()
               .HasIndex(u => u.Mediatype)
               .IsUnique();

            modelBuilder.Entity<MediaFile>()
               .HasIndex(u => u.MediaFileName)
               .IsUnique();

            modelBuilder.Entity<Question>()
               .HasIndex(u => u.QuestionPath)
               .IsUnique();

            modelBuilder.Entity<Answer>()
               .HasIndex(u => u.Content)
               .IsUnique();


            modelBuilder.Entity<MediaFile>()
            .HasOne(u => u.MediaTypes)
            .WithMany(a => a.MediaFiles)
            .HasForeignKey(u => u.FkMediaTypeId);

            modelBuilder.Entity<Question>()
            .HasOne(u => u.ApplicationUsers)
            .WithMany(a => a.Questions)
            .HasForeignKey(u => u.FkUserId);

            modelBuilder.Entity<Question>()
           .HasOne(u => u.MediaFiles)
           .WithMany(a => a.Questions)
           .HasForeignKey(u => u.FkFileId);

            modelBuilder.Entity<Answer>()
            .HasOne(u => u.Questions)
            .WithMany(a => a.Answers)
            .HasForeignKey(u => u.FkQuestionId);

            modelBuilder.Entity<QuizItem>()
            .HasOne(u => u.Questions)
            .WithMany(a => a.QuizItems)
            .HasForeignKey(u => u.FkQuestionId)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<QuizItem>()
            .HasOne(u => u.ApplicationUsers)
            .WithMany(a => a.QuizItems)
            .HasForeignKey(u => u.FkUserId);

            modelBuilder.Seed();
        }
    }
}
