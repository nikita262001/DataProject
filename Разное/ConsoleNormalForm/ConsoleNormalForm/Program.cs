using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;

namespace ConsoleNormalForm
{
    class Program
    {
        static string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "NormalForm.txt"); // Каталог, который служит общим хранилищем документов, и комбинируем с названем нашего txt файла
        static List<string> list; // лист трок двоичного кода
        static List<int> answers = new List<int>(); // лист ответов
        static int lines, quantity; // количество строк, переменных (x1,x2,x3...)
        static void Main(string[] args)
        {
            Console.WriteLine("Вводите пожалуйста правильно");
            Console.WriteLine("Введите кол-во переменных(x1,x2,x3...)");

            quantity = Convert.ToInt32(Console.ReadLine()); // Преобразование введенного числа в int
            lines = Convert.ToInt32(Math.Pow(2, quantity)); // Высчитывание количеста строк с которыми будем работать

            Console.WriteLine("Введите ответы(сверху вниз)");

            for (int i = 0; i < lines; i++) // Заполнение листа ответами
            {
                Console.Write($"№{i} ");
                answers.Add(int.Parse(Console.ReadKey().KeyChar.ToString())); // добавляем в лист ответов преобразованый символ в int
                Console.WriteLine();
            } 
            list = GetListBinaryCode(); // вызов метода который возвращает лист строк , который записываем в list

            string PDNF = "СДНФ: " + GetPDNF() + "\n"; // создание СДНФ строки и вызов метода который даст СДНФ строку (в конце сделал переход строки чтобы при записи в файл выглядело все красиво) (PDNF - Perfect Disjunctive Normal Form)
            string PCNF = "СКНФ: " + GetPCNF(); // создание СКНФ строки и вызов метода который даст СДНФ строку (PCNF - Perfect Conjunctive Normal Form)

            using (FileStream file = new FileStream(path, FileMode.Create)) // создает и открывает файл
            {
                byte[] infoPDNF = new UTF8Encoding(true).GetBytes(PDNF); // переводит строку в байты
                byte[] infoPCNF = new UTF8Encoding(true).GetBytes(PCNF);
                file.Write(infoPDNF, 0, infoPDNF.Length); // записывает в файл
                file.Write(infoPCNF, 0, infoPCNF.Length);
            }
            Console.WriteLine("Ссылка на ваш файл: " + path);
            Console.WriteLine(PCNF);
            Console.WriteLine(PDNF);
            Console.WriteLine("Нажмите на любую кнопку чтобы завершить...");
            Console.ReadKey();
        }
        static List<string> GetListBinaryCode() // метод который возвращает лист Binary строк
        {
            List<string> list = new List<string>(); // создание нового листа string
            for (int i = 0; i < lines; i++) // вызываем метод столько раз сколько дожно быть строк
                list.Add(GetLine(i)); // вызываем метод которые возвращает string и добавляем его в лист

            return list; // возвращем лист
        }
        static string GetLine(int number) // метод который возвращает Binary строку
        {
            string txtStart = ""; // начало Binary кода
            string txtEnd = Convert.ToString(number, 2); //в конец Binary кода записываем номер строки преобразованый в двоичный код
            for (int i = 0; i < quantity - txtEnd.Length; i++) // дописываем нехватающее кличество нулей
                txtStart += 0;

            return txtStart + txtEnd; // возвращаем Binary строку
        }
        static string GetPDNF() // метод который возвращает СДНФ строку
        {
            string txtAll = ""; // будующая строка СДНФ
            for (int i = 0; i < lines; i++) // перебираем лист трок двоичного кода
            {
                if (answers[i] == 1) // проверка строки ответа на 1 (y)
                {
                    string txt = ""; // будующая строка переменных. Пример X1!X2!X3
                    for (int j = 0; j < quantity; j++) // перебираем строку по символам в ней
                    {
                        if (list[i][j] == '0') // если символ в строке 0 то у X(икс) будет стоять знак отрицания
                        {
                            txt += "!";
                        }
                        txt += $"X{j + 1}"; // запись в строку переменных
                    }
                    txtAll += txt + "+"; // записывание в общую СДНФ, строку с переменными и в конце ставим + чтобы между строками переменных был знак. Например X1!X2!X3+
                }
            }
            if (txtAll.Length == 0) // Проверка на пустоту СДНФ теста (будет выполнено если ответы были только 0)
                return "(Пусто)"; // возвращаем (Пусто)
            else
            {
                txtAll = txtAll.Remove(txtAll.Length - 1); // удаляем последний символ +. Например ...+X1!X2!X3+
                return txtAll; // возвращаем СДНФ строку
            }
        }
        static string GetPCNF() // метод который возвращает СКНФ строку
        {
            string txtAll = ""; // будующая строка СКНФ
            for (int i = 0; i < lines; i++) // перебираем лист трок двоичного кода
            {
                if (answers[i] == 0) // проверка строки ответа на 0 (y)
                {
                    string txt = "(";  // в начале строки с переменными ставим '(' чтобы переменные были внутри скобок. Пример (X1+!X2+!X3)
                    for (int j = 0; j < quantity; j++) // перебираем строку по символам в ней
                    {
                        if (list[i][j] == '0') // если символ в строке 0 то у X(икс) будет стоять знак отрицания
                        {
                            txt += "!";
                        }
                        txt += $"X{j + 1}" + "+"; // запись в строку переменных и ставим +  между ними
                    }
                    txt = txt.Remove(txt.Length - 1); // удаляем лишний +. Например (X1+!X2+!X3+
                    txtAll += txt + ")"; // записывание в общую СКНФ, строку с переменными и в конце ставим ) чтобы закрыть
                }
            }
            if (txtAll.Length == 0)
                return "(Пусто)";
            else
                return txtAll;
        }
    }
}