using System;
using GraphQL.Types;

namespace DnDBeyond.GraphQL_.Inputs
{
    public class CharacterClassInput : InputObjectGraphType
    {
        public CharacterClassInput()
        {
            Name = "Class";
            Field<StringGraphType>("name");
            Field<IntGraphType>("hitDiceValue");
            Field<IntGraphType>("classLevel");
        }
    }
}