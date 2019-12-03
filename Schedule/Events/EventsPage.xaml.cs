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

namespace Schedule.Events
{
    /// <summary>
    /// Interaction logic for EventsPage.xaml
    /// </summary>
    public partial class EventsPage : Page
    {
        public EventsPage()
        {
            InitializeComponent();

            var gridView = new GridView();
            EventList.View = gridView;
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Name",
                DisplayMemberBinding = new Binding("Name")
            });
            /*gridView.Columns.Add(new GridViewColumn
            {
                Header = "Contact Name",
                DisplayMemberBinding = new Binding("ContactName")
            });*/
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Date",
                DisplayMemberBinding = new Binding("EventDate")
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Location",
                DisplayMemberBinding = new Binding("Location")
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Details",
                DisplayMemberBinding = new Binding("Details")
            });

            DatePicker.SelectedDate = DateTime.Now;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Global.instance.entireFrame.GoBack();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Global.instance.entireFrame.Navigate(new AddEvent(-1, null, this));
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            int selIndex = EventList.SelectedIndex;
            DateTime selectedDate = DatePicker.SelectedDate ?? DateTime.Now;
            List<Event> events = Global.instance.getEventsFromDate(selectedDate);
            if (selIndex == -1) return;

            Event selectedItem = events[selIndex];
            Global.instance.entireFrame.Navigate(new AddEvent(selIndex, selectedItem, this));
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            int selIndex = EventList.SelectedIndex;
            if (selIndex == -1) return;

            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                DateTime selectedDate = DatePicker.SelectedDate ?? DateTime.Now;
                List<Event> events = Global.instance.getEventsFromDate(selectedDate);

                Event selectedItem = events[selIndex];
                Global.instance.Events.Remove(selectedItem);
                reload();
            }
        }

        private void Date_Select(object sender, SelectionChangedEventArgs e)
        {
            reload();
        }

        public void reload()
        {
            EventList.Items.Clear();

            DateTime selectedDate = DatePicker.SelectedDate ?? DateTime.Now;
            List<Event> events = Global.instance.getEventsFromDate(selectedDate);
            for (int i = 0; i < events.Count; i++)
                EventList.Items.Add(events[i]);

            Global.instance.saveData();
        }
    }
}
