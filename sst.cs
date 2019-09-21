using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geoprocessing;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.SpatialAnalyst;
using ESRI.ArcGIS.GeoAnalyst;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.Geoprocessor;
using ESRI.ArcGIS.AnalysisTools;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.DataManagementTools;

namespace RS
{
    public partial class sst : Form
    {

        private IWorkspace workspace = null;//工作空间也就是geodatabase
        private IGeoDataset geodataset22;
        private IGeoDataset geodataset23;
        private IGeoDataset geodatasetSZ;
        private IGeoDataset result;//返回结果
        public MainForm main_frm;






        public sst(MainForm frm)
        {
            InitializeComponent();
            this.main_frm = frm;
        }

        private void btn_SST_Click(object sender, EventArgs e)
        {
            geodataset22 = getRstDataset(tb_B22.Text) as IGeoDataset;
            geodataset23 = getRstDataset(tb_B23.Text) as IGeoDataset;
            geodatasetSZ = getRstDataset(tb_SZ.Text) as IGeoDataset;

            IMapAlgebraOp RSalgebra = new RasterMapAlgebraOpClass();
            RSalgebra.BindRaster(geodataset22, "rst22");
            RSalgebra.BindRaster(geodataset23, "rst23");
            RSalgebra.BindRaster(geodatasetSZ, "rstsz");



            string cal = "0.677 + 1.026 * [rst22] + 0.469 * ([rst22] - [rst23]) + 1.470 / Cos([rstsz] * 0.01 * 3.1415926 / 180) - 0.1";

            result = RSalgebra.Execute(cal);

            IRasterLayer pOutRL = new RasterLayerClass();
            pOutRL.CreateFromRaster(result as IRaster);

            ILayer layer=pOutRL as ILayer ;
            IRasterLayer rst = new RasterLayerClass();
            rst.CreateFromRaster(result as IRaster);

            main_frm.addrstLayer(rst);
            main_frm.createSSTrst(layer);
          //  main_frm.addshpLayer();
            this.Close();

                     

        }

       

        private void tb_B22_MouseDown(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Tiff file(*.tif)|*tif|Imag file (*.img)|*.img";
                openFileDialog.Title = "打开影像数据";
                openFileDialog.Multiselect = false;

                string fileName = "";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    fileName = openFileDialog.FileName;
                    tb_B22.Text = fileName;
                }

               


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            this.Cursor = Cursors.Default;

        }

        private void tb_B23_MouseDown(object sender, MouseEventArgs e)
        {

            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Imag file (*.img)|*.img|Tiff file(*.tif)|*tif";
                openFileDialog.Title = "打开影像数据";
                openFileDialog.Multiselect = false;

             

                string fileName = "";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    fileName = openFileDialog.FileName;
                    tb_B23.Text = fileName;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void tb_SZ_MouseDown(object sender, MouseEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Imag file (*.img)|*.img|Tiff file(*.tif)|*tif";
            openFileDialog.Title = "打开影像数据";
            openFileDialog.Multiselect = false;

            
            string fileName = "";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                fileName = openFileDialog.FileName;
                tb_SZ.Text = fileName;
            }


        }


        private IRasterDataset getRstDataset(string fileName)
        {
            FileInfo fileInfo = new FileInfo(fileName);
            string filePath = fileInfo.DirectoryName;
            string fileN = fileInfo.Name;
            IWorkspaceFactory wsf = new RasterWorkspaceFactory();
            IWorkspace ws = wsf.OpenFromFile(filePath, 0);

            IRasterWorkspace rastWork = (IRasterWorkspace)ws;
            IRasterDataset rasterDatst=rastWork.OpenRasterDataset(fileN);      
            return rasterDatst;
        }



    }
}
