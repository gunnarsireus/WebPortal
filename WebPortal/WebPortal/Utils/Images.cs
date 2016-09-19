using System;
using System.IO;
using System.Drawing;

namespace WebPortal.Utils
{
    public class Images
    {
        // Emtpy string
        public static string EMPTY = "";
        // Icons
        public static string RAW_ADDRESS  = "/img/address.png";
        public static string RAW_CAMERA   = "/img/camera.png";
        public static string RAW_CLONE    = "/img/clone.png";
        public static string RAW_COMMENT  = "/img/comment.png";
        public static string RAW_CREATE   = "/img/create.png";
        public static string RAW_CUSTOMER = "/img/customer.png";
        public static string RAW_DELETE   = "/img/delete.png";
        public static string RAW_EDIT     = "/img/edit.png";
        public static string RAW_ERROR    = "/img/error.png";
        public static string RAW_GOTO     = "/img/goto.png";
        public static string RAW_HELP     = "/img/help.png";
        public static string RAW_INFO     = "/img/info.png";
        public static string RAW_PRICE    = "/img/price.png";
        public static string RAW_PRINT    = "/img/print.png";
        public static string RAW_PRIVATE  = "/img/private.png";
        public static string RAW_REJECT   = "/img/reject.png";
        public static string RAW_RETURN   = "/img/return.png";
        public static string RAW_SECURE   = "/img/secure.png";
        public static string RAW_SPEAK    = "/img/speak.png";
        public static string RAW_VIEW     = "/img/view.png";
        public static string ADDRESS () { return GetImage(RAW_ADDRESS , null, null); }
        public static string CAMERA  () { return GetImage(RAW_CAMERA  , null, null); }
        public static string CLONE   () { return GetImage(RAW_CLONE   , null, null); }
        public static string COMMENT () { return GetImage(RAW_COMMENT , null, null); }
        public static string CREATE  () { return GetImage(RAW_CREATE  , null, null); }
        public static string CUSTOMER() { return GetImage(RAW_CUSTOMER, null, null); }
        public static string DELETE  () { return GetImage(RAW_DELETE  , null, null); }
        public static string EDIT    () { return GetImage(RAW_EDIT    , null, null); }
        public static string ERROR   () { return GetImage(RAW_ERROR   , null, null); }
        public static string GOTO    () { return GetImage(RAW_GOTO    , null, null); }
        public static string HELP    () { return GetImage(RAW_HELP    , null, null); }
        public static string INFO    () { return GetImage(RAW_INFO    , null, null); }
        public static string PRICE   () { return GetImage(RAW_PRICE   , null, null); }
        public static string PRINT   () { return GetImage(RAW_PRINT   , null, null); }
        public static string PRIVATE () { return GetImage(RAW_PRIVATE , null, null); }
        public static string REJECT  () { return GetImage(RAW_REJECT  , null, null); }
        public static string RETURN  () { return GetImage(RAW_RETURN  , null, null); }
        public static string SECURE  () { return GetImage(RAW_SECURE  , null, null); }
        public static string SPEAK   () { return GetImage(RAW_SPEAK   , null, null); }
        public static string VIEW    () { return GetImage(RAW_VIEW    , null, null); }
        public static string ADDRESS (string id, string alt = null) { return GetImage(RAW_ADDRESS , id, alt); }
        public static string CAMERA  (string id, string alt = null) { return GetImage(RAW_CAMERA  , id, alt); }
        public static string CLONE   (string id, string alt = null) { return GetImage(RAW_CLONE   , id, alt); }
        public static string COMMENT (string id, string alt = null) { return GetImage(RAW_COMMENT , id, alt); }
        public static string CREATE  (string id, string alt = null) { return GetImage(RAW_CREATE  , id, alt); }
        public static string CUSTOMER(string id, string alt = null) { return GetImage(RAW_CUSTOMER, id, alt); }
        public static string DELETE  (string id, string alt = null) { return GetImage(RAW_DELETE  , id, alt); }
        public static string EDIT    (string id, string alt = null) { return GetImage(RAW_EDIT    , id, alt); }
        public static string ERROR   (string id, string alt = null) { return GetImage(RAW_ERROR   , id, alt); }
        public static string GOTO    (string id, string alt = null) { return GetImage(RAW_GOTO    , id, alt); }
        public static string HELP    (string id, string alt = null) { return GetImage(RAW_HELP    , id, alt); }
        public static string INFO    (string id, string alt = null) { return GetImage(RAW_INFO    , id, alt); }
        public static string PRICE   (string id, string alt = null) { return GetImage(RAW_PRICE   , id, alt); }
        public static string PRINT   (string id, string alt = null) { return GetImage(RAW_PRINT   , id, alt); }
        public static string PRIVATE (string id, string alt = null) { return GetImage(RAW_PRIVATE , id, alt); }
        public static string REJECT  (string id, string alt = null) { return GetImage(RAW_REJECT  , id, alt); }
        public static string RETURN  (string id, string alt = null) { return GetImage(RAW_RETURN  , id, alt); }
        public static string SECURE  (string id, string alt = null) { return GetImage(RAW_SECURE  , id, alt); }
        public static string SPEAK   (string id, string alt = null) { return GetImage(RAW_SPEAK   , id, alt); }
        public static string VIEW    (string id, string alt = null) { return GetImage(RAW_VIEW    , id, alt); }
        // News icons
        public static string NewsCategoryAsImagePath(long category)                             { return "/img/newscategory" + category + ".png";                 }
        public static string NewsCategoryAsImage    (long category)                             { return GetImage(NewsCategoryAsImagePath(category), null, null); }
        public static string NewsCategoryAsImage    (long category, string id, string alt=null) { return GetImage(NewsCategoryAsImagePath(category), id,   alt);  }
        // Offer icons
        public static string OfferCategoryAsImagePath(long category)                             { return "/img/offercategory" + category + ".png";                 }
        public static string OfferCategoryAsImage    (long category)                             { return GetImage(OfferCategoryAsImagePath(category), null, null); }
        public static string OfferCategoryAsImage    (long category, string id, string alt=null) { return GetImage(OfferCategoryAsImagePath(category), id,   alt);  }
        // Status icons
        public static string StatusAsImagePath    (long status)                             { return "/img/status" + status + ".png";                     }
        public static string StatusAsNextImagePath(long status)                             { return "/img/status" + status + "asnext.png";               }
        public static string StatusAsImage        (long status)                             { return GetImage(StatusAsImagePath    (status), null, null); }
        public static string StatusAsNextImage    (long status)                             { return GetImage(StatusAsNextImagePath(status), null, null); }
        public static string StatusAsImage        (long status, string id, string alt=null) { return GetImage(StatusAsImagePath    (status), id,   alt);  }
        public static string StatusAsNextImage    (long status, string id, string alt=null) { return GetImage(StatusAsNextImagePath(status), id,   alt);  }

        private static string GetImage(string png, string id, string alt)
        {
            id  = (id  == null) ? "" : " id=\""  + id  + "\"";
            alt = (alt == null) ? "" : " alt=\"" + alt + "\"";
            return "<img src=\"" + png + "\"" + id + alt + " class=\"site-clickable\"/>";
        }
    }
}