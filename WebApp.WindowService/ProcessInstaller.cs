using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace WebApp.WindowService
{
    [RunInstaller(true)]
    public partial class ProcessInstaller : Installer
    {
        private readonly ServiceInstaller _serviceInstaller;
        private readonly ServiceProcessInstaller _serviceProcessInstaller;

        private const string InternalServiceName = "SimpleWindowService";

        public ProcessInstaller()
        {
            _serviceInstaller = new ServiceInstaller()
            {
                StartType = ServiceStartMode.Automatic,
                ServiceName = InternalServiceName,
                DisplayName = "Simple Window Service",
                Description = "Demostrate how to build a Window service"
            };
            Installers.Add(_serviceInstaller);

            _serviceProcessInstaller = new ServiceProcessInstaller()
            {
                Account = ServiceAccount.User
            };
            Installers.Add(_serviceProcessInstaller);

            Committed += ProcessInstaller_Committed;
        }

        private void ProcessInstaller_Committed(object sender, InstallEventArgs e)
        {
            var controller = new ServiceController()
            {
                ServiceName = InternalServiceName
            };
            controller.Start();
        }

        public string GetContextParameter(string key)
        {
            string sValue;
            try
            {
                sValue = Context.Parameters[key];
            }
            catch
            {
                sValue = "";
            }
            return sValue;
        }

        protected override void OnBeforeInstall(IDictionary savedState)
        {
            base.OnBeforeInstall(savedState);

            var username = GetContextParameter("user").Trim();
            var password = GetContextParameter("password").Trim();

            if (!string.IsNullOrEmpty(username))
                _serviceProcessInstaller.Username = username;
            if (!string.IsNullOrEmpty(password))
                _serviceProcessInstaller.Password = password;
        }
    }
}
