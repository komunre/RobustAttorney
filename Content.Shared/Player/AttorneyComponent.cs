using Robust.Shared.IoC;
using Robust.Shared.Player;
using Robust.Shared.GameObjects;
using Robust.Shared.Network;
using Robust.Shared.Serialization;
using Robust.Shared.GameStates;
using System;
using System.Collections.Generic;

namespace Content.Shared.Player
{
    [RegisterComponent, NetworkedComponent]
    public class AttorneyComponent : Component
    {
        public override string Name => "Attorney";

        public string Phrase = "";
        public bool Changed = false;
        public string Avatar = "attorney.jpg";
        public bool Defense = false;
        public string AttorneyName = "Null attorney";
    }

    public class SharedAttorneyControlSystem : EntitySystem
    {
        public static Dictionary<AttorneyComponent, string> Phrases = new();
        public override void Initialize()
        {
            SubscribeLocalEvent<AttorneyComponent, ComponentGetState>(GetAttorneyState);
            SubscribeLocalEvent<AttorneyComponent, ComponentHandleState>(HandleAttorneyState);
        }

        private void GetAttorneyState(EntityUid id, AttorneyComponent attorney, ref ComponentGetState args)
        {
            args.State = new AttorneyState(attorney.Phrase, attorney.Changed, attorney.Avatar, attorney.Defense, attorney.AttorneyName);
        }

        private void HandleAttorneyState(EntityUid id, AttorneyComponent attorney, ref ComponentHandleState args)
        {
            if (args.Current is not AttorneyState state)
                return;

            attorney.Phrase = state.Phrase;
            Phrases[attorney] = state.Phrase;
            attorney.Changed = state.Changed;
            attorney.Avatar = state.Avatar;
            attorney.Defense = state.Defense;
            attorney.AttorneyName = state.Name;
        }
    }

    [NetSerializable, Serializable]
    public class AttorneyState : ComponentState
    {
        public string Phrase = "";
        public bool Changed;
        public string Avatar;
        public bool Defense = false;
        public string Name = "Null";
        public AttorneyState(string phrase, bool changed, string avatar, bool defense, string name)
        {
            Phrase = phrase;
            Changed = changed;
            Avatar = avatar;
            Defense = defense;
            Name = name;
        }
    }
}