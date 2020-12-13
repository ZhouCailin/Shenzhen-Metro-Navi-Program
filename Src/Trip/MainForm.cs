using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Configuration;
using System.Collections;

using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;

using Trip.IBLL;
using Trip.BLL;
using Trip.IDAL;
using Trip.DAL;
using Trip.Common;
using Trip.Model;

namespace Trip
{
    public sealed partial class MainForm : Form
    {
        #region class private members
        private IMapControl3 m_mapControl = null;
        private string m_mapDocumentName = string.Empty;
        #endregion

        private IUserBLL m_userBLL = null;
        private ITripBLL m_tripBLL = null;
        private DistBLL m_distBLL = null;//sig
        public static List<string> opPath = new List<string>();

        #region class constructor
        public MainForm()
        {
            InitializeComponent();
        }
        #endregion

        private void MainForm_Load(object sender, EventArgs e)
        {
            //get the MapControl
            m_mapControl = (IMapControl3)axMapControl1.Object;

            //disable the Save menu (since there is no document yet)
            menuSaveDoc.Enabled = false;

            string strAppPath = ConfigurationManager.ConnectionStrings["Trip.Properties.Settings.MxdPath"].ToString(); 
            axMapControl1.LoadMxFile(strAppPath);

            IUserRepository userRepository = TripApplication.Instance.DBFactory.CreateUserRepository();
            ITripRepository tripRepository = TripApplication.Instance.DBFactory.CreateTripRepository();
            IDistRepository distRepository = TripApplication.Instance.DBFactory.CreateDistRepository();

            m_userBLL = new UserBLL(userRepository);
            m_tripBLL = new TripBLL(tripRepository);
            m_distBLL = new DistBLL(distRepository);

            List<User> users = m_userBLL.GetAllUser();
            FillUserTree(users);
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

        private void mnuManageUser_Click(object sender, EventArgs e)
        {
            ManageUserForm fm = new ManageUserForm();
            fm.ShowDialog();
        }

        private void mnuManageTrip_Click(object sender, EventArgs e)
        {
            ManageTripForm fm = new ManageTripForm();
            fm.ShowDialog();
        }

        private void FillUserTree(List<User> users)
        {
            TreeNode treeNode = treeView1.Nodes[0];

            foreach (User user in users)
            {
                TreeNode tn1 = new TreeNode();
                tn1.ImageIndex = 0;
                tn1.SelectedImageIndex = tn1.ImageIndex;

                tn1.Name = user.userID;
                tn1.Text = user.name;
                tn1.ToolTipText = tn1.Name + " " + tn1.Text;

                treeNode.Nodes.Add(tn1);
            }

            treeView1.ExpandAll();
        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode hitNode = e.Node;

            List<TripRecord> trips = m_tripBLL.GetTrips(hitNode.Name);
            if (trips == null) return;

            ShowTrips(trips);
        }

        private void ShowTrips(List<TripRecord> trips)
        {
            IGraphicsContainer pGraphicsContainer = axMapControl1.Map as IGraphicsContainer;
            IActiveView pActiveView = axMapControl1.Map as IActiveView;

            // 得到“城市”图层
            IFeatureLayer pFeatureLayer = axMapControl1.get_Layer(0) as IFeatureLayer;
            IFeatureClass pFeatureClass = pFeatureLayer.FeatureClass;
            int indexFlield = pFeatureClass.Fields.FindField("PointName");

            IPolyline pPolyline = new PolylineClass();
            IPointCollection pPointCollection = pPolyline as IPointCollection;

            IQueryFilter pQueryFilter = new QueryFilterClass();
            // pQueryFilter.AddField("ADCODE93");

            foreach (TripRecord trip in trips)
            {
                pQueryFilter.WhereClause = "PointName ='" + trip.name+"'";
                IFeatureCursor pCursor = pFeatureClass.Search(pQueryFilter, true);

                IFeature pFeature = pCursor.NextFeature();

                System.Runtime.InteropServices.Marshal.ReleaseComObject(pCursor);
                GC.Collect();
                GC.WaitForPendingFinalizers();

                if (pFeature != null)
                {
                    ESRI.ArcGIS.Geometry.Point pPoint = pFeature.Shape as ESRI.ArcGIS.Geometry.Point;
                    pPointCollection.AddPoint(pPoint);
                }
            }

            object pSym = null;
            IRgbColor pColor;

            pColor = new RgbColorClass();
            pColor.Red = 255;
            pColor.Blue = 255;
            pColor.Green = 0;

            pSym = new SimpleLineSymbolClass();
            ISimpleLineSymbol pSym1 = pSym as ISimpleLineSymbol;
            pSym1.Width = 5;
            pSym1.Style = esriSimpleLineStyle.esriSLSSolid;
            pSym1.Color = pColor;

            ILineElement pLienElement = new LineElementClass();
            pLienElement.Symbol = pSym1;
            IElement pElement = pLienElement as IElement;
            pElement.Geometry = pPolyline as IGeometry;

            pGraphicsContainer.AddElement(pElement, 0);
            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);

        }

