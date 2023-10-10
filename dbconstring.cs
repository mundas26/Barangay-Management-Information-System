using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BMIS
{
    class dbconstring
    {
        public static string connection = File.ReadAllText(Environment.CurrentDirectory + @"\config.jer");
    }
}
