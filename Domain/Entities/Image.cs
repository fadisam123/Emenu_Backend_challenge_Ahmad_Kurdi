namespace Domain.Entities
{
    public class Image : BaseEntity
    {
        #region Properties
        public string Path { get; set; } = null!;
        public Boolean IsMain { get; set; } = false;
        #endregion


        #region Navigation Properties
        public virtual Product Product { get; set; } = null!;
        public virtual ICollection<VariantImage> VariantImages { get; set; } = new List<VariantImage>();
        #endregion
    }
}
