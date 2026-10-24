using System;
using System.Configuration;
using System.ServiceProcess;
using System.Timers;
using Timer = System.Timers.Timer;

namespace WebApp.WindowService
{
    // to run service as a console app. See Main is Service1.Designer.cs
    // http:///makit.net/post/running-a-windows-service-as-a-console-application
    public partial class Service1 : ServiceBase
    {
        private Timer _timer = new Timer();
        private DateTime time;

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // Add code here to start your service. This method should set things
            // in motion so your service can do its work.
            EventLogging.LogInformation($"Simple Windows service is starting: {DateTime.Now}");

            try
            {
                time = Convert.ToDateTime(ConfigurationManager.AppSettings["RunAtTime"]);
                EventLogging.LogInformation($"Loaded configuration: Application set to run at {time}");
            }
            catch (Exception exception)
            {
                DateTime now = DateTime.Now;
                time = new DateTime(now.Hour, now.Minute, now.Day, 6, 0, 0);

                string eventLogMessage = string.Format($"Loading configuration failed: Application set to run at {time}");
                eventLogMessage += string.Format(Environment.NewLine + "Message was: {0}", exception.Message);

                EventLogging.LogWarning(eventLogMessage);
            }

            _timer.Interval = 58000; //every 58 seconds
            _timer.Elapsed += TimerTick;
            _timer.Start();

            bool RunAtStartup = Convert.ToBoolean(ConfigurationManager.AppSettings["RunAtStartup"]);

            if (RunAtStartup)
            {
                // Run through once upon startup
                RunApplication();
            }
        }

        protected override void OnStop()
        {
            base.OnStop();
            EventLogging.LogInformation($"Simple Windows service has stopped: {DateTime.Now}");
        }

        protected override void OnPause()
        {
            base.OnPause();
            _timer.Stop();
            EventLogging.LogWarning("Paused");
        }

        protected override void OnContinue()
        {
            base.OnContinue();
            _timer.Start();
            EventLogging.LogInformation("Resumed");
        }

        private void TimerTick(object sender, ElapsedEventArgs e)
        {
            if (DateTime.Now.Hour == time.Hour && DateTime.Now.Minute == time.Minute)
                RunApplication();
        }

        private void RunApplication()
        {
            EventLogging.LogInformation($"Application is now running: {DateTime.Now}");

            var workerThread = new System.Threading.Thread(Program.Main)
            {
                IsBackground = true
            };
            workerThread.Start();
        }
    }
}
