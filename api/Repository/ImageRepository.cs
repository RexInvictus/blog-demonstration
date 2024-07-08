using api.Data;
using api.Interfaces;
using api.Models;
using System.Drawing.Imaging;
using System.IO;


namespace api.Repository
{
    public class ImageRepository : IImageRepository
    {
        private readonly ApplicationDBContext _context;
        private readonly IStorageServiceRepository _storageService;
        private readonly IConfiguration _config;

        public ImageRepository
        (
            ApplicationDBContext context,
            IStorageServiceRepository storageService,
            IConfiguration config
        )
        {
            _context = context;
            _storageService = storageService;
            _config = config;
        }

        public async Task<string?> CreateAsync(IFormFile? file)
        {
            if (file == null)
            {
                return null;
            }
            if (file.Length == 0 || !IsImageFile(file))
            {
                throw new InvalidDataException();
            }

            if (file.Length > 10 * 1024 * 1024)
            {
                throw new FileLoadException();
            }

            // process the file
            await using var memoryStr = new MemoryStream();
            await file.CopyToAsync(memoryStr);

            var fileExt = Path.GetExtension(file.Name);
            var objName = $"{Guid.NewGuid()}.{fileExt}";

            var s3obj = new S3object()
            {
                BucketName = "hikingblogimages",
                InputStream = memoryStr,
                Name = objName
            };

            var cred = new AwsCredentials()
            {
                AwsKey = _config["AwsConfiguration:AwsAccessKey"],
                AwsSecretKey = _config["AwsConfiguration:AwsSecretKey"]
            };

            var result = await _storageService.UploadFileAsync(s3obj, cred);

            if (result.StatusCode == 200)
            {
                return result.Url;
            }
            else
            {
                throw new FileNotFoundException();
            }
        }

        private static bool IsImageFile(IFormFile file)
        {
            var contentType = file.ContentType.ToLower();
            return contentType == "image/jpeg" || contentType == "image/png" || contentType == "image/jpg" || contentType == "image/webp";
        }


        public async Task<bool?> DeleteImageFromS3Async(string? url)
        {
            if (url == null)
            {
                return null;
            }
            var s3obj = new S3object()
            {
                BucketName = "imagesending",
                InputStream = null,
                Name = url.Replace("https://dlxfzwdunogf7.cloudfront.net/", "")
            };

            var cred = new AwsCredentials()
            {
                AwsKey = _config["AwsConfiguration:AwsAccessKey"],
                AwsSecretKey = _config["AwsConfiguration:AwsSecretKey"]
            };

            await _storageService.DeleteFileAsync(s3obj, cred);


            return true;
        }


    }
}