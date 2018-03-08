using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;
using OpenCvSharp;
using ZXing;
using System.Drawing;

namespace QRCameraApp
{
    public partial class Form1 : Form
    {
        private BarcodeReader barcodeReader = new BarcodeReader();
        private Bitmap img = new Bitmap(@"C:\Users\sekib\Pictures\Saved Pictures\qr.bmp");
        public Form1()
        {
            InitializeComponent();

            barcodeReader.AutoRotate = true;
            barcodeReader.TryInverted = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "QRCamerra";
        }

        private string readQR(Bitmap bmp)
        {
            string ret = "(not found)";

            try
            {
                Result result = barcodeReader.Decode(bmp);
                if (result != null)
                {
                    ret = result.Text;
                }
            }
            catch
            {
                //ZXingのバグ？barcodeReader.Decodeに失敗することがある ver 0.14.0.0 で確認
            }

            return ret;
        }
    

    //private void Cam(object sender, EventArgs e)
    //    {

    //        Mat src = new Mat(@"C:\Users\sekib\Pictures\Saved Pictures\1.png", ImreadModes.GrayScale);   // OpenCvSharp 3.x
    //        //Mat src = new Mat("lenna.png", LoadMode.GrayScale); // OpenCvSharp 2.4.x
    //        Mat dst = new Mat();

    //        Cv2.Canny(src, dst, 50, 200);
            
    //        //using (new Window("src image", src))
    //        //using (new Window("dst image", dst))
    //        {
    //            Cv2.WaitKey();
    //        }

    //    }

        private void button1_Click(object sender, EventArgs e)
        {
            Console.WriteLine("click");
            string hoge = readQR(img);
            Console.WriteLine(hoge);
           // Cam(sender,e);
        }






    }
}
