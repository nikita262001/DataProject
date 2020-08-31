using MongoDB.Bson;
using MongoDB.Driver;
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

namespace WpfAppWork
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ClickRefresh(object sender, RoutedEventArgs e) // обновление данных
        {
            IMongoCollection<Person> collectionPers = GetMongoDB();
            Person person = collectionPers.Find(new BsonDocument("_id", Convert.ToInt32(TId.Text))).ToList().First();

            TId.Text = $"{person.Id}";
            TName.Text = person.Name;
            TFam.Text = person.Surname;
            TOtch.Text = person.Otchestvo;

        }
        private void ClickDelete(object sender, RoutedEventArgs e) // Удаление данных
        {
            IMongoCollection<Person> collectionPers = GetMongoDB();
            collectionPers.DeleteOne(new BsonDocument("_id", Convert.ToInt32(TId.Text)));

        }
        private void ClickEdit(object sender, RoutedEventArgs e) // Изменение данных
        {
            IMongoCollection<Person> collectionPers = GetMongoDB();
            collectionPers.DeleteOne(new BsonDocument("_id", Convert.ToInt32(TId.Text)));
            collectionPers.InsertOne(new Person { Id = Convert.ToInt32(TId.Text), Name = TName.Text, Surname = TFam.Text, Otchestvo = TOtch.Text });
        }
        private void ClickAdd(object sender, RoutedEventArgs e) // Добовление данных
        {
            IMongoCollection<Person> collectionPers = GetMongoDB();
            collectionPers.InsertOne(new Person { Id = Convert.ToInt32(TId.Text), Name = TName.Text, Surname = TFam.Text, Otchestvo = TOtch.Text });
        }
        private IMongoCollection<Person> GetMongoDB()
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("Work");
            IMongoCollection<Person> collectionPers = database.GetCollection<Person>("Person");
            return collectionPers;
        }
    }
}
