using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Schedule
{
    [Serializable]
    public class Event
    {
        public int _id = 0;

        public Event()
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
                if (Global.instance.Events.Count == 0)
                    _id = 1;
                else _id = Global.instance.Events.Last().Id + 1;

                return _id;
            }
            set
            {
                _id = value;
            }
        }
        public string Name { get; set; }
        public int ContactId { get; set; }

        public string ContactName
        {
            get
            {
                for (int i = 0; i < Global.instance.Contacts.Count; i ++)
                {
                    if (Global.instance.Contacts[i].Id == ContactId)
                        return Global.instance.Contacts[i].Name;
                }
                return "";
            }
        }
        public DateTime EventDate { get; set; }

        public Boolean Recurring;

        public class ContentItem
        {
            public string Title;
            public string Content;
        }
        public List<ContentItem> _content = null;

        [XmlIgnore]
        public List<ContentItem> Content
        {
            get
            {
                if (_content == null) _content = new List<ContentItem>();
                return _content;
            }
            set
            {
                _content = value;
            }
        }
    }
}
