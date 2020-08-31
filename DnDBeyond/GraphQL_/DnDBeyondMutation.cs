using DnDBeyond.GraphQL_.Types;
using DnDBeyond.Models;
using DnDBeyond.Services;
using GraphQL.Types;

namespace DnDBeyond.GraphQL_
{
    public class DnDBeyondMutation : ObjectGraphType
    {
        public DnDBeyondMutation(ICharactersService charactersService)
        {
            Name = "Mutation";

            Field<CharacterType>(
              "createCharacter",
              arguments: new QueryArguments(
                new QueryArgument<NonNullGraphType<Inputs.CharacterInput>> { Name = "character" }),
              resolve: context =>
              {
                  var character = context.GetArgument<Character>("character");
                  return charactersService.CreateCharacter(character);
              });
        }
    }
}