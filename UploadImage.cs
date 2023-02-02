using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResizeImageConsoleApp.AzureServices
{
    public class UploadImage
    {
        public Image Image { get; set; }
        public IFormFile File { get; set; }
        public string ImageName { get; set; }
        public string Container { get; set; }
        public string Folder { get; set; }
        public string ConnectionString { get; set; }
    }
}
