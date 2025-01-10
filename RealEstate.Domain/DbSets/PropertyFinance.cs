using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using RealEstate.Domain.Abstracts;

namespace RealEstate.Domain.DbSets
{
    public class PropertyFinance: AuditableFields
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Property")]
        public int PropertyId { get; set; }
        public DateTime DateSale { get; set; }

        [MaxLength(150)]
        public required string Name { get; set; }
        public decimal Value {  get; set; }
        public decimal Tax { get; set; }

        public virtual Property Property { get; set; }
    }
}
