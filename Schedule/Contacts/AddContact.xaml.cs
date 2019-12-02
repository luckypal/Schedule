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
    /// Interaction logic for AddContact.xaml
    /// </summary>
    public partial class AddContact : Page
    {
        int index;
        Contact contact;
        ContactsPage parentPage;

        public AddContact(int _index, Contact item, ContactsPage _parentPage)
        {
            InitializeComponent();

            contact = item;
            if (item == null) contact = new Contact();

            Name.Text = contact.Name;
            Email.Text = contact.Email;
            Note.Text = contact.Note;

            index = _index;
            parentPage = _parentPage;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Global.instance.entireFrame.GoBack();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            if (Name.Text == "" || Email.Text == "")
            {
                return;
            }
            contact.Name = Name.Text;
            contact.Email = Email.Text;
            contact.Note = Note.Text;

            if (index == -1) {
                Global.instance.Contacts.Add(contact);
            }
            else
            {
                Global.instance.Contacts[index] = contact;
            }

            Global.instance.entireFrame.GoBack();
            parentPage.reload();
        }
    }
}
