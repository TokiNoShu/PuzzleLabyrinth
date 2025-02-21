using PuzzleLabyrinth.Utils;
using System.Windows;
using System.Windows.Controls;

namespace PuzzleLabyrinth
{
    public partial class MainWindow : Window
    {
        private Game game;
        public int RightAnswers;
        public MainWindow(string theme)
        {
            InitializeComponent();
            game = new Game(theme);
            UpdateUI();
            RightAnswers = 0;
        }

        private void UpdateUI()
        {
            if (game.Rooms.Count > 0 && game.CurrentRoomIndex < game.Rooms.Count)
            {
                var currentRoom = game.Rooms[game.CurrentRoomIndex];
                QuestionText.Text = currentRoom.Question;
                Option1Button.Content = currentRoom.Options[0];
                Option2Button.Content = currentRoom.Options[1];
                StatusText.Text = $"Жизни: {game.Lives} | Подсказки: {game.Hints} | Тугрики: {game.Tugriks} | Уровень {game.CurrentRoomIndex+1}/{game.Rooms.Count+1}";
            }
            else
            {
                MessageBox.Show("Комнаты не найдены или игра завершена.");
            }
        }

        private void OptionButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var choice = button.Content.ToString();
            var currentRoom = game.Rooms[game.CurrentRoomIndex];

            if (currentRoom.CheckAnswer(choice))
            {
                game.AddTugriks(3);
                LastRound.Text = "Правильно! +3 тугрика!";
                RightAnswers += 1;
            }
            else
            {
                game.LoseLife();
                if (game.IsGameOver())
                {
                    MessageBox.Show("Какой же ты дегенерат тупорылый, просто тупой олух, лучше убейся и попроси у мамы прощения за то, что ты родился. Не включай комп больше, идиот.");
                    this.Close();
                }
                else
                {
                    LastRound.Text = "Неправильно! -1 жизнь(";
                    UpdateUI();
                }
            }
            game.MoveToNextRoom();
            if (game.IsGameWon())
            {
                MessageBox.Show("Поздравляем, вы выиграли! Молодец! Я твой член сосал!");
                this.Close();
            }
            else UpdateUI();

        }

        private void HintButton_Click(object sender, RoutedEventArgs e)
        {
            if (game.Hints > 0)
            {
                var currentRoom = game.Rooms[game.CurrentRoomIndex];
                MessageBox.Show(currentRoom.Hint);
                game.UseHint();
                UpdateUI();
            }
            else
            {
                MessageBox.Show("У вас нет подсказок!");
            }
        }
        public void OpenShopWindow()
        {
            var shopWindow = new ShopWindow(game);
            shopWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            shopWindow.ShowDialog();
        }
    }
}