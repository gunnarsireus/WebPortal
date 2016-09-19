using ServerLibrary.Model;
using WebPortal.Utils;

namespace WebPortal.UIModel
{
    public class UITimeType_List
    {
        public int    id     { get; set; }
        public string code   { get; set; }
        public string active { get; set; }
        public string name   { get; set; }
        public string unit   { get; set; }

        /* Commands */
        public string editcmdlink   { get; set; }
        public string deletecmdlink { get; set; }

        public UITimeType_List(TimeType model)
        {
            this.id            = model.id;
            this.code          = model.code;
            this.active        = Activatable.ActiveAsString(model.active);
            this.name          = model.name;
            this.unit          = model.unit;
            this.editcmdlink   = Images.EDIT();
            this.deletecmdlink = Images.DELETE();
        }
    }
}
