using System.ComponentModel.DataAnnotations;

namespace RealEstate.Domain.DTO.PropertyFinance
{
    public class PropertyFinanceCreateDTO
    {
        [MaxLength(150)]
        public required string Name { get; set; }
        public int PropertyId { get; set; }
        public DateTime DateSale { get; set; }
        public decimal Value { get; set; }
        public decimal Tax { get; set; }

        public DbSets.PropertyFinance ToEntity()
        {
            return new DbSets.PropertyFinance
            {
                Name = Name,
                PropertyId = PropertyId,
                DateSale = DateSale,
                Value = Value,
                Tax = Tax
            };
        }
    }
}
