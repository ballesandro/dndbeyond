using DnDBeyond.Models;
using GraphQL.Types;

namespace DnDBeyond.GraphQL_.Types
{
    public class ModifierType : ObjectGraphType<Modifier>
    {
        public ModifierType()
        {
            Field(x => x.AffectedObject).Description("The affected object - currently only supports stats.");
            Field(x => x.AffectedValue).Description("The affected value of the affected object..");
            Field(x => x.Value).Description("The value to modify the affected value by.");
        }
    }
}
