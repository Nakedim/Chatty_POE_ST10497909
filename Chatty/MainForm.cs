<<<<<<< HEAD
﻿using System;
=======
﻿using Microsoft.VisualBasic;
using System;
>>>>>>> eeb40eb (Add project files.)
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chatty
{

    public partial class MainForm : Form
    {
<<<<<<< HEAD
        String Name;
        String Message;
        String botName = "ChatBotty";
        String logo = @"  ____      _               ____                       _ _                _                
 / ___|   _| |__   ___ _ __/ ___|  ___  ___ _   _ _ __(_) |_ _   _       / \   _ __  _ __  
| |  | | | | '_ \ / _ \ '__\___ \ / _ \/ __| | | | '__| | __| | | |     / _ \ | '_ \| '_ \ 
| |__| |_| | |_) |  __/ |   ___) |  __/ (__| |_| | |  | | |_| |_| |    / ___ \| |_) | |_) |
 \____\__, |_.__/ \___|_|  |____/ \___|\___|\__,_|_|  |_|\__|\__, |   /_/   \_\ .__/| .__/ 
      |___/                                                  |___/            |_|   |_|    ";
=======
        String Name = "Sugar";

        String Message;
        String botName = "ChatBotty";
        String logo = @" ++----------------------------------------------------------------------++
++----------------------------------------------------------------------++
||                                                                      ||
|| ____      _ ____ _ _   _             ||
||     / ___|   _| |__ ___ _ __   / ___| |__ __ _| |_| |_ _   _     ||
||    | |  | | | | '_ \ / _ \ '__| | |   | '_ \ / _` | __| __| | | |    ||
||    | |__| |_| | |_) |  __/ |    | |___| | | | (_| | |_| |_| |_| |    ||
||     \____\__, |_.__/ \___|_|     \____|_| |_|\__,_|\__|\__|\__, |    ||
||          |___/                                             |___/     ||
||                                                                      ||
++----------------------------------------------------------------------++
++----------------------------------------------------------------------++ ";


>>>>>>> eeb40eb (Add project files.)


        public MainForm()
        {
            InitializeComponent();
<<<<<<< HEAD
=======
            //greetings
            MainForm_Load(this, EventArgs.Empty);

            button1.Text = "Send M";
            


            //rtb 1: userChat

            UserWindowTxtArea.Text = "Sugar";
            BotWindowTxtArea.Text = "How are you ?";

>>>>>>> eeb40eb (Add project files.)
        }



        private void MainForm_Load(object sender, EventArgs e)
        {
<<<<<<< HEAD
            ShowWelcome(this, EventArgs.Empty);
=======
           
>>>>>>> eeb40eb (Add project files.)

            try
            {
                SoundPlayer player = new SoundPlayer("Welcome Message.wav");
                player.Play();

<<<<<<< HEAD

=======
               //ShowWelcome(this, EventArgs.Empty);
>>>>>>> eeb40eb (Add project files.)

            }
            catch
            {
                MessageBox.Show("No greeting or logo was found");

            }



        }


        private void greet(object sender, EventArgs e)
        {
            MessageBox.Show("Hello, Welcome to CyberBotty");


            MessageBox.Show("My name is " + botName + " How are you ?");



        }

        private void ShowWelcome(object sender, EventArgs e)
        {
<<<<<<< HEAD
            DialogResult result = MessageBox.Show("Welcome to the CyberSecurity App", "Welcome Message"
=======
            DialogResult result = MessageBox.Show("Welcome to the CyberSecurity App", "What is your Name"
>>>>>>> eeb40eb (Add project files.)
                , MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.No)
            {
                Application.Exit();
            }
<<<<<<< HEAD
=======
            if (result == DialogResult.Yes)
            {
                string name = Interaction.InputBox("Enter your Name");
            }
>>>>>>> eeb40eb (Add project files.)


        }
        private void ShowLogo()
        {

            MessageBox.Show(logo);


        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }


        private void button1_Click(object sender, EventArgs e)
        {
<<<<<<< HEAD

=======
            
>>>>>>> eeb40eb (Add project files.)
            MessageBox.Show("How are you " + Name);
        }

        private void textBox1_TextChanged(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                MessageBox.Show(Message);

            }
<<<<<<< HEAD
            Message = textBox1.Text;
=======

>>>>>>> eeb40eb (Add project files.)


        }

        private void botReply(object sender, EventArgs e)
        {
<<<<<<< HEAD
           
=======

>>>>>>> eeb40eb (Add project files.)
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
<<<<<<< HEAD
                MessageBox.Show(Message);
        }
=======
            MessageBox.Show(Message);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            string name = Interaction.InputBox("Enter your Name");
            Thread.Sleep(1000);
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
            TextBox textBox = new TextBox();
            textBox.Text = Message;
        }

        private void label4_Click_1(object sender, EventArgs e)
        {


        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        private void SetUserName(User user)
        {

            MessageBox.Show("Enter your name: ");
            user.Name = GetValidInput(
                IsValidUserName,
                "Invalid name. Letters only (2–30 characters)."
            );
        }

        private string GetValidUserName()
        {
            while (true)
            {
                Console.Write("Enter your name: ");
                string input = Console.ReadLine();

                if (IsValidUserName(input))
                    return input.Trim();
                //format error with red color to alert the user
                Console.ForegroundColor = ConsoleColor.Red;

                MessageBox.Show("Invalid name. Use letters only (2–30 characters).");
                Console.ResetColor();
            }
        }

        private bool IsValidUserName(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;

            input = input.Trim();

            if (input.Length < 2 || input.Length > 30)
                return false;

            foreach (char c in input)
            {
                if (!char.IsLetter(c) && c != ' ')
                    return false;
            }

            return true;
        }


        private bool ValidUserInputs(string input)
        {

            if (string.IsNullOrEmpty(input))
            {

                return false;
                input = input.Trim();

                //this will help user type meaningful message 
                if (input.Length < 2 || input.Length > 100)
                {

                    return false;

                }

            }
            return true;

        }


        private string GetValidInput(Func<string, bool> validator, string errorMessage)
        {

            while (true)

            {
                string input = Interaction.InputBox(errorMessage);
                if (ValidUserInputs(input))
                {
                    return input.Trim();
                }
                MessageBox.Show("Enter valid input");
            }


        }
        //helper method to help handle invalid inputs
>>>>>>> eeb40eb (Add project files.)
    }
}
