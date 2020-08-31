using GraphQL.Types;

namespace DnDBeyond.GraphQL_.Inputs
{

    public class CharacterStatsInput : InputObjectGraphType
    {
        public CharacterStatsInput()
        {
            Name = "CharacterStatsInput";
            Field<IntGraphType>("strength");
            Field<IntGraphType>("dexterity");
            Field<IntGraphType>("constitution");
            Field<IntGraphType>("intelligence");
            Field<IntGraphType>("wisdom");
            Field<IntGraphType>("charisma");
        }
    }
}