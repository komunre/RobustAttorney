using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Robust.Client.State;
using Robust.Client.UserInterface;
using Robust.Client.UserInterface.Controls;
using Robust.Shared.IoC;
using Robust.Shared.Timing;
using Robust.Client.Player;
using Content.Shared.Player;
using Robust.Shared.Log;

namespace Content.Client.UI
{
    class GameScreen : State
    {
        [Dependency] private readonly IUserInterfaceManager _userInterfaceManager = default;
        [Dependency] private readonly IPlayerManager _playerManager = default!;
        public static PhraseBox _phraseBox;

        public override void Shutdown()
        {
            
        }

        public override void Startup()
        {
            _phraseBox = new PhraseBox();
            var attorneyBox = new AttorneyBox();

            LayoutContainer.SetAnchorAndMarginPreset(_phraseBox, LayoutContainer.LayoutPreset.BottomRight);
            LayoutContainer.SetAnchorAndMarginPreset(attorneyBox, LayoutContainer.LayoutPreset.BottomLeft);

            _userInterfaceManager.StateRoot.AddChild(_phraseBox);
            _userInterfaceManager.StateRoot.AddChild(attorneyBox);
        }
    }
}
