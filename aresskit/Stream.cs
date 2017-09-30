﻿using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace aresskit
{
    // Thanks to: http://stackoverflow.com/a/18870847/5925502
    class Stream
    {
        public static string captureScreenshot()
        {
            string imagePath = default(string);
            // ScreenShot.CaptureAndSave(0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, @"C:\\development\\Capture.png");
            Bitmap myImage = Stream.Capture(0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            imagePath = System.IO.Path.GetTempPath() + Toolkit.RandomString(16) + ".png";
            myImage.Save(imagePath, ImageFormat.Png);

            // Upload file
            dynamic json = System.Web.Helpers.Json.Decode(FileHandler.uploadFile(imagePath, "http://uploads.im/api?upload"));
            return json.data.img_url;
        }

        private static Bitmap Capture(int x, int y, int width, int height)
        {
            Bitmap screenShotBMP = new Bitmap(width, height, PixelFormat.Format32bppArgb);

            Graphics screenShotGraphics = Graphics.FromImage(screenShotBMP);

            screenShotGraphics.CopyFromScreen(new Point(x, y), Point.Empty, new Size(width, height), CopyPixelOperation.SourceCopy);
            screenShotGraphics.Dispose();

            return screenShotBMP;
        }
    }
}
