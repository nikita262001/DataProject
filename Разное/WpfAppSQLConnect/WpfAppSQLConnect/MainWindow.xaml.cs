using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Diagnostics;
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

namespace WpfAppSQLConnect
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static string con = @"Data Source=DESKTOP-HREUB9A\SQLEXPRESS;Initial Catalog=TestConnection;Integrated Security=True";
        DataContext data = new DataContext(con);
        public MainWindow()
        {
            InitializeComponent();
            UpdateList();
        }
        private void UpdateList()
        {
            data.SubmitChanges();
            list.ItemsSource = data.GetTable<Person>().ToList();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Person pers = list.SelectedItem as Person;
            if (pers is Person)
            {
                IdPerson.Text = $"{pers.IdPerson}";
                Fam.Text = pers.Fam;
                Name.Text = pers.Name;
                Otch.Text = pers.Otch;
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var table = data.GetTable<Person>();
            table.InsertOnSubmit(new Person { Name = Name.Text,Fam = Fam.Text,Otch = Otch.Text  });
            UpdateList();
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            var table = data.GetTable<Person>();
            table.DeleteOnSubmit(list.SelectedItem as Person);
            UpdateList();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            var table = data.GetTable<Person>();
            Person person = table.ToList().Where((s) => s.IdPerson == (list.SelectedItem as Person).IdPerson).FirstOrDefault();
            person.Name = Name.Text;
            person.Fam = Fam.Text;
            person.Otch = Otch.Text;
            UpdateList();
        }
    }
}
