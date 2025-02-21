using System.Collections.Generic;

namespace PuzzleLabyrinth.Utils
{
    public class Game
    {
        public List<Room> Rooms { get; }
        public int CurrentRoomIndex { get; set; }
        public int Lives { get; set; }
        public int Hints { get; set; }
        public int Tugriks { get; set; }

        public Game(string theme)
        {
            Rooms = GetRoomsByTheme(theme);
            CurrentRoomIndex = 0;
            Lives = 3;
            Hints = 2;
            Tugriks = 0;
        }

        private List<Room> GetRoomsByTheme(string theme)
        {
            switch (theme)
            {
                case "Загадки":
                    return new List<Room>
            {
                new Room("Что больше: 2 + 2 или 3 + 3", "2 + 2", "3 + 2"),
                new Room("Какой месяц имеет 28 дней?", "Февраль", "Все месяцы"),
                new Room("Я всегда впереди, но никогда не могу двигаться", "Время", "Свет"),
            };
                case "Фильмы":
                    return new List<Room>
            {
                new Room("Какой фильм получил Оскар в 2020 году?", "Паразиты", "Джокер"),
                new Room("Кто режиссер фильма 'Начало'?", "Кристофер Нолан", "Стивен Спилберг"),
            };
                default:
                    return new List<Room>();
            }
        }

        public void MoveToNextRoom()
        {
            if (CurrentRoomIndex < Rooms.Count - 1)
            {
                CurrentRoomIndex++;
            }
        }

        public void UseHint()
        {
            if (Hints > 0)
            {
                Hints--;
            }
        }

        public void AddTugriks(int amount)
        {
            Tugriks += amount;
        }

        public void LoseLife()
        {
            Lives--;
        }

        public void AddLife()
        {
            Lives++;
        }

        public bool IsGameOver()
        {
            return Lives <= 0;
        }

        public bool IsGameWon()
        {
            return CurrentRoomIndex == Rooms.Count - 1;
        }

        public bool IsShopRoom()
        {
            return (CurrentRoomIndex + 1) % 6 == 0;
        }
    }
}