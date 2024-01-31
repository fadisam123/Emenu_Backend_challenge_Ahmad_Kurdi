using Domain.Enums.EntitiesEnums;

namespace Domain.Entities
{
    public class Attribute : BaseEntity
    {
        #region Properties
        public string Name { get; set; } = null!;
        public AttributeTypeEnum Type { get; set; } = AttributeTypeEnum.Text;
        #endregion

        #region Navigation Properties
        public virtual ICollection<Variant> Variants { get; set; } = new List<Variant>();
        #endregion
    }
}
