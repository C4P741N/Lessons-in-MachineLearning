using sun.swing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lessons_in_MachineLearning
{
    internal static class UserSettings
    {
        private static string _file_path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, "userSettings.txt");
        public static void SaveSettings(string key, string value)
        {
            try
            {
                // Check if file exists, if not, create a new one
                if (!File.Exists(_file_path))
                {
                    using (StreamWriter sw = File.CreateText(_file_path))
                    {
                        sw.WriteLine($"{key}:{value}"); // Write initial key-value pair
                    }
                    Console.WriteLine("File created and settings saved.");
                }
                else
                {
                    // Clear the file by writing an empty string to it
                    File.WriteAllText(_file_path, string.Empty);

                    // Append the new setting to the existing file
                    using (StreamWriter sw = File.AppendText(_file_path))
                    {
                        sw.WriteLine($"{key}:{value}"); // Append key-value pair
                    }
                    Console.WriteLine("Settings appended successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving settings: {ex.Message}");
            }
        }
        public static string LoadSettings()
        {
            try
            {
                if (File.Exists(_file_path))
                {
                    var settings_text = File.ReadAllText(_file_path);
                    return settings_text;
                }
                else
                {
                    Console.WriteLine("Settings file not found.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading settings: {ex.Message}");
                return null;
            }
        }

    }
}
