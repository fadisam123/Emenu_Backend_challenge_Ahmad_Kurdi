namespace Domain.Entities
{
    public class ProductVariant : Base
    {
        #region Properties

        #endregion

        #region Navigation Properties
        public virtual Product Product { get; set; } = null!;
        public virtual Variant Variant { get; set; } = null!;
        public virtual ICollection<VariantImage> VariantImages { get; set; } = new List<VariantImage>();
        #endregion
    }
}
