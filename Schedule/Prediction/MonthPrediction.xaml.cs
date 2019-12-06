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

namespace Schedule.Prediction
{
    /// <summary>
    /// Interaction logic for MonthPrediction.xaml
    /// </summary>
    public partial class MonthPrediction : Page
    {
        public MonthPrediction()
        {
            InitializeComponent();

            var gridView = new GridView();
            EventList.View = gridView;
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Contact Name",
                DisplayMemberBinding = new Binding("ContactName")
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Day of Week",
                DisplayMemberBinding = new Binding("WeekNumber")
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Time",
                DisplayMemberBinding = new Binding("Time")
            });

            loadData();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Global.instance.entireFrame.GoBack();
        }

        List<PredictionEvent> predictionEvents = new List<PredictionEvent>();
        public void loadData()
        {
            List<Event> events = Global.instance.Events;
            DateTime currentDate = DateTime.Now;

            int dayOfWeek = (int)currentDate.DayOfWeek;
            DateTime curWeekStartDate = currentDate - new TimeSpan(dayOfWeek, 0, 0, 0);
            DateTime predictionStartDate = curWeekStartDate - new TimeSpan(7 * 4, 0, 0, 0);

            for (int i = 0; i < events.Count; i++)
            {
                if ((predictionStartDate <= events[i].EventDate && events[i].EventDate < curWeekStartDate)
                    || events[i].Recurring)
                {
                    Event curEvent = events[i];
                    PredictionEvent predictionEvent = new PredictionEvent();
                    predictionEvent.ContactName = curEvent.ContactName;
                    string eventDayOfWeek = curEvent.EventDate.DayOfWeek.ToString();
                    if (curEvent.Recurring)
                    {
                        predictionEvent.WeekNumber = string.Format("Every Week, {0}", eventDayOfWeek);
                    }
                    else
                    {
                        int weekNumber = (curEvent.EventDate - predictionStartDate).Days / 7;
                        predictionEvent.WeekNumber = string.Format("{0}th week, {1}", weekNumber, eventDayOfWeek);
                    }
                    predictionEvent.Time = string.Format("{0} : {1} : {2}", curEvent.EventDate.Hour, curEvent.EventDate.Minute, curEvent.EventDate.Second);
                    EventList.Items.Add(predictionEvent);
                }
            }
        }
    }
}
