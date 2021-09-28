using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Content.Shared.Player;
using Robust.Shared.IoC;
using Robust.Shared.GameObjects;

namespace Content.Server.Player
{
    class AttorneySystem : SharedAttorneySystem
    {
        [Dependency] private readonly IEntityManager _entityManager = default!;
        public override void Initialize()
        {
            SubscribeNetworkEvent<AttorneyFocusRequest>(HandleAttorneyFocusRequest);
            SubscribeNetworkEvent<ChangePhraseRequest>(HandleChangePhraseRequest);
        }

        private void HandleAttorneyFocusRequest(AttorneyFocusRequest request)
        {
            foreach (var attorney in _entityManager.EntityQuery<AttorneyComponent>())
            {
                attorney.Changed = false;
                attorney.Dirty();
            }
            var attoneyToFocus = EntityManager.GetEntity(request.Id).GetComponent<AttorneyComponent>();
            attoneyToFocus.Changed = true;
            attoneyToFocus.Dirty();
        }

        private void HandleChangePhraseRequest(ChangePhraseRequest request)
        {
            EntityManager.GetEntity(request.Id).GetComponent<AttorneyComponent>().Phrase = request.Phrase;
        }
    }
}
