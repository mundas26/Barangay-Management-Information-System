using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMIS
{
    public class vars
    {
        public static string Users { get; set; }

        public static string _title = "BARANGAY MANAGEMENT AND INFORMATION SYSTEM";
        public static string _user;
        public static string _defaultName = "mundas26";
        public static string _defaultUser = "mundas26";
        public static string _defaultPass = "admin123";
        public static string _defaultRole = "ADMINISTRATOR";

        public static string folder = @".\Images";
        public static string path = System.IO.Path.Combine(folder);
        public static string defaultImagePath = Application.StartupPath + @"\admin.jpg";
        public static Image _defaultImage = Image.FromFile(File.Exists(path) ? path : defaultImagePath);

        // Generates a random string of numbers with the specified length
        public static string GetRandomNumbers(int length)
        {
            // Initializes a new Random object for generating random numbers
            Random random = new Random();

            // Defines the characters to be used in the random string
            const string chars = "0123456789";

            // Creates a new string by repeating the characters and selecting random ones
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        // Declares a static DateTime variable
        public static DateTime dateTime;

        // Forms a string by concatenating the day from the dateTime variable and its suffix
        public static string formattedDay = dateTime.Day.ToString() + GetDaySurffix(dateTime.Day);

        // Gets the suffix for a given day and assigns it based on specific rules
        public static string GetDaySurffix(int day)
        {
            // Updates the dateTime variable with the current date and time
            dateTime = DateTime.Now;

            // Checks if the day falls within the range 11 to 13, inclusive
            if (day >= 11 && day <= 13)
                return "th";

            // Determines the suffix based on the last digit of the day
            switch (day % 10)
            {
                case 1: return "st";
                case 2: return "nd";
                case 3: return "rd";
                default: return "th";
            }
        }

    }
}
