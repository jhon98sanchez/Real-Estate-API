using Common.Helpers;
using Common.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using RealEstate.Business.Contracts;
using System.Net;

namespace RealEstate.Business.Implement
{
    public class FileManager(IWebHostEnvironment webHostEnvironment) : IFileManager
    {
        private readonly string[] _allowedExtensions = [".jpg", ".jpeg", ".png"] ;
        private readonly IWebHostEnvironment _webHostEnvironment = webHostEnvironment;

        public async Task<ResponseBase<string>> Upload(IFormFile file)
        {
			try
			{
                if (!_allowedExtensions.Any(x => x == Path.GetExtension(file.FileName)))
                {
                    return new ResponseBase<string>
                    {
                        Success = false,
                        Code = HttpStatusCode.BadRequest,
                        Message = $"Only {string.Join(",", _allowedExtensions.Select(x => x))}, files are allowed"
                    };
                }
                var uploadsFolder = Path.Combine(_webHostEnvironment.ContentRootPath, "Uploads");
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                var filePath = Path.Combine(uploadsFolder, fileName);
                using var fileStream = new FileStream(filePath, FileMode.Create);
                await file.CopyToAsync(fileStream);
                return new ResponseBase<string>
                {
                    Success = true,
                    Data = fileName,
                    Message = "File Upload"
                };
            }
			catch (Exception ex)
			{
                return GenericUtility.ResponseBaseCatch<string>(true, ex, HttpStatusCode.InternalServerError);
            }
        }
    }
}
