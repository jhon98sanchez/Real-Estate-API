using Common.Implement;
using RealEstate.Business.Contracts;
using RealEstate.Domain.DbSets;
using RealEstate.Repository.Contracts;
using RealEstate.Repository.SQLServer;

namespace RealEstate.Business.Implement
{
    public class PropertyFinanceService(IPropertyFinanceRepository repository) : GenericService<PropertyFinance, RepositoryDbContext>(repository), IPropertyFinanceService
    {
    }
}
