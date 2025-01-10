namespace RealEstate.Domain.DTO.PropertyFinance
{
    public class PropertyFinanceReadDTO: PropertyFinanceCreateDTO
    {
        public int Id { get; set; }

        public static PropertyFinanceReadDTO? FromEntity(DbSets.PropertyFinance? entity)
        {
            if (entity is null) return null;
            return new PropertyFinanceReadDTO
            {
                Id = entity.Id,
                Name = entity.Name,
                PropertyId = entity.PropertyId,
                DateSale = entity.DateSale,
                Value = entity.Value,
                Tax = entity.Tax
            };
        }
    }
}
