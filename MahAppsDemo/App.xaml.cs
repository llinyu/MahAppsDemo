using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MahApps.Metro;

namespace MahAppsDemo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            
            var theme = ThemeManager.DetectAppStyle(this);
            var appTheme = ThemeManager.GetAppTheme("BaseDark");
            ThemeManager.ChangeAppStyle(this, theme.Item2, appTheme);

            Splash.Instance.Show();
            var win = new MainWindow();
            win.Show();
            Splash.Instance.Close();
        }
    }
}
