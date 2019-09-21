using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RS
{
    public partial class HIstogramForm : Form
    {
        private int maxCount;
        private double[] pixelCounts;
        private double minPixel;
        private double maxPixel;
        private int[] xGraduations;

        public HIstogramForm(double[] pixelCounts, double minPixel, double maxPixel)
        {
            InitializeComponent();
            this.pixelCounts = pixelCounts;
            this.minPixel = minPixel;
            this.maxPixel = maxPixel;

            xGraduations = new int[6];
            for (int i = 0; i < 6; i++)
                xGraduations[i] = (int)((maxPixel - minPixel) / 5 * i + minPixel);
            maxCount = (int)pixelCounts[0];
            for (int i = 0; i < pixelCounts.Length; i++)
                if (maxCount < pixelCounts[i]) maxCount = (int)pixelCounts[i];
        }

        private void HIstogramForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen pen = new Pen(Brushes.Black, 1);

            g.DrawLine(pen, 50, 240, 320, 240);
            g.DrawLine(pen, 320, 240, 316,236);
            g.DrawLine(pen, 320, 240, 316, 244);
            g.DrawString("灰度值", new Font("宋体", 9), Brushes.Black, new PointF(318, 243));

            g.DrawLine(pen, 50, 240, 50, 30);
            g.DrawLine(pen, 50, 30, 46, 34);
            g.DrawLine(pen, 50, 30, 54, 34);
            g.DrawString("像元数", new Font("宋体", 9), Brushes.Black, new PointF(10, 20));

            //绘制并标识x的刻度
            g.DrawLine(pen, 100, 240, 100, 242);
            g.DrawLine(pen, 150, 240, 150, 242);
            g.DrawLine(pen, 200, 240, 200, 242);
            g.DrawLine(pen, 250, 240, 250, 242);
            g.DrawLine(pen, 300, 240, 300, 242);

            g.DrawString(xGraduations[0].ToString(), new Font("New Timer", 8), Brushes.Black, new PointF(46, 242));
            g.DrawString(xGraduations[1].ToString(), new Font("New Timer", 8), Brushes.Black, new PointF(92, 242));
            g.DrawString(xGraduations[2].ToString(), new Font("New Timer", 8), Brushes.Black, new PointF(139, 242));
            g.DrawString(xGraduations[3].ToString(), new Font("New Timer", 8), Brushes.Black, new PointF(189, 242));
            g.DrawString(xGraduations[4].ToString(), new Font("New Timer", 8), Brushes.Black, new PointF(239, 242));
            g.DrawString(xGraduations[5].ToString(), new Font("New Timer", 8), Brushes.Black, new PointF(289, 242));

            g.DrawLine(pen, 48, 40, 50, 40);
            g.DrawString("0", new Font("New Timer", 8), Brushes.Black, new PointF(34, 234));

            g.DrawString(maxCount.ToString(), new Font("New Timer", 8), Brushes.Black, new PointF(9, 34));

            //绘制直方图
            double xTemp = 0;
            double yTemp = 0;

            for (int i = 0; i < pixelCounts.Length; i++)
            {
                xTemp = i * 1.0 / pixelCounts.Length * (300 - 50);
                yTemp = 200.0 * pixelCounts[i] / maxCount;

                g.DrawLine(pen, 50 + (int)xTemp, 240, 50 + (int)xTemp, 240 - (int)yTemp);
            }


            pen.Dispose();

        }





    }

}
