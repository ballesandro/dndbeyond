using dndbeyond.Models;
using dndbeyond.Models.Enum;

namespace dndbeyond.Controllers.Payload
{
    public class NewCharacterPayload
    {
        public NewCharacterPayload()
        {
        }

        public HitPointsMethod Method { get; set; }
        public Character Character { get; set; }
    }
}
