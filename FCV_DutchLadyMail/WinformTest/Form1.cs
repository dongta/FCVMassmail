using IVG.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinformTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            DomainProcess dop = new DomainProcess();
            List<string> a = dop.getUserDomain1("dongnv");
            string user = string.Empty;
            foreach (string item in a)
            {
                user += " " + item + ",";
            }
            MessageBox.Show(user);
        }
    }
}
