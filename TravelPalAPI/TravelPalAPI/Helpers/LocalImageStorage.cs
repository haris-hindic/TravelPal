using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TravelPalAPI.Helpers
{
    public class LocalImageStorage : IFileStorageService
    {
        private readonly IWebHostEnvironment env;
        private readonly IHttpContextAccessor http;

        public LocalImageStorage(IWebHostEnvironment env,IHttpContextAccessor http)
        {
            this.env = env;
            this.http = http;
        }

        public string SaveFile(string containerName, IFormFile file)
        {

            var extension = Path.GetExtension(file.FileName);
            var fileName = $"{Guid.NewGuid()}{extension}";
            var folderPath = Path.Combine(env.WebRootPath, containerName);

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            var fileRoute = Path.Combine(folderPath, fileName);
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                var content = ms.ToArray();
                File.WriteAllBytes(fileRoute, content);
            }

            var url = $"{http.HttpContext.Request.Scheme}://{http.HttpContext.Request.Host}";
            var urlForDb = Path.Combine(url, containerName, fileName).Replace("\\","/");
            return urlForDb;
        }

        public void DeleteFile(string fileRoute, string containerName)
        {
            if (string.IsNullOrEmpty(fileRoute)) return;

            var fileName = Path.GetFileName(fileRoute);
            var RouteToFile = Path.Combine(env.WebRootPath, containerName, fileName);

            if (File.Exists(RouteToFile)) File.Delete(RouteToFile);
        }

        public string EditFile(string containerName, IFormFile file, string fileRoute)
        {
            DeleteFile(fileRoute, containerName);
            return SaveFile(containerName, file);
        }

    }
}
