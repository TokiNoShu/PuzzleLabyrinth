using PuzzleLabyrinth.Utils;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Threading;

namespace PuzzleLabyrinth
{
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer;
        private Game game;
        public int RightAnswers;
        public TimeSpan RemainingTime { get; set; }
        public MainWindow(string theme)
        {
            InitializeComponent();
            game = new Game(theme);
            UpdateUI();
            RightAnswers = 0;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            RemainingTime = TimeSpan.FromMinutes(15);
            string imagePath = "";
            switch (theme)
            {
                case "Дота":
                    imagePath = "Images/dota.webp";
                    break;
                case "Загадки":
                    imagePath = "Images/riddler.jpg";
                    break;
            }
            ImageBrush backgroundBrush = (ImageBrush)this.Resources["BackgroundBrush"];
            backgroundBrush.ImageSource = new BitmapImage(new Uri(imagePath, UriKind.Relative));
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
                game.AddTugriks(5);
                LastRound.Text = "Правильно! +5 тугриков!";
                RightAnswers += 1;
            }
            else
            {
                game.LoseLife();
                if (game.IsGameOver())
                {
                    timer.Stop();
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
            if (game.IsShopRoom())
            {
                OpenShopWindow();
            }

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
        private void Timer_Tick(object sender, EventArgs e)
        {
            RemainingTime -= timer.Interval;
            Timer.Text = RemainingTime.ToString(@"mm\:ss");
            if (RemainingTime <= TimeSpan.Zero)
            {
                timer.Stop();
                MessageBox.Show("Какой же ты дегенерат тупорылый, просто тупой олух, лучше убейся и попроси у мамы прощения за то, что ты родился. Не включай комп больше, идиот.");
                this.Close();
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            timer.Start();
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            timer.Stop();
        }
    }
}