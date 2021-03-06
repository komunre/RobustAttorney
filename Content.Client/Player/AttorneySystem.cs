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

        public override void Initialize()
        {
            base.Initialize();
        }
        public void RequestFocus(EntityUid id)
        {
            RaiseNetworkEvent(new AttorneyFocusRequest(id));
        }

        public void ChangePhrase(EntityUid id, string phrase)
        {
            RaiseNetworkEvent(new ChangePhraseRequest(id, phrase));
        }

        public void EndPhrase()
        {
            RaiseNetworkEvent(new EndPhraseRequest());
        }

        public void Objection(EntityUid id)
        {
            RaiseNetworkEvent(new ObjectionRequest(id));
        }
    }
}
