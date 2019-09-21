using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Runtime.InteropServices;

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
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.DataManagementTools;
using ESRI.ArcGIS.Output;

namespace RS
{
    public sealed partial class MainForm : Form
    {
        #region class private members
        private IMapControl3 m_mapControl = null;
        private string m_mapDocumentName = string.Empty;
        private IWorkspace workspace = null;//工作空间也就是geodatabase
        private ILayer TOCRightLayer;
        private Color m_FromColor = Color.Red;
        private Color m_ToColor = Color.Blue;
        #endregion

        #region class constructor
        public MainForm()
        {
            InitializeComponent();
            m_FromColor = Color.Red;
            m_ToColor = Color.Blue;
            //RefreshColors(m_FromColor, m_ToColor);
        }
        #endregion

        private void MainForm_Load(object sender, EventArgs e)
        {
            //get the MapControl
            m_mapControl = (IMapControl3)axMapControl1.Object;

            //disable the Save menu (since there is no document yet)
            menuSaveDoc.Enabled = false;
        }

        #region Main Menu event handlers
        private void menuNewDoc_Click(object sender, EventArgs e)
        {
            //execute New Document command
            ICommand command = new CreateNewDocument();
            command.OnCreate(m_mapControl.Object);
            command.OnClick();
        }

        private void menuOpenDoc_Click(object sender, EventArgs e)
        {
            //execute Open Document command
            ICommand command = new ControlsOpenDocCommandClass();
            command.OnCreate(m_mapControl.Object);
            command.OnClick();
        }

        private void menuSaveDoc_Click(object sender, EventArgs e)
        {
            //execute Save Document command
            if (m_mapControl.CheckMxFile(m_mapDocumentName))
            {
                //create a new instance of a MapDocument
                IMapDocument mapDoc = new MapDocumentClass();
                mapDoc.Open(m_mapDocumentName, string.Empty);

                //Make sure that the MapDocument is not readonly
                if (mapDoc.get_IsReadOnly(m_mapDocumentName))
                {
                    MessageBox.Show("Map document is read only!");
                    mapDoc.Close();
                    return;
                }

                //Replace its contents with the current map
                mapDoc.ReplaceContents((IMxdContents)m_mapControl.Map);

                //save the MapDocument in order to persist it
                mapDoc.Save(mapDoc.UsesRelativePaths, false);

                //close the MapDocument
                mapDoc.Close();
            }
        }

        private void menuSaveAs_Click(object sender, EventArgs e)
        {
            //execute SaveAs Document command
            ICommand command = new ControlsSaveAsDocCommandClass();
            command.OnCreate(m_mapControl.Object);
            command.OnClick();
        }

        private void menuExitApp_Click(object sender, EventArgs e)
        {
            //exit the application
            Application.Exit();
        }
        #endregion

        //listen to MapReplaced evant in order to update the statusbar and the Save menu
        private void axMapControl1_OnMapReplaced(object sender, IMapControlEvents2_OnMapReplacedEvent e)
        {
            //get the current document name from the MapControl
            m_mapDocumentName = m_mapControl.DocumentFilename;

            //if there is no MapDocument, diable the Save menu and clear the statusbar
            if (m_mapDocumentName == string.Empty)
            {
                menuSaveDoc.Enabled = false;
                statusBarXY.Text = string.Empty;
            }
            else
            {
                //enable the Save manu and write the doc name to the statusbar
                menuSaveDoc.Enabled = true;
                statusBarXY.Text = System.IO.Path.GetFileName(m_mapDocumentName);
            }
        }

        private void axMapControl1_OnMouseMove(object sender, IMapControlEvents2_OnMouseMoveEvent e)
        {
            statusBarXY.Text = string.Format("{0}, {1}  {2}", e.mapX.ToString("#######.##"), e.mapY.ToString("#######.##"), axMapControl1.MapUnits.ToString().Substring(4));
        }




        private void SST_Click(object sender, EventArgs e)
        {
            sst newSST = new sst(this);
            newSST.ShowDialog();
            newSST.Close();

        }

