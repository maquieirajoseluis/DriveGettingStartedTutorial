using System;
using System.ComponentModel;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using WcfServiceLibrary;

namespace WpfHostApplication
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceHost authServiceHost;
        private static AuthService authService = new AuthService();
        private static bool createdNew;
        private bool isExit;
        private static Mutex mutex = new Mutex(true, "F7125545-C3CB-4AAB-A2B8-8ECCA1B772FA", out createdNew);
        private System.Windows.Forms.NotifyIcon notifyIcon;

        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                if (!mutex.WaitOne(TimeSpan.Zero, true))
                {
                    Current.Shutdown();
                    return;
                }
            }
            catch (AbandonedMutexException)
            {

            }

            var baseAddress = new Uri("net.pipe://localhost/WcfServiceLibrary/");
            authServiceHost = new ServiceHost(typeof(AuthService), baseAddress);

            try
            {
                authServiceHost.AddServiceEndpoint(typeof(IAuthService), new NetNamedPipeBinding(), "AuthService");
                authServiceHost.Open();
            }
            catch (CommunicationException exception)
            {
                Console.WriteLine("An exception occurred: {0}", exception.Message);
                authServiceHost.Abort();
            }

            base.OnStartup(e);
            MainWindow = new MainWindow();
            MainWindow.Closing += MainWindow_Closing;

            notifyIcon = new System.Windows.Forms.NotifyIcon();
            notifyIcon.Icon = WpfHostApplication.Properties.Resources.AppIcon;
            notifyIcon.Visible = true;

            CreateContextMenuStrip();
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            if (!isExit)
            {
                e.Cancel = true;
                MainWindow.Hide();
            }
        }

        private void CreateContextMenuStrip()
        {
            notifyIcon.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip();
            notifyIcon.ContextMenuStrip.Items.Add("Sign In").Click += async (s, e) => await ShowSignInView();
            notifyIcon.ContextMenuStrip.Items.Add("Exit").Click += (s, e) => ExitApplication();
        }

        private void ExitApplication()
        {
            isExit = true;
            MainWindow.Close();
            notifyIcon.Dispose();
            notifyIcon = null;
        }

        private async Task ShowSignInView()
        {
            await authService.AuthorizeAsync();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            authServiceHost?.Close();
            if (createdNew)
            {
                mutex.ReleaseMutex();
            }
            base.OnExit(e);
        }
    }
}
