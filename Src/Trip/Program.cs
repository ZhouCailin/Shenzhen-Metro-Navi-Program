using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ESRI.ArcGIS;

using Trip.IDAL;
using Trip.DAL;

namespace Trip
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (!RuntimeManager.Bind(ProductCode.Engine))
            {
                if (!RuntimeManager.Bind(ProductCode.Desktop))
                {
                    MessageBox.Show("Unable to bind to ArcGIS runtime. Application will be shut down.");
                    return;
                }
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new MainForm());
            Application.Run(new LoginForm());
        }
    }

    public sealed class TripApplication
    {
        private static TripApplication m_application = null;
        private static ITripFactory m_tripFactory = null;

        public static TripApplication Instance
        {
            get
            {
                if (m_application == null)
                {
                    m_application = new TripApplication();
                    m_tripFactory = new AccessTripFactory();

                }
                return m_application;
            }
        }

        public ITripFactory DBFactory
        {
            get { return m_tripFactory;  }
        }
    }
}