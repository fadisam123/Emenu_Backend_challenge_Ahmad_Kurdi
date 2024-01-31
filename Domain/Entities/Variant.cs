namespace Domain.Entities
{
    public class Variant : BaseEntity
    {
        #region Properties
        public string Value { get; set; } = null!;
        #endregion

        #region Navigation Properties
        public virtual Attribute Attribute { get; set; } = null!;
        public virtual ICollection<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();
        #endregion
    }
}
