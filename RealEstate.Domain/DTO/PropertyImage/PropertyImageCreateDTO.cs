using Microsoft.AspNetCore.Http;

namespace RealEstate.Domain.DTO.PropertyImage
{
    public class PropertyImageCreateDTO
    {
        public int PropertyId { get; set; }
        public required IFormFile ImageFile { get; set; }
    }
}