        public void ShowNavi()
        {
            IGraphicsContainer pGraphicsContainer = axMapControl1.Map as IGraphicsContainer;
            IActiveView pActiveView = axMapControl1.Map as IActiveView;

            // 得到“城市”图层
            IFeatureLayer pFeatureLayer = axMapControl1.get_Layer(0) as IFeatureLayer;
            IFeatureClass pFeatureClass = pFeatureLayer.FeatureClass;
            int indexFlield = pFeatureClass.Fields.FindField("PointName");

            IPolyline pPolyline = new PolylineClass();
            IPointCollection pPointCollection = pPolyline as IPointCollection;

            IQueryFilter pQueryFilter = new QueryFilterClass();
            pQueryFilter.AddField("PointName");

            foreach (string staString in opPath)
            {
                pQueryFilter.WhereClause = "PointName='" +staString+"'";
                IFeatureCursor pCursor = pFeatureClass.Search(pQueryFilter, true);

                IFeature pFeature = pCursor.NextFeature();

                System.Runtime.InteropServices.Marshal.ReleaseComObject(pCursor);
                GC.Collect();
                GC.WaitForPendingFinalizers();

                if (pFeature != null)
                {
                    ESRI.ArcGIS.Geometry.Point pPoint = pFeature.Shape as ESRI.ArcGIS.Geometry.Point;
                    pPointCollection.AddPoint(pPoint);
                }
            }

            object pSym = null;
            IRgbColor pColor;

            pColor = new RgbColorClass();
            pColor.Red = 0;
            pColor.Blue = 255;
            pColor.Green = 0;

            pSym = new SimpleLineSymbolClass();
            ISimpleLineSymbol pSym1 = pSym as ISimpleLineSymbol;
            pSym1.Width = 6;
            pSym1.Style = esriSimpleLineStyle.esriSLSDash;
            pSym1.Color = pColor;

            ILineElement pLienElement = new LineElementClass();
            pLienElement.Symbol = pSym1;
            IElement pElement = pLienElement as IElement;
            pElement.Geometry = pPolyline as IGeometry;

            pGraphicsContainer.AddElement(pElement, 0);
            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);

        }

        private void tsbRefresh_Click(object sender, EventArgs e)
        {
            TreeNode treeNode = treeView1.Nodes[0];
            treeNode.Nodes.Clear();

            List<User> users = m_userBLL.GetAllUser();
            FillUserTree(users);
        }

        private void tsbClean_Click(object sender, EventArgs e)
        {
            IGraphicsContainer pGraphicsContainer = axMapControl1.Map as IGraphicsContainer;
            IActiveView pActiveView = axMapControl1.Map as IActiveView;

            pGraphicsContainer.DeleteAllElements();
            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        private void 导航NToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NaviForm fm = new NaviForm(m_distBLL);//sig
            fm.ShowDialog();
        }

        private void 显示VToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowNavi();
        }

        private void MnuViewComment_Click(object sender, EventArgs e)
        {
            CommentViewForm commentViewForm = new CommentViewForm();
            commentViewForm.Show();
        }

        private void MnuComment_Click(object sender, EventArgs e)
        {
            CommentForm commentForm = new CommentForm();
            commentForm.Show();
        }
    }
}