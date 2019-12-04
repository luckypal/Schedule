using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace Schedule.Events
{
    /// <summary>
    /// Interaction logic for AddEvent.xaml
    /// </summary>
    public partial class AddEvent : Page
    {
        int index;
        Event curEvent = null;
        EventsPage parentPage;

        public AddEvent(int _index, Event item, EventsPage _parentPage)
        {
            InitializeComponent();

            index = _index;
            parentPage = _parentPage;

            ContactSelect.SelectedValuePath = "Key";
            ContactSelect.DisplayMemberPath = "Value";
            for (int i = 0; i < Global.instance.Contacts.Count; i ++)
            {
                Contact contact = Global.instance.Contacts[i];
                ContactSelect.Items.Add(new KeyValuePair<int, string>(contact.Id, contact.Name));
            }

            if (item != null)
            {
                curEvent = item;
                EventName.Text = item.Name;
                ContactSelect.SelectedValue = item.ContactId;
                eventDate.SelectedDate = item.EventDate.Date;
                Hour.Text = item.EventDate.Hour.ToString();
                Minute.Text = item.EventDate.Minute.ToString();
                Second.Text = item.EventDate.Second.ToString();
            }
            else curEvent = new Event();

            eventDate.SelectedDate = DateTime.Now;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Global.instance.entireFrame.GoBack();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            var selectedContactId = ContactSelect.SelectedValue;

            if (selectedContactId == null
                || EventName.Text == "")
            {
                return;
            }

            curEvent.Name = EventName.Text;
            curEvent.ContactId = (int)selectedContactId;
            DateTime selectedDate = eventDate.SelectedDate ?? DateTime.Now;

            TimeSpan ts = new TimeSpan(int.Parse(Hour.Text), int.Parse(Minute.Text), int.Parse(Second.Text));
            curEvent.EventDate = selectedDate.Date + ts;

            if (index == -1)
            {
                Global.instance.Events.Add(curEvent);
            }
            else {
                int eventIndex = Global.instance.getEventIndexFromId(curEvent.Id);
                if (eventIndex == -1)
                    Global.instance.Events.Add(curEvent);
                else
                    Global.instance.Events[eventIndex] = curEvent;
            }

            Global.instance.entireFrame.GoBack();
            parentPage.reload();
        }
    }
}
