using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QRCameraApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }



        private void Cam(object sender, EventArgs e)
        {
            Clipboard.Clear();
            Clipboard.SetText("カメラ");

            SendKeys.Send("^{ESC}");
            Application.DoEvents();

            SendKeys.Send("^v");
            Application.DoEvents();

            SendKeys.Send("{ENTER}");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cam(sender,e);
        }
    }
}
