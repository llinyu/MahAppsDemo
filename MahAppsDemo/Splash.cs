using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace MahAppsDemo
{
    /// <summary>
    /// Splash screen control
    /// </summary>
    public sealed class Splash
    {
        private SplashWindow _splash;
        private Dispatcher _dispatcher;
        private bool _isShowing;

        public static Splash Instance { get; } = new Splash();

        /// <summary>
        /// Show splash screen.
        /// </summary>
        public void Show()
        {
            if (_isShowing)
            {
                return;
            }

            var signal = new AutoResetEvent(false);

            ThreadStart showSplash = () =>
            {
                _splash = new SplashWindow();

                _dispatcher = _splash.Dispatcher;

                _dispatcher.BeginInvoke((Action)(() =>
                {
                    _splash.Closed += (s, e) =>
                    {
                        _isShowing = false;
                        _dispatcher.InvokeShutdown();
                    };
                    _splash.Show();
                    _isShowing = true;
                    signal.Set();
                }));

                Dispatcher.Run();
            };

            var splashThread = new Thread(showSplash)
            {
                Name = "SplashThread",
                IsBackground = true
            };
            splashThread.SetApartmentState(ApartmentState.STA);
            splashThread.Start();

            signal.WaitOne();
        }
        
        /// <summary>
        /// Close splash screen
        /// </summary>
        public void Close()
        {
            if (_isShowing)
            {
                _dispatcher.Invoke(() => _splash.Close());
            }
        }

    }
}
