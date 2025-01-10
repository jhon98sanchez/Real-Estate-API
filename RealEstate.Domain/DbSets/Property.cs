using RealEstate.Domain.Abstracts;
using RealEstate.Domain.Utils;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstate.Domain.DbSets
{
    public class Property: AuditableFields
    {
        public Property()
        {
            PropertyImages = [];
            PropertyFinances = [];
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Owner")]
        public int IdOwner { get; set; }

        public StatusProperty Status { get; set; }

        [MaxLength(100)]
        public required string Name { get; set; }

        [MaxLength(150)]
        public required string Address { get; set; }

        public required decimal Price { get; set; }

        [MaxLength(50)]
        public required string CodeInternal { get; set; }

        public required int Year { get; set; }

        public Owner Owner { get; set; }
        public virtual ICollection<PropertyImage> PropertyImages { get; set; }
        public virtual ICollection<PropertyFinance> PropertyFinances { get; set; }
    }
}
