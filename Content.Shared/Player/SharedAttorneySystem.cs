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

    [Serializable, NetSerializable]
    public class ChangePhraseRequest : EntityEventArgs
    {
        public EntityUid Id;
        public string Phrase;

        public ChangePhraseRequest(EntityUid id, string phrase)
        {
            Id = id;
            Phrase = phrase;
        }
    }

    [Serializable, NetSerializable]
    public class ObjectionRequest : EntityEventArgs
    {
        public EntityUid Id;
        public ObjectionRequest(EntityUid id)
        {
            Id = id;
        }
    }

    [Serializable, NetSerializable]
    public class EndPhraseRequest : EntityEventArgs
    {
        
    }

    [Serializable, NetSerializable]
    public class PlayObjection : EntityEventArgs
    {

    }

    [Serializable, NetSerializable]
    public class PlayPhraseEnd : EntityEventArgs
    {

    }
    public class SharedAttorneySystem : EntitySystem
    {
    }

}
