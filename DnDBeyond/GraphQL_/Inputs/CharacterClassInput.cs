using System;
using GraphQL.Types;

namespace DnDBeyond.GraphQL_.Inputs
{
    public class CharacterClassInput : InputObjectGraphType
    {
        public CharacterClassInput()
        {
            Name = "Class";
            Field<IntGraphType>("strength");
            Field<IntGraphType>("dexterity");
            Field<IntGraphType>("constitution");
            Field<IntGraphType>("intelligence");
            Field<IntGraphType>("wisdom");
            Field<IntGraphType>("charisma");
        }
    }
}
