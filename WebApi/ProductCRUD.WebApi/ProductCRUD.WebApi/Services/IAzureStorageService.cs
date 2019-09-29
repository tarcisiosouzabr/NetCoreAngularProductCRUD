using System;
using System.Threading.Tasks;

namespace ProductCRUD.WebApi.Services
{
    public interface IAzureStorageService
    {
        Task<Guid> UploadFile(string base64Image);
    }
}