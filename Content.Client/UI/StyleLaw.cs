using Robust.Client.Graphics;
using Robust.Client.UserInterface;
using Robust.Client.ResourceManagement;
using Robust.Client.UserInterface.Controls;
using Robust.Client.UserInterface.CustomControls;
using Robust.Shared.Maths;
using Robust.Shared.IoC;
using Robust.Client.ResourceManagement;

namespace Content.Client.UI
{
    class StyleLaw
    {
        public static string ImportantButton = "important-button";
        public static string LawButton = "button";
        public static string LawLineEdit = "line-edit";
        public StyleLaw()
        {
            var resCache = IoCManager.Resolve<IResourceCache>();

            var notoSansRes = resCache.GetResource<FontResource>("/Fonts/NotoSans/NotoSans-Regular.ttf");

            var importantButton = new StyleBoxFlat()
            {
                BackgroundColor = Color.FromHex("#e32b2b")
            };

            var button = new StyleBoxFlat()
            {
                BackgroundColor = Color.FromHex("#2ba6e3")
            };

            var panel = new StyleBoxFlat()
            {
                BackgroundColor = Color.FromHex("#c9edff")
            };

            var styleRules = new[]
            {
                new StyleRule(new SelectorElement(null, null, null, null),
                new []
                {
                    new StyleProperty("font", new VectorFont(notoSansRes, 16)),
                    new StyleProperty(PanelContainer.StylePropertyPanel, panel),
                }),
                new StyleRule(new SelectorElement(typeof(Button), new[] { LawButton }, null, null),
                new []
                {
                     new StyleProperty(Button.StylePropertyStyleBox, button)
                }),
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
