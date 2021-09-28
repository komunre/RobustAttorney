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

namespace Content.Client.UI
{
    class AttorneyBox : Control
    {
        [Dependency] private readonly IComponentManager _componentManager = default!;
        private Label _content;
        public AttorneyBox()
        {
            AddChild(new PanelContainer() {
                MinWidth = 300f,
                Children = {
                    new BoxContainer()
                    {
                        Children =
                        {
                            (_content = new Label()
                            {
                                MinWidth = 299f,
                            }),
                        }
                    }
                }
            });
        }

        public void Update()
        {
            foreach (var attorney in _componentManager.EntityQuery<AttorneyComponent>())
            {
                if (attorney.Changed)
                {
                    _content.Text = attorney.Phrase;
                }
            }
        }
    }
}
