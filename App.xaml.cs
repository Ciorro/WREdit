using System.Windows;
using WREdit.ViewModels;

namespace WREdit;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        new MainWindow
        {
            DataContext = new AppViewModel()
        }.Show();

        base.OnStartup(e);
    }
}

