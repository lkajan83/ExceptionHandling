using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using iBuyCL;
using System.Timers;


namespace PunchOutCA
{
    partial class Schedular : ServiceBase
    {
        Timer _timer;
        private bool status = false;
        public Schedular()
        {
            InitializeComponent();
        }
        private double TimeInterval
        {
            get
            {
                return 600000;
                //Convert.ToDouble(ConfigurationManager.AppSettings["TimerInterval"].ToString(), CultureInfo.InvariantCulture);
            }
        }

        //Write the following code in the OnStart() method and timer1_Tick():
        protected override void OnStart(string[] args)
        {
            status = false;
            Library.WriteErrorLog("service started", "Log", "", 0);
            // TODO: Add code here to start your service.
        }

        private void InitializeTimer(double timeInterval)
        {
            if (timeInterval > 0)
            {
                ServiceTimer.Dispose();
                ServiceTimer = new Timer();
                ServiceTimer.Enabled = true;
                ServiceTimer.Elapsed += new ElapsedEventHandler(timer1_Tick);
                ServiceTimer.Interval = 10000;
            }
        }
        private Timer ServiceTimer
        {
            get
            {
                if (_timer == null)
                {
                    _timer = new Timer();
                }
                return _timer;
            }
            set
            {
                _timer = value;
            }
        }
        private void timer1_Tick(object sender, ElapsedEventArgs e)
        {
            status = true;
            Program obj = new iBuyCL.Program();
            string str = obj.ProcessXMLFiles();
            //obj.SaveDatabase(str);
        }

        //Write the following code in the OnStop() method:
        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
            Library.WriteErrorLog("service stoped", "Log", "", 0);
        }
    }
}
