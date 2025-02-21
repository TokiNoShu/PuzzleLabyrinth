using System.Windows;

namespace PuzzleLabyrinth
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var themeWindow = new Utils.ThemeSelectionWindow();
            themeWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            themeWindow.Show();
        }
    }

}