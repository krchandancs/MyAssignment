using System;
using System.Collections.Generic;
using System.Linq;

namespace MyAssignment
{
    public class Menu
    {
        private IList<Option> Options { get; }

        public Menu()
        {
            Options = new List<Option>();
        }

        public void Display()
        {
            for (var i = 0; i < Options.Count; i++)
            {
                Console.WriteLine("{0}. {1}", i + 1, Options[i].Name);
            }

            var choice = Input.ReadInt("Choose an option:", min: 1, max: Options.Count);

            Options[choice - 1].Callback();
        }

        public Menu Add(string option, Action callback)
        {
            return Add(new Option(option, callback));
        }

        public Menu Add(Option option)
        {
            Options.Add(option);
            return this;
        }

        public bool Contains(string option)
        {
            return Options.FirstOrDefault((op) => op.Name.Equals(option)) != null;
        }
    }
    public class Option
    {
        public string Name { get; }

        public Action Callback { get; }

        public Option(string name, Action callback)
        {
            Name = name;
            Callback = callback;
        }

        public override string ToString()
        {
            return Name;
        }
    }
    public static class Output
    {
        public static void WriteLine(ConsoleColor color, string format, params object[] args)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(format, args);
            Console.ResetColor();
        }

        public static void WriteLine(ConsoleColor color, string value)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(value);
            Console.ResetColor();
        }

        public static void WriteLine(string format, params object[] args)
        {
            Console.WriteLine(format, args);
        }

        public static void DisplayPrompt(string format, params object[] args)
        {
            format = format.Trim() + " ";
            Console.Write(format, args);
        }
    }
    public static class Input
    {
        public static int ReadInt(string prompt, int min, int max)
        {
            Output.DisplayPrompt(prompt);
            return ReadInt(min, max);
        }

        public static int ReadInt(int min, int max)
        {
            var value = ReadInt();

            while (value < min || value > max)
            {
                Output.DisplayPrompt("Please enter an integer between {0} and {1} (inclusive)", min, max);
                value = ReadInt();
            }

            return value;
        }

        public static int ReadInt()
        {
            var input = Console.ReadLine();
            int value;

            while (!int.TryParse(input, out value))
            {
                Output.DisplayPrompt("Please enter an integer");
                input = Console.ReadLine();
            }

            return value;
        }

        public static long ReadLong(string prompt, long min, long max)
        {
            Output.DisplayPrompt(prompt);
            return ReadLong(min, max);
        }

        public static long ReadLong(long min, long max)
        {
            var value = ReadLong();

            while (value < min || value > max)
            {
                Output.DisplayPrompt("Please enter an integer between {0} and {1} (inclusive)", min, max);
                value = ReadLong();
            }

            return value;
        }

        public static long ReadLong()
        {
            string input = Console.ReadLine();
            long value;

            while (!long.TryParse(input, out value))
            {
                Output.DisplayPrompt("Please enter an integer");
                input = Console.ReadLine();
            }

            return value;
        }

        public static string ReadString(string prompt)
        {
            Output.DisplayPrompt(prompt);
            return Console.ReadLine();
        }

        public static TEnum ReadEnum<TEnum>(string prompt) where TEnum : struct, IConvertible, IComparable, IFormattable
        {
            var type = typeof(TEnum);

            if (!type.IsEnum)
            {
                throw new ArgumentException("TEnum must be an enumerated type");
            }

            Output.WriteLine(prompt);
            var menu = new Menu();

            var choice = default(TEnum);
            foreach (var value in Enum.GetValues(type))
            {
                menu.Add(Enum.GetName(type, value), () => { choice = (TEnum)value; });
            }

            menu.Display();

            return choice;
        }
    }

}
