using ServerLibrary.Model;

namespace WebPortal.UIModel
{
    public class UICustomer_CRU
    {
        public int    id           { get; set; }
        public int    type         { get; set; }
        public int    managementid { get; set; }
        public int    technicianid { get; set; }
        public string name         { get; set; }
        public string address      { get; set; }
        public string zip          { get; set; }
        public string city         { get; set; }
        public string orgnumber    { get; set; }
        public string contact      { get; set; }
        public string phone        { get; set; }
        public string email        { get; set; }
        public string vismaref     { get; set; }
        public int    active       { get; set; }
        public string note         { get; set; }

        public UICustomer_CRU()
        {
        }

        public UICustomer_CRU(Customer model)
        {
            this.id           = model.id;
            this.type         = model.type;
            this.managementid = model.managementid;
            this.technicianid = model.technicianid;
            this.name         = model.name;
            this.address      = model.address;
            this.zip          = model.zip;
            this.city         = model.city;
            this.orgnumber    = model.orgnumber;
            this.contact      = model.contact;
            this.phone        = model.phone;
            this.email        = model.email;
            this.vismaref     = model.vismaref;
            this.active       = model.active;
            this.note         = model.note;
        }

        public Customer CreateModel()
        {
            return UpdateModel(new Customer());
        }

        public Customer UpdateModel(Customer model)
        {
            if (model != null)
            {
                model.type         = this.type;
                model.managementid = this.managementid;
                model.technicianid = this.technicianid;
                model.name         = this.name;
                model.address      = this.address;
                model.city         = this.city;
                model.zip          = this.zip;
                model.orgnumber    = this.orgnumber;
                model.contact      = this.contact;
                model.phone        = this.phone;
                model.email        = this.email;
                model.vismaref     = this.vismaref;
                model.active       = this.active;
                model.note         = this.note;
            }
            return model;
        }
    }
}