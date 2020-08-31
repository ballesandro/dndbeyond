using GraphQL;
using GraphQL.Types;

namespace DnDBeyond.GraphQL_
{
    public class DnDBeyondSchema : Schema
    {
        public DnDBeyondSchema(IDependencyResolver provider)
            : base(provider)
        {
            Query = provider.Resolve<DnDBeyondQuery>();
            Mutation = provider.Resolve<DnDBeyondMutation>();
        }
    }
}
