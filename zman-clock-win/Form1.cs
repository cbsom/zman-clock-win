using System.Runtime.InteropServices;
using Microsoft.Web.WebView2.Core;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "zman-clock");
            Environment.SetEnvironmentVariable("WEBVIEW2_USER_DATA_FOLDER", path);

            InitializeComponent();
            UseImmersiveDarkMode(this.Handle, true);
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            await webView21.EnsureCoreWebView2Async();
            webView21.CoreWebView2.SetVirtualHostNameToFolderMapping("zman-clock",
                         Path.Combine(Directory.GetCurrentDirectory(), "dist"),
                         CoreWebView2HostResourceAccessKind.DenyCors);
            webView21.CoreWebView2.Navigate("http://zman-clock/index.html");
        }

        [DllImport("dwmapi.dll")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

        private const int DWMWA_USE_IMMERSIVE_DARK_MODE_BEFORE_20H1 = 19;
        private const int DWMWA_USE_IMMERSIVE_DARK_MODE = 20;

        private static bool UseImmersiveDarkMode(IntPtr handle, bool enabled)
        {
            if (IsWindows10OrGreater(17763))
            {
                var attribute = DWMWA_USE_IMMERSIVE_DARK_MODE_BEFORE_20H1;
                if (IsWindows10OrGreater(18985))
                {
                    attribute = DWMWA_USE_IMMERSIVE_DARK_MODE;
                }

                int useImmersiveDarkMode = enabled ? 1 : 0;
                return DwmSetWindowAttribute(handle, (int)attribute, ref useImmersiveDarkMode, sizeof(int)) == 0;
            }

            return false;
        }

        private static bool IsWindows10OrGreater(int build = -1)
        {
            return Environment.OSVersion.Version.Major >= 10 && Environment.OSVersion.Version.Build >= build;
        }
    }
}
