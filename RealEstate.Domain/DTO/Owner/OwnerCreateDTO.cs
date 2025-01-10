using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace RealEstate.Domain.DTO.Owner
{
    public class OwnerCreateDTO
    {
        [MaxLength(150)]
        public required string Name { get; set; }

        [MaxLength(150)]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public required string Email { get; set; }
        public required DateTime BirthDay { get; set; }

        public IFormFile? Photo { get; set; }

        public DbSets.Owner ToEntity(string? pathPhoto)
        {
            return new DbSets.Owner
            {
                Name = Name,
                BirthDay = BirthDay,
                Email = Email,
                Photo = pathPhoto
            };
        }
    }
}
