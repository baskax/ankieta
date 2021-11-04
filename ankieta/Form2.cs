using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ankieta
{
    public partial class Form2 : Form
    {
        public Form2(string filename)
        {
            string executableLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string fileLocation = Path.Combine(executableLocation, filename);
            InitializeComponent();
            pictureBox1.Image = Image.FromFile(filename);
            label1.Text = filename.Substring(0, filename.Length - 4);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
