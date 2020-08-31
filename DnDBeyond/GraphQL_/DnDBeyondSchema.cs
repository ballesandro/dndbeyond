using System;
using Microsoft.Extensions.DependencyInjection;

namespace DnDBeyond.GraphQL_
{
    public class DnDBeyondSchema : Schema
    {
        public DnDBeyondSchema(IServiceProvider provider)
            : base(provider)
        {
            Query = provider.GetRequiredService<DnDBeyondQuery>();
            Mutation = provider.GetRequiredService<DnDBeyondMutation>();
        }
    }
}
