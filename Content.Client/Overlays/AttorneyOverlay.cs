using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Robust.Shared.GameObjects;
using Robust.Client.Graphics;
using Robust.Shared.Enums;
using Robust.Shared.IoC;
using Robust.Shared.Prototypes;
using Content.Shared.Player;
using Robust.Client.ResourceManagement;
using Robust.Shared.Maths;
using Robust.Shared.Log;

namespace Content.Client.Overlays
{
    class AttorneyOverlay : Overlay
    {
        [Dependency] private readonly IPrototypeManager _prototypeManager = default!;
        [Dependency] private readonly IComponentManager _componentManager = default!;
        [Dependency] private readonly IResourceCache _resCache = default!;
        public override OverlaySpace Space => OverlaySpace.ScreenSpaceBelowWorld;

        private readonly ShaderInstance _shader;

        private Dictionary<string, Texture> _attorneyTextures = new();

        public AttorneyOverlay()
        {
            IoCManager.InjectDependencies(this);
            _shader = _prototypeManager.Index<ShaderPrototype>("unshaded").Instance();
        }

        protected override void Draw(in OverlayDrawArgs args)
        {
            var handle = args.ScreenHandle;

            handle.UseShader(_shader);
            handle.DrawRect(new UIBox2(new Vector2(0, 0), new Vector2(800, 600)), Color.White, false);

            foreach (var attorney in _componentManager.EntityQuery<AttorneyComponent>())
            {
                if (attorney.Changed)
                {
                    switch (attorney.Defense)
                    {
                        case false:
                            if (!_attorneyTextures.ContainsKey(attorney.Avatar))
                            {
                                _attorneyTextures.Add(attorney.Avatar, _resCache.GetResource<TextureResource>("/Textures/attorneys/" + attorney.Avatar).Texture);
                            }
                            handle.DrawTextureRect(_attorneyTextures[attorney.Avatar], new UIBox2(new Vector2(0, 0), new Vector2(150, 150)));
                            break;
                        case true:
                            if (!_attorneyTextures.ContainsKey(attorney.Avatar))
                            {
                                _attorneyTextures.Add(attorney.Avatar, _resCache.GetResource<TextureResource>("/Textures/attorneys/" + attorney.Avatar).Texture);
                            }
                            handle.DrawTextureRect(_attorneyTextures[attorney.Avatar], new UIBox2(new Vector2(300, 0), new Vector2(300+150, 150)));
                            break;
                    }
                }
            }

            handle.UseShader(null);
        }
    }
}
