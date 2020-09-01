using GraphQL.Types;

namespace DnDBeyond.GraphQL_.Inputs
{
    public class CharacterInput : InputObjectGraphType
    {
        public CharacterInput()
        {
            Name = "CharacterInput";
            Field<StringGraphType>("name");
            Field<IntGraphType>("level");
            Field<StringGraphType>("hitPointsMethod");
            Field<CharacterStatsInput>("stats");
            Field<ListGraphType<CharacterClassInput>>("classes");
            Field<ListGraphType<CharacterDefenseInput>>("defenses");
            Field<ListGraphType<ItemInput>>("items");
        }
    }
}