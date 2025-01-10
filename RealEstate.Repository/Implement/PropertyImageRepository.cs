using Common.Implement;
using Microsoft.Extensions.DependencyInjection;
using RealEstate.Domain.DbSets;
using RealEstate.Repository.Contracts;
using RealEstate.Repository.SQLServer;

namespace RealEstate.Repository.Implement
{
    public class PropertyImageRepository(IServiceScopeFactory serviceScope) : GenericRepository<PropertyImage, RepositoryDbContext>(serviceScope), IPropertyImageRepository
    {
    }
}
