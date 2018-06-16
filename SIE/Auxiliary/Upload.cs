using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;

namespace SIE.Auxiliary
{
    public static class Upload
    {
        private const string DIR_PATH = "../Interface/static/{0}";
        public static List<string> Files(IFormFileCollection files, string dir)
        {
            var path = string.Format(DIR_PATH, dir);
            var pathList = new List<string>();
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            foreach (var file in files)
            {
                if (file.Length <= 0) continue;

                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim().ToString();
                var myUniqueFileName = Convert.ToString(Guid.NewGuid());

                var fileExtension = Path.GetExtension(fileName);

                var newFileName = myUniqueFileName + fileExtension;

                pathList.Add($"static/{dir}/{newFileName}");

                fileName = Path.Combine(path, $"{newFileName}");

                using (var fs = File.Create(fileName))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                }
            }

            return pathList;
        }

        public static List<string> CopyFromTo(List<string> files, string dirSource, string dirTarget)
        {
            var result = new List<string>();
            if (!Directory.Exists($"../Interface/static/{dirTarget}"))
            {
                Directory.CreateDirectory($"../Interface/static/{dirTarget}");
            }
            foreach (var file in files)
            {
                var newFile = file.Replace("\\", "/").Replace($"/{dirSource}/", $"/{dirTarget}/");
                var currentLocation = $"../Interface/{file}";
                var targetLocation = $"../Interface/{newFile}";
                File.Copy(currentLocation, targetLocation, true);
                result.Add(newFile);
            }

            return result;
        }
    }
}
