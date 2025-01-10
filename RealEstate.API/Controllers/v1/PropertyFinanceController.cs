using Common.Models;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Business.Contracts;
using RealEstate.Domain.DTO.PropertyFinance;
using Common.Helpers;
namespace RealEstate.API.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PropertyFinanceController(IPropertyFinanceService propertyFinanceService) : ControllerBase
    {
        private readonly IPropertyFinanceService _propertyFinanceService = propertyFinanceService;

        [HttpPost]
        public async Task<IActionResult> Create(PropertyFinanceCreateDTO propertyFinance)
        {
            var result = await _propertyFinanceService.Create(propertyFinance.ToEntity());
            return new ResponseBase<PropertyFinanceReadDTO?>
            {
                Message = result.Message,
                Code = result.Code,
                Success = result.Success,
                Data = PropertyFinanceReadDTO.FromEntity(result.Data)

            }.ToResponse();
        }
    }
}
