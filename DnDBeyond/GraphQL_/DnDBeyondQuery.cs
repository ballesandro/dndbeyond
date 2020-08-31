using DnDBeyond.GraphQL_.Types;
using DnDBeyond.Services;
using GraphQL.Types;

namespace DnDBeyond.GraphQL_
{
    public class DnDBeyondQuery : ObjectGraphType
    {
        public DnDBeyondQuery(ICharactersService charactersService)
        {
            Name = "Query";

            Field<CharacterType>(
                "character",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id", Description = "id of the character" }),
                resolve: context => charactersService.GetCharacter(context.GetArgument<long>("id")));
            Field<ListGraphType<CharacterType>>(
                "characters",
                resolve: context => charactersService.GetCharacters());
        }
    }
}