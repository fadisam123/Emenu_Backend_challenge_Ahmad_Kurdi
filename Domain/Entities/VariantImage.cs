namespace Domain.Entities
{
    public class VariantImage : Base
    {
        #region Properties

        #endregion

        #region Navigation Properties
        public virtual Image Image { get; set; } = null!;
        public virtual ProductVariant ProductVariant { get; set; } = null!;
        #endregion
    }
}
