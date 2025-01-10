namespace RealEstate.Domain.DTO.Owner
{
    public class OwnerReadDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required DateTime BirthDay { get; set; }
        public string? PathPhoto { get; set; }
        public static OwnerReadDTO? FromEntity(DbSets.Owner? entity)
        {
            if (entity is null) return null;
            return new OwnerReadDTO 
            { 
                Id = entity.Id, BirthDay = entity.BirthDay, 
                Email = entity.Email, 
                Name = entity.Name,
                PathPhoto = entity.Photo 
            };
        }
    }
}
