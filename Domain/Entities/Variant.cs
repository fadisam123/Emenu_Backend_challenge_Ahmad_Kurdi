namespace Domain.Entities
{
    public class Variant : Base
    {
        #region Properties
        public String Value { get; set; } = null!;
        #endregion

        #region Navigation Properties
        public virtual Attribute Attribute { get; set; } = null!;
        public virtual ICollection<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();
        #endregion
    }
}
