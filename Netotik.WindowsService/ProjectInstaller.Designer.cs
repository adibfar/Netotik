namespace Netotik.WindowsService
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.NetotikMikrotikLoggerProcessInstaller1 = new System.ServiceProcess.ServiceProcessInstaller();
            this.NetotikMikrotikLoggerInstaller1 = new System.ServiceProcess.ServiceInstaller();
            // 
            // NetotikMikrotikLoggerProcessInstaller1
            // 
            this.NetotikMikrotikLoggerProcessInstaller1.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.NetotikMikrotikLoggerProcessInstaller1.Password = null;
            this.NetotikMikrotikLoggerProcessInstaller1.Username = null;
            // 
            // NetotikMikrotikLoggerInstaller1
            // 
            this.NetotikMikrotikLoggerInstaller1.ServiceName = "MikrotikLogger";
            this.NetotikMikrotikLoggerInstaller1.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.NetotikMikrotikLoggerProcessInstaller1,
            this.NetotikMikrotikLoggerInstaller1});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller NetotikMikrotikLoggerProcessInstaller1;
        private System.ServiceProcess.ServiceInstaller NetotikMikrotikLoggerInstaller1;
    }
}