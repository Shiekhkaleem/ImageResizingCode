using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using ResizeImageConsoleApp.AzureServices;
using System;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace ResizeImageConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AzureFileDownloader downloader = new AzureFileDownloader();
            downloader.DownloadImage();

        }
        
    }
}
