// See https://aka.ms/new-console-template for more information
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace HelloWorldApplication
{
    class List
    {
        public void Menu()
        {
            Console.WriteLine("Console Menu");
            Console.WriteLine("1 - Import words from File");
            Console.WriteLine("2 - Bubble sort words");
            Console.WriteLine("3 - LINQ/Lambda sort words");
            Console.WriteLine("4 - Count the distinct words");
            Console.WriteLine("5 - Take the first 50 words");
            Console.WriteLine("6 - Reverse print the words");
            Console.WriteLine("7 - Get and display words that end with 'a' and display the count");
            Console.WriteLine("8 - Get and display words that include 'm' and display the count");
            Console.WriteLine("9 - Get and display words that are less than 4 characters long and include the letter 'I', and display the count");
            Console.WriteLine("x - Exit");

        }

        public void ImportFromFile(IList<string> words)
        {
            try
            {
                words.Clear();
                string File = "Words.txt";
                using (StreamReader reader = new StreamReader(File))
                {
                    string Line;
                    while ((Line = reader.ReadLine())
                        != null)
                    {
                        words.Add(Line);
                    }
                }

                Console.WriteLine($"Read successfully, the number of words is: {words.Count}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error reading!{e.Message}");
            }
        }

        public IList<string> BubbleSort(IList<string> words)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            var order = words;

            for (int i = 0; i < order.Count - 1; i++)
            {
                for (int j = 0; j < order.Count - 1 - i; j++)
                {
                    if (string.Compare(order[j], order[j + 1]) > 0)
                    {
                        string temp = order[j];
                        order[j] = order[j + 1];
                        order[j + 1] = temp;
                    }
                }
            }
            watch.Stop();
            Console.WriteLine($"BubbleSort took {watch.ElapsedMilliseconds} ms.");

            return order;
        }

        public IList<string> LINQSort(IList<string> words)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            var order = words.OrderBy(w => w).ToList();

            watch.Stop();
            Console.WriteLine($"LINQSort took {watch.ElapsedMilliseconds} ms.");

            return order;
        }

        public void CountDistinct(IList<string> words)
        {
            var number = words.Distinct().Count();
            Console.WriteLine($"The number of unique words is: { number}");
        }

        public void First50(IList<string> words)
        {
            var fifty = words.Take(50).ToList();
            foreach (var item in fifty)
                Console.WriteLine(item);
        }

        public void ReversePrint(IList<string> words)
        {
            var Reverse = words.Reverse().ToList();
            foreach (var item in Reverse)
                Console.WriteLine(item);
        }
        public void EndingWithA(IList<string> words)
        {
            var EndA = words.Where(w => w.EndsWith("a")).ToList();
            var number = words.Where(w => w.EndsWith("a")).Count();
            foreach (var item in EndA)
                Console.WriteLine(item);
            Console.WriteLine($"The number of words found is: {number}");
        }
        public void IncludingM(IList<string> words)
        {
            var IncludM = words.Where(w => w.Contains("m")).ToList();
            var number = words.Where(w => w.Contains("m")).Count();
            foreach (var item in IncludM)
                Console.WriteLine(item);
            Console.WriteLine($"The number of words found is: {number}");
        }
        public void IncludingI(IList<string> words)
        {
            var IncludI = words.Where(w => w.Contains("i") && w.Length<4 ).ToList();
            var number = words.Where(w => w.Contains("i") && w.Length < 4).Count();
            foreach (var item in IncludI)
                Console.WriteLine(item);
            Console.WriteLine($"The number of words found is: {number}");
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            IList<string> words = new List<string>();

            List l = new List();

            while (true)
            {
                try
                {
                    l.Menu();

                    Console.Write("Select an option: ");
                    char choice;
                    choice = Char.Parse(Console.ReadLine());
                    Console.WriteLine();

                    switch (choice)
                    {
                        case '1':
                            l.ImportFromFile(words);
                            break;
                        case '2':
                            words = l.BubbleSort(words);
                            break;
                        case '3':
                            words = l.LINQSort(words);
                            break;
                        case '4':
                            l.CountDistinct(words);
                            break;
                        case '5':
                            l.First50(words);
                            break;
                        case '6':
                            l.ReversePrint(words);
                            break;
                        case '7':
                            l.EndingWithA(words);
                            break;
                        case '8':
                            l.IncludingM(words);
                            break;
                        case '9':
                            l.IncludingI(words);
                            break;
                        case 'x':
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }

                    Console.WriteLine();
                }
                catch (Exception)
                {
                    Console.WriteLine();
                    Console.WriteLine("Input errors, please re-enter");
                    Console.WriteLine();
                }
            }

        }

    }
}
