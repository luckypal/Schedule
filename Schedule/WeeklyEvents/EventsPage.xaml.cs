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

namespace Schedule.WeeklyEvents
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
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Contact Name",
                DisplayMemberBinding = new Binding("ContactName")
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Date",
                DisplayMemberBinding = new Binding("EventDate")
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Day of Week",
                DisplayMemberBinding = new Binding("DayOfWeek")
            });

            DatePicker.SelectedDate = DateTime.Now;
        }

        private void Date_Select(object sender, SelectionChangedEventArgs e)
        {
            reload();
        }

        public void reload()
        {
            EventList.Items.Clear();

            DateTime selectedDate = DatePicker.SelectedDate ?? DateTime.Now;

            int dayOfWeek = (int)selectedDate.DayOfWeek;
            DateTime startDate = selectedDate - new TimeSpan(dayOfWeek, 0, 0, 0);
            DateTime endDate = startDate + new TimeSpan(7, 0, 0, 0);

            List<Event> events = Global.instance.Events;
            for (int i = 0; i < events.Count; i++)
            {
                if ((startDate <= events[i].EventDate && events[i].EventDate < endDate)
                    || events[i].Recurring)
                {
                    string eventDayOfWeek = events[i].EventDate.DayOfWeek.ToString();
                    if (events[i].Recurring)
                    {
                        events[i].DayOfWeek = string.Format("Every Week, {0}", eventDayOfWeek);
                    }
                    else
                    {
                        events[i].DayOfWeek = string.Format("Current Week, {0}", eventDayOfWeek);
                    }

                    EventList.Items.Add(events[i]);
                }
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Global.instance.entireFrame.GoBack();
        }
    }
}
