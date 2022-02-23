using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace Starware.DatingApp.Core.InfrastructureContracts
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> Upload(IFormFile formFile);
        Task<DeletionResult> DeletePhoto(string publicId);

    }
}
