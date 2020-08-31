using GraphQL.Types;

namespace DnDBeyond.GraphQL_.Inputs
{
    public class CharacterInput : InputObjectGraphType
    {
        public CharacterInput()
        {
            Name = "CharacterInput";
            Field<StringGraphType>("name");
            Field<StringGraphType>("HitPointsMethod");
            Field<CharacterStatsInput>("stats");
            Field<ListGraphType<CharacterClassInput>>("classes");
            Field<ListGraphType<CharacterDefenseInput>>("defenses");
            Field<ListGraphType<ItemInput>>("items");
        }
    }
}