namespace RealEstate.Domain.DTO.Property
{
    public class PropertyUpdateDTO: PropertyCreateDTO
    {
        public int Id { get; set; }

        public new DbSets.Property ToEntity()
        {
            return new DbSets.Property
            {
                Id = Id,
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
