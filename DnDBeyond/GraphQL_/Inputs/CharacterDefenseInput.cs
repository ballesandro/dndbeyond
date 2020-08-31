using DnDBeyond.GraphQL_.Types;
using GraphQL.Types;

namespace DnDBeyond.GraphQL_.Inputs
{
    public class CharacterDefenseInput : InputObjectGraphType
    {
        public CharacterDefenseInput()
        {
            Name = "Defense";
            Field<StringGraphType>("type");
            Field<DefenseDegreeType>("defense");
        }
    }
}
