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
                m_menu.ShowInTaskbar = false;
            }

            if (!m_menu.IsActive)
            {
                System.Drawing.Point clickPosition = Control.MousePosition;
                Rect desktop = System.Windows.SystemParameters.WorkArea;

                m_menu.Top = clickPosition.Y - ((desktop.Height / 2.0 < clickPosition.Y) ? m_menu.Height : 0);
                m_menu.Left = clickPosition.X - ((desktop.Width / 2.0 < clickPosition.X) ? m_menu.Width : 0);

                m_menu.Show();
                m_menu.Activate();
            }
        }

        private void OnMenuDeactivated(object sender, EventArgs e)
        {
            m_menu.Hide();
        }
    }
}
