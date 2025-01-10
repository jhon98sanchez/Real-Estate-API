using Common.Helpers;
using Common.Implement;
using Common.Models;
using RealEstate.Business.Contracts;
using RealEstate.Domain.DbSets;
using RealEstate.Repository.Contracts;
using RealEstate.Repository.SQLServer;
using System.Net;

namespace RealEstate.Business.Implement
{
    public class PropertyService(IPropertyRepository repository) : GenericService<Property, RepositoryDbContext>(repository), IPropertyService
    {
        public async Task<ResponseBase<bool>> UpdatePrice(int propertyId, decimal newPrice)
        {
			try
			{
				var entity = await ReadOne(x => x.Id == propertyId);
				if (entity.Data is null) 
				{
					return new ResponseBase<bool>()
					{
						Code = entity.Code,
						Message = entity.Message,
						Data = false
					};
                }
                if (entity.Data.Status == Domain.Utils.StatusProperty.Sold)
                {
                    return new ResponseBase<bool>()
                    {
                        Code = HttpStatusCode.BadRequest,
                        Message = "This property has already been sold",
                        Data = false
                    };
                }
				entity.Data.Price = newPrice;
				var update = await Update(entity.Data);
                return new ResponseBase<bool>()
                {
                    Code = update.Code,
                    Message = update.Message,
                    Data = true,
                    Success = update.Success
                };
            }
			catch (Exception ex)
			{
                return GenericUtility.ResponseBaseCatch<bool>(true, ex, HttpStatusCode.InternalServerError);
            }
        }
    }
}
