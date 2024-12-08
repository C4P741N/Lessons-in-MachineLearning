using Accord.MachineLearning;
using javax.smartcardio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using static edu.stanford.nlp.patterns.surface.SurfacePatternFactory;
using static edu.stanford.nlp.pipeline.CoreNLPProtos;

namespace Lessons_in_MachineLearning
{
    internal class MainProgram
    {
        private static Dictionary<string, Action> _chapterMapping = new Dictionary<string, Action>
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

        private static readonly List<string> _chapters = new List<string> 
            {
                "Chapter 1.Basics of Machine Learning",
                "Chapter 2.Spam Email Filtering",
                "Chapter 3.Twitter Sentiment Analysis",
                "Chapter 4.Foreign Exchange Rate Forecast",
                "Chapter 5.Fair Value of House and Property",
                "Chapter 6.Customer Segmentation",
                "Chapter 7.Music Genre Recommendation",
                "Chapter 8.Handwritten Digit Recognition",
                "Chapter 9.Cyber Attack Detection",
                "Chapter 10.Credit Card Fraud Detection",
                "Chapter 11.What is Next?"
            };
    public static void Main(string[] args)
        {
            string section;

            while (true)
            {
                section = LoadCheckPoint();

                Console.Clear();

                if (section == null)
                {
                    Console.Clear();

                    (Dictionary<string, Action> sections, string chapter_number) = SetChapater();

                    var segment_input = FetchSegment(sections, int.Parse(chapter_number));

                    section = $"{chapter_number}_{segment_input}";
                }

                if (!_chapterMapping.TryGetValue(section, out var run_action))
                {
                    Console.WriteLine("Invalid value");
                    continue;
                }

                int sect = int.Parse(section.Split('_')[0]);

                SimulateTyping($"{_chapters[sect - 1]}... Lets Begin...");
                Console.WriteLine();
                Console.WriteLine();

                UserSettings.SaveSettings(key: "chapter_section", value: section);

                run_action();
            }
        }

        private static string FetchSegment(Dictionary<string, Action> sections, int chapter_number)
        {
            while (true)
            {
                Thread.Sleep(1000);
                SimulateTyping("Which segment?");
                Console.WriteLine();

                foreach (var item in sections)
                {
                    SimulateTyping($"{_chapters[chapter_number - 1]} | Segment {item.Key}");
                    Console.WriteLine();
                }

                var segment_input = Console.ReadLine();

                if (!_chapterMapping.TryGetValue($"{chapter_number}_{segment_input}", out var run_action))
                {
                    Console.WriteLine("Invalid value");
                    continue;
                }

                return segment_input;
            }
        }
        private static (Dictionary<string, Action> sections, string chapter_number) SetChapater()
        {
            while (true)
            {
                Console.Clear();

                SimulateTyping("Which chapter do you want to start with? Or press Q to quit");
                Console.WriteLine();

                Thread.Sleep(1000);
                

                foreach (var chapter in _chapters)
                {
                    SimulateTyping(chapter);
                    Console.WriteLine();
                    Thread.Sleep(500);
                }

                var chapter_input = Console.ReadLine();

                Thread.Sleep(1000);
                if (chapter_input.Equals("Q", StringComparison.CurrentCultureIgnoreCase)) break;

                var selectedChapter = _chapterMapping.Where(c => c.Key.Contains($"{chapter_input}_")).ToDictionary(c => c.Key, c => c.Value);

                if (!selectedChapter.Any())
                {
                    Thread.Sleep(1000);
                    SimulateTyping("Invalid value");
                    Console.WriteLine();
                    continue;
                }

                return (selectedChapter, chapter_input);
            }

            return (null,null);
        }
        private static string LoadCheckPoint()
        {

            var checkpoint = UserSettings.LoadSettings();

            if(!string.IsNullOrEmpty(checkpoint))
            {
                while (true)
                {
                    SimulateTyping("Proceed with preious save? y/n");
                    Console.WriteLine();
                    var ans = Console.ReadLine();

                    if (ans.Equals("n", StringComparison.CurrentCultureIgnoreCase))
                    {
                        return null;
                    }
                    else if(ans.Equals("y", StringComparison.CurrentCultureIgnoreCase))
                    {
                        return checkpoint.Replace("chapter_section:","").Replace("\r\n","");
                    }
                    else
                    {
                        SimulateTyping("Invalid input...");
                        Console.WriteLine();
                    } 
                }
            }

            return null;
        }
        private static void SimulateTyping(string word)
        {
            foreach (var c in word)
            {
                Console.Write(c);  // Print each character without a newline
                Thread.Sleep(10); // Delay between each character to simulate typing
            }
        }
    }
}
