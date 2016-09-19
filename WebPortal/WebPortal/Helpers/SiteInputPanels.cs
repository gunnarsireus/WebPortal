using System;
using System.IO;
using System.Web.Mvc;

namespace WebPortal.Helpers
{
    public static class SiteInputPanels
    {
        public const int NONE   = 0x0000;
        public const int OPEN   = 0x0001;
        public const int CLOSE  = 0x0002;
        public const int CREATE = 0x0004;
        public const int APPLY  = 0x0008;
        public const int SEARCH = 0x0010;
        public const int CLONE  = 0x0020;
        public const int DELETE = 0x0040;
        public const int FETCH  = 0x0080;
        public const int NOW    = 0x0100;

        public const string STRING_OPEN   = "Öppna";
        public const string STRING_CLOSE  = "Stäng";
        public const string STRING_CREATE = "Skapa";
        public const string STRING_APPLY  = "Spara";
        public const string STRING_SEARCH = "Sök";
        public const string STRING_CLONE  = "Kopiera";
        public const string STRING_DELETE = "Ta bort";
        public const string STRING_FETCH  = "Hämta";
        public const string STRING_NOW    = "Nu!";

        public static Panel SiteInputPanel(this HtmlHelper html, string id, string title, int leftbuttons, int rightbuttons, Enums.PanelStyle style = Enums.PanelStyle.Default)
        {
            return new Panel(html, id, title, leftbuttons, rightbuttons, style);
        }

        public class Panel : IDisposable
        {
            private readonly TextWriter _writer;
            private readonly string     _id;
            private readonly int        _leftbuttons;
            private readonly int        _rightbuttons;

            public Panel(HtmlHelper helper, string id, string title, int leftbuttons, int rightbuttons, Enums.PanelStyle style)
            {
                _writer       = helper.ViewContext.Writer;
                _id           = id;
                _leftbuttons  = leftbuttons;
                _rightbuttons = rightbuttons;

                var paneldiv = new TagBuilder("div");
                paneldiv.AddCssClass("panel-" + style.ToString().ToLower());
                paneldiv.AddCssClass("panel");
                _writer.WriteLine(paneldiv.ToString(TagRenderMode.StartTag));

                var panelheadingdiv = new TagBuilder("div");
                panelheadingdiv.AddCssClass("panel-heading");
                _writer.WriteLine(panelheadingdiv.ToString(TagRenderMode.StartTag));

                TagBuilder paneltitle = new TagBuilder("h3");
                paneltitle.AddCssClass("panel-title");
                paneltitle.Attributes.Add("id", "title");
                paneltitle.SetInnerText(title);
                _writer.WriteLine(paneltitle.ToString());
                
                _writer.WriteLine(panelheadingdiv.ToString(TagRenderMode.EndTag));

                var panelbodydiv = new TagBuilder("div");
                panelbodydiv.AddCssClass("panel-body");
                _writer.WriteLine(panelbodydiv.ToString(TagRenderMode.StartTag));

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
                    if ((_leftbuttons & CLONE) != 0)
                    {
                        _writer.WriteLine(CreateAction(CLONE, STRING_CLONE));
                    }
                    if ((_leftbuttons & DELETE) != 0)
                    {
                        _writer.WriteLine(CreateAction(DELETE, STRING_DELETE));
                    }
                    if ((_leftbuttons & FETCH) != 0)
                    {
                        _writer.WriteLine(CreateAction(FETCH, STRING_FETCH));
                    }
                    if ((_leftbuttons & NOW) != 0)
                    {
                        _writer.WriteLine(CreateAction(NOW, STRING_NOW));
                    }
                    _writer.WriteLine(leftcoldiv.ToString(TagRenderMode.EndTag));

                    var rightcoldiv = new TagBuilder("div");
                    rightcoldiv.AddCssClass("col-md-6");
                    _writer.WriteLine(rightcoldiv.ToString(TagRenderMode.StartTag));

                    if ((_rightbuttons & NOW) != 0)
                    {
                        _writer.WriteLine(CreateAction(NOW, STRING_NOW, true));
                    }
                    if ((_rightbuttons & FETCH) != 0)
                    {
                        _writer.WriteLine(CreateAction(FETCH, STRING_FETCH, true));
                    }
                    if ((_rightbuttons & DELETE) != 0)
                    {
                        _writer.WriteLine(CreateAction(DELETE, STRING_DELETE, true));
                    }
                    if ((_rightbuttons & CLONE) != 0)
                    {
                        _writer.WriteLine(CreateAction(CLONE, STRING_CLONE, true));
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
                _writer.Write("</div></div>");
            }

            private string CreateAction(int action, string label, bool pullright = false)
            {
                var button = new TagBuilder("button");
                button.Attributes.Add("class", _id + "-buttons");
                button.Attributes.Add("type", "button");
                button.Attributes.Add("data-action", action.ToString());
                button.InnerHtml = label;
                button.AddCssClass("btn");

                if (action == DELETE)
                {
                    button.AddCssClass("btn-danger");
                }
                else if (action == CREATE || action == CLONE)
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