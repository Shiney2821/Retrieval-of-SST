using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Carto;


namespace RS
{
    public partial class SelectBandsForm : Form
    {
        //储存要绘制对比直方图的栅格图层对象
        private IRasterLayer m_rstLayer;
        //储存栅格图层的波段总数
        private int m_bandnum;
        //储存栅格图层的选择波段
        private int[] m_selband;
        //
        public SelectBandsForm(IRasterLayer rstlayer)
        {
            InitializeComponent();
            m_rstLayer = rstlayer;
            IRaster2 raster2 = rstlayer.Raster as IRaster2;
            IRasterDataset rstDataset = raster2.RasterDataset;
            IRasterBandCollection rstBandCollection = rstDataset as IRasterBandCollection;
             int BandCount = rstlayer.BandCount;
             for (int i = 0; i < BandCount; i++)
             {
                 int BandIdx = i + 1;
                 CLB_Band.Items.Add("波段" + BandIdx);
             }
                        
        }

        private void btn_DrawCompareHistogram_Click(object sender, EventArgs e)
        {
            int k = 0;
            for (int i = 0; i < CLB_Band.Items.Count; i++)
            {
                if (CLB_Band.GetItemChecked(i))
                    k++;
            }
            m_selband = new int[k];

            int j = 0;
            for(int i=0;i < CLB_Band.Items.Count; i++)
            {
                if (CLB_Band.GetItemChecked(i))
                {
                    m_selband[j] = i;
                    j++;
                }
            }

            HIstogramCompareForm HistogramCompare = new HIstogramCompareForm(m_rstLayer, m_selband);
            HistogramCompare.ShowDialog();



        }




    }

}
