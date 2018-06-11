using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;

namespace SIE.Auxiliary
{
    public static class Upload
    {
        public static List<string> Files(IFormFileCollection files)
        {
            var pathList = new List<string>();
            if (!Directory.Exists("../Interface/static/uploads"))
            {
                Directory.CreateDirectory("../Interface/static/uploads");
            }
            foreach (var file in files)
            {
                if (file.Length <= 0) continue;

                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim().ToString();
                var myUniqueFileName = Convert.ToString(Guid.NewGuid());

                var fileExtension = Path.GetExtension(fileName);

                var newFileName = myUniqueFileName + fileExtension;

                pathList.Add(newFileName);

                fileName = Path.Combine("../Interface/static/uploads", $"{newFileName}");

                using (var fs = File.Create(fileName))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                }
            }

            return pathList;
        }
    }
}
