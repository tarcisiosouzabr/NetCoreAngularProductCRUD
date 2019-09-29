using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCRUD.WebApi.Services
{
    public class AzureStorageService : IAzureStorageService
    {

        public async Task<Guid> UploadFile(string base64Image)
        {
            string accountname = "easysoccer";
            string accesskey = "Dimz4dus4oGSiqHViFjrRqp5MUcB4XEJYpQ/xwFqKA22U35IlTdWgnV0mHKDpbZEsLdG6u+VA3hVSUUSvq4PwA==";
            StorageCredentials creden = new StorageCredentials(accountname, accesskey);

            CloudStorageAccount acc = new CloudStorageAccount(creden, useHttps: true);

            CloudBlobClient client = acc.CreateCloudBlobClient();

            CloudBlobContainer cont = client.GetContainerReference("product");

            await cont.CreateIfNotExistsAsync();

            await cont.SetPermissionsAsync(new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            });
            var fileName = Guid.NewGuid();
            CloudBlockBlob cblob = cont.GetBlockBlobReference(fileName.ToString() + ".png");
            base64Image = base64Image
                            .Replace("data:image/png", string.Empty)
                            .Replace("data:image/jpeg", string.Empty)
                            .Replace("base64", string.Empty)
                            .Replace(",", string.Empty)
                            .Replace(";", string.Empty)
                            .Replace(":", string.Empty);
            var imageBytes = Convert.FromBase64String(base64Image);
            await cblob.UploadFromByteArrayAsync(imageBytes, 0, imageBytes.Count());
            return fileName;
        }
    }
}
