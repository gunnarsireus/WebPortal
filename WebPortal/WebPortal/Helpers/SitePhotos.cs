/* See JS component site-xcomp-photos.js */

using System;
using System.Web;
using System.Web.Mvc;

namespace WebPortal.Helpers
{
    public static class SitePhotos
    {
        public const int WRITEABLE = 1;
        public const int READONLY  = 2;

        public static IHtmlString SitePhoto(this HtmlHelper helper, string deleteImage, string deleteId, int mode = WRITEABLE)
        {
            TagBuilder div = new TagBuilder("div");
            div.Attributes.Add("style", "text-align:center");

            TagBuilder figure = new TagBuilder("figure");
            figure.Attributes.Add("id", "figure");
            figure.Attributes.Add("hidden", "hidden");

            TagBuilder photo = new TagBuilder("img");
            photo.Attributes.Add("id", "photo");
            photo.Attributes.Add("style", "padding:1px;border:thin solid gray;");
            photo.Attributes.Add("data-photoid", "");
            photo.Attributes.Add("src", "");
            photo.Attributes.Add("alt", "");

            TagBuilder command = new TagBuilder("img");
            command.Attributes.Add("id", deleteId);
            command.Attributes.Add("style", "cursor:pointer; top:5px; margin-left:-25px; position:absolute;");
            command.Attributes.Add("src", deleteImage);

            TagBuilder caption = new TagBuilder("figcaption");
            caption.Attributes.Add("id", "caption");
            caption.Attributes.Add("style", "margin-top: 10px;");

            return new MvcHtmlString(String.Format("{0}{1}{2}{3}{4}{5}{6}{7}",
                div.ToString(TagRenderMode.StartTag),
                figure.ToString(TagRenderMode.StartTag),
                photo.ToString(TagRenderMode.SelfClosing),
                (mode == WRITEABLE) ? command.ToString(TagRenderMode.SelfClosing) : "",
                caption.ToString(TagRenderMode.StartTag),
                caption.ToString(TagRenderMode.EndTag),
                figure.ToString(TagRenderMode.EndTag),
                div.ToString(TagRenderMode.EndTag)
                ));
        }

        public static IHtmlString SiteThumbnail(this HtmlHelper helper, string cameraImage, string openId)
        {
            TagBuilder div = new TagBuilder("div");
            div.AddCssClass("thumbnail");

            TagBuilder img = new TagBuilder("img");
            img.AddCssClass("photo-thumbnail");
            img.Attributes.Add("src", cameraImage);
            img.Attributes.Add("id", openId);
            img.Attributes.Add("alt", "");
            img.Attributes.Add("data-photoid", "");
            img.Attributes.Add("style", "cursor:pointer;");

            return new MvcHtmlString(String.Format("{0}{1}{2}", div.ToString(TagRenderMode.StartTag), img.ToString(), div.ToString(TagRenderMode.EndTag)));
        }
    }
}