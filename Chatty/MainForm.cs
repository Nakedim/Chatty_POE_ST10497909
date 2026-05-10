using System;
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
        String Name;
        String Message;
        String botName = "ChatBotty";
        String logo = @"  ____      _               ____                       _ _                _                
 / ___|   _| |__   ___ _ __/ ___|  ___  ___ _   _ _ __(_) |_ _   _       / \   _ __  _ __  
| |  | | | | '_ \ / _ \ '__\___ \ / _ \/ __| | | | '__| | __| | | |     / _ \ | '_ \| '_ \ 
| |__| |_| | |_) |  __/ |   ___) |  __/ (__| |_| | |  | | |_| |_| |    / ___ \| |_) | |_) |
 \____\__, |_.__/ \___|_|  |____/ \___|\___|\__,_|_|  |_|\__|\__, |   /_/   \_\ .__/| .__/ 
      |___/                                                  |___/            |_|   |_|    ";


        public MainForm()
        {
            InitializeComponent();
        }



        private void MainForm_Load(object sender, EventArgs e)
        {
            ShowWelcome(this, EventArgs.Empty);

            try
            {
                SoundPlayer player = new SoundPlayer("Welcome Message.wav");
                player.Play();



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
            DialogResult result = MessageBox.Show("Welcome to the CyberSecurity App", "Welcome Message"
                , MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.No)
            {
                Application.Exit();
            }


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
            Message = textBox1.Text;


        }

        private void botReply(object sender, EventArgs e)
        {
           
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
                MessageBox.Show(Message);
        }
    }
}
