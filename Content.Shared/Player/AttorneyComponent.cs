using Robust.Shared.IoC;
using Robust.Shared.Player;
using Robust.Shared.GameObjects;
using Robust.Shared.Network;
using Robust.Shared.Serialization;
using Robust.Shared.GameStates;
using System;
using System.Collections.Generic;
using Robust.Shared.Maths;

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
        public Color Color = Color.Red;
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
            args.State = new AttorneyState(attorney.Phrase, attorney.Changed, attorney.Avatar, attorney.Defense, attorney.AttorneyName, attorney.Color);
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
            attorney.Color = state.Color;
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
        public Color Color = Color.Red;
        public AttorneyState(string phrase, bool changed, string avatar, bool defense, string name, Color color)
        {
            Phrase = phrase;
            Changed = changed;
            Avatar = avatar;
            Defense = defense;
            Name = name;
            Color = color;
        }
    }
}