using DnDBeyond.Models;
using GraphQL.Types;

namespace DnDBeyond.GraphQL_.Types
{
    public class CharacterType : ObjectGraphType<Character>
    {
        public CharacterType()
        {
            Field(x => x.Id).Description("The Id of the character.");
            Field(x => x.Name).Description("The name of the character.");
            Field(x => x.Level).Description("The level of the character.");
            Field(x => x.MaxHitPoints).Description("The max hit points of the character.");
            Field(x => x.CurrentHitPoints).Description("The current hit points of the character.");
            Field(x => x.TemporaryHitPoints).Description("The temporary hit points of the character.");
            Field(
                name: "HitPointsMethod",
                type: typeof(HitPointsMethodType),
                resolve: context => context.Source.HitPointsMethod,
                description: "The method used to determine character's hit points.");
            Field(
                name: "Stats",
                type: typeof(CharacterStatsType),
                resolve: context => context.Source.Stats,
                description: "The stats of the character.");
            Field(
                name: "Classes",
                type: typeof(ListGraphType<CharacterClassType>),
                resolve: context => context.Source.Classes,
                description: "The classes of the character.");
            Field(
                name: "Defenses",
                type: typeof(ListGraphType<CharacterDefenseType>),
                resolve: context => context.Source.Defenses,
                description: "The defenses of the character.");
            Field(
                name: "Items",
                type: typeof(ListGraphType<ItemType>),
                resolve: context => context.Source.Items,
                description: "The items of the character.");
        }
    }
}