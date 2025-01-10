using Common.Models;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Business.Contracts;
using RealEstate.Domain.DTO.Owner;
using System.Net;
using Common.Helpers;

namespace RealEstate.API.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OwnerController(IOwnerService ownerService, IFileManager fileManager) : ControllerBase
    {
        private readonly IFileManager _fileManager = fileManager;
        private readonly IOwnerService _ownerService = ownerService;

        [HttpPost]
        public async Task<IActionResult> Create(OwnerCreateDTO owner)
        {
            var existEmail = await _ownerService.Exists(x => x.Email.Equals(owner.Email));
            if (existEmail) 
            {
                return new ResponseBase<OwnerReadDTO>
                {
                    Message = $"The email {owner.Email} is already in use",
                    Code = HttpStatusCode.Conflict

                }.ToResponse();
            }

            string? filePath = null;
            if (owner.Photo is not null)
            {
                var uploadResult = await _fileManager.Upload(owner.Photo);
                if (!uploadResult.Success)
                {
                    return new ResponseBase<OwnerReadDTO>
                    {
                        Message = uploadResult.Message,
                        Code = uploadResult.Code,
                        Success = uploadResult.Success

                    }.ToResponse();
                }
                filePath = uploadResult.Data;
            }
            var create = await _ownerService.Create(owner.ToEntity(filePath));
            return new ResponseBase<OwnerReadDTO>
            {
                Message = create.Message,
                Code = create.Code,
                Data = OwnerReadDTO.FromEntity(create.Data)

            }.ToResponse();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _ownerService.ReadOne(x => x.Id == id);
            return new ResponseBase<OwnerReadDTO>
            {
                Message = result.Message,
                Code = result.Code,
                Success = result.Success,
                Data = OwnerReadDTO.FromEntity(result.Data)

            }.ToResponse();
        }

    }
}
