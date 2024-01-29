namespace Domain.Entities
{
    public class Image : BaseEntity
    {
        #region Properties
        public String Path { get; set; } = null!;
        #endregion

        // Here I will configure the foreign keys of Product manually (Using Fluent API)
        #region Navigation Properties
        public Guid MainProductId { get; set; }
        public virtual Product MainProduct { get; set; } = null!;

        public Guid AdditionalProductId { get; set; }
        public virtual Product AdditionalProduct { get; set; } = null!;

        public virtual ICollection<VariantImage> VariantImages { get; set; } = new List<VariantImage>();
        #endregion
    }
}
