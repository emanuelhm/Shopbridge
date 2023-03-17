using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using dotenv.net;
using ShopBridge.Application.Services.Interfaces;

namespace ShopBridge.Application.Services.Implementations
{
    public class ImageUploadService : IImageUploadService
    {
        private readonly Cloudinary cloudinary;

        public ImageUploadService()
        {
            DotEnv.Load(options: new DotEnvOptions(probeForEnv: true));
            cloudinary = new Cloudinary(Environment.GetEnvironmentVariable("CLOUDINARY_URL"));
            cloudinary.Api.Secure = true;
        }

        public async Task<ImageUploadResult> Upload(string fileName, Stream fileStream)
        {
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(fileName, fileStream),
                UseFilename = true,
                UniqueFilename = true,
                Overwrite = false,
            };

            return await cloudinary.UploadAsync(uploadParams);
        }
    }
}
