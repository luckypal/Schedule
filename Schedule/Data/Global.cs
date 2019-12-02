using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Schedule
{
    [Serializable]
    [XmlRootAttribute("Schedule", Namespace = "http://www.cpandl.com", IsNullable = false)]
    public class Global
    {
        [XmlIgnore]
        private string filePath = "schedule.xml";

        [XmlIgnore]
        static private Global _instance = null;
        static public Global instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Global();
                    _instance.restoreData();
                }
                return _instance;
            }
        }

        [XmlIgnore]
        public Frame entireFrame;

        [XmlArrayItem("Contacts")]
        private List<Contact> _contacts;

        public List<Contact> Contacts
        {
            get
            {
                if (_contacts != null) return _contacts;
                _contacts = new List<Contact>();
                return _contacts;
            }
            set
            {
                _contacts = value;
            }
        }

        public void saveData()
        {
            System.IO.FileStream file = System.IO.File.Create(filePath);
            XmlSerializer x = new XmlSerializer(GetType());
            x.Serialize(file, this);
            file.Close();
        }

        public void restoreData()
        {
            try
            {
                System.IO.StreamReader file = System.IO.File.OpenText(filePath);
                System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(GetType());
                Global temp = (Global)x.Deserialize(file);
                file.Close();

                Contacts = temp.Contacts;
            } catch (Exception _)
            {
                return;
            }
        }
    }
}
