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

namespace Content.Client.UI
{
    class AttorneyBox : Control
    {
        [Dependency] private readonly IEntityManager _entityManager = default!;
        private Label _content;
        private Label _name;
        public AttorneyBox()
        {
            IoCManager.InjectDependencies(this);
            AddChild(new PanelContainer() {
                MinWidth = 300f,
                MinHeight = 200f,
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
                                FontColorOverride = new Color(0, 0, 0),
                            }),
                            (_content = new Label()
                            {
                                Align = Label.AlignMode.Left,
                                VerticalExpand = true,
                                MinHeight = 200f,
                                MinWidth = 300f,
                                FontColorOverride = new Color(0, 0, 0),
                            }),
                        }
                    }
                }
            });
        }

        public void Update()
        {
            foreach (var attorney in _entityManager.EntityQuery<AttorneyComponent>())
            {
                if (attorney.Changed)
                {
                    _content.Text = attorney.Phrase;
                }
            }
        }
    }
}
