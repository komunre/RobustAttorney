using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Content.Shared.Player;
using Robust.Client.UserInterface;
using Robust.Client.UserInterface.Controls;
using Robust.Shared.Maths;
using Robust.Shared.Log;
using Robust.Shared.IoC;
using Robust.Shared.GameObjects;
using Content.Client.Player;

namespace Content.Client.UI
{
    class PhraseBox : Control
    {
        [Dependency] private readonly IEntitySystemManager _systemManager = default!;
        private HistoryLineEdit _phrase;
        private Button _send;
        private Button _objection;
        private AttorneyComponent _attorney = null;
        public PhraseBox()
        {
            IoCManager.InjectDependencies(this);
            AddChild(new PanelContainer()
            {
                MinWidth = 300f,
                MinHeight = 300f,
                Children =
                {
                    new BoxContainer()
                    {
                        Orientation = BoxContainer.LayoutOrientation.Vertical,
                        Margin = new Thickness(0, -30),
                        Children =
                        {
                            (_objection = new Button()
                            {
                                MaxHeight = 30f,
                                MaxWidth = 100f,
                                TextAlign = Label.AlignMode.Center,
                                Text = "OBJECTION!",
                            }),
                        }
                    },
                    new BoxContainer()
                    {
                        Orientation = BoxContainer.LayoutOrientation.Horizontal,
                        Margin = new Thickness(0, 100),
                        Children =
                        {
                            (_phrase = new HistoryLineEdit()
                            {
                                MaxHeight = 30f,
                                MinWidth = 200f,
                                MaxWidth = 300f,
                                HorizontalExpand = true,
                            }),
                        }
                    }
                }
            });


            Setup();
        }

        public void Setup()
        {
            _objection.OnButtonDown += args =>
            {
                if (_attorney == null)
                    return;
                _systemManager.GetEntitySystem<AttorneySystem>().RequestFocus(_attorney.Owner.Uid);
                Logger.Log(LogLevel.Debug, "This client is now active");
            };

            _objection.OnButtonDown += args =>
            {
                _systemManager.GetEntitySystem<AttorneySystem>().Objection(_attorney.Owner.Uid);
            };

            _phrase.OnTextEntered += args =>
            {
                _phrase.Text = "";
                _systemManager.GetEntitySystem<AttorneySystem>().EndPhrase();
            };

            _phrase.OnTextChanged += args =>
            {
                if (_attorney == null)
                {
                    Logger.Error("No attorney attached. Can not set changed and phrase");
                    return;
                }
                var attorneySystem = _systemManager.GetEntitySystem<AttorneySystem>();
                attorneySystem.ChangePhrase(_attorney.Owner.Uid, _phrase.Text);
                attorneySystem.RequestFocus(_attorney.Owner.Uid);
            };
        }

        public void AttachPlayer(AttorneyComponent attorney)
        {
            _attorney = attorney;
        }
    }
}
