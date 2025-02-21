using System.Windows;
using System.Windows.Controls;

namespace PuzzleLabyrinth.Utils
{
    public partial class ThemeSelectionWindow : Window
    {
        public ThemeSelectionWindow()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (ThemeComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                string selectedTheme = selectedItem.Content.ToString();
                var mainWindow = new MainWindow(selectedTheme);
                mainWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Выберите тематику!");
            }
        }
    }
}