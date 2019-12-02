using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Schedule
{
    [Serializable]
    public class Contact
    {
        public int _id = 0;

        public Contact()
        {
            int _ = Id;
        }

        [XmlIgnore]
        public int Id
        {
            get
            {
                if (_id != 0)
                    return _id;
                if (Global.instance.Contacts.Count == 0)
                    _id = 1;
                else _id = Global.instance.Contacts.Last().Id + 1;

                return _id;
            }
            set
            {
                _id = value;
            }
        }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Note { get; set; }
    }
}
