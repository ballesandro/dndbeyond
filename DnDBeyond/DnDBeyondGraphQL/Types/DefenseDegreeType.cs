using GraphQL.Types;

namespace DnDBeyond.GraphQL_.Types
{
    public class DefenseDegreeType : EnumerationGraphType
    {
        public DefenseDegreeType()
        {
            Name = "Defense";
            Description = "Degree of defense for a given damage type.";
            AddValue("immunity", "Fully immune to given damage type", "immunity");
            AddValue("resistance", "Partially resistant to given damage type", "resistance");
        }
    }
}
