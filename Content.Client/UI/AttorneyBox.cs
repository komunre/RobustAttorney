using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Robust.Client.UserInterface.Controls;
using Robust.Client.UserInterface;
using Robust.Shared.IoC;
using Robust.Shared.GameObjects;
using Content.Shared.Player;
using Robust.Shared.Maths;
using Robust.Client.Graphics;
using Robust.Shared.Timing;

namespace Content.Client.UI
{
    class AttorneyBox : Control
    {
        [Dependency] private readonly IEntityManager _entityManager = default!;
        [Dependency] private readonly IClyde _clyde = default!;
        private RichTextLabel _content;
        private Label _name;
        private float panelWidth = 1280;
        private PanelContainer _mainContainer;
        public AttorneyBox()
        {
            //panelWidth = _clyde.ScreenSize.X;
            Margin = new Thickness(-(1280/2), 0);
            IoCManager.InjectDependencies(this);
            AddChild((_mainContainer = new PanelContainer() {
                MinWidth = 800f,
                //MaxWidth = panelWidth,
                MinHeight = 200f,
                MaxHeight = 200f,
                Children = {
                    new BoxContainer()
                    {
                        Orientation = BoxContainer.LayoutOrientation.Vertical,
                        Children =
                        {
                            (_name = new Label()
                            {
                                Align = Label.AlignMode.Center,
                                MinWidth = 100f,
                                MinHeight = 20f,
                                FontColorOverride = new Color(255, 255, 255),
                                Text = "Null Attorney!"
                            }),
                            new ScrollContainer() {
                                MinWidth = panelWidth,
                                MinHeight = 180f,
                                MaxHeight = 180f,
                                Children = {
                                    (_content = new RichTextLabel()
                                    {
                                        MinWidth = 300f,
                                        MaxWidth = 800f,
                                        RectClipContent = true,
                                    }),
                                }
                            }
                        }
                    }
                }
            }));
        }

        public void Update()
        {
            foreach (var attorney in _entityManager.EntityQuery<AttorneyComponent>())
            {
                if (attorney.Changed)
                {
                    _name.Text = attorney.AttorneyName;
                    _content.SetMessage(attorney.Phrase);
                }
            }
        }

        protected override void FrameUpdate(FrameEventArgs args)
        {
            base.FrameUpdate(args);
            _mainContainer.SetWidth = _clyde.ScreenSize.X;
            _mainContainer.Margin = new Thickness(-(_clyde.ScreenSize.X / 2), 100);
        }
    }
}