        private void txb_ClipFeature_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        public void createSSTrst(ILayer layer)
        {
            IRasterClassifyColorRampRenderer pRClassRend = new RasterClassifyColorRampRenderer() as IRasterClassifyColorRampRenderer;
            IRasterRenderer pRRend = pRClassRend as IRasterRenderer;

            IRasterLayer rstLayer=layer as IRasterLayer;
 
            IRaster pRaster = rstLayer.Raster;
            IRasterBandCollection pRBandCol = pRaster as IRasterBandCollection;
            IRasterBand pRBand = pRBandCol.Item(0);
            if (pRBand.Histogram == null)
            {
                pRBand.ComputeStatsAndHist();
            }
            pRRend.Raster = pRaster;
            pRClassRend.ClassCount = 10;
            pRRend.Update();
 
            IRgbColor pFromColor = new RgbColor() as IRgbColor;
            pFromColor.Red = 0;
            pFromColor.Green = 0;
            pFromColor.Blue = 255;
            IRgbColor pToColor = new RgbColor() as IRgbColor;
            pToColor.Red = 255;
            pToColor.Green = 0;
            pToColor.Blue = 0;
 
            IAlgorithmicColorRamp colorRamp = new AlgorithmicColorRamp() as IAlgorithmicColorRamp;
            colorRamp.Size = 10;
            colorRamp.FromColor = pFromColor;
            colorRamp.ToColor = pToColor;          
            bool createColorRamp;
 
            colorRamp.CreateRamp(out createColorRamp);
 
            IFillSymbol fillSymbol = new SimpleFillSymbol() as IFillSymbol;
            for (int i = 0; i < pRClassRend.ClassCount; i++)
            {
                fillSymbol.Color = colorRamp.get_Color(i);
                pRClassRend.set_Symbol(i, fillSymbol as ISymbol);
                pRClassRend.set_Label(i, pRClassRend.get_Break(i).ToString("0.00"));
            }
            rstLayer.Renderer = pRRend;

            axMapControl1.AddLayer(rstLayer);

            this.axMapControl1.ActiveView.Refresh();
            this.axTOCControl1.Update();

        }

        private void copyToPageLayout()
        {
            IObjectCopy objectCopy = new ObjectCopyClass();
            object copyFromMap = axMapControl1.Map;
            object copyMap = objectCopy.Copy(copyFromMap);
            object copyToMap = axPageLayoutControl1.ActiveView.FocusMap;
            objectCopy.Overwrite(copyMap, ref copyToMap);
        }

        private void miPageLayout_Click(object sender, EventArgs e)
        {
            if (miPageLayout.Checked == false)
            {
                axToolbarControl1.SetBuddyControl(axPageLayoutControl1.Object);
                axTOCControl1.SetBuddyControl(axPageLayoutControl1.Object);
                copyToPageLayout();

                axPageLayoutControl1.Show();
                axMapControl1.Hide();
                axPageLayoutControl1.Refresh();

                miPageLayout.Checked = true;
                miOutput.Enabled = true;
                miMap.Checked = false;

            }
            else
            {
                axToolbarControl1.SetBuddyControl(axMapControl1.Object);
                axTOCControl1.SetBuddyControl(axMapControl1.Object);

                axPageLayoutControl1.Hide();
                axMapControl1.Show();

                miPageLayout.Checked = false;
                miOutput.Enabled = false;
                miMap.Checked = true;
            }
        }

        private void miMap_Click(object sender, EventArgs e)
        {
            if (miMap.Checked == false)
            {
                axToolbarControl1.SetBuddyControl(axMapControl1.Object);
                axTOCControl1.SetBuddyControl(axMapControl1.Object);

                axPageLayoutControl1.Hide();
                axMapControl1.Show();

                miPageLayout.Checked = false;
                miOutput.Enabled = false;
                miMap.Checked = true;

            }
            else
            {
                axToolbarControl1.SetBuddyControl(axPageLayoutControl1.Object);
                axTOCControl1.SetBuddyControl(axPageLayoutControl1.Object);
                copyToPageLayout();

                axPageLayoutControl1.Show();
                axMapControl1.Hide();
                axPageLayoutControl1.Refresh();

                miPageLayout.Checked = true;
                miOutput.Enabled = true;
                miMap.Checked = false;
            }
        }

