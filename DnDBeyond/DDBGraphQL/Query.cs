using System.Collections.Generic;
using System.Threading.Tasks;
using DnDBeyond.DB;
using DnDBeyond.Models;
using GraphQL;

namespace DnDBeyond.DDBGraphQL
{
    public class Query
    {
        private static CharactersRepository _repo;

        public Query(IDependencyResolver resolver)
        {
            _repo = resolver.Resolve<CharactersRepository>();
        }

        [GraphQLMetadata("characters")]
        public async Task<IEnumerable<Character>> GetCharacters()
        {
            return await _repo.GetAll();
        }

        [GraphQLMetadata("character")]
        public async Task<Character> GetCharacter(long id)
        {
            return await _repo.GetById(id);
        }
    }
}
