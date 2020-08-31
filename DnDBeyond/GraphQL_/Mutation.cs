using System.Collections.Generic;
using System.Threading.Tasks;
using DnDBeyond.DB;
using DnDBeyond.Models;
using DnDBeyond.Services;
using GraphQL;
using GraphQL.Types;

namespace DnDBeyond.GraphQL_
{
    public class Mutation
    {
        private readonly ICharactersService _characterService;

        public Mutation(IDependencyResolver resolver)
        {
            _characterService = resolver.Resolve<ICharactersService>();
        }

        [GraphQLMetadata("create")]
        public async Task<Character> CreateCharacter(CharacterInput character)
        {
            return await _characterService.CreateCharacter(character);
        }

        [GraphQLMetadata("createCharacter")]
        public Character CreateCharacterInput(Character character)
        {
            return character;
        }
    }

    public class CharacterInput : Character
    {
        public CharacterInput()
        {
        }
    }
}
