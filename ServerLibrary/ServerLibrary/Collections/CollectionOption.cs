using System;

namespace ServerLibrary.Collections
{
    public class CollectionOption
    {
        public const int SKIP_ANY = 0;
        public const int WITH_ANY = 1;

        public string text     { get; set; }
        public string value    { get; set; }
        public bool   selected { get; set; }

        public CollectionOption()
        {
            this.text     = "";
            this.value    = "";
            this.selected = false;
        }

        public CollectionOption(string text, string value, bool selected = false)
        {
            this.text     = text;
            this.value    = value;
            this.selected = selected;
        }
    }
}
