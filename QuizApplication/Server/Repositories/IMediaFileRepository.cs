using QuizApplication.Server.Models.Domain;

namespace QuizApplication.Server.Repositories
{
    public interface IMediaFileRepository
    {
        Task<MediaFile> Upload(MediaFile media);
        Task<MediaFile?> GetMedia(string mediaFileName);
        Task<MediaFile?> GetMediaPath(Guid mediaFileId);
    }
}
