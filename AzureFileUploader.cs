using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResizeImageConsoleApp.AzureServices
{
    public class AzureFileUploader
    {
        public static string connectionString = string.Empty;
        public static string containerName = string.Empty;
        private readonly BlobServiceClient blobServiceClient;

        public AzureFileUploader(IConfiguration config)
        {
            connectionString = "DefaultEndpointsProtocol=https;AccountName=mtfstorage;AccountKey=tVuTpccnokRfmb1U0DLMM/Y1zBy9Rj1iRtk2DTYlq453T+VSD+c6LkXl5Unu6NI1a9QF8fWwg6hU+ASt1L7O6w==;EndpointSuffix=core.windows.net";
            blobServiceClient = new BlobServiceClient(connectionString);
        }
        public static string UploadFileToBlob(UploadImage uploadMTF)
        {
            string url = string.Empty;
            try
            {
                var blobClient = new BlobContainerClient(uploadMTF.ConnectionString, uploadMTF.Container);
                using (MemoryStream stream = new MemoryStream())
                {
                    if (uploadMTF.Image != null)
                    {
                        uploadMTF.Image.Save(stream, ImageFormat.Png);
                    }
                    else
                    {
                        uploadMTF.File.CopyTo(stream);
                    }

                    stream.Position = 0;
                    var blob = blobClient.GetBlobClient(uploadMTF.Folder + "/" + uploadMTF.ImageName + "." + ImageFormat.Png.ToString().ToLower());
                    blob.UploadAsync(stream).Wait();
                    url = blob.Uri.AbsoluteUri.ToString();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message, e);
               
            }
            return url;
        }
    }
}
