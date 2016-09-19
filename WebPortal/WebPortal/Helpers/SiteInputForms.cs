using System;
using System.IO;
using System.Web.Mvc;

namespace WebPortal.Helpers
{
    public static class SiteInputForms
    {
        public const int NONE   = 0x00;
        public const int OPEN   = 0x01;
        public const int CLOSE  = 0x02;
        public const int CREATE = 0x04;
        public const int APPLY  = 0x08;
        public const int SEARCH = 0x10;
        public const int RELOAD = 0x20;

        public const string STRING_OPEN   = "Öppna";
        public const string STRING_CLOSE  = "Stäng";
        public const string STRING_CREATE = "Skapa";
        public const string STRING_APPLY  = "Spara";
        public const string STRING_SEARCH = "Sök";
        public const string STRING_RELOAD = "Uppdatera";

        public static Form SiteInputForm(this HtmlHelper html, string id, int leftbuttons, int rightbuttons)
        {
            return new Form(html, id, leftbuttons, rightbuttons);
        }

        public class Form : IDisposable
        {
            private readonly TextWriter _writer;
            private readonly string     _id;
            private readonly int        _leftbuttons;
            private readonly int        _rightbuttons;

            public Form(HtmlHelper helper, string id, int leftbuttons, int rightbuttons)
            {
                _writer       = helper.ViewContext.Writer;
                _id           = id;
                _leftbuttons  = leftbuttons;
                _rightbuttons = rightbuttons;

                var form = new TagBuilder("form");
                form.Attributes.Add("id", id);
                _writer.WriteLine(form.ToString(TagRenderMode.StartTag));
            }

            public void Dispose()
            {
                if (_leftbuttons > 0 || _rightbuttons > 0)
                {
                    var rowdiv = new TagBuilder("div");
                    rowdiv.AddCssClass("row");
                    _writer.WriteLine(rowdiv.ToString(TagRenderMode.StartTag));

                    var leftcoldiv = new TagBuilder("div");
                    leftcoldiv.AddCssClass("col-md-6");
                    _writer.WriteLine(leftcoldiv.ToString(TagRenderMode.StartTag));
                    if ((_leftbuttons & OPEN) != 0)
                    {
                        _writer.WriteLine(CreateAction(OPEN, STRING_OPEN));
                    }
                    if ((_leftbuttons & CLOSE) != 0)
                    {
                        _writer.WriteLine(CreateAction(CLOSE, STRING_CLOSE));
                    }
                    if ((_leftbuttons & CREATE) != 0)
                    {
                        _writer.WriteLine(CreateAction(CREATE, STRING_CREATE));
                    }
                    if ((_leftbuttons & APPLY) != 0)
                    {
                        _writer.WriteLine(CreateAction(APPLY, STRING_APPLY));
                    }
                    if ((_leftbuttons & SEARCH) != 0)
                    {
                        _writer.WriteLine(CreateAction(SEARCH, STRING_SEARCH));
                    }
                    if ((_leftbuttons & RELOAD) != 0)
                    {
                        _writer.WriteLine(CreateAction(RELOAD, STRING_RELOAD));
                    }
                    _writer.WriteLine(leftcoldiv.ToString(TagRenderMode.EndTag));

                    var rightcoldiv = new TagBuilder("div");
                    rightcoldiv.AddCssClass("col-md-6");
                    _writer.WriteLine(rightcoldiv.ToString(TagRenderMode.StartTag));

                    if ((_rightbuttons & RELOAD) != 0)
                    {
                        _writer.WriteLine(CreateAction(RELOAD, STRING_RELOAD, true));
                    }
                    if ((_rightbuttons & SEARCH) != 0)
                    {
                        _writer.WriteLine(CreateAction(SEARCH, STRING_SEARCH, true));
                    }
                    if ((_rightbuttons & APPLY) != 0)
                    {
                        _writer.WriteLine(CreateAction(APPLY, STRING_APPLY, true));
                    }
                    if ((_rightbuttons & CREATE) != 0)
                    {
                        _writer.WriteLine(CreateAction(CREATE, STRING_CREATE, true));
                    }
                    if ((_rightbuttons & CLOSE) != 0)
                    {
                        _writer.WriteLine(CreateAction(CLOSE, STRING_CLOSE, true));
                    }
                    if ((_rightbuttons & OPEN) != 0)
                    {
                        _writer.WriteLine(CreateAction(OPEN, STRING_OPEN, true));
                    }
                    _writer.WriteLine(rightcoldiv.ToString(TagRenderMode.EndTag));
                    _writer.WriteLine(rowdiv.ToString(TagRenderMode.EndTag));
                }
                _writer.Write("</form>");
            }

            private string CreateAction(int action, string label, bool pullright = false)
            {
                var button = new TagBuilder("button");
                button.Attributes.Add("class", _id + "-buttons");
                button.Attributes.Add("type", "button");
                button.Attributes.Add("data-action", action.ToString());
                button.InnerHtml = label;
                button.AddCssClass("btn");

                if (action == CREATE)
                {
                    button.AddCssClass("btn-success");
                }
                else if (action == APPLY)
                {
                    button.AddCssClass("btn-info");
                }
                else
                {
                    button.AddCssClass("btn-default");
                }

                if (pullright)
                {
                    button.AddCssClass("pull-right");
                    button.AddCssClass("site-buttonmargin-left");
                }
                else
                {
                    button.AddCssClass("site-buttonmargin-right");
                }
                return button.ToString();
            }
        }
    }
}