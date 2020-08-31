using System;
using GraphQL;
using GraphQL.Types;

namespace DnDBeyond.DDBGraphQL
{
    public class DDBSchema : Schema
    {

        private ISchema _schema;

        public DDBSchema(IServiceProvider sp)
        {
            this._schema = Schema.For(
            @"
                enum HitPointsMethod {
                    average,
                    random
                }

                enum DefenseDegree {
                    resistance,
                    immunity
                }

                type CharacterStats {
                    strength: Int,
                    dexterity: Int,
                    constitution: Int,
                    intelligence: Int,
                    wisdom: Int,
                    charisma: Int,
                }

                type CharacterClass {
                    name: String,
                    hitDiceValue: Int,
                    classLevel: Int
                }

                type CharacterDefense {
                    type: String,
                    defense: DefenseDegree
                }

                type Modifier {
                    affectedObject: String,
                    affectedValue: String,
                    value: Int
                }

                type Item {
                    id: ID,
                    name: String,
                    modifier: Modifier 
                }

                type Character {
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
                    items: [Item]
                }

                type Query {
                    characters: [Character],
                    character(id: ID): Character
                }
             ", _ =>
            {
                _.DependencyResolver = new FuncDependencyResolver(t => sp.GetService(t));
                _.Types.Include<Query>();

            });
            Query = _schema.Query;
        }
    }
}
