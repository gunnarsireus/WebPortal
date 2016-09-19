using System;

namespace WebPortal.Helpers
{
    public class Enums
    {
        public enum ButtonPosition
        {
            Left, Center, Right
        }

        public enum ButtonStyle
        {
            Default, Primary, Success, Info, Warning, Danger, Link
        }
        
        public enum ButtonSize
        {
            Large, Small, ExtraSmall, Normal
        }

        public enum LinkPosition
        {
            Left, Center, Right
        }

        public enum MessageFieldStyle
        {
            Success, Info, Warning, Danger
        }
        
        public enum PanelStyle
        {
            Default, Primary, Success, Info, Warning, Danger
        }

        public enum PanelPadding
        {
            Default, NoHorizontal, NoVertical, None
        }

        public enum SpanType
        {
            InputGroupAddon, Invisible
        }

        public enum InputModalStyle
        {
            Create, Update, Info, View
        }

        public enum InputModalSize
        {
            Default, Large, Full
        }
    }
}
