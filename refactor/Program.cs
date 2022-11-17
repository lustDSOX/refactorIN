using System;
using System.Collections.Generic;
using static System.Console;

namespace ConsoleApp1
{
    internal class Program
    {
        /// <summary>
        /// ТС = транспортные средства
        /// </summary>
        static List<Transport> transports = new List<Transport>(); // список всех ТС
        static void Main(string[] args)
        {
            //заполнение списка
            transports.Add(new Automobile("Nisan", "V8", 4));
            transports.Add(new MotoBike("Yamaha", "14.1", 1, false));
            transports.Add(new Truck("Honda", "SEX PISTOLS", 10, true));

            while (true)
            {
                Print();
            }
        }

        static void Print()
        {
            for (int i = 0; i < transports.Count; i++)//выводим характеристики всех ТС
            {
                Transport item = transports[i];
                item.GetLC();
                item.PrintCharacteristics();
                WriteLine("\n");
            }
            WriteLine("чтобы перейтив режим поиска напишите search, для выхода - exit");
            switch (ReadLine())
            {
                case "search":
                    Search();
                    break;
                case "exit":
                    Environment.Exit(0);
                    break;
                default:
                    Clear();
                    break;
            }
        }

        static void Search()
        {
            string[] text;

            while (true)//пока пользователь не введет корректные данные
            {
                Clear();
                WriteLine("Введите нужные рамки по грузоподъемности (например 10-13)");
                text = ReadLine().Split('-');
                if (text.Length >= 2)
                {
                    if (int.TryParse(text[0], out int i) && int.TryParse(text[1], out int o))
                    {
                        break;
                    }
                }
            }

            WriteLine("___________________________________________");
            int min = Convert.ToInt32(text[0]);
            int max = Convert.ToInt32(text[1]);

            foreach (Transport item in transports)
            {
                if (item.LoadCapacity >= min && item.LoadCapacity <= max)//ищем подходящие ТС и выводим их характеристики
                {
                    item.PrintCharacteristics();
                    WriteLine("\n");
                }
            }

            WriteLine("Чтобы вернуться назад нажмите enter, чтобы воспользоваться поиском еще раз - retry");
            string endText = ReadLine();
            switch (endText)
            {
                case "":
                    Clear();
                    break;
                case "retry":
                    Search();
                    break;
            }
        }

        abstract class Transport
        {
            public string Mark { get; set; }
            public string Model { get; set; }
            public double Horsepower { get; set; }
            public double LoadCapacity { get; set; }
            public virtual void PrintCharacteristics()
            {
                WriteLine("Марка - {0,5} \nмодель - {1,5} \nлошадиные силы - {2,4} \nгрузоподъесность - {3,4}", Mark, Model, Horsepower, LoadCapacity);
            }

            public virtual double GetLC()
            {
                return Horsepower * 2.5 / Math.PI;
            }
        }

        class Automobile : Transport
        {
            public Automobile(string mark, string model, double horsepower)
            {
                Mark = mark;
                Model = model;
                Horsepower = horsepower;
                LoadCapacity = GetLC();
            }
        }
        class MotoBike : Transport
        {
            public bool Wheelchair;
            public override double GetLC()
            {
                double LC = 0;
                if (Wheelchair)
                {
                    LC = Horsepower * 2.5 / Math.PI;
                }
                else
                {
                    LC = 0;
                }
                return LC;
            }
            public MotoBike(string mark, string model, double horsepower, bool wheelchair)
            {
                Mark = mark;
                Model = model;
                Horsepower = horsepower;
                Wheelchair = wheelchair;
                LoadCapacity = GetLC();
            }
            public override void PrintCharacteristics()
            {
                string WC = "";
                if (Wheelchair)
                {
                    WC = "есть";
                }
                else
                {
                    WC = "нет";
                }
                WriteLine("Марка - {0,5} \nмодель - {1,5} \nлошадиные силы - {2,4} \nгрузоподъесность - {3,4}\nколяска - {4,4}", Mark, Model, Horsepower, LoadCapacity, WC);
            }
        }
        class Truck : Transport
        {
            public bool Trailer;
            public override double GetLC()
            {
                double LC = 0;
                if (Trailer)
                {
                    LC = Horsepower * 2.5 / Math.PI * 2;
                }
                else
                {
                    LC = Horsepower * 2.5 / Math.PI;
                }
                return LC;
            }
            public Truck(string mark, string model, double horsepower, bool trailer)
            {
                Mark = mark;
                Model = model;
                Horsepower = horsepower;
                Trailer = trailer;
                LoadCapacity = GetLC();
            }
            public override void PrintCharacteristics()
            {
                string trailer = "";
                if (Trailer)
                {
                    trailer = "есть";
                }
                else
                {
                    trailer = "нет";
                }
                WriteLine("Марка - {0,5} \nмодель - {1,5} \nлошадиные силы - {2,4} \nгрузоподъесность - {3,4}\nтрейлер - {4,4}", Mark, Model, Horsepower, LoadCapacity, trailer);
            }
        }
    }
}
