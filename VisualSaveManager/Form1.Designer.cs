namespace VisualSaveManager
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.BackupButton = new System.Windows.Forms.Button();
            this.RestoreButton = new System.Windows.Forms.Button();
            this.BackupNameField = new System.Windows.Forms.TextBox();
            this.SaveSelect = new System.Windows.Forms.ComboBox();
            this.GameSelect = new System.Windows.Forms.ComboBox();
            this.AddGame = new System.Windows.Forms.Button();
            this.GameNameField = new System.Windows.Forms.TextBox();
            this.delBackup = new System.Windows.Forms.Button();
            this.delGame = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BackupButton
            // 
            this.BackupButton.Location = new System.Drawing.Point(12, 14);
            this.BackupButton.Name = "BackupButton";
            this.BackupButton.Size = new System.Drawing.Size(75, 23);
            this.BackupButton.TabIndex = 0;
            this.BackupButton.Text = "Backup";
            this.BackupButton.UseVisualStyleBackColor = true;
            this.BackupButton.Click += new System.EventHandler(this.BackupButton_Click);
            // 
            // RestoreButton
            // 
            this.RestoreButton.Location = new System.Drawing.Point(12, 43);
            this.RestoreButton.Name = "RestoreButton";
            this.RestoreButton.Size = new System.Drawing.Size(75, 23);
            this.RestoreButton.TabIndex = 1;
            this.RestoreButton.Text = "Restore";
            this.RestoreButton.UseVisualStyleBackColor = true;
            this.RestoreButton.Click += new System.EventHandler(this.RestoreButton_Click);
            // 
            // BackupNameField
            // 
            this.BackupNameField.Location = new System.Drawing.Point(94, 16);
            this.BackupNameField.Name = "BackupNameField";
            this.BackupNameField.Size = new System.Drawing.Size(259, 20);
            this.BackupNameField.TabIndex = 2;
            this.BackupNameField.Text = "Backup Name";
            this.BackupNameField.MouseClick += new System.Windows.Forms.MouseEventHandler(this.BackupNameField_MouseClick);
            // 
            // SaveSelect
            // 
            this.SaveSelect.FormattingEnabled = true;
            this.SaveSelect.Location = new System.Drawing.Point(94, 45);
            this.SaveSelect.Name = "SaveSelect";
            this.SaveSelect.Size = new System.Drawing.Size(178, 21);
            this.SaveSelect.TabIndex = 3;
            this.SaveSelect.Text = "Select Backup";
            // 
            // GameSelect
            // 
            this.GameSelect.FormattingEnabled = true;
            this.GameSelect.Location = new System.Drawing.Point(12, 72);
            this.GameSelect.Name = "GameSelect";
            this.GameSelect.Size = new System.Drawing.Size(260, 21);
            this.GameSelect.TabIndex = 4;
            this.GameSelect.Text = "Select Game";
            this.GameSelect.SelectedIndexChanged += new System.EventHandler(this.GameSelect_SelectedIndexChanged);
            // 
            // AddGame
            // 
            this.AddGame.Location = new System.Drawing.Point(12, 99);
            this.AddGame.Name = "AddGame";
            this.AddGame.Size = new System.Drawing.Size(75, 23);
            this.AddGame.TabIndex = 5;
            this.AddGame.Text = "Add Game";
            this.AddGame.UseVisualStyleBackColor = true;
            this.AddGame.Click += new System.EventHandler(this.AddGameButton_Click);
            // 
            // GameNameField
            // 
            this.GameNameField.Location = new System.Drawing.Point(94, 101);
            this.GameNameField.Name = "GameNameField";
            this.GameNameField.Size = new System.Drawing.Size(259, 20);
            this.GameNameField.TabIndex = 6;
            this.GameNameField.Text = "Game Name";
            this.GameNameField.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBox2_MouseClick);
            // 
            // delBackup
            // 
            this.delBackup.Location = new System.Drawing.Point(278, 45);
            this.delBackup.Name = "delBackup";
            this.delBackup.Size = new System.Drawing.Size(75, 23);
            this.delBackup.TabIndex = 7;
            this.delBackup.Text = "Delete";
            this.delBackup.UseVisualStyleBackColor = true;
            this.delBackup.Click += new System.EventHandler(this.delBackup_Click);
            // 
            // delGame
            // 
            this.delGame.Location = new System.Drawing.Point(278, 72);
            this.delGame.Name = "delGame";
            this.delGame.Size = new System.Drawing.Size(75, 23);
            this.delGame.TabIndex = 8;
            this.delGame.Text = "Delete";
            this.delGame.UseVisualStyleBackColor = true;
            this.delGame.Click += new System.EventHandler(this.delGame_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(366, 134);
            this.Controls.Add(this.delGame);
            this.Controls.Add(this.delBackup);
            this.Controls.Add(this.GameNameField);
            this.Controls.Add(this.AddGame);
            this.Controls.Add(this.GameSelect);
            this.Controls.Add(this.SaveSelect);
            this.Controls.Add(this.BackupNameField);
            this.Controls.Add(this.RestoreButton);
            this.Controls.Add(this.BackupButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Save Manager";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BackupButton;
        private System.Windows.Forms.Button RestoreButton;
        private System.Windows.Forms.TextBox BackupNameField;
        private System.Windows.Forms.ComboBox SaveSelect;
        private System.Windows.Forms.ComboBox GameSelect;
        private System.Windows.Forms.Button AddGame;
        private System.Windows.Forms.TextBox GameNameField;
        private System.Windows.Forms.Button delBackup;
        private System.Windows.Forms.Button delGame;
    }
}

