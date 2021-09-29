using Robust.Client.Graphics;
using Robust.Client.UserInterface;
using Robust.Client.ResourceManagement;
using Robust.Client.UserInterface.Controls;
using Robust.Client.UserInterface.CustomControls;
using Robust.Shared.Maths;
using Robust.Shared.IoC;
using static Robust.Client.UserInterface.StylesheetHelpers;

namespace Content.Client.UI
{
    class StyleLaw
    {
        public static string ImportantButton = "important-button";
        public static string LawButton = "button";
        public static string LawLineEdit = "line-edit";
        public static string LawLabel = "label";
        public static int fontSize = 12;
        public StyleLaw()
        {
            var resCache = IoCManager.Resolve<IResourceCache>();

            var notoSansRes = resCache.GetResource<FontResource>("/Fonts/NotoSans/NotoSans-Regular.ttf");
            var notoSans = new VectorFont(notoSansRes, 12);

            var importantButton = new StyleBoxFlat()
            {
                BackgroundColor = Color.FromHex("#e32b2b")
            };

            var button = new StyleBoxFlat()
            {
                BackgroundColor = Color.FromHex("#2ba6e3")
            };

            var panelColor = Color.FromHex("#416578");
            panelColor = panelColor.WithAlpha(0.67f);
            var panel = new StyleBoxFlat()
            {
                BackgroundColor = panelColor
            };

            var lineEdit = new StyleBoxFlat()
            {
                BorderColor = Color.Blue,
                BackgroundColor = Color.FromHex("#505c53"),
            };

            var styleRules = new StyleRule[]
            {
                Element().Prop("font", notoSans).Prop(PanelContainer.StylePropertyPanel, panel),
                Element<Button>().Prop(Button.StylePropertyStyleBox, button),
                Element<Label>().Prop(Label.StylePropertyFont, notoSans),
                Element<HistoryLineEdit>().Prop(HistoryLineEdit.StylePropertyCursorColor, Color.Black).Prop(HistoryLineEdit.StylePropertyStyleBox, lineEdit),
                Element<ScrollContainer>().Prop(ScrollContainer.StylePropertyModulateSelf, Color.FromHex("#c9edff")),
                new StyleRule(new SelectorElement(typeof(Button), new[] { ImportantButton }, null, null),
                new []
                {
                    new StyleProperty(Button.StylePropertyStyleBox, importantButton)
                }),
            };

            IoCManager.Resolve<IUserInterfaceManager>().Stylesheet = new Stylesheet(styleRules);
        }
    }
}
