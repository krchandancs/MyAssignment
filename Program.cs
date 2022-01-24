using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace MyAssignment
{
    class Program
    {
        public static void Main(string[] args)
        {
            bool continueAction = true;
            while (continueAction)
            {
                Console.WriteLine("*------------ Menu -------------*");
                var menu = new Menu()
                    .Add("Word Count", () => WordCount())
                    .Add("Social Distnace", () => SocialDistance())
                    .Add("Exit", () => continueAction = false);
                menu.Display();
            }
        }

        private static void WordCount()
        {
            try
            {
                string text = string.Empty;
                //Reading text file from URL
                using (HttpClient wc = new HttpClient())
                {
                    text = wc.GetStringAsync("https://raw.githubusercontent.com/krchandancs/Sample/master/sample.txt?_sm_au_=iVVjQ2nQtSjlPjS6tQfsjK3vssBWM").Result;
                }

                //Uncomment this block if want to read from local files.
                #region Read From Local file
                //string filePath = @"C:\Users\Contents\sample.txt";
                //string text = System.IO.File.ReadAllText(filePath);
                #endregion

                string[] strarr = text.TrimEnd().Split(" ");
                Dictionary<string, int> wordCounter = new Dictionary<string, int>();
                foreach (string s in strarr)
                {
                    wordCounter[s] = wordCounter.TryGetValue(s, out int value) ? value + 1 : 1;
                }
                var result = wordCounter.OrderByDescending(x => x.Value);
                Console.WriteLine("*----------- Result ------------*");
                foreach (var item in result)
                {
                    Console.WriteLine($"{item.Key} : {item.Value}");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        private static void SocialDistance()
        {
            Console.WriteLine("Enter binary string : ");
            string strcontent = Console.ReadLine();
            Console.WriteLine("Enter distance(k): ");
            int k = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("*----------- Result ------------*");
            int resultcount = 0, len = strcontent.Length;
            int zerocounter = 0;
            if (!strcontent.Contains('1'))
                zerocounter = 1;
            for (int j = 0; j < len; ++j)
            {
                if (strcontent[j] == '0')
                    zerocounter++;
                else
                    zerocounter--;

                if (zerocounter == k + 1)
                {
                    resultcount++;
                    zerocounter = 0;
                }

                if (zerocounter < 0)
                    zerocounter = 0;
            }
            Console.WriteLine(resultcount);
        }
    }
}
