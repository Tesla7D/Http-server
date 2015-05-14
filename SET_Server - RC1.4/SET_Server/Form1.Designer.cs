namespace SET_Server
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.PortLabel = new System.Windows.Forms.Label();
            this.PortBox = new System.Windows.Forms.NumericUpDown();
            this.DirectoryTextbox = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnDirectory = new System.Windows.Forms.Button();
            this.DefaultFileBox = new System.Windows.Forms.ComboBox();
            this.DefaultFileLabel = new System.Windows.Forms.Label();
            this.DirecotryLabel = new System.Windows.Forms.Label();
            this.NumOfClients = new System.Windows.Forms.NumericUpDown();
            this.NumOfClientsLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PortBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumOfClients)).BeginInit();
            this.SuspendLayout();
            // 
            // PortLabel
            // 
            this.PortLabel.AutoSize = true;
            this.PortLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PortLabel.Location = new System.Drawing.Point(22, 37);
            this.PortLabel.Name = "PortLabel";
            this.PortLabel.Size = new System.Drawing.Size(36, 18);
            this.PortLabel.TabIndex = 1;
            this.PortLabel.Text = "Port";
            // 
            // PortBox
            // 
            this.PortBox.BackColor = System.Drawing.Color.PowderBlue;
            this.PortBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PortBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PortBox.Location = new System.Drawing.Point(96, 35);
            this.PortBox.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.PortBox.Name = "PortBox";
            this.PortBox.Size = new System.Drawing.Size(122, 24);
            this.PortBox.TabIndex = 2;
            // 
            // DirectoryTextbox
            // 
            this.DirectoryTextbox.BackColor = System.Drawing.Color.PowderBlue;
            this.DirectoryTextbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DirectoryTextbox.Enabled = false;
            this.DirectoryTextbox.Location = new System.Drawing.Point(25, 144);
            this.DirectoryTextbox.Name = "DirectoryTextbox";
            this.DirectoryTextbox.Size = new System.Drawing.Size(193, 20);
            this.DirectoryTextbox.TabIndex = 3;
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.Color.SkyBlue;
            this.btnStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.Location = new System.Drawing.Point(25, 288);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 37);
            this.btnStart.TabIndex = 4;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.BackColor = System.Drawing.Color.SkyBlue;
            this.btnStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStop.Location = new System.Drawing.Point(143, 288);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 37);
            this.btnStop.TabIndex = 5;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = false;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnDirectory
            // 
            this.btnDirectory.BackColor = System.Drawing.Color.SkyBlue;
            this.btnDirectory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDirectory.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDirectory.Location = new System.Drawing.Point(25, 181);
            this.btnDirectory.Name = "btnDirectory";
            this.btnDirectory.Size = new System.Drawing.Size(193, 30);
            this.btnDirectory.TabIndex = 6;
            this.btnDirectory.Text = "Choose Directory";
            this.btnDirectory.UseVisualStyleBackColor = false;
            this.btnDirectory.Click += new System.EventHandler(this.btnDirectory_Click);
            // 
            // DefaultFileBox
            // 
            this.DefaultFileBox.BackColor = System.Drawing.Color.PowderBlue;
            this.DefaultFileBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DefaultFileBox.FormattingEnabled = true;
            this.DefaultFileBox.Location = new System.Drawing.Point(25, 245);
            this.DefaultFileBox.Name = "DefaultFileBox";
            this.DefaultFileBox.Size = new System.Drawing.Size(193, 21);
            this.DefaultFileBox.TabIndex = 7;
            // 
            // DefaultFileLabel
            // 
            this.DefaultFileLabel.AutoSize = true;
            this.DefaultFileLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DefaultFileLabel.Location = new System.Drawing.Point(78, 224);
            this.DefaultFileLabel.Name = "DefaultFileLabel";
            this.DefaultFileLabel.Size = new System.Drawing.Size(77, 18);
            this.DefaultFileLabel.TabIndex = 8;
            this.DefaultFileLabel.Text = "DefaultFile";
            // 
            // DirecotryLabel
            // 
            this.DirecotryLabel.AutoSize = true;
            this.DirecotryLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DirecotryLabel.Location = new System.Drawing.Point(65, 123);
            this.DirecotryLabel.Name = "DirecotryLabel";
            this.DirecotryLabel.Size = new System.Drawing.Size(114, 18);
            this.DirecotryLabel.TabIndex = 9;
            this.DirecotryLabel.Text = "DefaultDirectory";
            // 
            // NumOfClients
            // 
            this.NumOfClients.BackColor = System.Drawing.Color.PowderBlue;
            this.NumOfClients.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NumOfClients.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NumOfClients.Location = new System.Drawing.Point(131, 79);
            this.NumOfClients.Name = "NumOfClients";
            this.NumOfClients.Size = new System.Drawing.Size(87, 24);
            this.NumOfClients.TabIndex = 11;
            // 
            // NumOfClientsLabel
            // 
            this.NumOfClientsLabel.AutoSize = true;
            this.NumOfClientsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NumOfClientsLabel.Location = new System.Drawing.Point(22, 81);
            this.NumOfClientsLabel.Name = "NumOfClientsLabel";
            this.NumOfClientsLabel.Size = new System.Drawing.Size(103, 18);
            this.NumOfClientsLabel.TabIndex = 10;
            this.NumOfClientsLabel.Text = "Num of clients";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSkyBlue;
            this.ClientSize = new System.Drawing.Size(253, 337);
            this.Controls.Add(this.NumOfClients);
            this.Controls.Add(this.NumOfClientsLabel);
            this.Controls.Add(this.DirecotryLabel);
            this.Controls.Add(this.DefaultFileLabel);
            this.Controls.Add(this.DefaultFileBox);
            this.Controls.Add(this.btnDirectory);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.DirectoryTextbox);
            this.Controls.Add(this.PortBox);
            this.Controls.Add(this.PortLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "SET Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.PortBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumOfClients)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label PortLabel;
        private System.Windows.Forms.NumericUpDown PortBox;
        private System.Windows.Forms.TextBox DirectoryTextbox;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnDirectory;
        private System.Windows.Forms.ComboBox DefaultFileBox;
        private System.Windows.Forms.Label DefaultFileLabel;
        private System.Windows.Forms.Label DirecotryLabel;
        private System.Windows.Forms.NumericUpDown NumOfClients;
        private System.Windows.Forms.Label NumOfClientsLabel;
    }
}

