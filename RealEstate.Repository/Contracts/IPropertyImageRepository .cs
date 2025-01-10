using Common.Contracts;
using RealEstate.Domain.DbSets;
using RealEstate.Repository.SQLServer;

namespace RealEstate.Repository.Contracts
{
    public interface IPropertyImageRepository: IGenericRepository<PropertyImage, RepositoryDbContext>
    {
    }
}
