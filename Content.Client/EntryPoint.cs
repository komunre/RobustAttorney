using Robust.Client;
using Robust.Client.Graphics;
using Robust.Shared.ContentPack;
using Robust.Shared.GameObjects;
using Robust.Shared.IoC;
using Robust.Shared.Prototypes;
using Robust.Shared.Timing;
using Content.Client.Overlays;
using Content.Client.UI;
using Robust.Client.State;
using Robust.Client.Player;
using Content.Shared.Player;

// DEVNOTE: Games that want to be on the hub are FORCED use the "Content." prefix for assemblies they want to load.
namespace Content.Client
{
    public class EntryPoint : GameClient
    {
        private StyleLaw _styleLaw;
        private bool playerAttachedToPhraseBox = false;
        public override void Init()
        {
            var factory = IoCManager.Resolve<IComponentFactory>();
            var prototypes = IoCManager.Resolve<IPrototypeManager>();

            factory.DoAutoRegistrations();

            foreach (var ignoreName in IgnoredComponents.List)
            {
                factory.RegisterIgnore(ignoreName);
            }

            foreach (var ignoreName in IgnoredPrototypes.List)
            {
                prototypes.RegisterIgnore(ignoreName);
            }

            ClientContentIoC.Register();

            IoCManager.BuildGraph();

            factory.GenerateNetIds();

            // DEVNOTE: This is generally where you'll be setting up the IoCManager further.
        }

        public override void PostInit()
        {
            base.PostInit();
            
            // DEVNOTE: The line below will disable lighting, so you can see in-game sprites without the need for lights
            //IoCManager.Resolve<ILightManager>().Enabled = false;

            // DEVNOTE: Further setup...
            var client = IoCManager.Resolve<IBaseClient>();

            // DEVNOTE: You might want a main menu to connect to a server, or start a singleplayer game.
            // Be sure to check out StateManager for this! Below you'll find examples to start a game.

            // If you want to connect to a server...
            // client.ConnectToServer("ip-goes-here", 1212);

            // Optionally, singleplayer also works!
            // client.StartSinglePlayer();
            IoCManager.Resolve<ILightManager>().Enabled = false;

            var overlayManager = IoCManager.Resolve<IOverlayManager>();
            overlayManager.AddOverlay(new AttorneyOverlay());

            _styleLaw = new StyleLaw();

            IoCManager.Resolve<IStateManager>().RequestStateChange<GameScreen>();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            
            // DEVNOTE: You might want to do a proper shutdown here.
        }

        public override void Update(ModUpdateLevel level, FrameEventArgs frameEventArgs)
        {
            base.Update(level, frameEventArgs);
            // DEVNOTE: Game update loop goes here. Usually you'll want some independent GameTicker.
            if (!playerAttachedToPhraseBox && IoCManager.Resolve<IPlayerManager>().LocalPlayer?.ControlledEntity != null)
            {
                GameScreen._phraseBox.AttachPlayer(IoCManager.Resolve<IPlayerManager>().LocalPlayer.ControlledEntity.GetComponent<AttorneyComponent>());
                playerAttachedToPhraseBox = true;
            }
        }
    }

    
}