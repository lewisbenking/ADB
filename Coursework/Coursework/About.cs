using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Coursework
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show(); this.Hide();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This is the about page, describing what the application does.");
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            DialogResult = MessageBox.Show("Are you sure you want to exit?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (DialogResult == DialogResult.Yes) Application.Exit();
        }
    }
}
