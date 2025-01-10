using Common.Implement;
using RealEstate.Business.Contracts;
using RealEstate.Domain.DbSets;
using RealEstate.Repository.Contracts;
using RealEstate.Repository.SQLServer;

namespace RealEstate.Business.Implement
{
    public class PropertyImageService : GenericService<PropertyImage, RepositoryDbContext>, IPropertyImageService
    {
        public PropertyImageService(IPropertyImageRepository repository) : base(repository)
        {
        }
    }
}
