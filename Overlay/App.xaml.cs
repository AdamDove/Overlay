using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using Overlay.Properties;
using Overlay.Model;

namespace Overlay
{
    public partial class App
    {
        private List<Window> m_windows;

        NotifyIcon m_notifyIcon;
        NotifyMenuWindow m_menu;

        void StartAllWindows(object sender, StartupEventArgs e)
        {
            m_windows = new List<Window>();
            m_windows.Add(new ClockWindow());
            m_windows.Add(new DriveWindow());
            m_windows.Add(new ProcessWindow());

            foreach(Window window in m_windows)
            {
                window.ShowInTaskbar = false;
                window.Left = GetSetting<double>(window, "Left");
                window.Top = GetSetting<double>(window, "Top");
                if (GetSetting<bool>(window, "Enabled"))
                {
                    window.Show();
                }
            }

            m_notifyIcon = new NotifyIcon();
            m_notifyIcon.Icon = Icon.ExtractAssociatedIcon(Assembly.GetEntryAssembly().Location);
            m_notifyIcon.MouseClick += OnNotifyIconMouseClick;
            m_notifyIcon.Text = "Overlay Widgets";
            m_notifyIcon.Visible = true;
        }

        private T GetSetting<T>(Window window, string setting)
        {
            string propertyName = window.GetType().Name + setting;
            object property = Settings.Default[propertyName];

            if (property is T)
            {
                return (T)property;
            }

            throw new ArgumentException();
        }

        private void OnNotifyIconMouseClick(object sender, MouseEventArgs e)
        {
            if (m_menu == null)
            {
                m_menu = new NotifyMenuWindow();
                m_menu.Deactivated += OnMenuDeactivated;
                m_menu.Closed += OnMenuClosed;
                m_menu.ShowInTaskbar = false;
            }

            if (!m_menu.IsActive)
            {
                System.Drawing.Point clickPosition = Control.MousePosition;
                Rect desktop = System.Windows.SystemParameters.WorkArea;


                m_menu.Show();
                PositionWindowAtTaskbar(m_menu, Control.MousePosition);
                m_menu.Activate();
            }
        }

        private void OnMenuClosed(object sender, EventArgs e)
        {
            m_menu = null;
        }

        private void PositionWindowAtTaskbar(Window window, System.Drawing.Point referencePoint)
        {
            Rectangle workingArea = Screen.GetWorkingArea(referencePoint);

            if (referencePoint.X < workingArea.Left)
            {
                referencePoint.X = workingArea.Left;
            }
            else if (referencePoint.X > workingArea.Right)
            {
                referencePoint.X = workingArea.Right;
            }

            if (referencePoint.Y > workingArea.Bottom)
            {
                referencePoint.Y = workingArea.Bottom;
            }
            else if (referencePoint.Y < workingArea.Top)
            {
                referencePoint.Y = workingArea.Top;
            }
            
            window.Top = referencePoint.Y - ((workingArea.Height / 2.0 < referencePoint.Y) ? window.Height : 0);
            window.Left = referencePoint.X - ((workingArea.Width / 2.0 < referencePoint.X) ? window.Width : 0);
        }

        private void OnMenuDeactivated(object sender, EventArgs e)
        {
            m_menu.Hide();
        }

        private void OnApplicationExit(object sender, ExitEventArgs e)
        {
            m_notifyIcon.Visible = false;
            m_notifyIcon = null;
        }
    }
}
