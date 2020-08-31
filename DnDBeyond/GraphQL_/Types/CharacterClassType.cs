using DnDBeyond.Models;
using GraphQL.Types;

namespace DnDBeyond.GraphQL_.Types
{
    public class CharacterClassType : ObjectGraphType<CharacterClass>
    {
        public CharacterClassType()
        {
            Field(x => x.Name).Description("The name of the class.");
            Field(x => x.HitDiceValue).Description("The hit dice value of the class.");
            Field(x => x.ClassLevel).Description("The level of the class.");
        }
    }
}