using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace Count_Unique_Words
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = "notes/";// Укажите расположение директории с необходимыми блокнотиками
            Dictionary<string, int> countOfUniqueWords = new Dictionary<string, int> { };

            //Считывание с текстового файла Text.txt и вывод содержимого на экран
            var kingAndJester = File.ReadAllText(path + "Text.txt", System.Text.Encoding.Default);
            Console.WriteLine(kingAndJester);

            File.WriteAllText(path + "TextEncoding.txt", kingAndJester); // Копирование файла в TextEncoding.txt

            //Основное действие - правила, подсчёт, смена регистра, запись результата в словарик
            var words = kingAndJester.Split(' ', '-', '\n', ',', ':', '\t', '.', '\0', '"', '\'', '!', '?').Where(emptiness => !string.IsNullOrEmpty(emptiness));
            var uniqueWords = words.Select(emptiness => emptiness.ToLower().Trim()).Distinct();
            var result = new Dictionary<string, int>();
            foreach (var word in uniqueWords)
            {
                result.Add(word, words.Count(emptiness => emptiness.ToLower().Equals(word)));
            }
            result = result.OrderByDescending(emptiness => emptiness.Value).ToList().ToDictionary(key => key.Key, value => value.Value);

            //Запись результата в файл Count.txt
            using (StreamWriter streamWriter = new StreamWriter(path + "Count.txt"))
            {
                foreach (KeyValuePair<string, int> keyValue in result)
                {
                    streamWriter.WriteLine(keyValue.Key + " - " + keyValue.Value);
                }
            }
        }
    }
}
