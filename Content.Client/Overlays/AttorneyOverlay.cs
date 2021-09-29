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
        [Dependency] private readonly IEntityManager _entityManager = default!;
        [Dependency] private readonly IResourceCache _resCache = default!;
        [Dependency] private readonly IClyde _clyde = default!;
        public override OverlaySpace Space => OverlaySpace.ScreenSpaceBelowWorld;

        private readonly ShaderInstance _shader;

        private Dictionary<string, Texture> _attorneyTextures = new();
        private Texture _lawCourt;
        public AttorneyOverlay()
        {
            IoCManager.InjectDependencies(this);
            _shader = _prototypeManager.Index<ShaderPrototype>("unshaded").Instance();
            _lawCourt = _resCache.GetResource<TextureResource>("/Textures/law_court.png").Texture;
        }

        protected override void Draw(in OverlayDrawArgs args)
        {
            var handle = args.ScreenHandle;

            handle.UseShader(_shader);
            handle.DrawRect(new UIBox2(new Vector2(0, 0), new Vector2(800, 600)), Color.White, false);
            handle.DrawTextureRect(_lawCourt, new UIBox2(0, 0, _clyde.ScreenSize.X, _clyde.ScreenSize.Y));

            foreach (var attorney in _entityManager.EntityQuery<AttorneyComponent>())
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
                            //handle.DrawTextureRect(_attorneyTextures[attorney.Avatar], new UIBox2(new Vector2(0, 0), new Vector2(150, 150)));
                            handle.DrawRect(new UIBox2(0, 0, 150, 150), attorney.Color);
                            break;
                        case true:
                            if (!_attorneyTextures.ContainsKey(attorney.Avatar))
                            {
                                _attorneyTextures.Add(attorney.Avatar, _resCache.GetResource<TextureResource>("/Textures/attorneys/" + attorney.Avatar).Texture);
                            }
                            //handle.DrawTextureRect(_attorneyTextures[attorney.Avatar], new UIBox2(new Vector2(300, 0), new Vector2(300+150, 150)));
                            handle.DrawRect(new UIBox2(650, 0, 650 + 150, 150), attorney.Color);
                            break;
                    }
                }
            }

            handle.UseShader(null);
        }
    }
}
