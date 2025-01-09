using Common.Contracts;
using RealEstate.Domain.DbSets;
using RealEstate.Repository.SQLServer;

namespace RealEstate.Repository.Contracts
{
    public interface IPropertyRepository: IGenericRepository<Property, RepositoryDbContext>
    {
    }
}
