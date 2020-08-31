using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace StartToOpenLinksKtits
{
    class Program
    {
        static ProcessStartInfo prs = new ProcessStartInfo(@"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe");

        const string systemProgPath = "https://dom.mck-ktits.ru/mod/chat/gui_ajax/index.php?id=170";
        const string razrabProgModulePath = "https://dom.mck-ktits.ru/mod/chat/gui_ajax/index.php?id=138";
        const string testProgModulePath = "https://dom.mck-ktits.ru/mod/chat/gui_ajax/index.php?id=137";
        const string razragMobPrilojenPath = "https://dom.mck-ktits.ru/mod/chat/gui_ajax/index.php?id=136";
        const string engPath = "https://dom.mck-ktits.ru/mod/chat/gui_ajax/index.php?id=84";
        const string fizraVafinPath = "https://dom.mck-ktits.ru/mod/chat/gui_ajax/index.php?id=146";
        const string ychebPract = "https://dom.mck-ktits.ru/mod/chat/gui_ajax/index.php?id=376";

        const string firstСouple = "07:50:00";
        const string secondСouple = "09:30:00";
        const string thirdСouple = "11:40:00";
        const string fourthСouple = "13:30:00";
        const string fifthСouple = "15:10:00";
        const string sixthСouple = "16:50:00";

        static void Main(string[] args)
        {
            #region рассписние пар
            Console.WriteLine("Первая пара: " + firstСouple);
            Console.WriteLine("Вторая пара: " + secondСouple);
            Console.WriteLine("Третья пара: " + thirdСouple);
            Console.WriteLine("Четвертая пара: " + fourthСouple);
            Console.WriteLine("Пятая пара: " + fifthСouple);
            Console.WriteLine("Шестая пара: " + sixthСouple);
            #endregion
            ThreadPool.RegisterWaitForSingleObject(new AutoResetEvent(false), StartToOpenLinksYchebPract, null, 1000, false);
            while (true)
            {
                Console.ReadKey();
            }
            #region testExcel
            /*ProcessStartInfo prsf = new ProcessStartInfo(@"C:\Program Files (x86)\Microsoft Office\Office15\EXCEL.EXE");
            var a = Process.Start(prsf);
            Thread.Sleep(5000);
            a.Kill();
            Thread.Sleep(5000);*/
            #endregion
        }
        private static void StartToOpenLinksYchebPract(object e, bool timeOut)
        {
            var time = DateTime.Now;
            switch (time.DayOfWeek)
            {
                case DayOfWeek.Monday: // 1

                    switch (time.ToString("HH:mm:ss"))
                    {
                        case fourthСouple: // 4
                            StartLink(ychebPract);
                            break;
                    }

                    break;

                case DayOfWeek.Tuesday: // 2


                    switch (time.ToString("HH:mm:ss"))
                    {
                        case fourthСouple: // 4
                            StartLink(ychebPract);
                            break;
                    }

                    break;

                case DayOfWeek.Wednesday: // 3


                    switch (time.ToString("HH:mm:ss"))
                    {
                        case fourthСouple: // 4
                            StartLink(ychebPract);
                            break;
                    }

                    break;

                case DayOfWeek.Thursday: // 4


                    switch (time.ToString("HH:mm:ss"))
                    {
                        case fourthСouple: // 4
                            StartLink(ychebPract);
                            break;
                    }

                    break;

                case DayOfWeek.Friday: // 5


                    switch (time.ToString("HH:mm:ss"))
                    {
                        case fourthСouple: // 4
                            StartLink(ychebPract);
                            break;
                    }

                    break;

                case DayOfWeek.Saturday: // 6


                    switch (time.ToString("HH:mm:ss"))
                    {
                        case fourthСouple: // 4
                            StartLink(ychebPract);
                            break;
                    }

                    break;
            }
        }

        private static void StartToOpenLinks27April2May(object e, bool timeOut)
        {
            var time = DateTime.Now;
            switch (time.DayOfWeek)
            {
                case DayOfWeek.Monday: // 1

                    switch (time.ToString("HH:mm:ss"))
                    {
                        case secondСouple: // 2
                            StartLink(razrabProgModulePath);
                            break;
                        case thirdСouple: // 3
                            StartLink(razragMobPrilojenPath);
                            break;
                        case fourthСouple: // 4
                            StartLink(razragMobPrilojenPath);
                            break;
                        case fifthСouple: // 5
                            StartLink(systemProgPath);
                            break;
                    }

                    break;

                case DayOfWeek.Tuesday: // 2


                    switch (time.ToString("HH:mm:ss"))
                    {
                        case thirdСouple: // 3
                            StartLink(razrabProgModulePath);
                            break;
                        case fourthСouple: // 4
                            StartLink(razrabProgModulePath);
                            break;
                        case fifthСouple: // 5
                            StartLink(testProgModulePath);
                            break;
                        case sixthСouple: // 6
                            StartLink(testProgModulePath);
                            break;
                    }

                    break;

                case DayOfWeek.Wednesday: // 3


                    switch (time.ToString("HH:mm:ss"))
                    {
                        case firstСouple: // 1
                            StartLink(systemProgPath);
                            break;
                        case secondСouple: // 2
                            StartLink(systemProgPath);
                            break;
                        case thirdСouple: // 3
                            StartLink(razragMobPrilojenPath);
                            break;
                        case fourthСouple: // 4
                            StartLink(testProgModulePath);
                            break;
                    }

                    break;

                case DayOfWeek.Thursday: // 4


                    switch (time.ToString("HH:mm:ss"))
                    {
                        case firstСouple: // 1
                            StartLink(razragMobPrilojenPath);
                            break;
                        case secondСouple: // 2
                            StartLink(razragMobPrilojenPath);
                            break;
                        case thirdСouple: // 3
                            StartLink(testProgModulePath);
                            break;
                        case fourthСouple: // 4
                            StartLink(fizraVafinPath);
                            break;
                    }

                    break;

                case DayOfWeek.Saturday: // 6


                    switch (time.ToString("HH:mm:ss"))
                    {
                        case firstСouple: // 1
                            StartLink(systemProgPath);
                            break;
                        case secondСouple: // 2
                            StartLink(systemProgPath);
                            break;
                    }

                    break;
            }
        }

        private static void StartToOpenLinks(object e, bool timeOut)
        {
            var time = DateTime.Now;
            switch (time.DayOfWeek)
            {
                case DayOfWeek.Monday: // 1

                    switch (time.ToString("HH:mm:ss"))
                    {
                        case firstСouple: // 1
                            StartLink(fizraVafinPath);
                            break;
                        case secondСouple: // 2
                            StartLink(razrabProgModulePath);
                            break;
                        case thirdСouple: // 3
                            StartLink(razragMobPrilojenPath);
                            break;
                        case fourthСouple: // 4
                            StartLink(razragMobPrilojenPath);
                            break;
                    }

                    break;

                case DayOfWeek.Tuesday: // 2


                    switch (time.ToString("HH:mm:ss"))
                    {
                        case thirdСouple: // 3
                            StartLink(razrabProgModulePath);
                            break;
                        case fourthСouple: // 4
                            StartLink(razrabProgModulePath);
                            break;
                        case fifthСouple: // 5
                            StartLink(testProgModulePath);
                            break;
                    }

                    break;

                case DayOfWeek.Wednesday: // 3


                    switch (time.ToString("HH:mm:ss"))
                    {
                        case firstСouple: // 1
                            StartLink(systemProgPath);
                            break;
                        case secondСouple: // 2
                            StartLink(systemProgPath);
                            break;
                        case thirdСouple: // 3
                            StartLink(razrabProgModulePath);
                            break;
                        case fourthСouple: // 4
                            StartLink(testProgModulePath);
                            break;
                    }

                    break;

                case DayOfWeek.Thursday: // 4


                    switch (time.ToString("HH:mm:ss"))
                    {
                        case firstСouple: // 1
                            StartLink(engPath);
                            break;
                        case secondСouple: // 2
                            StartLink(razragMobPrilojenPath);
                            break;
                    }

                    break;

                case DayOfWeek.Friday: // 5


                    switch (time.ToString("HH:mm:ss"))
                    {
                        case fourthСouple: // 4
                            StartLink(testProgModulePath);
                            break;
                        case fifthСouple: // 5
                            StartLink(testProgModulePath);
                            break;
                        case sixthСouple: // 6
                            StartLink(razragMobPrilojenPath);
                            break;
                    }

                    break;

                case DayOfWeek.Saturday: // 6


                    switch (time.ToString("HH:mm:ss"))
                    {
                        case firstСouple: // 1
                            StartLink(systemProgPath);
                            break;
                        case secondСouple: // 2
                            StartLink(systemProgPath);
                            break;
                    }

                    break;
            }
        }

        private static void StartLink(string path)
        {
            prs.Arguments = path;
            Process.Start(prs);
        }

        /*
        Sunday = 0,
        Monday = 1,
        Tuesday = 2,
        Wednesday = 3,
        Thursday = 4,
        Friday = 5,
        Saturday = 6
        */
    }
}
