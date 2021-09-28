using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Robust.Shared.GameObjects;
using Robust.Shared.Serialization;
using System;

namespace Content.Shared.Player
{
    [Serializable, NetSerializable]
    public class AttorneyFocusRequest : EntityEventArgs
    {
        public EntityUid Id;
        public AttorneyFocusRequest(EntityUid id)
        {
            Id = id;
        }
    }
    public class SharedAttorneySystem : EntitySystem
    {
    }
}
