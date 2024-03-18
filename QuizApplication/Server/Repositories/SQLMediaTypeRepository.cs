using Microsoft.EntityFrameworkCore;
using QuizApplication.Server.Data;
using QuizApplication.Server.Models.Domain;

namespace QuizApplication.Server.Repositories
{
    public class SQLMediaTypeRepository : IMediaTypeRepository
    {
        private readonly ApplicationDbContext _context;

        public SQLMediaTypeRepository(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<MediaType?> GetMediaType(string mediaType)
        {
            if (_context.MediaType == null)
            {
                throw new Exception("Entity 'MediaFiles' not found.");
            }

            var media = await _context.MediaType.FirstOrDefaultAsync(mt => mt.Mediatype == mediaType);

            return media;
        }
    }
}
