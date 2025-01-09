using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using RealEstate.Domain.Abstracts;

namespace RealEstate.Domain.DbSets
{
    public class PropertyImage: AuditableFields
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Property")]
        public int PropertyId { get; set; }

        [MaxLength(50)]
        public required string File { get; set; }

        public bool Enabled { get; set; }

        public virtual required Property Property { get; set; }
    }
}
