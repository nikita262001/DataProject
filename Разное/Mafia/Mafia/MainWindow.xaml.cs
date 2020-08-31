using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Mafia
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<string> peopleRegister = new ObservableCollection<string>();
        List<string> teams = new List<string>();
        string target = String.Empty;
        string targetRole = String.Empty;

        ObservableCollection<string> listMafia = new ObservableCollection<string>();
        ObservableCollection<string> listCitizens = new ObservableCollection<string>();
        Team team = new Team();
        public MainWindow()
        {
            InitializeComponent();
            //MessageBox.Show("Чтобы выбрать цель в Register Users нужно сделать двойной клик");
            listBox.ItemsSource = peopleRegister;
            comboMafia.ItemsSource = listMafia;
            comboCitizens.ItemsSource = listCitizens;
            RefreshComboTeams();
        }
        private async void RefreshComboTeams()
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("Mafia");
            IMongoCollection<Team> collectionDoc = database.GetCollection<Team>("Teams");

            foreach (var item in await collectionDoc.Find(new BsonDocument()).ToListAsync())
            {
                teams.Add(item.TeamName);
            }

            comboTeams.ItemsSource = null;
            comboTeams.ItemsSource = teams;
        }

        private void Register(object sender, RoutedEventArgs e)
        {
            bool newP = true;
            foreach (var item in peopleRegister)
            {
                if (NickName.Text == item)
                {
                    newP = false;
                }
            }
            if (newP)
            {
                peopleRegister.Add(NickName.Text);
            }
            else
            {
                MessageBox.Show("Такой NickName уже есть");
            }
        }
        private void Clear(object sender, RoutedEventArgs e)
        {
            bool clear = false;
            foreach (var item in peopleRegister)
            {
                if (item == target)
                {
                    clear = true;
                }
            }
            if (clear)
            {
                peopleRegister.Remove(target);
            }
            else
            {
                MessageBox.Show($"Такого {target} нет");
            }
        }

        private void listDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //target = peopleRegister[(int)e.Source - 1];if (e.ChangedButton == MouseButton.Left)

            var selector = (Selector)sender;

            if (e.ChangedButton == MouseButton.Left && e.OriginalSource is Visual source)
            {
                var container = selector.ContainerFromElement(source);
                if (container != null)
                {
                    var index = selector.ItemContainerGenerator.IndexFromContainer(container);
                    if (index >= 0)
                    {
                        target = peopleRegister[index];
                    }
                }
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            targetRole = (String)((RadioButton)sender).Content;
        }

        private void SetRole(object sender, RoutedEventArgs e)
        {
            bool targetHaveRole = false;

            #region targetHaveRole
            foreach (var item in listMafia)
            {
                if (item == target)
                {
                    targetHaveRole = true;
                    break;
                }
            }
            if ((string)labelBoss.Content == target)
            {
                targetHaveRole = true;
            }
            if ((string)labelSherif.Content == target)
            {
                targetHaveRole = true;
            }
            if ((string)labelDoctor.Content == target)
            {
                targetHaveRole = true;
            }
            if ((string)labelManiac.Content == target)
            {
                targetHaveRole = true;
            }
            foreach (var item in listCitizens)
            {
                if (item == target)
                {
                    targetHaveRole = true;
                    break;
                }
            }
            #endregion

            bool roleLock = false;

            #region roleLock
            switch (targetRole)
            {
                case "Boss":
                    string txtBoss = (string)labelBoss.Content;
                    if (txtBoss != "(пусто)" && txtBoss != "unserred")
                    { roleLock = true; }
                    break;
                case "Sherif":
                    string txtSherif = (string)labelSherif.Content;
                    if (txtSherif != "(пусто)" && txtSherif != "unserred")
                    { roleLock = true; }
                    break;
                case "Doctor":
                    string txtDoctor = (string)labelDoctor.Content;
                    if (txtDoctor != "(пусто)" && txtDoctor != "unserred")
                    { roleLock = true; }
                    break;
                case "Maniac":
                    string txtManiac = (string)labelManiac.Content;
                    if (txtManiac != "(пусто)" && txtManiac != "unserred")
                    { roleLock = true; }
                    break;
            }
            #endregion

            if (targetHaveRole)
            {
                MessageBox.Show("У цели есть роль");
            }
            else
            {
                if (roleLock)
                { MessageBox.Show("Роль занята"); }
                else
                { WriteInGame(); }
            }
        }

        private void Rerole(object sender, RoutedEventArgs e)
        {
            #region deleteRole
            foreach (var item in listMafia)
            {
                if (item == target)
                {
                    listMafia.Remove(target);
                    break;
                }
            }
            if ((string)labelBoss.Content == target)
            {
                labelBoss.Content = "(пусто)";
            }
            if ((string)labelSherif.Content == target)
            {
                labelSherif.Content = "(пусто)";
            }
            if ((string)labelDoctor.Content == target)
            {
                labelDoctor.Content = "(пусто)";
            }
            if ((string)labelManiac.Content == target)
            {
                labelManiac.Content = "(пусто)";
            }
            foreach (var item in listCitizens)
            {
                if (item == target)
                {
                    listCitizens.Remove(target);
                    break;
                }
            }
            #endregion

            WriteInGame();
        }

        private void WriteInGame()
        {
            switch (targetRole)
            {
                case "Mafia":
                    listMafia.Add(target);
                    break;
                case "Boss":
                    labelBoss.Content = target;
                    break;
                case "Sherif":
                    labelSherif.Content = target;
                    break;
                case "Doctor":
                    labelDoctor.Content = target;
                    break;
                case "Maniac":
                    labelManiac.Content = target;
                    break;
                case "Citizen":
                    listCitizens.Add(target);
                    break;
            }
        }

        private void ClearRole(object sender, RoutedEventArgs e)
        {
            #region deleteRole
            foreach (var item in listMafia)
            {
                if (item == target)
                {
                    listMafia.Remove(target);
                    break;
                }
            }
            if ((string)labelBoss.Content == target)
            {
                labelBoss.Content = "(пусто)";
            }
            if ((string)labelSherif.Content == target)
            {
                labelSherif.Content = "(пусто)";
            }
            if ((string)labelDoctor.Content == target)
            {
                labelDoctor.Content = "(пусто)";
            }
            if ((string)labelManiac.Content == target)
            {
                labelManiac.Content = "(пусто)";
            }
            foreach (var item in listCitizens)
            {
                if (item == target)
                {
                    listCitizens.Remove(target);
                    break;
                }
            }
            #endregion
        }

        private void RandomRole(object sender, RoutedEventArgs e)
        {

            if (peopleRegister.Count >= 6)
            {
                #region deleteRole
                var coutMaf = listMafia.Count;
                for (int i = 0; i < coutMaf; i++)
                {
                    listMafia.Remove(listMafia.Last());
                }
                labelBoss.Content = "(пусто)";
                labelSherif.Content = "(пусто)";
                labelDoctor.Content = "(пусто)";
                labelManiac.Content = "(пусто)";

                var coutCit = listCitizens.Count;
                for (int i = 0; i < coutCit; i++)
                {
                    listCitizens.Remove(listCitizens.Last());
                }
                #endregion

                Random random = new Random();
                foreach (var item in peopleRegister)
                {
                    var a = random.Next(0, 6);
                    switch (a)
                    {
                        case 0:
                            listMafia.Add(item);
                            break;
                        case 1:
                            string txtBoss = (string)labelBoss.Content;
                            if (txtBoss == "(пусто)" || txtBoss == "unserred")
                            { labelBoss.Content = item; }
                            else
                            { listMafia.Add(item); }
                            break;

                        case 2:
                            string txtSherif = (string)labelSherif.Content;
                            if (txtSherif == "(пусто)" || txtSherif == "unserred")
                            { labelSherif.Content = item; }
                            else
                            { listCitizens.Add(item); }
                            break;

                        case 3:
                            string txtDoctor = (string)labelDoctor.Content;
                            if (txtDoctor == "(пусто)" || txtDoctor == "unserred")
                            { labelDoctor.Content = item; }
                            else
                            { listMafia.Add(item); }
                            break;

                        case 4:
                            string txtManiac = (string)labelManiac.Content;
                            if (txtManiac == "(пусто)" || txtManiac == "unserred")
                            { labelManiac.Content = item; }
                            else
                            { listCitizens.Add(item); }
                            break;

                        case 5:
                            listCitizens.Add(item);
                            break;
                    }
                }
            }
            else
            {
                MessageBox.Show("У вас мало Register Users");
            }
        }

        private async void SaveTeam(object sender, RoutedEventArgs e)
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("Mafia");
            IMongoCollection<BsonDocument> collectionDoc = database.GetCollection<BsonDocument>("Teams");


            if (teamName.Text != "")
            {
                team.TeamName = teamName.Text;
                team.Mafia = listMafia;
                team.Boss = (string)labelBoss.Content;
                team.Sheriff = (string)labelSherif.Content;
                team.Maniac = (string)labelManiac.Content;
                team.Doctor = (string)labelDoctor.Content;
                team.Citizens = listCitizens;
                await collectionDoc.InsertOneAsync(team.ToBsonDocument());
            }
            RefreshComboTeams();
        }

        private async void comboTeamsSelection(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            var selectedItem = (string)comboBox.SelectedItem;

            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("Mafia");
            IMongoCollection<Team> collectionDoc = database.GetCollection<Team>("Teams");

            foreach (var itemTeam in await collectionDoc.Find(new BsonDocument("_id", selectedItem)).ToListAsync())
            {
                teamName.Text = itemTeam.TeamName;
                listMafia = itemTeam.Mafia;
                labelBoss.Content = itemTeam.Boss;
                labelSherif.Content = itemTeam.Sheriff;
                labelManiac.Content = itemTeam.Maniac;
                labelDoctor.Content = itemTeam.Doctor;
                listCitizens = itemTeam.Citizens;
            }
        }
    }
}