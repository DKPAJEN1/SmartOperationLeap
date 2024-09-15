using DevExpress.Xpf.Core;
using System.Windows;

namespace SmartOperationDx
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        static App()
        {
            ApplicationThemeHelper.ApplicationThemeName = Theme.Win11DarkName;

        }
    }
}
