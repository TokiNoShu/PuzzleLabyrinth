using System.Windows;
using PuzzleLabyrinth.Utils;

namespace PuzzleLabyrinth.Utils
{
    public partial class ShopWindow : Window
    {
        private Game game;

        public ShopWindow(Game game)
        {
            InitializeComponent();
            this.game = game;
            UpdateUI();
        }

        private void UpdateUI()
        {
            CurrencyText.Text = $"Ваши тугрики: {game.Tugriks}";
        }

        private void BuyHintButton_Click(object sender, RoutedEventArgs e)
        {
            if (game.Tugriks >= 10 && game.Hints < 5)
            {
                game.Hints++;
                game.Tugriks -= 10;
                MessageBox.Show("Подсказка куплена!");
                UpdateUI();
            }
            else
            {
                MessageBox.Show("Недостаточно тугриков или достигнуто максимальное количество подсказок.");
            }
        }

        private void BuyLifeButton_Click(object sender, RoutedEventArgs e)
        {
            if (game.Tugriks >= 20 && game.Lives < 5)
            {
                game.Lives++;
                game.Tugriks -= 20;
                MessageBox.Show("Жизнь куплена!");
                UpdateUI();
            }
            else
            {
                MessageBox.Show("Недостаточно тугриков или достигнуто максимальное количество жизней.");
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}