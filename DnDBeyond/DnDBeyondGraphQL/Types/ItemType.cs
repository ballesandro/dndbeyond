using DnDBeyond.Models;
using GraphQL.Types;

namespace DnDBeyond.GraphQL_.Types
{
    public class ItemType : ObjectGraphType<Item>
    {
        public ItemType()
        {
            Field(x => x.Name).Description("The name of the item.");
            Field(
                name: "Modifier",
                type: typeof(ModifierType),
                resolve: context => context.Source.Modifier,
                description: "The item's modifier.");
        }
    }
}