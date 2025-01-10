using Common.Contracts;
using Common.Models;
using RealEstate.Domain.DbSets;
using RealEstate.Repository.SQLServer;

namespace RealEstate.Business.Contracts
{
    public interface IPropertyService: IGenericService<Property, RepositoryDbContext>
    {
        Task<ResponseBase<bool>> UpdatePrice(int propertyId, decimal newPrice);
    }
}
