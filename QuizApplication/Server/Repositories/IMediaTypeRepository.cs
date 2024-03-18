using QuizApplication.Server.Models.Domain;

namespace QuizApplication.Server.Repositories
{
    public interface IMediaTypeRepository
    {
        Task<MediaType?> GetMediaType(string mediaType);
    }
}
