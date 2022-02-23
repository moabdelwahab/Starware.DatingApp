using Microsoft.Extensions.Options;
using Starware.DatingApp.Core.InfrastructureContracts;
using Starware.DatingApp.Infrastructure.Models;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace Starware.DatingApp.Infrastructure
{
    public class PhotoService : IPhotoService
    {
        private readonly IOptions<CloudinarySettings> config;

        Account account;
        Cloudinary _cloudinary;
        public PhotoService(IOptions<CloudinarySettings> config)
        {
            this.config = config;
            account = new Account(this.config.Value.CloudName, this.config.Value.ApiKey, this.config.Value.ApiSecret);
            this._cloudinary = new Cloudinary(account);
        }

        public async Task<DeletionResult> DeletePhoto(string publicId)
        {
            DeletionParams deletionParams = new DeletionParams(publicId);
            DeletionResult deletionResult = null;
            if (!string.IsNullOrEmpty(publicId))
            {
                deletionResult = await _cloudinary.DestroyAsync(deletionParams);
            }
            return deletionResult;
        }

        public async Task<ImageUploadResult> Upload(IFormFile file)
        {
            ImageUploadResult imageUploadResult = null;

            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                ImageUploadParams imageUploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Width(500).Height(500).Crop("fill").Gravity("face")
                };
                imageUploadResult = await _cloudinary.UploadAsync(imageUploadParams);
            }
            return imageUploadResult;
        }
    }
}
