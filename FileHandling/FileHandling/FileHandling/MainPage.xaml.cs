using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FileHandling
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            var pathToFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            var pathToFile =  pathToFolder + @"\testFile2.txt";
            var stream = File.Create(pathToFile); //поток связан с файлом
            stream.Close();
            stream = new FileStream(pathToFile, FileMode.OpenOrCreate);

            var data = "Просто текст (UPW)" + DateTime.Now.ToString();
            var array = Encoding.UTF8.GetBytes(data);//перевел в байты

            Debug.WriteLine("ID потока до: " + Thread.CurrentThread.ManagedThreadId);
           var  task = stream.WriteAsync(array, 0, array.Length);
            //stream.Write(array,0,array.Length);

            stream.Close();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            string path1 = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            Debug.WriteLine("Application data: " + path1);

            string path2 = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            Debug.WriteLine("Personal: " + path2);

            string path3 = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments); // нет на рабочем
            Debug.WriteLine("Common Documents: " + path3);

            string path4 = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            Debug.WriteLine("My documents: " + path4);

            string path5 = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); // наша папка
            Debug.WriteLine("Desktop: " + path5);

            string path6 = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            Debug.WriteLine("Desktop Directory: " + path6);

            string path7 = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            Debug.WriteLine("My Pictures: " + path7);

            string path8 = Environment.GetFolderPath(Environment.SpecialFolder.CommonPictures);
            Debug.WriteLine("Common Pictures: " + path8);

            string path9 = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            Debug.WriteLine("Local Application Data: " + path9);

            string path10 = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            Debug.WriteLine("Common Application Data: " + path10);
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    #region Android
                    var pathToFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

                    var pathToFile = pathToFolder + "/testFile2.txt";
                    var stream = File.Create(pathToFile);

                    foreach (FileInfo item in Directory.CreateDirectory(pathToFile).GetFiles())
                    {
                        Debug.WriteLine("Файл: " + item.FullName);
                    }

                    foreach (DirectoryInfo item in Directory.CreateDirectory(pathToFolder).GetDirectories())
                    {
                        Debug.WriteLine("Папка: " + item.FullName);
                    }

                    var data = "Просто текст (UPW)" + DateTime.Now.ToString();
                    byte[] array = Encoding.UTF8.GetBytes(data);

                    Debug.WriteLine("ID потока до: " + Thread.CurrentThread.ManagedThreadId);
                    Task task = stream.WriteAsync(array, 0, array.Length);

                    stream.Close();


                    Debug.WriteLine("ID потока до: " + Thread.CurrentThread.ManagedThreadId);
                    task = stream.WriteAsync(array, 0, array.Length);
                    text.IsVisible = true;

                    //Device.BeginInvokeOnMainThread();
                    #endregion
                    break;

                case Device.UWP:
                    #region UWP
                    pathToFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

                    pathToFile = pathToFolder + "/testFile.txt";

                    foreach (FileInfo item in Directory.CreateDirectory(pathToFile).GetFiles())
                    {
                        Debug.WriteLine("Файл: " + item.FullName);
                    }

                    foreach (DirectoryInfo item in Directory.CreateDirectory(pathToFolder).GetDirectories())
                    {
                        Debug.WriteLine("Папка: " + item.FullName);
                    }

                    stream = File.Create(pathToFile); //поток связан с файлом
                    stream = new FileStream(pathToFile, FileMode.OpenOrCreate);

                    data = "Просто текст (UPW)" + DateTime.Now.ToString();
                    array = Encoding.UTF8.GetBytes(data);//перевел в байты

                    Debug.WriteLine("ID потока до: " + Thread.CurrentThread.ManagedThreadId);
                    task = stream.WriteAsync(array, 0, array.Length);
                    //stream.Write(array,0,array.Length);

                    stream.Close();

                    Debug.WriteLine("ID потока до: " + Thread.CurrentThread.ManagedThreadId);
                    text.Text = File.ReadAllText(pathToFile);
                    text.IsVisible = true;

                    #endregion
                    break;

                default:
                    break;
            }
        }

        private async void Button_Clicked_2(object sender, EventArgs e)
        {
            Label textEmber = new Label();
            if (!textEmber.IsVisible)
            {
                var assembly = IntrospectionExtensions.GetTypeInfo(typeof(MainPage)).Assembly;
                Stream stream = assembly.GetManifestResourceStream("FileHandling.Assets.embedded file.Txt");
                using (var reader = new StreamReader(stream))
                {
                    textEmber.Text = await reader.ReadToEndAsync();//такая же как и Task
                    textEmber.IsVisible = true;
                }
            }
        }

        private async void Button_Clicked_3(object sender, EventArgs e)
        {

            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(MainPage)).Assembly;
            Stream stream = assembly.GetManifestResourceStream("FileHandling.txt.json");

            using (var reader = new StreamReader(stream))
            {
                //Stopwatch stopwatch = new Stopwatch();
                //stopwatch.Start();
                //Thread.Sleep(5000);
                string jsonDataPath = await reader.ReadToEndAsync();

                //Person p = JsonConvert.DeserializeObject<Person>(await reader.ReadToEndAsync());
                //Person p = JsonConvert.DeserializeObject<Person>(jsonDataPath);
                Task<Person> taskP = Deserialise(jsonDataPath);
                Person p = await taskP; // я сделал , получите данные те кто ждал меня

                textJson.Text = await reader.ReadToEndAsync();//такая же как и Task
                //textJson.Text = $"{p.Name}";
                textJson.IsVisible = true;

                //stopwatch.Stop();
                //Debug.WriteLine(stopwatch.ElapsedMilliseconds);

                //Debug.WriteLine("End");
            }
        }
        async Task<Person> Deserialise(string path)
        {
            return await Task<Person>.Factory.StartNew(() =>
            {
                Thread.Sleep(1000);
                textJson.Text = "10%"; //должна быть ошибка
                Thread.Sleep(1000);
                Device.BeginInvokeOnMainThread(()=> textJson.Text = "50%");
                Thread.Sleep(1000);
                return JsonConvert.DeserializeObject<Person>(path);
            });
        }
    }

}
