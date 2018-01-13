using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.SQLite;
using Dapper;
using Dapper.Contrib;
using Dapper.Contrib.Extensions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace VisualSaveManager
{
    public partial class Form1 : Form
    {
        public string selectedSave;
        public string paths = "~/backups/";
        public string gameDir;
        //IList<string> saveList = new List<string>();
        //IList<string> gameList = new List<string>();
        
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            List<string> games;
            using (IDbConnection con = Modules.DBConnection())
            {
                con.Open();
                games = con.Query<string>("SELECT Name FROM GAMES").ToList<string>();
                con.Close();
            }

            foreach (string game in games)
            {
                GameSelect.Items.Add(game);
            }

        }


        private void delBackup_Click(object sender, EventArgs e)
        {
            DialogResult cont = MessageBox.Show("Are you sure you want to delete the selected backup?", "Warning", MessageBoxButtons.YesNo);

            if (cont == DialogResult.No)
            {
                return;
            }
            else if (cont == DialogResult.Yes)
            {
                using (SQLiteConnection con = Modules.DBConnection())
                {
                    con.Open();
                    new SQLiteCommand("DELETE FROM SAVES WHERE Name = '" + SaveSelect.Text + "'", con).ExecuteNonQuery();
                    con.Close();
                }

                Directory.Delete("~/backups/" + GameSelect.Text + '/' + SaveSelect.Text + '/', true);
                SaveSelect.Items.Remove(SaveSelect.Text);
                SaveSelect.SelectedIndex = -1;
                MessageBox.Show("Backup deleted.");
            }
        }


        private void delGame_Click(object sender, EventArgs e)
        {
            DialogResult cont = MessageBox.Show("Are you sure you want to delete all this game's backups?", "Warning", MessageBoxButtons.YesNo);
            if (cont == DialogResult.No)
            {
                return;
            }
            else if (cont == DialogResult.Yes)
            {

                using (SQLiteConnection con = Modules.DBConnection())
                {
                    con.Open();
                    new SQLiteCommand("DELETE FROM GAMES WHERE Name = '" + GameSelect.Text + "'", con).ExecuteNonQuery();
                    new SQLiteCommand("DELETE FROM SAVES WHERE Game = '" + GameSelect.Text + "'", con).ExecuteNonQuery();
                    con.Close();
                }

                Directory.Delete("~/backups/" + GameSelect.Text + '/', true);

                GameSelect.Items.Remove(GameSelect.Text);
                SaveSelect.Items.Clear();
                GameSelect.SelectedIndex = -1;
                SaveSelect.SelectedIndex = -1;
                MessageBox.Show("All backups deleted.");
            }


        }

        public static long GetDirSize(string dir)
        {
            DirectoryInfo d = new DirectoryInfo(dir);
            return DirSize(d);
        }

        public static long DirSize(DirectoryInfo d)
        {
            long size = 0;
            // Add file sizes.
            FileInfo[] fis = d.GetFiles();
            foreach (FileInfo fi in fis)
            {
                size += fi.Length;
            }
            // Add subdirectory sizes.
            DirectoryInfo[] dis = d.GetDirectories();
            foreach (DirectoryInfo di in dis)
            {
                size += DirSize(di);
            }
            return size;
        }


        private void textBox2_MouseClick(object sender, MouseEventArgs e)
        {
            if (GameNameField.Text == "Game Name")
                GameNameField.Text = "";
        }

        private void GameSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GameSelect.SelectedIndex == -1)
                return;

            using (IDbConnection con = Modules.DBConnection())
            {
                con.Open();
                string gamePath = con.Query<string>("SELECT Path FROM GAMES WHERE Name = '" + GameSelect.Text + "'").First();

                if (!Directory.Exists(gamePath))
                {
                    GameSelect.SelectedIndex = -1;
                    MessageBox.Show("Game save folder not found. Has it moved, or is it located on a removed drive?");
                    con.Close();
                    return;
                }

                List<string> saves = con.Query<string>("SELECT Name FROM SAVES WHERE Game = '" + GameSelect.Text + "'").ToList<string>();
                con.Close();
                SaveSelect.Items.Clear();
                foreach (string save in saves)
                {
                    SaveSelect.Items.Add(save);
                }
            }
        }

        private void AddGameButton_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog fbd = new CommonOpenFileDialog();
            fbd.IsFolderPicker = true;
            if (fbd.ShowDialog() == CommonFileDialogResult.Ok)
            {
                if (GetDirSize(fbd.FileName) > 5000000)
                {
                    var confirmResult = MessageBox.Show("Directory size exceeds 50MB, save directories are usually much smaller. Proceeding with backup may take a while, continue anyway?", "Large Directory Detected", MessageBoxButtons.OKCancel);
                    if (confirmResult == DialogResult.Cancel)
                    {
                        fbd.Dispose();
                        return;
                    }
                }
                string result = fbd.FileName;

                using (IDbConnection con = Modules.DBConnection())
                {
                    GameObj game = new GameObj();
                    game.Name = GameNameField.Text;
                    game.Path = result;

                    con.Open();
                    con.Insert<GameObj>(game);
                    con.Close();
                }
                GameSelect.Items.Add(GameNameField.Text);
                Directory.CreateDirectory("~/backups/" + GameNameField.Text + "/");
                MessageBox.Show("Success!");
            }
            else
            {
                fbd.Dispose();
                return;
            }
            fbd.Dispose();
        }

        private void RestoreButton_Click(object sender, EventArgs e)
        {
            string path;

            if (GameSelect.Text == "Select Game")
            {
                MessageBox.Show("You must select a game");
                return;
            }

            if (SaveSelect.Text == null)
            {
                MessageBox.Show("Select a backup to restore.");
                return;
            }

            using (IDbConnection con = Modules.DBConnection())
            {
                con.Open();
                path = con.Query<string>("SELECT Path FROM GAMES WHERE Name = '" + GameSelect.Text + "'").First<string>();
                con.Close();
            }

                DirectoryCopyExample.DirectoryCopy("~/backups/" + GameSelect.Text + '/' + SaveSelect.Text + '/', path, true);
            MessageBox.Show("Success!");
        }

        private void BackupButton_Click(object sender, EventArgs e)
        {
            if (GameSelect.SelectedIndex == -1)
            {
                MessageBox.Show("You must select a game");
                return;
            }

            using (IDbConnection con = Modules.DBConnection())
            {
                con.Open();
                if (con.Query<string>("SELECT Game FROM SAVES WHERE Name = '" + BackupNameField.Text + "' AND Game = '" + GameSelect.Text + "'").ToList<string>().Count > 0)
                {
                    MessageBox.Show("A backup with this name already exists, please enter another name.");
                    con.Close();
                    return;
                }

                SaveObj newSave = new SaveObj();
                newSave.Game = GameSelect.Text;
                newSave.Name = BackupNameField.Text;
                newSave.Path = "~/backups/" + GameSelect.Text + '/' + BackupNameField.Text + '/';

                con.Insert<SaveObj>(newSave);
                string path = con.Query<string>("SELECT Path FROM GAMES WHERE Name = '" + GameSelect.Text + "'").First<string>();
                con.Close();

                Directory.CreateDirectory(newSave.Path);
                DirectoryCopyExample.DirectoryCopy(path, newSave.Path, true);
                SaveSelect.Items.Add(BackupNameField.Text);
            }

            MessageBox.Show("Success!");
        }

        private void BackupNameField_MouseClick(object sender, MouseEventArgs e)
        {
            if (BackupNameField.Text == "Backup Name")
                BackupNameField.Text = "";
        }
    }
}
