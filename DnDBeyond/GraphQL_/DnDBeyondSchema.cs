using GraphQL;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;

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
