namespace ServerLibrary.Utils
{
    public static class StringUtils
    {
        // Returns "a b c" if s="  a b c  "
        public static string Trim(string s)
        {
            if (s != null)
            {
                return s.Trim();
            }
            return "";
        }

        // Returns "abc" if s="  a b c  "
        public static string TrimInside(string s)
        {
            if (s != null)
            {
                return s.Replace(" ", string.Empty);
            }
            return "";
        }

        // Returns "Abc..."  if s="Abcdef" and maxlength=3
        public static string Shorten(string s, int maxlength)
        {
            if (s != null)
            {
                return (s.Length <= maxlength) ? s : s.Substring(0, maxlength) + " ...";
            }
            return "";
        }

        // Returns "Abc" if s="Abcdef" and maxlength=3
        public static string Cutoff(string s, int maxlength)
        {
            if (s != null)
            {
                return (s.Length <= maxlength) ? s : s.Substring(0, maxlength);
            }
            return "";
        }

        // Returns "apa" from text=Skapande!" if delimleft="Sk" and delimright="n"
        public static string ExtractFrom(string text, string delimleft, string delimright)
        {
            int start = text.IndexOf(delimleft);
            if (start < 0)
            {
                return null;
            }
            start += delimleft.Length;
            int end = text.IndexOf(delimright, start);
            if (end < 0)
            {
                return null;
            }
            return text.Substring(start, (end - start));
        }
    }
}
