using System;
using System.Collections.Generic;

namespace aaaa
{
    class Program
    {
        static void Main(string[] args)
        {
            Dict<string, string> persons = new Dict<string, string>();
            persons.Add("Олег", "Директор");
            persons.Add("Не Олег", "Не директор");
            persons.Add("Мария", "Женщина");
            persons.Add("Эркюль", "Детектив");
            persons.Add("Меченый", "Стрелок");
            persons.Add("Лара", "Крофт");
            persons.Add("Геральт", "Ведьмак");
            persons.Add("Мышь", "Кродется");
            Console.WriteLine("Элементы: ");
            foreach (var item in persons)
                Console.WriteLine("{0} {1}", item.Key, item.Value);
            Console.WriteLine("Количество = {0}", persons.Count);
            Console.WriteLine();
            persons["Мария"] = "Кродется";
            persons["Мышь"] = "Женщина";
            Console.WriteLine("После смены местами Женщина и Кродется:");
            foreach (string key in persons.Keys)
                Console.WriteLine("{0} {1}", key, persons[key]);
            Console.WriteLine();
            persons.Remove("Олег");
            Console.WriteLine("После удаления Олега: ");
            foreach (string key in persons.Keys)
                Console.WriteLine("{0} {1}", key, persons[key]);
            Console.WriteLine();
            Console.WriteLine("Присутствует ли Олег? ");
            if (persons.ContainsKey("Олег") == false) {
                Console.WriteLine("Нет Олега");
            }
            else { Console.WriteLine("Есть Олег"); }
            Console.WriteLine();
            persons.TryGetValue("Лара", out string value);
            Console.WriteLine();
            Console.WriteLine("TryGetValue Лара: {0}", value);
            Console.WriteLine();
            KeyValuePair<string, string>[] array = new KeyValuePair<string, string>[9];
            persons.CopyTo(array, 1);
            Console.WriteLine("Копировать Бонд Джеймс Бонд");
            Console.WriteLine();
            KeyValuePair<string, string> first = new KeyValuePair<string, string>("Бонд", "Джеймс Бонд");
            array[0] = first;
            Console.WriteLine("CopyTo :");
            foreach (var item in array)
                Console.WriteLine("{0} {1}", item.Key, item.Value);
          
        }
    }
}