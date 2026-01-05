using Microsoft.AspNetCore.Http;

namespace E_learning_platform.Services
{
    public interface IUploadService
    {
        Task<string> UploadImageAsync(IFormFile file);
        Task<string> UploadVideoAsync(IFormFile file);
        Task<string> UploadFileAsync(IFormFile file);
    }
}