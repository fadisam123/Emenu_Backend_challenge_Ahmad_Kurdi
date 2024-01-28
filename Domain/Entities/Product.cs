namespace Domain.Entities
{
    public class Product : Base
    {
        #region Properties
        public String Name { get; set; } = null!;
        public String? Desc { get; set; }
        #endregion

        #region Navigation Properties
        public virtual ICollection<ProductLanguage> ProductLanguages { get; set; } = new List<ProductLanguage>();

        public int? MainImageId { get; set; }
        public virtual Image? MainImage { get; set; }

        public virtual ICollection<Image> Images { get; set; } = new List<Image>();
        public virtual ICollection<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();
        #endregion
    }
}
