using Common.Implement;
using Common.Models;
using RealEstate.Business.Contracts;
using RealEstate.Domain.DbSets;
using RealEstate.Repository.Contracts;
using RealEstate.Repository.SQLServer;

namespace RealEstate.Business.Implement
{
    public class PropertyFinanceService(IPropertyFinanceRepository propertyFinanceRepository, IPropertyRepository propertyRepository) : GenericService<PropertyFinance, RepositoryDbContext>(propertyFinanceRepository), IPropertyFinanceService
    {
        private readonly IPropertyRepository _propertyRepository = propertyRepository;
        public override async Task<ResponseBase<PropertyFinance>> Create(PropertyFinance entity)
        {
            var property = await _propertyRepository.ReadOne(x => x.Id == entity.PropertyId);
            if (property == null)
            {
                ResponseBase<PropertyFinance> response = new()
                {
                    Success = false,
                    Code = System.Net.HttpStatusCode.BadRequest,
                    Message = "Property not found"
                };
                return response;
            }

            if (property.Price != entity.Value)
            {
                ResponseBase<PropertyFinance> response = new()
                {
                    Success = false,
                    Code = System.Net.HttpStatusCode.BadRequest,
                    Message = "Invalid property value"
                };
                return response;
            }


            if (property.Status == Domain.Utils.StatusProperty.Sold)
            {
                ResponseBase<PropertyFinance> response = new()
                {
                    Success = false,
                    Code = System.Net.HttpStatusCode.BadRequest,
                    Message = "This property has already been sold"
                };
                return response;
            }
            property.Status = Domain.Utils.StatusProperty.Sold;
            await _propertyRepository.Update(property);
            return await base.Create(entity);
        }
    }
}
