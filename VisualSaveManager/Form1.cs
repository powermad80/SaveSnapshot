using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
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
            if (comboBox2.Text == "Select Game")
            {
                MessageBox.Show("You must select a game");
                return;
            }
            string game = comboBox2.Text;
            string name = textBox1.Text;
            bool alreadyExists = false;
            var fS = new FileStream("~/backups/" + game + '/' + "saves.txt", FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fS, Encoding.UTF8))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    if (name == line)
                    {
                        alreadyExists = true;
                    }
                }
            }

            if (!alreadyExists)
            {
                using (StreamWriter file =
                    new StreamWriter("~/backups/" + game + '/' + "saves.txt", true))
                {
                    file.WriteLine(name);
                }
            }

            Directory.CreateDirectory("~/backups/" + game + '/' + name + '/');
            DirectoryCopyExample.DirectoryCopy(gameDir, "~/backups/" + game + '/' + name + '/', true);
            if (!alreadyExists)
                comboBox1.Items.Add(name);
            MessageBox.Show("Success!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox2.Text == "Select Game")
            {
                MessageBox.Show("You must select a game");
                return;
            }

            if (comboBox1.Text == null)
            {
                MessageBox.Show("Select a backup to restore.");
                return;
            }

            string game = comboBox2.Text;
            string name = comboBox1.Text;
            DirectoryCopyExample.DirectoryCopy("~/backups/" + game + '/' + name + '/', gameDir, true);
            MessageBox.Show("Success!");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedSave = comboBox1.Text;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == -1)
                return;

            var fS = new FileStream("games.txt", FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fS, Encoding.UTF8))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    if (comboBox2.Text == line)
                    {
                        gameDir = streamReader.ReadLine();
                        if (!Directory.Exists(gameDir))
                        {
                            gameDir = null;
                            comboBox2.SelectedIndex = -1;
                            MessageBox.Show("Game save folder not found. Has it moved, or is it located on a removed drive?");
                            return;
                        }
                    }

                }
                loadList(comboBox1, "~/backups/" + comboBox2.Text + "/saves.txt");
                return;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            loadList(comboBox2, "games.txt");
        }

        public void loadList(ComboBox lst, string file)
        {
            lst.Items.Clear();
            string line;
            StreamReader readFile = new StreamReader(file);
            while ((line = readFile.ReadLine()) != null)
            {
                if ((line[0] != '/') && (line[1] != ':'))
                    lst.Items.Add(line);
            }
            readFile.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
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
                        return;
                    }
                }
                string result = fbd.FileName;
                comboBox2.Items.Add(textBox2.Text);
                using (StreamWriter file = new StreamWriter("games.txt", true))
                {
                    file.WriteLine(textBox2.Text);
                    file.WriteLine(result);
                }
                Directory.CreateDirectory("~/backups/" + textBox2.Text + "/");
                File.Create("~/backups/" + textBox2.Text + "/saves.txt").Close();
                MessageBox.Show("Success!");
            }
            else
            {
                return;
            }
            fbd.Dispose();
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
                string game = comboBox2.Text;
                string name = comboBox1.Text;
                Directory.Delete("~/backups/" + game + '/' + name + '/', true);

                string[] savefile = File.ReadAllLines("~/backups/" + game + '/' + "saves.txt");
                using (StreamWriter f = new StreamWriter("~/backups/" + game + '/' + "saves.txt"))
                {
                    foreach (string s in savefile)
                    {
                        if (s != name)
                            f.WriteLine(s);
                    }
                }
                comboBox1.Items.Remove(name);
                savefile = null;
                MessageBox.Show("Backup deleted.");
            }
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBox1.Text == "Backup Name")
                textBox1.Text = "";
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
                string game = comboBox2.Text;
                bool skip = false;
                Directory.Delete("~/backups/" + game + '/', true);

                string[] gamefile = File.ReadAllLines("games.txt");
                using (StreamWriter f = new StreamWriter("games.txt"))
                {
                    foreach (string s in gamefile)
                    {
                        if (s == game)
                        {
                            skip = true;
                        }
                        else if (!skip)
                        {
                            f.WriteLine(s);
                            skip = false;
                        }
                    }
                }
                gamefile = null;
                comboBox2.Items.Remove(game);
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

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBox2.Text == "Game Name")
                textBox2.Text = "";
        }
    }
}
