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
        private void MainForm_Load(object sender, EventArgs e)
        {

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

        private void label1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Hello");
        }
    }
}
