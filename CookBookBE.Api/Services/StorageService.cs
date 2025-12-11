using Azure.Storage.Blobs;
using CookBookBE.Api.Services.Interfaces;

namespace CookBookBE.Api.Services
{
    public class StorageService : IStorageService
    {
        private readonly string connectionStringBuilder = "https://cookbookprojectstorage.blob.core.windows.net";

        public async Task AddFileToRecipeAsync(Guid recipeId, string filePath)
        {
            // Connect to Azure somehow
            var containerClient = await GetOrCreateContainerAsync(recipeId.ToString());
            await UploadBlockBlobAsync(containerClient, filePath);
        }

        private async Task<BlobContainerClient> GetOrCreateContainerAsync(string containerName)
        {
            var blobServiceClient = new BlobServiceClient(connectionStringBuilder);

            var container = blobServiceClient.GetBlobContainerClient(containerName);
            if (container != null)
            {
                return container;
            }

            try
            {
                return await blobServiceClient.CreateBlobContainerAsync(containerName);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task UploadBlockBlobAsync(BlobContainerClient containerClient, string filePath)
        {
            var fileName = Path.GetFileName(filePath);
            var blobClient = containerClient.GetBlobClient(fileName);

            await blobClient.UploadAsync(filePath);
        }
    }
}
