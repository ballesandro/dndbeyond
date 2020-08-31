using System;
using DnDBeyond.Models;
using GraphQL;
using GraphQL.Types;

namespace DnDBeyond.GraphQL_
{
    public class Schema : GraphQL.Types.Schema
    {

        private ISchema _schema;

        // Assuming inputs and outputs are the same for this exercise.

        const string STATS = @"
            strength: Int,
            dexterity: Int,
            constitution: Int,
            intelligence: Int,
            wisdom: Int,
            charisma: Int";

        const string CLASS = @"
            name: String,
            hitDiceValue: Int,
            classLevel: Int";

        const string DEFENSE = @"
            type: String,
            defense: DefenseDegree";

        const string MODIFIER = @"
            affectedObject: String,
            affectedValue: String,
            value: Int";

        const string ITEM = @"
            id: ID,
            name: String,
            modifier: Modifier";

        const string CHARACTER = @"
            id: ID,
            name: String,
            level: Int,
            maxHitPoints: Int,
            currentHitPoints: Int,
            temporaryHitPoints: Int,
            hitPointsMethod: HitPointsMethod,
            stats: CharacterStats,
            classes: [CharacterClass],
            defenses: [CharacterDefense],
            items: [Item]";

        public Schema(IServiceProvider sp)
        {
            this._schema = GraphQL.Types.Schema.For(
            @"
                enum HitPointsMethod {
                    average,
                    random
                }

                enum DefenseDegree {
                    resistance,
                    immunity
                }

                type CharacterStats {" +
                    STATS + @"
                }

                type CharacterClass {" +
                    CLASS + @"
                }

                type CharacterDefense {" +
                    DEFENSE + @"
                }

                type Modifier {" +
                    MODIFIER + @"
                }

                type Item {" +
                    ITEM + @"
                }

                type Character {" +
                    CHARACTER + @"
                }

                input CharacterStatsInput {" +
                    STATS + @"
                }

                input CharacterClassInput {" +
                    CLASS + @"
                }

                input CharacterDefenseInput {" +
                    DEFENSE + @"
                }

                input ModifierInput {" +
                    MODIFIER + @"
                }

                input ItemInput {" +
                    ITEM + @"
                }

                input CharacterInput {" +
                    CHARACTER + @"
                }

                type Query {
                    characters: [Character],
                    character(id: ID): Character
                }

                type Mutation {
                    create(character: CharacterInput): Character
                }
             ", _ =>
            {
                _.DependencyResolver = new FuncDependencyResolver(t => sp.GetService(t));
                _.Types.Include<Query>();
                _.Types.Include<Mutation>();
            });

            //Query = _schema.Query;
            //Mutation = _schema.Mutation;

            //Mutation.AddField(new FieldType() {
            //    Name = "createCharacter",
            //    Arguments = new QueryArguments(new QueryArgument<NonNullGraphType<CharacterInput>> { Name = "character" }),
            //    Resolver = context =>
            //    {
            //        var character = context.GetArgument<Character>("character");
            //        return repository.CreateOwner(owner);
            //    }
            //);
        //);
        }
    }
}
