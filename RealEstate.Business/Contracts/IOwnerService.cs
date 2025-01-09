using Common.Contracts;
using RealEstate.Domain.DbSets;
using RealEstate.Repository.SQLServer;

namespace RealEstate.Business.Contracts
{
    public interface IOwnerService : IGenericService<Owner, RepositoryDbContext>
    {
    }
}
