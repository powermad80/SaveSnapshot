using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace VisualSaveManager
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (!File.Exists("saves.sqlite"))
            {
                SQLiteConnection.CreateFile("saves.sqlite");
                using (SQLiteConnection con = Modules.DBConnection())
                {
                    con.Open();
                    new SQLiteCommand("CREATE TABLE GAMES (Name varchar(100), Path varchar(1000))", con).ExecuteNonQuery();
                    new SQLiteCommand("CREATE TABLE SAVES (Game varchar (100), Name varchar(100), Path varchar(1000))", con).ExecuteNonQuery();
                    con.Close();
                }

            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
