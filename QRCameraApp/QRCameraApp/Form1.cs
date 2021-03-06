﻿using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;
using OpenCvSharp;
using ZXing;
using System.Drawing;
using System.ComponentModel;
using System.IO;

namespace QRCameraApp
{
    public partial class Form1 : Form
    {
        private BarcodeReader barcodeReader = new BarcodeReader();

        int WIDTH = 640;
        int HEIGHT = 500;
        Mat frame;
        VideoCapture capture;
        Bitmap bmp;
        Graphics graphic;

        public Form1()
        {
            InitializeComponent();
            barcodeReader.AutoRotate = true;
            barcodeReader.TryInverted = true;

            this.Width = WIDTH;
            this.Height = 600;

            //カメラ画像取得用VideoCapture
            capture = new VideoCapture(0);
            if (!capture.IsOpened())
            {
                MessageBox.Show("cannot open camera");
                this.Close();
            }
            capture.Set(3,Width);
            capture.Set(4, (int)(this.Height*0.8));
            //capture.FrameWidth = WIDTH;
            //capture.FrameHeight = HEIGHT;
            //取得先のMat作成
            frame = new Mat((int)(this.Height*0.8), Width, MatType.CV_8UC3);
            Console.WriteLine(frame.Cols);
           //表示用のBitmap作成
            bmp = new Bitmap(frame.Cols, frame.Rows, (int)frame.Step(), System.Drawing.Imaging.PixelFormat.Format24bppRgb, frame.Data);

            //PictureBoxを出力サイズに合わせる
            pictureBox1.Width = Width;//frame.Cols;
            pictureBox1.Height = (int)(this.Height*0.8); // frame.Rows;
            //描画用のGraphics作成
            graphic = pictureBox1.CreateGraphics();

            //画像取得スレッド開始
            backgroundWorker1.RunWorkerAsync();


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // サイズ変更不可の直線ウィンドウに変更する
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.Text = "QRCamerra";

            button1.Height = 80;
            button1.Location = new System.Drawing.Point(100, 480);
        }

        private string ReadQR(Bitmap bmp)
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

        private void button1_Click(object sender, EventArgs e)
        {
            Console.WriteLine("click");
            string dir = ReadQR(bmp);         

            if (Directory.Exists(dir))
            {
                //フォルダを開く
                System.Diagnostics.Process.Start(dir);
            }
            else
            {
                Directory.CreateDirectory(dir);
                //フォルダを開く
                System.Diagnostics.Process.Start(dir);
            }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //スレッドの終了を待機
            backgroundWorker1.CancelAsync();
            while (backgroundWorker1.IsBusy)
                Application.DoEvents();
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            BackgroundWorker bw = (BackgroundWorker)sender;

            while (!backgroundWorker1.CancellationPending)
            {
                //画像取得
                //capture.Read(frame); //これだとエラー
                capture.Grab();
                NativeMethods.videoio_VideoCapture_operatorRightShift_Mat(capture.CvPtr, frame.CvPtr);

                bw.ReportProgress(0);
            }

        }

        private void backgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            //描画
            graphic.DrawImage(bmp, 0, 0, frame.Cols, frame.Rows);
        }
    }
}
