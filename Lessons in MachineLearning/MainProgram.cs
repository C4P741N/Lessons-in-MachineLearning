using System;
using System.Collections.Generic;
using System.Linq;

namespace Lessons_in_MachineLearning
{
    internal class MainProgram
    {
        public static void Main(string[] args)
        {
            var chapterMapping = new Dictionary<string, Action>
            {
                { "1_1", Chapter01_1.Program.Run },
                { "1_2", Chapter01_2.Program.Run },
                { "2_1", Chapter02_1.Program.Run },
                { "2_2", Chapter02_2.Program.Run },
                { "2_3", Chapter02_3.Program.Run },
                { "3_1", Chapter03_1.Program.Run },
                { "3_2", Chapter03_2.Program.Run },
                { "3_3", Chapter03_3.Program.Run },
                { "3_4", Chapter03_4.Program.Run },
                { "4_1", Chapter04_1.Program.Run },
                { "4_2", Chapter04_2.Program.Run },
                { "4_3", Chapter04_3.Program.Run },
                { "4_4", Chapter04_4.Program.Run },
                { "5_1", Chapter05_1.Program.Run },
                { "5_2", Chapter05_2.Program.Run },
                { "5_3", Chapter05_3.Program.Run },
                { "6_1", Chapter06_1.Program.Run },
                { "6_2", Chapter06_2.Program.Run },
                { "6_3", Chapter06_3.Program.Run },
                { "7_1", Chapter07_1.Program.Run },
                { "7_2", Chapter07_2.Program.Run },
                { "8_1", Chapter08_1.Program.Run },
                { "8_2", Chapter08_2.Program.Run },
                { "8_3", Chapter08_3.Program.Run },
                { "9_1", Chapter09_1.Program.Run },
                { "9_2", Chapter09_2.Program.Run },
                { "9_3", Chapter09_3.Program.Run },
                { "10_1", Chapter10_1.Program.Run },
                { "10_2", Chapter10_2.Program.Run },
                { "10_3", Chapter10_3.Program.Run },
                { "11_1", Chapter11_1.Program.Run },
                { "11_2", Chapter11_2.Program.Run },
                { "11_3", Chapter11_3.Program.Run }
            };

            if (args.Length > 0 && chapterMapping.ContainsKey(args[0]))
            {
                chapterMapping[args[0]]();
            }
            else
            {
                var possibleEntries = string.Join(" |", chapterMapping.Keys.Where(k => k.Contains(args[0])).ToList());

                if (string.IsNullOrEmpty(possibleEntries))
                {
                    Console.WriteLine($"Input is not even close budy");
                }
                else Console.WriteLine($"Invalid input...were you to input {possibleEntries}?");
            }
        }
    }
}
