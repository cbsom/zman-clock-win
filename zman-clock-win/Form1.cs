using Microsoft.Web.WebView2.Core;

namespace ZmanClockWin
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "zman-clock");
            Environment.SetEnvironmentVariable("WEBVIEW2_USER_DATA_FOLDER", path);

            InitializeComponent();
            WindowsDarkMode.SetImmersiveDarkMode(this, true);
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            await webView21.EnsureCoreWebView2Async();
            webView21.CoreWebView2.SetVirtualHostNameToFolderMapping("zman-clock",
                         Path.Combine(Directory.GetCurrentDirectory(), "dist"),
                         CoreWebView2HostResourceAccessKind.Deny);
            webView21.CoreWebView2.Navigate("http://zman-clock/index.html");
        }
    }
}