        private void miOutput_Click(object sender, EventArgs e)
        {
            SaveFileDialog m_save = new SaveFileDialog();
            m_save.Filter = "jpeg图片(*.jpg)|*.jpg|tiff图片(*.tif)|*.tif|bmp图片(*.bmp)|*.bmp|emf图片(*.emf)|*.emf|png图片(*.png)|*.png|gif图片(*.gif)|*.gif";
            m_save.ShowDialog();
            string Outpath = m_save.FileName;
            if (Outpath != "")
            {
                //分辨率
                //double resulotion = axMapControl1.ActiveView.ScreenDisplay.DisplayTransformation.Resolution;
                double resulotion = axPageLayoutControl1.ActiveView.ScreenDisplay.DisplayTransformation.Resolution;
                IExport m_export = null;
                if (Outpath.EndsWith(".jpg"))
                {
                    m_export = new ExportJPEG() as IExport;

                }
                else if (Outpath.EndsWith(".tig"))
                {
                    m_export = new ExportTIFF() as IExport;

                }
                else if (Outpath.EndsWith(".bmp"))
                {
                    m_export = new ExportBMP() as IExport;

                }
                else if (Outpath.EndsWith(".emf"))
                {
                    m_export = new ExportEMF() as IExport;
                }
                else if (Outpath.EndsWith(".png"))
                {
                    m_export = new ExportPNG() as IExport;
                }
                else if (Outpath.EndsWith(".gif"))
                {
                    m_export = new ExportGIF() as IExport;
                }
                //设置输出的路径
                m_export.ExportFileName = Outpath;
                //设置输出的分辨率
                m_export.Resolution = resulotion;
                tagRECT piexPound;
                //piexPound = axMapControl1.ActiveView.ScreenDisplay.DisplayTransformation.get_DeviceFrame();
                piexPound = axPageLayoutControl1.ActiveView.ScreenDisplay.DisplayTransformation.get_DeviceFrame();
                IEnvelope m_envelope = new Envelope() as IEnvelope;
                m_envelope.PutCoords(piexPound.left, piexPound.bottom, piexPound.right, piexPound.top);
                //设置输出的IEnvelope
                m_export.PixelBounds = m_envelope;

                ITrackCancel m_trackCancel = new CancelTracker();
                //输出的方法
                // axMapControl1.ActiveView.Output(m_export.StartExporting(), (short)resulotion, ref piexPound, axMapControl1.ActiveView.Extent, m_trackCancel);
                axPageLayoutControl1.ActiveView.Output(m_export.StartExporting(), (short)resulotion, ref piexPound, axPageLayoutControl1.ActiveView.Extent, m_trackCancel);
                m_export.FinishExporting();
            }
            else
            {
                MessageBox.Show("输出图像名称不能为空！");
            }
        }

        private void mi_AddLegend_Click(object sender, EventArgs e)
        {
            //Get the GraphicsContainer
            IGraphicsContainer graphicsContainer = axPageLayoutControl1.GraphicsContainer;

            //Get the MapFrame
            IMapFrame mapFrame = (IMapFrame)graphicsContainer.FindFrame(axPageLayoutControl1.ActiveView.FocusMap);
            if (mapFrame == null) return;

            //Create a legend
            UID uID = new UIDClass();
            uID.Value = "esriCarto.Legend";

            //Create a MapSurroundFrame from the MapFrame
            IMapSurroundFrame mapSurroundFrame = mapFrame.CreateSurroundFrame(uID, null);
            if (mapSurroundFrame == null) return;
            if (mapSurroundFrame.MapSurround == null) return;
            //Set the name 
            mapSurroundFrame.MapSurround.Name = "Legend";

            //Envelope for the legend
            IEnvelope envelope = new EnvelopeClass();
            envelope.PutCoords(1, 1, 3.4, 2.4);

            //Set the geometry of the MapSurroundFrame 
            IElement element = (IElement)mapSurroundFrame;
            element.Geometry = envelope;

            //Add the legend to the PageLayout
            axPageLayoutControl1.AddElement(element, Type.Missing, Type.Missing, "Legend", 0);

            //Refresh the PageLayoutControl
            axPageLayoutControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);

        }

