using System.Collections.Generic;

namespace PuzzleLabyrinth.Utils
{
    public class Room
    {
        public string Question { get; }
        public List<string> Options { get; }
        public string CorrectAnswer { get; }

        public Room(string question, string option1, string option2, string rightOption)
        {
            Question = question;
            Options = new List<string>() { option1, option2 };
            CorrectAnswer = rightOption;
        }

        public bool CheckAnswer(string choice)
        {
            return choice == CorrectAnswer;
        }
    }
}