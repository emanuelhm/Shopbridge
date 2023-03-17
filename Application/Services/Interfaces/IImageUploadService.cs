using CloudinaryDotNet.Actions;

namespace ShopBridge.Application.Services.Interfaces
{
    public interface IImageUploadService
    {
        Task<ImageUploadResult> Upload(string fileName, Stream fileStream);
    }
}
