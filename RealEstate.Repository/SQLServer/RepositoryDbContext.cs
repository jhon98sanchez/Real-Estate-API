using Microsoft.EntityFrameworkCore;
using RealEstate.Domain.Abstracts;
using RealEstate.Domain.DbSets;

namespace RealEstate.Repository.SQLServer
{
    public class RepositoryDbContext(DbContextOptions<RepositoryDbContext> options) : DbContext(options)
    {
        public virtual DbSet<Owner> Owner { get; set; }
        public virtual DbSet<Property> Property { get; set; }
        public virtual DbSet<PropertyFinance> PropertyFinance { get; set; }
        public virtual DbSet<PropertyImage> PropertyImage { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<IAuditableFields>().ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.UpdatedDate = DateTime.UtcNow;
                        entry.Entity.CreateDate = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedDate = DateTime.UtcNow;
                        break;
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
