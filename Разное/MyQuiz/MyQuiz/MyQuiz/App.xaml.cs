using SQLite;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyQuiz
{
    public partial class App : Application
    {
        static string pathBD = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MyQuiz.db"); // ссылка на БД
        static SQLiteAsyncConnection database; //  база данных

        static PackItem dbPack; // обращение к каждой таблице
        static QuestionItem dbQuestoin;
        static AnswerItem dbAnswer;
        public App()
        {
            Task.Run(async () =>
            {
                database = new SQLiteAsyncConnection(pathBD); // подключается и не создается если существует
                await database.CreateTableAsync<Pack>(); // не создается если существует
                await database.CreateTableAsync<Question>();
                await database.CreateTableAsync<Answer>();
            }).Wait();

            InitializeComponent();

            MainPage = new NavigationPage(new Menu());
        }

        protected override void OnStart()
        {
            if (dbPack == null) // для инициализации данных в бд
            {
                dbPack = new PackItem(database);
            }
            if (dbQuestoin == null)
            {
                dbQuestoin = new QuestionItem(database);
            }
            if (dbAnswer == null)
            {
                dbAnswer = new AnswerItem(database);
            }
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
        public static PackItem DatabasePack
        {
            get
            {
                if (dbPack == null)
                {
                    dbPack = new PackItem(database);
                }
                return dbPack;
            }
        }
        public static QuestionItem DatabaseQuestion
        {
            get
            {
                if (dbQuestoin == null)
                {
                    dbQuestoin = new QuestionItem(database);
                }
                return dbQuestoin;
            }
        }
        public static AnswerItem DatabaseAnswer
        {
            get
            {
                if (dbAnswer == null)
                {
                    dbAnswer = new AnswerItem(database);
                }
                return dbAnswer;
            }
        }
    }
}
