namespace Domain.Entities
{
    public class ProductLanguage : BaseEntity
    {
        #region Properties
        public string languageCode { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Desc { get; set; }
        #endregion

        #region Navigation Properties
        public virtual Product Product { get; set; } = null!;
        #endregion
    }
}
