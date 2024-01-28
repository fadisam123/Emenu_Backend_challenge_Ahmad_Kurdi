namespace Domain.Entities
{
    public class ProductLanguage : Base
    {
        #region Properties
        public String languageCode { get; set; } = null!;
        public String Name { get; set; } = null!;
        public String? Desc { get; set; };
        #endregion

        #region Navigation Properties
        public virtual Product Product { get; set; } = null!;
        #endregion
    }
}
