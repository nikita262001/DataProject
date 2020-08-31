using System;
using System.Dynamic;

namespace ExchangerBlue
{
    class Program
    {
        static string[] masStartItems = { "Синяя птица", "Чай", "Курица", "Икра", "Пирог" };
        static string[] mas = new string[6];
        static int[] deleteCountsIndex = new int[2];
        static bool deleteColumn;
        static void Main(string[] args)
        {
            StartMas();
            while (true)
            {
                GetInfoMas();
                Delete();
                Replacement();
            }
        }
        static void OpenMenu()
        {
            int i = 1;
            foreach (var item in masStartItems)
            {
                Console.WriteLine($"{i}) {item}.");
                i++;
            }
        }
        static void StartMas()
        {
            for (int i = 0; i < 6; i++)
            {
                OpenMenu();
                Console.WriteLine($"Введите {i} номер предмета.");
                int count = Convert.ToInt32(Console.ReadKey().KeyChar.ToString());
                switch (count)
                {
                    case 1:
                        mas[i] = "Синяя птица";
                        break;
                    case 2:
                        mas[i] = "Чай";
                        break;
                    case 3:
                        mas[i] = "Курица";
                        break;
                    case 4:
                        mas[i] = "Икра";
                        break;
                    case 5:
                        mas[i] = "Пирог";
                        break;
                }
                Console.Clear();
            }
        }
        static void GetInfoMas()
        {
            for (int i = 0; i < 2; i++)
            {
                string txt = "\n\t\t";
                for (int j = 0; j < 3; j++)
                {
                    if (i == 0)
                    {
                        txt += mas[j] + "\t\t";
                    }
                    else if (i == 1)
                    {
                        txt += mas[j + 3] + "\t\t";
                    }
                }
                Console.WriteLine(txt);
            }
        }
        static void Delete()
        {
            Console.WriteLine("\n\t\t1)Удаление столбца.\n\t\t2)Обычное удаление.");
            int countDelete = Convert.ToInt32(Console.ReadKey().KeyChar.ToString());
            switch (countDelete)
            {
                case 1:
                    Console.WriteLine("\nНапишите номер удаления столбца от 1 до 3");
                    int countColumn = Convert.ToInt32(Console.ReadKey().KeyChar.ToString());
                    deleteCountsIndex[0] = countColumn - 1;
                    deleteCountsIndex[1] = countColumn + 2;
                    deleteColumn = true;
                    break;
                case 2:
                    Console.WriteLine("\nНапишите номер удаления от 1 до 6");
                    for (int i = 0; i < 2; i++)
                    {
                        int count = Convert.ToInt32(Console.ReadKey().KeyChar.ToString());
                        if (count >= 0 && count <= 3)
                        {
                            deleteCountsIndex[i] = count - 1;
                        }
                        else if (count >= 4 && count <= 6)
                        {
                            deleteCountsIndex[i] = count - 4;
                            mas[count - 1] = mas[count - 4];
                        }
                    }
                    if (deleteCountsIndex[0] > deleteCountsIndex[1])
                    {
                        int value = deleteCountsIndex[0];
                        deleteCountsIndex[0] = deleteCountsIndex[1];
                        deleteCountsIndex[1] = value;
                    }
                    deleteColumn = false;
                    break;
            }
            Console.Clear();
        }
        static void Replacement()
        {
            OpenMenu();
            if (deleteColumn)
            {
                Console.WriteLine("Выберите 2 полученные картинки сверху вниз");
            }
            else
            {
                Console.WriteLine("Выберите 2 полученные картинки слева направо");
            }
            for (int i = 0; i < 2; i++)
            {
                int count = Convert.ToInt32(Console.ReadKey().KeyChar.ToString());
                var a = deleteCountsIndex[i];
                var b = masStartItems[count - 1];
                mas[deleteCountsIndex[i]] = masStartItems[count - 1];
            }
        }
    }
}
