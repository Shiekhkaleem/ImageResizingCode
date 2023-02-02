using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Rest;
using ResizeImageConsoleApp.ResizeBitMap;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace ResizeImageConsoleApp.AzureServices
{
   
    public class AzureFileDownloader
    {
        public string connectionString = string.Empty;
        public  string containerName = string.Empty;
        private readonly BlobServiceClient blobServiceClient;
        public  AzureFileDownloader()
        {
            connectionString = "DefaultEndpointsProtocol=https;AccountName=stomispartesdev;AccountKey=fkg3tTtsgqPcbNR9z8cqkjDhnVc6JnpQ9L2v38cHdInzSkelxrfqT9gtb0URmwsgTCbBJn8UHkNolzFQhEUMkg==;EndpointSuffix=core.windows.net";
            blobServiceClient = new BlobServiceClient(connectionString);
        }
        public void DownloadImage()
        {
            string containerName = "resizeimg";
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            Pageable<BlobItem> blobs = containerClient.GetBlobs(prefix: "pendingimagesV2");
            int count = 0;
            foreach (BlobItem blobItem in blobs)
            {
                count++;
                //reszie the image and save in relative folder
                var blobClient = containerClient.GetBlobClient(blobItem.Name);
                using Stream stream = blobClient.OpenRead();
                Image myImage = Image.FromStream(stream);
                string imgName = blobItem.Name.Split(".").FirstOrDefault().Replace("pendingimagesV2/", "");
                var url500 = Resize(myImage, imgName, 500, 500);
                var url800 = Resize(myImage, imgName, 800, 800);
                var url1200 = Resize(myImage, imgName, 1200, 1200);
                var url2000 = Resize(myImage, imgName, 2000, 2000);
                //delete image from folder
                containerClient.DeleteBlob(blobItem.Name);
                Console.WriteLine(count + ": The image -" + blobItem.Name.ToString() + "- resized and deleted successfully");
            }
        }
        public string Resize(Image img, string imgName, int width,int hight)
        {
            ResizeImage resize = new ResizeImage();
            UploadImage upload = new UploadImage();
            upload.Image = resize.FixedSize(img, width,hight);
            // for adding images in specific folder
           // upload.Image = img;
            upload.ImageName = imgName;
            upload.Container = "resizeimg";
            upload.ConnectionString = connectionString;
            upload.Folder = "/Done"+ width.ToString() + "x" + hight.ToString();
            // for creating specific folders in containers
           // upload.Folder = "pendingimagesV2/MAGMA";
            var url = AzureFileUploader.UploadFileToBlob(upload);
            return url;
        }
    }
}
