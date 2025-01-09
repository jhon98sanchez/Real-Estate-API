using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using RealEstate.Domain.Abstracts;

namespace RealEstate.Domain.DbSets
{
    public class Owner: AuditableFields
    {
        public Owner()
        {
            Properties = [];
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(150)]
        public required string Name { get; set; }  
        public required DateTime BirthDay { get; set; }
        public virtual ICollection<Property> Properties { get; set; }
    }
}
