using Common.Models;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Business.Contracts;
using RealEstate.Domain.DTO.PropertyImage;
using Common.Helpers;
using System.Net;

namespace RealEstate.API.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PropertyImageController(IPropertyImageService propertyImageService, IFileManager fileManager) : ControllerBase
    {
        private readonly IFileManager _fileManager = fileManager;
        private readonly IPropertyImageService _propertyImageService = propertyImageService;

        [HttpPost("upload")]
        public async Task<IActionResult> UploadPropertyImage(PropertyImageCreateDTO propertyImage)
        {
            try
            {
                var filePath = await _fileManager.Upload(propertyImage.ImageFile);
                return filePath.ToResponse();
            }
            catch (Exception ex)
            {
                return GenericUtility.ResponseBaseCatch<string>(true, ex, HttpStatusCode.InternalServerError).ToResponse();
            }
        }

        [HttpGet("files/{propertyId}")]
        public async Task<IActionResult> Files(int propertyId)
        {
            var result = await _propertyImageService.Read(x => x.PropertyId == propertyId);
            return new ResponseBase<string[]>
            {
                Message = result.Message,
                Data = result.Data?.Select(x => x.File).ToArray(),
                Code = result.Code

            }.ToResponse();
        }
    }
}
