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
    public partial class ChooseParameters : Form
    {
        DateTime startDate = DateTime.Now, endDate = DateTime.Now;
        bool isStartDateValid, isEndDateValid;

        public ChooseParameters()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(textBox1.Text) && !String.IsNullOrWhiteSpace(textBox2.Text))
            {
                startDate = DateTime.Parse(textBox1.Text);
                endDate = DateTime.Parse(textBox2.Text);
            }

            if (String.IsNullOrWhiteSpace(textBox1.Text)) startDate = DateTime.Parse("1753-01-01");
            if (String.IsNullOrWhiteSpace(textBox2.Text)) endDate = DateTime.Parse("2013-01-01");

            ViewResults viewResults = new ViewResults();
            viewResults.Show();
            this.Hide();
        }
    }
}
