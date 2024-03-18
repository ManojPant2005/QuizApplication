using Microsoft.EntityFrameworkCore;
using QuizApplication.Server.Data;
using QuizApplication.Server.Models.Domain;

namespace QuizApplication.Server.Repositories
{
    public class LocalMediaFileRepository : IMediaFileRepository
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ApplicationDbContext _dbContext;

        public LocalMediaFileRepository(IWebHostEnvironment environment, IHttpContextAccessor contextAccessor, ApplicationDbContext dbContext)
        {
            _environment = environment;
            _contextAccessor = contextAccessor;
            _dbContext = dbContext;
        }

        public async Task<MediaFile?> GetMedia(string mediaFileName)
        {
            if (_dbContext.MediaFiles == null)
            {
                throw new Exception("Entity 'MediaFiles' not found.");
            }

            var media = await _dbContext.MediaFiles.FirstOrDefaultAsync(x => x.MediaFileName == mediaFileName);

            if (media == null)
            {
                return null;
            }

            return media;
        }

        public async Task<MediaFile?> GetMediaPath(Guid mediaFileId)
        {
            if (_dbContext.MediaFiles == null)
            {
                throw new Exception("Entity 'MediaFiles' not found.");
            }

            var media = await _dbContext.MediaFiles
                .Where(x => x.MediaFileId == mediaFileId)
                .Select(m => new MediaFile
                {
                    MediaFileName = m.MediaFileName,
                    FilePath = m.FilePath,
                    FileExtension = m.FileExtension
                }).FirstOrDefaultAsync();

            if (media == null)
            {
                return null;
            }

            return media;
        }

        public async Task<MediaFile> Upload(MediaFile media)
        {
            var localFilePath = Path.Combine(_environment.ContentRootPath, "wwwroot", "Medias", $"{media.MediaFileName}{media.FileExtension}");

            if (media.MediaFileName.Length > 0 && media.File != null)
            {
                // Upload Media to Local Path
                using var stream = new FileStream(localFilePath, FileMode.Create);
                await media.File.CopyToAsync(stream);
            }


            // https://localhost:1234/images/image.jpg
            var urlFilePath = $"{_contextAccessor?.HttpContext?.Request.Scheme}:" +
                $"//{_contextAccessor?.HttpContext?.Request.Host}" +
                $"{_contextAccessor?.HttpContext?.Request.PathBase}" +
                $"/Medias/{media.MediaFileName}{media.FileExtension}";

            media.FilePath = urlFilePath;

            if (_dbContext.MediaFiles != null)
            {
                //add Media to Media Table
                await _dbContext.MediaFiles.AddAsync(media);
                await _dbContext.SaveChangesAsync();
            }

            return media;
        }
    }
}
