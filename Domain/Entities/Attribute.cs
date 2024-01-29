using Domain.Enums.EntitiesEnums;

namespace Domain.Entities
{
    public class Attribute : BaseEntity
    {
        #region Properties
        public String Name { get; set; } = null!;
        public AttributeTypeEnum Type { get; set; }
        #endregion

        #region Navigation Properties
        public virtual ICollection<Variant> Variants { get; set; } = new List<Variant>();
        #endregion
    }
}
