using System.ComponentModel.DataAnnotations;

namespace RealEstate.Domain.DTO.Property
{
    public class PropertyCreateDTO
    {
        public required int IdOwner { get; set; }

        [MaxLength(100)]
        public required string Name { get; set; }

        [MaxLength(150)]
        public required string Address { get; set; }

        public required decimal Price { get; set; }

        [MaxLength(50)]
        public required string CodeInternal { get; set; }

        public required int Year { get; set; }

        public DbSets.Property ToEntity()
        {
            return new DbSets.Property
            {
                IdOwner = IdOwner,
                Name = Name,
                Address = Address,
                Price = Price,
                CodeInternal = CodeInternal,
                Year = Year
            };
        }
    }
}
