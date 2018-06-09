using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;

namespace exact.api.Storage
{
    public class StorageRepository: IStorageRepository
    {
        private CloudBlobClient _blobClient;
        
        public StorageRepository(string accountName, string accountKey)
        {
            Task.Run(() => InitAsync(accountName, accountKey)).GetAwaiter().GetResult();
        }
        
        public async void InitAsync(string accountName, string accountKey)
        {
            var storageCredentials = new StorageCredentials(accountName, accountKey);
            var cloudStorageAccount = new CloudStorageAccount(storageCredentials, true);
            _blobClient = cloudStorageAccount.CreateCloudBlobClient();
 
            var container = _blobClient.GetContainerReference("images");    
            await container.CreateIfNotExistsAsync();
            await container.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Container });
           
        }

        public async Task<string> UploadImage(string image)
        {
            if (string.IsNullOrEmpty(image) || !IsBase64String(image))
            {
                return string.Empty;
            }
            
            var filename = Guid.NewGuid().ToString() + ".jpg";
            var container = await GetContainer("images");
            var imageBytes = Convert.FromBase64String(image);
            var blob = container.GetBlockBlobReference(filename);
            blob.Properties.ContentType = "image/jpg";
            await blob.UploadFromByteArrayAsync(imageBytes, 0, imageBytes.Length);
            return blob.StorageUri.PrimaryUri.AbsoluteUri;
        }

        public async Task<string> UploadPdf(string pdf)
        {
            if (string.IsNullOrEmpty(pdf) || !IsBase64String(pdf))
            {
                return string.Empty;
            }

            var filename = Guid.NewGuid().ToString() + ".pdf";
            var container = await GetContainer("pdf");
            var pdfBytes = Convert.FromBase64String(pdf);
            var blob = container.GetBlockBlobReference(filename);
            blob.Properties.ContentType = "application/pdf";
            await blob.UploadFromByteArrayAsync(pdfBytes, 0, pdfBytes.Length);

            return blob.StorageUri.PrimaryUri.AbsoluteUri;
        }

        public async Task<string> UploadPdfByBytes(byte[] pdfBytes)
        {
            if (pdfBytes == null)
            {
                return string.Empty;
            }

            var filename = Guid.NewGuid().ToString() + ".pdf";
            var container = await GetContainer("pdf");
            var blob = container.GetBlockBlobReference(filename);
            blob.Properties.ContentType = "application/pdf";
            await blob.UploadFromByteArrayAsync(pdfBytes, 0, pdfBytes.Length);

            return blob.StorageUri.PrimaryUri.AbsoluteUri;
        }

        private async Task<CloudBlobContainer> GetContainer(string name)
        {
            var container = _blobClient.GetContainerReference(name);
            await container.CreateIfNotExistsAsync();
            await container.SetPermissionsAsync(new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Container
            });

            return container;
        }
        
        public bool IsBase64String(string s)
        {
            s = s.Trim();
            return (s.Length % 4 == 0) && Regex.IsMatch(s, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);

        }
    }
}