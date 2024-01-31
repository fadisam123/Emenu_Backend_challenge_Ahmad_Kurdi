using Domain.Enums.EntitiesEnums;
using System.Linq.Expressions;

namespace Application.Services
{
    public static class HelperMethod
    {
        public static AttributeTypeEnum MapAttributeTypeToEnum(string input)
        {
            bool success = Enum.TryParse(input, true, out AttributeTypeEnum result);
            if (success)
                return result;
            else
                return AttributeTypeEnum.Text;
        }
    }
}
