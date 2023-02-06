using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Footbal_Original.Models
{
    public static class FileExtension
    {
        public static bool IsImage(this IFormFile file)
        {
            return file.ContentType == "image/jpeg" ||
                file.ContentType == "image/jpg" ||
                file.ContentType == "image/gif" ||
                file.ContentType == "image/png" ||
                file.ContentType == "image/jfif";
        }
        public static bool IsCv(this IFormFile file)
        {
            return file.ContentType == "application/pdf" ||
                file.ContentType == "application/doc" ||
                file.ContentType == "application/docx" ||
                file.ContentType == "application/pptx";
        }
        public async static Task<string> SaveAsync(this IFormFile file, string root, string folder)
        {
            // @"images\uploads"
            string path = Path.Combine(root, folder);
            //filename ise meslen bizde  3 4 dene user menim.jpg seklini yukleyir nlari qatisdirmamq ucun asagidaki kodu yaziriq
            string fileNamwe = Path.Combine(Guid.NewGuid().ToString() + Path.GetFileName(file.FileName));
            string resultPath = Path.Combine(path, fileNamwe);
            using (FileStream fileStream = new FileStream(resultPath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return fileNamwe;
        }
        public static void DeleteImage(string root, string folder, string filename)
        {
            string path = Path.Combine(root, folder, filename);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}