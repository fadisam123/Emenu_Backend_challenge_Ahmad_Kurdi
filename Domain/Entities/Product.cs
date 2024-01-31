namespace Domain.Entities
{
    public class Product : BaseEntity
    {
        #region Properties
        public string Name { get; set; } = null!;
        public string? Desc { get; set; }
        #endregion

        #region Navigation Properties
        public virtual ICollection<ProductLanguage> ProductLanguages { get; set; } = new List<ProductLanguage>();
        public virtual ICollection<Image> Images { get; set; } = new List<Image>();
        public virtual ICollection<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();
        #endregion
    }
}
