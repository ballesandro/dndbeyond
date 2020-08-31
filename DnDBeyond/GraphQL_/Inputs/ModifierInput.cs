using GraphQL.Types;

namespace DnDBeyond.GraphQL_.Inputs
{
    public class ModifierInput : InputObjectGraphType
    {
        public ModifierInput()
        {
            Name = "Modifier";
            Field<StringGraphType>("affectedObject");
            Field<StringGraphType>("affectedValue");
            Field<IntGraphType>("value");
        }
    }
}