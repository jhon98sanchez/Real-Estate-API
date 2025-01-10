namespace RealEstate.Domain.DTO.Property
{
    public class PropertyReadDTO: PropertyCreateDTO
    {
        public int Id { get; set; }

        public static PropertyReadDTO? FromEntity(DbSets.Property? entity)
        {
            if (entity is null) return null;
            return new PropertyReadDTO
            {
                Id = entity.Id,
                IdOwner = entity.IdOwner,
                Name = entity.Name,
                Address = entity.Address,
                Price = entity.Price,
                CodeInternal = entity.CodeInternal,
                Year = entity.Year,
            };
        }
    }
}
