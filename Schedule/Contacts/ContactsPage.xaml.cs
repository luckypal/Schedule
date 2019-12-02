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

namespace Schedule.Contacts
{
    /// <summary>
    /// Interaction logic for Contacts.xaml
    /// </summary>
    public partial class ContactsPage : Page
    {
        public ContactsPage()
        {
            InitializeComponent();

            var gridView = new GridView();
            ContactList.View = gridView;
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Name",
                DisplayMemberBinding = new Binding("Name")
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Email",
                DisplayMemberBinding = new Binding("Email")
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Note",
                DisplayMemberBinding = new Binding("Note")
            });

            reload();
        }

        public void reload()
        {
            ContactList.Items.Clear();

            for (int i = 0; i < Global.instance.Contacts.Count; i ++)
                ContactList.Items.Add(Global.instance.Contacts [i]);

            Global.instance.saveData();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Global.instance.entireFrame.GoBack();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Global.instance.entireFrame.Navigate(new AddContact(-1, null, this));
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            int selIndex = ContactList.SelectedIndex;
            if (selIndex == -1) return;

            Contact selectedItem = Global.instance.Contacts[selIndex];
            Global.instance.entireFrame.Navigate(new AddContact(selIndex, selectedItem, this));
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            int selIndex = ContactList.SelectedIndex;
            if (selIndex == -1) return;

            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                Global.instance.Contacts.RemoveAt(selIndex);
                reload();
            }
        }
    }
}
