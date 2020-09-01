using GraphQL.Types;

namespace DnDBeyond.GraphQL_.Types
{
    public class HitPointsMethodType : EnumerationGraphType
    {
        public HitPointsMethodType()
        {
            Name = "HitPointsMethod";
            Description = "Method for generating hit point maximum.";
            AddValue("random", "random method for generating hit points", "random");
            AddValue("average", "average method for generating hit points", "average");
        }
    }
}
