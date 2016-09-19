using ServerLibrary.Model;

namespace WebPortal.UIModel
{
    public class UITimeType_RU
    {
        public int    id          { get; set; }
        public string code        { get; set; }
        public string name        { get; set; }
        public string unit        { get; set; }
        public string description { get; set; }
        public int    active      { get; set; }

        public UITimeType_RU()
        {
        }

        public UITimeType_RU(TimeType model)
        {
            this.id          = model.id;
            this.code        = model.code;
            this.name        = model.name;
            this.unit        = model.unit;
            this.description = model.description;
            this.active      = model.active;
        }

        public TimeType UpdateModel(TimeType model)
        {
            if (model != null)
            {
                // All fields readonly except "active" field (data imported from Visma)
                model.active = this.active;
            }
            return model;
        }
    }
}