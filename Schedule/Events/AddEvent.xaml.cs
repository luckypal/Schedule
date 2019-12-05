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

        const int Dynamic_Primary_left = 10;
        const int Dynamic_Primary_top = 10;
        const int Dynamic_Label_width = 100;
        const int Dynamic_Content_width = 500;

        public class DynamicItem
        {
            public TextBox Title;
            public TextBox Content;
        }
        List<DynamicItem> DynamicItems;

        public AddEvent(int _index, Event item, EventsPage _parentPage, int primaryEventCount)
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
                Recurring.IsChecked = item.Recurring;

                DynamicItems = new List<DynamicItem>();
                for (var i = 0; i < item.Content.Count; i++)
                {
                    TextBox title = new TextBox();
                    title.Margin = new Thickness(Dynamic_Primary_left, Dynamic_Primary_top + i * 35, 0, 0);
                    title.Height = 30;
                    title.Width = 100;
                    title.HorizontalAlignment = HorizontalAlignment.Left;
                    title.VerticalAlignment = VerticalAlignment.Top;
                    title.Text = item.Content[i].Title;
                    DynamicView.Children.Add(title);

                    TextBox content = new TextBox();
                    content.Margin = new Thickness(Dynamic_Primary_left + Dynamic_Label_width + 5, Dynamic_Primary_top + i * 35, 0, 0);
                    content.Height = 30;
                    content.Width = Dynamic_Content_width;
                    content.HorizontalAlignment = HorizontalAlignment.Left;
                    content.VerticalAlignment = VerticalAlignment.Top;
                    content.Text = item.Content[i].Title;
                    DynamicView.Children.Add(content);

                    DynamicItems.Add(new DynamicItem() { Title = title, Content = content });
                }
                DynamicView.Height = 40 + 35 * item.Content.Count;
            }
            else
            {
                curEvent = new Event();
                DynamicItems = new List<DynamicItem>();

                for (int i = 0; i < primaryEventCount; i++)
                {
                    TextBox title = new TextBox();
                    title.Margin = new Thickness(Dynamic_Primary_left, Dynamic_Primary_top + i * 35, 0, 0);
                    title.Height = 30;
                    title.Width = 100;
                    title.HorizontalAlignment = HorizontalAlignment.Left;
                    title.VerticalAlignment = VerticalAlignment.Top;
                    DynamicView.Children.Add(title);

                    TextBox content = new TextBox();
                    content.Margin = new Thickness(Dynamic_Primary_left + Dynamic_Label_width + 5, Dynamic_Primary_top + i * 35, 0, 0);
                    content.Height = 30;
                    content.Width = Dynamic_Content_width;
                    content.HorizontalAlignment = HorizontalAlignment.Left;
                    content.VerticalAlignment = VerticalAlignment.Top;
                    DynamicView.Children.Add(content);

                    DynamicItems.Add(new DynamicItem() { Title = title, Content = content });
                }

                DynamicView.Height = 40 + 35 * primaryEventCount;

                eventDate.SelectedDate = DateTime.Now;
            }
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
            curEvent.Recurring = Recurring.IsChecked ?? false;
            curEvent.Content = new List<Event.ContentItem>();
            for (int i = 0; i < DynamicItems.Count; i ++)
            {
                string title = DynamicItems[i].Title.Text;
                string content = DynamicItems[i].Content.Text;
                curEvent.Content.Add(new Event.ContentItem() { Title = title, Content = content });
            }

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

        private void AddView_Click(object sender, RoutedEventArgs e)
        {
            int index = DynamicItems.Count;

            TextBox title = new TextBox();
            title.Margin = new Thickness(Dynamic_Primary_left, Dynamic_Primary_top + index * 35, 0, 0);
            title.Height = 30;
            title.Width = 100;
            title.HorizontalAlignment = HorizontalAlignment.Left;
            title.VerticalAlignment = VerticalAlignment.Top;
            DynamicView.Children.Add(title);

            TextBox content = new TextBox();
            content.Margin = new Thickness(Dynamic_Primary_left + Dynamic_Label_width + 5, Dynamic_Primary_top + index * 35, 0, 0);
            content.Height = 30;
            content.Width = Dynamic_Content_width;
            content.HorizontalAlignment = HorizontalAlignment.Left;
            content.VerticalAlignment = VerticalAlignment.Top;
            DynamicView.Children.Add(content);

            DynamicView.Height = 40 + 35 * index;

            DynamicItems.Add(new DynamicItem() { Title = title, Content = content });
        }

        void resizeDynamicView()
        {
            for (int i = 0; i < DynamicItems.Count; i ++)
            {
                TextBox title = DynamicItems[i].Title;
                TextBox content = DynamicItems[i].Content;

                title.Margin = new Thickness(Dynamic_Primary_left, Dynamic_Primary_top + index * 35, 0, 0);
                content.Margin = new Thickness(Dynamic_Primary_left + Dynamic_Label_width + 5, Dynamic_Primary_top + index * 35, 0, 0);
            }
        }
    }
}
