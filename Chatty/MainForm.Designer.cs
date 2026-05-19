using Microsoft.VisualBasic.ApplicationServices;
using System.Drawing.Text;

namespace Chatty
{
    partial class MainForm : Form
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
        /// 
       private void ShowMedia()
        {
           
            this.SuspendLayout();

        }
        private void InitializeComponent()
        {
<<<<<<< HEAD
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            textBox1 = new TextBox();
            button1 = new Button();
=======
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            label1 = new Label();
            label3 = new Label();
            panel2 = new Panel();
            button1 = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            UserWindowTxtArea = new RichTextBox();
            BotWindowTxtArea = new RichTextBox();
            pictureBox1 = new PictureBox();
            panel2.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
>>>>>>> eeb40eb (Add project files.)
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.SlateBlue;
            label1.Location = new Point(192, 9);
            label1.Name = "label1";
            label1.Size = new Size(0, 15);
            label1.TabIndex = 0;
            label1.Click += label1_Click;
            // 
<<<<<<< HEAD
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(284, 210);
            label2.Name = "label2";
            label2.Size = new Size(56, 15);
            label2.TabIndex = 1;
            label2.Text = "ChatBot: ";
            // 
=======
>>>>>>> eeb40eb (Add project files.)
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(274, 35);
            label3.Name = "label3";
            label3.Size = new Size(0, 15);
            label3.TabIndex = 3;
            label3.Click += label3_Click;
            // 
<<<<<<< HEAD
            // textBox1
            // 
            textBox1.Location = new Point(284, 239);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(211, 23);
            textBox1.TabIndex = 5;
            textBox1.Multiline = true;
            textBox1.KeyDown += textBox1_TextChanged;
            // 
            // button1
            // 
            button1.Location = new Point(289, 271);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 6;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click_1;
=======
            // panel2
            // 
            panel2.Controls.Add(button1);
            panel2.Location = new Point(891, 394);
            panel2.Name = "panel2";
            panel2.Size = new Size(103, 99);
            panel2.TabIndex = 7;
            // 
            // button1
            // 
            button1.BackColor = Color.Transparent;
            button1.BackgroundImage = (Image)resources.GetObject("button1.BackgroundImage");
            button1.BackgroundImageLayout = ImageLayout.Center;
            button1.Location = new Point(17, 16);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 0;
            button1.Text = "Send";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click_2;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 89.1640854F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10.8359137F));
            tableLayoutPanel1.Controls.Add(panel2, 1, 2);
            tableLayoutPanel1.Controls.Add(UserWindowTxtArea, 0, 2);
            tableLayoutPanel1.Controls.Add(BotWindowTxtArea, 0, 0);
            tableLayoutPanel1.Location = new Point(12, 155);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 87.29792F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 12.7020788F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 104F));
            tableLayoutPanel1.Size = new Size(997, 496);
            tableLayoutPanel1.TabIndex = 8;
            // 
            // UserWindowTxtArea
            // 
            UserWindowTxtArea.Location = new Point(3, 394);
            UserWindowTxtArea.Name = "UserWindowTxtArea";
            UserWindowTxtArea.Size = new Size(882, 99);
            UserWindowTxtArea.TabIndex = 8;
            UserWindowTxtArea.Text = "";
            // 
            // BotWindowTxtArea
            // 
            BotWindowTxtArea.Location = new Point(3, 3);
            BotWindowTxtArea.Name = "BotWindowTxtArea";
            BotWindowTxtArea.Size = new Size(882, 329);
            BotWindowTxtArea.TabIndex = 9;
            BotWindowTxtArea.Text = "";
            BotWindowTxtArea.Click += ShowWelcome;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(66, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(572, 137);
            pictureBox1.TabIndex = 9;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
>>>>>>> eeb40eb (Add project files.)
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1021, 750);
<<<<<<< HEAD
            Controls.Add(button1);
            Controls.Add(textBox1);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "MainForm";
=======
            Controls.Add(pictureBox1);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(label3);
            Controls.Add(label1);
            Name = "MainForm";
            panel2.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
>>>>>>> eeb40eb (Add project files.)
            ResumeLayout(false);
            PerformLayout();



        }

        #endregion

        private Label label1;
<<<<<<< HEAD
        private Label label2;
        private Label label3;
        private TextBox textBox1;
        private Button button1;
=======
        private Label label3;
        private Panel panel2;
        private Button button1;
        private TableLayoutPanel tableLayoutPanel1;
        private RichTextBox BotWindowTxtArea;
        private RichTextBox UserWindowTxtArea;
        private PictureBox pictureBox1;
>>>>>>> eeb40eb (Add project files.)
    }
}