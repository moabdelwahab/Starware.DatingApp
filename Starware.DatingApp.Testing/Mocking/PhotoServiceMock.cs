using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Starware.DatingApp.Core.InfrastructureContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starware.DatingApp.Testing.Mocking
{
    public class PhotoServiceMock : IPhotoService
    {
        public Task<DeletionResult> DeletePhoto(string publicId)
        {
            return Task.FromResult(new DeletionResult()
            {
                Result = "Ok",
                StatusCode = System.Net.HttpStatusCode.OK
            });
        }

        public Task<ImageUploadResult> Upload(IFormFile formFile)
        {
            return Task.FromResult(new ImageUploadResult()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                PublicId = "123456789"
            });
        }
    }
}
