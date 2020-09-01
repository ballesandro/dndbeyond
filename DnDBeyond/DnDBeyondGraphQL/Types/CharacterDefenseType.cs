using DnDBeyond.Models;
using GraphQL.Types;

namespace DnDBeyond.GraphQL_.Types
{
    public class CharacterDefenseType : ObjectGraphType<CharacterDefense>
    {
        public CharacterDefenseType()
        {
            Field(x => x.Type).Description("The type of defense.");
            Field(
                name: "Defense",
                type: typeof(DefenseDegreeType),
                resolve: context => context.Source.Defense,
                description: "The degree of defense for type.");
        }
    }
}