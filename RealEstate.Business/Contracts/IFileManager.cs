using Common.Models;
using Microsoft.AspNetCore.Http;

namespace RealEstate.Business.Contracts
{
    public interface IFileManager
    {
        Task<ResponseBase<string>> Upload(IFormFile file);
    }
}
