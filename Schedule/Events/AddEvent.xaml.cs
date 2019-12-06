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
        EventsPage parentPage;
        List<EventItem> dynamicItems;
        
        public AddEvent(int _index, Event item, EventsPage _parentPage, int primaryEventCount)
        {
            InitializeComponent();

            parentPage = _parentPage;
            dynamicItems = new List<EventItem>();

            if (item == null)
            {//Add New Event
                for (int i = 0; i < primaryEventCount; i++)
                {
                    EventItem eventView = new EventItem(-1, null);
                    eventView.Margin = new Thickness(0, 320 * i, 0, 0);
                    eventView.Width = 700;
                    eventView.Height = 320;
                    eventView.HorizontalAlignment = HorizontalAlignment.Left;
                    eventView.VerticalAlignment = VerticalAlignment.Top;
                    eventView.BorderThickness = new Thickness(0, 0, 0, 3);
                    eventView.BorderBrush = System.Windows.Media.Brushes.Gray;
                    DynamicContent.Children.Add(eventView);
                    dynamicItems.Add(eventView);
                }
                DynamicContent.Height = 320 * primaryEventCount;
            } else
            {//Edit event
                EventItem eventView = new EventItem(_index, item);
                eventView.Margin = new Thickness(0, 0, 0, 0);
                eventView.Width = 700;
                eventView.Height = 320;
                DynamicContent.Children.Add(eventView);
                dynamicItems.Add(eventView);
                DynamicContent.Height = 320;
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Global.instance.entireFrame.GoBack();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < dynamicItems.Count; i++)
                dynamicItems[i].saveEvent();
            Global.instance.entireFrame.GoBack();
            parentPage.reload();
        }

        /*void resizeDynamicView()
        {
            for (int i = 0; i < DynamicItems.Count; i ++)
            {
                TextBox title = DynamicItems[i].Title;
                TextBox content = DynamicItems[i].Content;

                title.Margin = new Thickness(Dynamic_Primary_left, Dynamic_Primary_top + index * 35, 0, 0);
                content.Margin = new Thickness(Dynamic_Primary_left + Dynamic_Label_width + 5, Dynamic_Primary_top + index * 35, 0, 0);
            }
        }*/
    }
}
