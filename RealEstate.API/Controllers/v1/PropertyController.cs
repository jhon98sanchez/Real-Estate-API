using Common.Helpers;
using Common.Models;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Business.Contracts;
using RealEstate.Domain.DTO.Property;
using RealEstate.Domain.Utils;

namespace RealEstate.API.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PropertyController(IPropertyService propertyService, IOwnerService ownerService) : ControllerBase
    {
        private readonly IPropertyService _propertyService = propertyService;
        private readonly IOwnerService _ownerService = ownerService;

        [HttpPost]
        public async Task<IActionResult> Create(PropertyCreateDTO property)
        {
            var existOwner = await _ownerService.Exists(x => x.Id == property.IdOwner);
            if (!existOwner) 
            {
                return new ResponseBase<PropertyReadDTO?>
                {
                    Message = "Owner not found",
                    Code = System.Net.HttpStatusCode.BadRequest,
                    Success = false

                }.ToResponse();
            }
            var entity = property.ToEntity();
            entity.Status = StatusProperty.OnSale;
            var create = await _propertyService.Create(entity);
            return new ResponseBase<PropertyReadDTO?>
            {
                Message = create.Message,
                Code = create.Code,
                Success = create.Success,
                Data = PropertyReadDTO.FromEntity(create.Data)

            }.ToResponse();
        }

        [HttpPut("update_price/{propertyid}/{newPrice}")]
        public async Task<IActionResult> UpdatePrice(int propertyid, decimal newPrice)
        {
            var result = await _propertyService.UpdatePrice(propertyid, newPrice);
            return new ResponseBase<bool?>
            {
                Message = result.Message,
                Code = result.Code,
                Success = result.Success,
                Data = result.Data

            }.ToResponse();
        }

        [HttpPut("update_property")]
        public async Task<IActionResult> UpdateProperty(PropertyUpdateDTO property)
        {
            var result = await _propertyService.Update(property.ToEntity());
            return new ResponseBase<PropertyReadDTO>
            {
                Message = result.Message,
                Code = result.Code,
                Success = result.Success,
                Data = PropertyReadDTO.FromEntity(result.Data)

            }.ToResponse();
        }

        [HttpGet("onsale/{propertyid}")]
        public async Task<IActionResult> Onsale(int propertyid)
        {
            var result = await _propertyService.ReadOne(x => x.Id == propertyid);
            if (result.Data is null)
            {
                return new ResponseBase<bool?>
                {
                    Message = result.Message,
                    Code = result.Code,
                    Success = result.Success
                    
                }.ToResponse();
            }
            result.Data.Status = StatusProperty.OnSale;
            var updateStatus = await _propertyService.Update(result.Data);
            return new ResponseBase<bool?>
            {
                Message = updateStatus.Message,
                Code = updateStatus.Code,
                Success = updateStatus.Success,
                Data = true

            }.ToResponse();
        }

        [HttpGet("read")]
        public async Task<IActionResult> Read(int page, int size, string search = "")
        {
            ResponseBase<PagedResult<PropertyReadDTO>?> response = new()
            {
                Data = new()
            };
            var result = await _propertyService.Read(x => x.Name.Contains(search) || x.CodeInternal.Contains(search) ,null, o => o.OrderBy(i => i.CreateDate), page, size);

            response.Data.Results = result.Data.Results?.Select(x => PropertyReadDTO.FromEntity(x)).ToList() ?? [];
            response.Data.CurrentPage = result.Data.CurrentPage;
            response.Data.PageCount = result.Data.PageCount;
            response.Data.RowCount = result.Data.RowCount;
            response.Success = result.Success;
            response.Code = result.Code;
            return response.ToResponse();
        }
    }
}
