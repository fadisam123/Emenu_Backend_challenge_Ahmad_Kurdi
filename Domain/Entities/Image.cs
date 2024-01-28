namespace Domain.Entities
{
    public class Image : Base
    {
        #region Properties
        public String Path { get; set; } = null!;
        #endregion

        // Here I am configuring the foreign keys of Product manually (Using Fluent API)
        #region Navigation Properties
        public int MainProductId { get; set; }
        public virtual Product MainProduct { get; set; } = null!;

        public int AdditionalProductId { get; set; }
        public virtual Product AdditionalProduct { get; set; } = null!;

        public virtual ICollection<VariantImage> VariantImages { get; set; } = new List<VariantImage>();
        #endregion
    }
}