        private void mi_AddScaleBar_Click(object sender, EventArgs e)
        {
            IActiveView pActiveView = axPageLayoutControl1.PageLayout as IActiveView;
            IMap pMap = pActiveView.FocusMap as IMap;
            IGraphicsContainer pGraphicsContainer = pActiveView as IGraphicsContainer;
            IMapFrame pMapFrame = pGraphicsContainer.FindFrame(pMap) as IMapFrame;
            //设置比例尺样式
            IMapSurround pMapSurround;
            IScaleBar pScaleBar;
            pScaleBar = new ScaleLineClass();
            pScaleBar.Name = "比例尺";
            pScaleBar.Units = esriUnits.esriKilometers;
            pScaleBar.UnitLabel = "千米";
            pScaleBar.UnitLabelGap = 4;
            pScaleBar.Divisions = 2;
            pScaleBar.Subdivisions = 4;
            pScaleBar.DivisionsBeforeZero = 0;
            pScaleBar.LabelPosition = esriVertPosEnum.esriBelow;
            pScaleBar.LabelGap = 4;
            pScaleBar.LabelFrequency = esriScaleBarFrequency.esriScaleBarDivisionsAndFirstMidpoint;
            pMapSurround = pScaleBar;
            pMapSurround.Name = "ScaleBar";
            //定义UID
            UID uid = new UIDClass();
            uid.Value = "esriCarto.ScaleLine";
            //定义MapSurroundFrame对象
            IMapSurroundFrame pMapSurroundFrame = pMapFrame.CreateSurroundFrame(uid, null);
            pMapSurroundFrame.MapSurround = pMapSurround;
            //定义Envelope设置Element摆放的位置
            IEnvelope pEnvelope = new EnvelopeClass();
            pEnvelope.PutCoords(15, 1, 15, 2);
            IElement pElement = pMapSurroundFrame as IElement;
            pElement.Geometry = pEnvelope;
            pGraphicsContainer.AddElement(pElement, 0);
        }

        private void mi_AddNorthArrow_Click(object sender, EventArgs e)
        {
            IActiveView pActiveView = axPageLayoutControl1.PageLayout as IActiveView;
            IMap pMap = pActiveView.FocusMap as IMap;
            IGraphicsContainer pGraphicsContainer = pActiveView as IGraphicsContainer;
            IMapFrame pMapFrame = pGraphicsContainer.FindFrame(pMap) as IMapFrame;
            IMapSurround pMapSurround;
            INorthArrow pNorthArrow;
            pNorthArrow = new MarkerNorthArrowClass();
            pMapSurround = pNorthArrow;
            pMapSurround.Name = "NorthArrow";
            //定义UID
            UID uid = new UIDClass();
            uid.Value = "esriCarto.MarkerNorthArrow";
            //定义MapSurroundFrame对象
            IMapSurroundFrame pMapSurroundFrame = pMapFrame.CreateSurroundFrame(uid, null);
            pMapSurroundFrame.MapSurround = pMapSurround;
            //定义Envelope设置Element摆放的位置
            IEnvelope pEnvelope = new EnvelopeClass();
            pEnvelope.PutCoords(17, 25, 17, 25);


            IElement pElement = pMapSurroundFrame as IElement;
            pElement.Geometry = pEnvelope;
            pGraphicsContainer.AddElement(pElement, 0);
        }

        private void resultImg_Click(object sender, EventArgs e)
        {

        }

        public void addrstLayer(IRasterLayer layer)
        {
            axMapControl1.AddLayer(layer);
            this.axMapControl1.ActiveView.Refresh();
            this.axTOCControl1.Update();
        }

        public void addshpLayer()
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Shapefiles (*.shp)|*.shp";
                openFileDialog.Title = "打开矢量数据";
                openFileDialog.Multiselect = false;

                string fileName = "";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    fileName = openFileDialog.FileName;
                    FileInfo fileInfo = new FileInfo(fileName);
                    string filePath = fileInfo.DirectoryName;
                    string fileN = fileInfo.Name;

                    //工作空间（实例化）
                    IWorkspaceFactory wsf = new ShapefileWorkspaceFactory();
                    IWorkspace ws = wsf.OpenFromFile(filePath, 0);
                    IFeatureWorkspace fws = ws as IFeatureWorkspace;//强制转换
                    IFeatureClass fc = fws.OpenFeatureClass(fileN);

                    IFeatureLayer featureLayer = new FeatureLayer();
                    featureLayer.FeatureClass = fc;

                    axMapControl1.AddLayer(featureLayer);

                    this.axMapControl1.ActiveView.Refresh();
                    this.axTOCControl1.Update();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }


        }










         

                    

   



    }
}

       

    