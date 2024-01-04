using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        public static SqlCommand _SqlCommand; 
        public static string Users { get; set; }

        public static string _title = "BARANGAY MANAGEMENT AND INFORMATION SYSTEM";
        public static string _user;
        public static string _defaultName = "admin";
        public static string _defaultUser = "admin";
        public static string _defaultPass = "admin123";
        public static string _defaultRole = "ADMINISTRATOR";

        public static string folder = @".\Images";
        public static string path = System.IO.Path.Combine(folder);
        public static string defaultImagePath = Application.StartupPath + @"\admin.jpg";
        public static Image _defaultImage = Image.FromFile(File.Exists(path) ? path : defaultImagePath);

        public static string DbString = @"Data Source = .; Initial Catalog = bmis; Integrated Security = True";

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
        public static void GenerateDatabase()
        {
            string initialConnectionString = "Server=.;Database=master;Integrated Security=True;TrustServerCertificate=True";
            SqlConnection initialConnection = new SqlConnection(initialConnectionString);
            initialConnection.Open();

            // Check if the 'bmis' database exists
            string checkDatabaseQuery = "SELECT COUNT(*) FROM sys.databases WHERE name = 'bmis';";
            SqlCommand checkDatabaseCommand = new SqlCommand(checkDatabaseQuery, initialConnection);
            int databaseCount = (int)checkDatabaseCommand.ExecuteScalar();

            // If the 'bmis' database does not exist, create it
            if (databaseCount == 0)
            {
                string createDatabaseQuery = "CREATE DATABASE bmis;";
                _SqlCommand = new SqlCommand(createDatabaseQuery, initialConnection);
                _SqlCommand.ExecuteNonQuery();
            }

            string[] additionalTables = { "tblUser", "tblOfficial", "tblResident", "tblChairmanship", "tblPosition", "tblBlotter", "tblPayment", "tblPurok", "tblVaccine", "tblDocument", "viewVaccine" };

            foreach (var tableName in additionalTables)
            {
                string checkTableQuery = $"USE bmis; SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '{tableName}';";
                SqlCommand checkTableCommand = new SqlCommand(checkTableQuery, initialConnection);
                int tableCount = (int)checkTableCommand.ExecuteScalar();

                if (tableCount == 0)
                {
                    string createTableQuery = GetCreateTableQuery(tableName);
                    SqlCommand createTableCommand = new SqlCommand(createTableQuery, initialConnection);
                    createTableCommand.ExecuteNonQuery();
                }
            }
        }

        private static string GetCreateTableQuery(string tableName)
        {
            switch (tableName)
            {
                case "tblUser":
                return @"
                    USE bmis;
                    CREATE TABLE tblUser (
                        id       INT PRIMARY KEY IDENTITY(1,1),
                        name     VARCHAR(50) NOT NULL,
                        username VARCHAR(50) NOT NULL,
                        password VARCHAR(50) NOT NULL,
                        role     VARCHAR(50) NOT NULL,
                        pic      NVARCHAR(255) NOT NULL);
                        ";

                case "tblOfficial":
                return @"
                    USE bmis;
                    CREATE TABLE tblOfficial (
                        id            INT PRIMARY KEY IDENTITY(1,1),
                        name          VARCHAR(50) NOT NULL,
                        position      VARCHAR(50) NOT NULL,
                        username      VARCHAR(50) NOT NULL,
                        password      VARCHAR(50) NOT NULL,
                        idPic         NVARCHAR(255) NOT NULL,
                        accountStatus VARCHAR(50) NOT NULL);
                        ";

                case "tblResident":
                return @"
                    USE bmis;
                    CREATE TABLE tblResident (
                        id          INT PRIMARY KEY IDENTITY(1,1),
                        nid         VARCHAR(50) NOT NULL,
                        lname       VARCHAR(50) NOT NULL,
                        fname       VARCHAR(50) NOT NULL,
                        mname       VARCHAR(50) NOT NULL,
                        alias       VARCHAR(50) NOT NULL,
                        bdate       DATE NOT NULL,
                        bplace      VARCHAR(50) NOT NULL,
                        age         INT NOT NULL,
                        civilstatus VARCHAR(50) NOT NULL,
                        gender      VARCHAR(50) NOT NULL,
                        religion    VARCHAR(50) NOT NULL,
                        email       VARCHAR(50) NOT NULL,
                        contact     VARCHAR(20) NOT NULL,
                        voters      VARCHAR(20) NOT NULL,
                        precint     VARCHAR(20) NOT NULL,
                        purok       VARCHAR(50) NOT NULL,
                        educational VARCHAR(50) NOT NULL,
                        occupation  VARCHAR(50) NOT NULL,
                        address     NVARCHAR(255) NOT NULL,
                        category    VARCHAR(50) NOT NULL,
                        house       VARCHAR(50) NOT NULL,
                        head        VARCHAR(50) NOT NULL,
                        disability  VARCHAR(50) NOT NULL,
                        status      VARCHAR(50) NOT NULL,
                        pic         VARBINARY(MAX) NOT NULL);
                        ";

                case "tblChairmanship":
                return @"
                    USE bmis;
                    CREATE TABLE tblChairmanship (
                        id          INT PRIMARY KEY IDENTITY(1,1),
                        role        VARCHAR(50) NOT NULL,
                        status      VARCHAR(50) NOT NULL);
                        ";

                case "tblPosition":
                return @"
                    USE bmis;
                    CREATE TABLE tblPosition (
                        id          INT PRIMARY KEY IDENTITY(1,1),
                        position    VARCHAR(50) NOT NULL,
                        status      VARCHAR(50) NOT NULL);
                        ";

                case "tblBlotter":
                return @"
                    USE bmis;
                    CREATE TABLE tblBlotter (
                        id          INT PRIMARY KEY IDENTITY(1,1),
                        fileno      VARCHAR(100) NOT NULL,
                        barangay    VARCHAR(50) NOT NULL,
                        purok       VARCHAR(50) NOT NULL,
                        incident    VARCHAR(255) NOT NULL,
                        place       VARCHAR(255) NOT NULL,
                        idate       DATE NOT NULL,
                        itime       VARCHAR(50) NOT NULL,
                        complainant VARCHAR(100) NOT NULL,
                        witness1    VARCHAR(100) NOT NULL,
                        witness2    VARCHAR(100) NOT NULL,
                        narrative   TEXT NOT NULL,
                        status      VARCHAR(50) DEFAULT 'Unsettled' NOT NULL);
                        ";

                case "tblPayment":
                return @"
                    USE bmis;
                    CREATE TABLE tblPayment (
                        id          INT PRIMARY KEY IDENTITY(1,1),
                        refno       VARCHAR(100) NOT NULL,
                        name        VARCHAR(100) NOT NULL,
                        type        VARCHAR(50) NOT NULL,
                        amount      DECIMAL(18, 2) NOT NULL,
                        sdate       DATE NOT NULL,
                        status      VARCHAR(50) DEFAULT 'Pending' NOT NULL,
                        username    VARCHAR(50) NOT NULL);
                        ";

                case "tblPurok":
                return @"
                    USE bmis;
                    CREATE TABLE tblPurok (
                        id          INT PRIMARY KEY IDENTITY(1,1),
                        purok       VARCHAR(50) NOT NULL,
                        chairman    VARCHAR(100) NOT NULL);
                        ";

                case "tblVaccine":
                return @"
                    USE bmis;
                    CREATE TABLE tblVaccine (
                        id          INT PRIMARY KEY IDENTITY(1,1),
                        rid         INT NOT NULL,
                        vaccine     VARCHAR(50) NOT NULL,
                        status      VARCHAR(50) NOT NULL);
                        ";

                case "tblDocument":
                return @"
                    USE bmis;
                    CREATE TABLE tblDocument (
                        id          INT PRIMARY KEY IDENTITY(1,1),
                        refno       VARCHAR(50) NOT NULL,
                        type        VARCHAR(50) NOT NULL,
                        details1    VARCHAR(50) NULL,
                        details2    VARCHAR(50) NULL,
                        details3    VARCHAR(50) NULL,
                        details4    VARCHAR(50) NULL,
                        details5    VARCHAR(50) NULL,
                        details6    VARCHAR(50) NULL,
                        idate       DATE NOT NULL,
                        [user]      VARCHAR(50) NOT NULL);
                        ";

                case "viewVaccine":
                return @"
                    CREATE VIEW viewVaccine 
                    AS SELECT  
                        resident.id,
                        resident.lname, 
                        resident.fname, 
                        resident.mname,  
                        ISNULL(vaccine.vaccine,'') vaccine,
                        ISNULL(vaccine.status, 'NOT YET VACCINATED') status 
                    FROM tblResident resident 
                    LEFT JOIN tblVaccine vaccine 
                        ON resident.id = vaccine.id";

                default:
                return string.Empty;
            }
        }
    }
}
