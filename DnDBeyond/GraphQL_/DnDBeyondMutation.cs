using DnDBeyond.GraphQL_.Types;
using DnDBeyond.Models;
using DnDBeyond.Services;
using GraphQL.Types;

namespace DnDBeyond.GraphQL_
{
    public class DnDBeyondMutation : ObjectGraphType
    {
        public DnDBeyondMutation(ICharactersService charactersService, IHitPointsService hitPointsService)
        {
            Name = "Mutation";

            // Create new character
            Field<CharacterType>(
              "createCharacter",
              arguments: new QueryArguments(
                new QueryArgument<NonNullGraphType<Inputs.CharacterInput>> { Name = "character" }),
              resolve: context =>
              {
                  var character = context.GetArgument<Character>("character");
                  return charactersService.CreateCharacter(character);
              });

            // Update character's temporary hit points
            Field<CharacterType>(
              "updateTemporaryHitPoints",
              arguments: new QueryArguments(
                new QueryArgument<IdGraphType> { Name = "id" },
                new QueryArgument<IntGraphType> { Name = "temporaryHitPoints" }),
              resolve: context =>
              {
                  var id = context.GetArgument<long>("id");
                  var temporaryHitPoints = context.GetArgument<int>("temporaryHitPoints");
                  return hitPointsService.UpdateTemporaryHitPoints(id, temporaryHitPoints);
              });

            // Damage a character
            Field<CharacterType>(
              "damageCharacter",
              arguments: new QueryArguments(
                new QueryArgument<IdGraphType> { Name = "id" },
                new QueryArgument<IntGraphType> { Name = "damage" },
                new QueryArgument<StringGraphType> { Name = "damageType" }),
              resolve: context =>
              {
                  var id = context.GetArgument<long>("id");
                  var damage = context.GetArgument<int>("damage");
                  var damageType = context.GetArgument<string>("damageType");
                  return hitPointsService.DamageCharacter(id, damage, damageType);
              });

            // Heal a character
            Field<CharacterType>(
              "healCharacter",
              arguments: new QueryArguments(
                new QueryArgument<IdGraphType> { Name = "id" },
                new QueryArgument<IntGraphType> { Name = "heal" }),
              resolve: context =>
              {
                  var id = context.GetArgument<long>("id");
                  var heal = context.GetArgument<int>("heal");
                  return hitPointsService.HealCharacter(id, heal);
              });
        }
    }
}