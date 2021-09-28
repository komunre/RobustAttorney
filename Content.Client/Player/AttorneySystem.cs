using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Robust.Shared.GameObjects;
using Content.Shared.Player;
using Robust.Shared.Player;
using Robust.Shared.IoC;
using Robust.Client.Player;

namespace Content.Client.Player
{
    class AttorneySystem : SharedAttorneySystem
    {
        [Dependency] private readonly IPlayerManager _playerManager = default!;
        public void RequestFocus(EntityUid id)
        {
            RaiseNetworkEvent(new AttorneyFocusRequest(id));
        }
    }
}
