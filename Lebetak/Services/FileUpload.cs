using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Lebetak.UnitOfWork;

namespace Lebetak.Services
{
    public static class FileUpload
    {
        public static async Task<ImageUploadResult> UploadAsync(IFormFile file, Cloudinary _cloudinary,string folder)
        {
            // SSN => for SSN Images
            // Projects => for images related to ProjectWorker and ProjectCompany
            // ProfileImages => for profile images for users
            // other => for logo and cover for comapny
            var result = new ImageUploadResult();

            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();

                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Folder = $"my_app/{folder}"
                };

                result = await _cloudinary.UploadAsync(uploadParams);
            }

            return result;
        }

        public static async Task<bool> DeleteImageAsync(string publicId, Cloudinary cloudinary,string folder)
        {
            publicId = ExtractPublicId(publicId,folder);
            var deletionParams = new DeletionParams(publicId)
            {
                ResourceType = ResourceType.Image
            };

            var result = await cloudinary.DestroyAsync(deletionParams);

            return result.Result == "ok";
        }
        public static string ExtractPublicId(string url,string folder)
        {
            var fileName = url.Split('/').Last().Split('.').First(); // image123
            return $"my_app/{folder}/{fileName}";
        }


    }
}
