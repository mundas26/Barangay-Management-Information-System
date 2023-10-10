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
        public static string _defaultPass = "admin1234";
        public static string _defaultRole = "ADMINISTRATOR";

        public static string folder = @".\Images";
        public static string path = System.IO.Path.Combine(folder);
        public static string defaultImagePath = Application.StartupPath + @"\admin.jpg";
        public static Image _defaultImage = Image.FromFile(File.Exists(path) ? path : defaultImagePath);

        public static string GetRandomNumbers(int length)
        {
            Random random = new Random();
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s [random.Next(s.Length)]).ToArray());
        }

        public static DateTime dateTime;
        public static string formattedDay = dateTime.Day.ToString() + GetDaySurffix(dateTime.Day);
        public static string GetDaySurffix(int day)
        {
            dateTime = DateTime.Now;
            if (day >= 11 && day <= 13)
                return "th";
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
