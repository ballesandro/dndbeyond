using DnDBeyond.Models;
using GraphQL.Types;

namespace DnDBeyond.GraphQL_.Types
{
    public class CharacterStatsType : ObjectGraphType<CharacterStats>
    {
        public CharacterStatsType()
        {
            Field(x => x.Strength).Description("The strength of the character.");
            Field(x => x.Dexterity).Description("The dexterity of the character.");
            Field(x => x.Constitution).Description("The constitution of the character.");
            Field(x => x.Intelligence).Description("The intelligence of the character.");
            Field(x => x.Wisdom).Description("The wisdom of the character.");
            Field(x => x.Charisma).Description("The charisma of the character.");
        }
    }
}
