using Robust.Client.UserInterface.CustomControls;
using Robust.Client.UserInterface.Controls;
using Robust.Client.UserInterface;

namespace Content.Client.UI
{
    class NotesWindow : SS14Window
    {
        private HistoryLineEdit _lineEdit;
        private RichTextLabel _notes;
        private string _notesText = "Notes is draggable and scrollable";
        public NotesWindow()
        {
            Title = "Notes";
            MinHeight = 210f;
            MinWidth = 160f;
            Contents.AddChild(new BoxContainer()
            {
                MinHeight = 200f,
                MaxHeight = 200f,
                MinWidth = 150f,
                MaxWidth = 150f,
                Orientation = BoxContainer.LayoutOrientation.Vertical,
                Children =
                {
                    (_lineEdit = new HistoryLineEdit()
                    {
                        
                    }),
                    new ScrollContainer() {
                        MinWidth = 150f,
                        MinHeight = 150f,
                        MaxHeight = 150f,
                        Children = {
                            (_notes = new RichTextLabel()
                            {
                                RectClipContent = true,
                                MaxWidth = 150f,
                            }),
                        }
                    }
                }
            });

            Setup();
        }

        private void Setup()
        {
            _notes.SetMessage(_notesText);
            _lineEdit.OnTextEntered += args =>
            {
                _notesText += "\n" + _lineEdit.Text;
                _notes.SetMessage(_notesText);
                _lineEdit.Text = "";
            };

            OnClose += () =>
            {
                OpenCentered();
            };
        }
    }
}
