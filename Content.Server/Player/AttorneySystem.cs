using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Content.Shared.Player;
using Robust.Shared.IoC;
using Robust.Shared.GameObjects;
using Robust.Shared.Audio;
using Robust.Shared.Player;
using Robust.Shared.ContentPack;
using Robust.Shared.Utility;

namespace Content.Server.Player
{
    class AttorneySystem : SharedAttorneySystem
    {
        [Dependency] private readonly IEntityManager _entityManager = default!;
        [Dependency] private readonly IAudioSystem _audioSystem = default!;
        [Dependency] private readonly IResourceManager _resourceManager = default!;
        public override void Initialize()
        {
            SubscribeNetworkEvent<AttorneyFocusRequest>(HandleAttorneyFocusRequest);
            SubscribeNetworkEvent<ChangePhraseRequest>(HandleChangePhraseRequest);
            SubscribeNetworkEvent<ObjectionRequest>(HandleObjectionRequest);
            SubscribeNetworkEvent<EndPhraseRequest>(HandlePhraseEnd);
        }

        private void HandleAttorneyFocusRequest(AttorneyFocusRequest request)
        {
            ChangeFocus(request.Id);
        }

        private void ChangeFocus(EntityUid id)
        {
            foreach (var attorney in _entityManager.EntityQuery<AttorneyComponent>())
            {
                attorney.Changed = false;
                attorney.Dirty();
            }
            var attoneyToFocus = EntityManager.GetEntity(id).GetComponent<AttorneyComponent>();
            attoneyToFocus.Changed = true;
            attoneyToFocus.Dirty();
        }

        private void HandleChangePhraseRequest(ChangePhraseRequest request)
        {
            EntityManager.GetEntity(request.Id).GetComponent<AttorneyComponent>().Phrase = request.Phrase;
        }

        private void HandleObjectionRequest(ObjectionRequest request)
        {
            ChangeFocus(request.Id);
            _audioSystem.Play(Filter.Broadcast(), "/Sounds/objection.ogg");
        }

        private void HandlePhraseEnd(EndPhraseRequest request)
        {
            _audioSystem.Play(Filter.Broadcast(), "/Sounds/ping.ogg");
        }
    }
}
