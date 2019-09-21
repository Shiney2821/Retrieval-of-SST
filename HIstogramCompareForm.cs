using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Geodatabase;


namespace RS
{
    public partial class HIstogramCompareForm : Form
    {
        private IRasterLayer m_rstlayer;

        private int[] m_selband;

        public HIstogramCompareForm( IRasterLayer rstlayer,int[] selband)
        {
            InitializeComponent();
            m_rstlayer = rstlayer;
            m_selband = selband;
        }


        private void HIstogramCompareForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen pen = new Pen(Brushes.Black, 1);
            Pen pennoteline = new Pen(Brushes.Red, 1);

            g.DrawLine(pen, 50, 240, 320, 240);
            g.DrawLine(pen, 320, 240, 316, 236);
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

            g.DrawString("0", new Font("New Timer", 8), Brushes.Black, new PointF(46, 242));
            g.DrawString("51", new Font("New Timer", 8), Brushes.Black, new PointF(92, 242));
            g.DrawString("102", new Font("New Timer", 8), Brushes.Black, new PointF(139, 242));
            g.DrawString("153", new Font("New Timer", 8), Brushes.Black, new PointF(189, 242));
            g.DrawString("204", new Font("New Timer", 8), Brushes.Black, new PointF(239, 242));
            g.DrawString("255", new Font("New Timer", 8), Brushes.Black, new PointF(289, 242));

            g.DrawLine(pen, 48, 40, 50, 40);
            g.DrawString("0", new Font("New Timer", 8), Brushes.Black, new PointF(34, 234));

            //计算所有波段最大像元数
            int MaxBandCount=GetMaxBandCount();

            for(int i=0;i<m_selband.Length;i++)
            {
                int j=m_selband[i];
                DrawHisto(j, MaxBandCount, g);
            }

        }

        private void DrawHisto(int index, int maxst, Graphics g)
        {
            Pen pen = new Pen(Brushes.Black, 1);
            Color color = Color.Black;
            switch (index)
            {
                case 0:
                    color = Color.Red;
                    break;
                case 1:
                    color = Color.Orange;
                    break;
                case 2:
                    color = Color.Yellow;
                    break;
                case 3:
                    color = Color.Green;
                    break;
                case 4:
                    color = Color.Blue;
                    break;
                case 5:
                    color = Color.Purple;
                    break;
                case 6:
                    color = Color.Peru;
                    break;
            }
            pen.Color = color;
            IRaster2 raster2 = m_rstlayer.Raster as IRaster2;
            IRasterDataset rstDataset = raster2.RasterDataset;
            IRasterBandCollection rstBandCollection = rstDataset as IRasterBandCollection;
            IRasterBand rstBand = rstBandCollection.Item(index);

            bool hasStat = false;
            rstBand.HasStatistics(out hasStat);
            if (null == rstBand.Statistics || !hasStat || null == rstBand.Histogram)
            {
                //转换Irasterbandedit2接口，调用computestshistogram方法用于波段信息统计以及直方图绘制
                IRasterBandEdit2 rstBandEdit = rstBand as IRasterBandEdit2;
                rstBandEdit.ComputeStatsHistogram(0);
            }
            //获取每个像元值的统计个数
            double[] pixelCounts = rstBand.Histogram.Counts as double[];
            double xTemp = 0;
            double yTemp = 0;

            for (int i = 0; i < pixelCounts.Length; i++)
            {
                xTemp = i * 1.0 / pixelCounts.Length * (300 - 50);
                yTemp = 200.0 * pixelCounts[i] / maxst;
                g.DrawLine(pen, 50 + (int)xTemp, 240, 50 + (int)xTemp, 240 - (int)yTemp);
         
            }
            pen.Dispose();
        }

        private int GetMaxBandCount()
        {
            int maxCount = 0;
            IRaster2 raster2 = m_rstlayer.Raster as IRaster2;
            IRasterDataset rstDataset = raster2.RasterDataset;
            IRasterBandCollection rstBandCollection = rstDataset as IRasterBandCollection;


            int[] max = new int[m_selband.Length];
            for (int i = 0; i < m_selband.Length; i++)
            {
                IRasterBand rstBand = rstBandCollection.Item(m_selband[i]);
                bool hasStat = false;
                rstBand.HasStatistics(out hasStat);
                if (null == rstBand.Statistics || !hasStat || null == rstBand.Histogram)
                {
                    //转换Irasterbandedit2接口，调用computestshistogram方法用于波段信息统计以及直方图绘制
                    IRasterBandEdit2 rstBandEdit = rstBand as IRasterBandEdit2;
                    rstBandEdit.ComputeStatsHistogram(0);
                }
                //获取每个像元值的统计个数
                double[] pixelCounts = rstBand.Histogram.Counts as double[];
                max[i] = (int)pixelCounts[0];
                for (int j = 0; j < pixelCounts.Length; j++)
                    if (max[i] < pixelCounts[j]) max[i] = (int)pixelCounts[j];

                maxCount = max[0];
                for (int j = 0; j < m_selband.Length; j++)
                    if (maxCount < max[j]) maxCount = max[j];
            }
            return maxCount;
        }


    
    
    
    }
}
