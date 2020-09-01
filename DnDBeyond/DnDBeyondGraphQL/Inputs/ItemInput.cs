using GraphQL.Types;

namespace DnDBeyond.GraphQL_.Inputs
{
    public class ItemInput : InputObjectGraphType
    {
        public ItemInput()
        {
            Name = "Item";
            Field<StringGraphType>("name");
            Field<ModifierInput>("modifier");
        }
    }
}