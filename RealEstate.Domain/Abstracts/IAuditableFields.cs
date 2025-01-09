namespace RealEstate.Domain.Abstracts
{
    public interface IAuditableFields
    {
        public DateTime CreateDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
