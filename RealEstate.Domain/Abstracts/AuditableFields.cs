using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Domain.Abstracts
{
    public abstract class AuditableFields : IAuditableFields
    {
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedDate { get; set; }
    }
}
