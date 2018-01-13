using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;
using Dapper;
using Dapper.Contrib;
using Dapper.Contrib.Extensions;

namespace VisualSaveManager
{
    public static class Modules
    {
        public static SQLiteConnection DBConnection()
        {
            return new SQLiteConnection("Data Source=saves.sqlite;Version=3;");
        }

    }

    [Table("GAMES")]
    public class GameObj
    {
        public string Name { get; set; }

        public string Path { get; set; }
    }

    [Table("SAVES")]
    public class SaveObj
    {
        public string Game { get; set; }

        public string Name { get; set; }

        public string Path { get; set; }
    }
}

