using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;
using OpenCvSharp;

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

            Mat src = new Mat(@"C:\Users\sekib\Pictures\Saved Pictures\1.png", ImreadModes.GrayScale);   // OpenCvSharp 3.x
            //Mat src = new Mat("lenna.png", LoadMode.GrayScale); // OpenCvSharp 2.4.x
            Mat dst = new Mat();

            Cv2.Canny(src, dst, 50, 200);
            
            //using (new Window("src image", src))
            //using (new Window("dst image", dst))
            {
                Cv2.WaitKey();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cam(sender,e);
        }

    }
}
