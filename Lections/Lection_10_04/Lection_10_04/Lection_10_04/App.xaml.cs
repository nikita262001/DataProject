using SQLite;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Lection_10_04
{
    public partial class App : Application
    {
        private string _databaseFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        private string _databaseName = "People_test.db";

        private static string _databasePath;

        public static readonly SQLiteOpenFlags flags = SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache | SQLiteOpenFlags.ReadWrite;
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
            _databasePath = Path.Combine(_databaseFolder, _databaseName);

        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
        public static string DatabasePath => _databasePath;
    }
}
