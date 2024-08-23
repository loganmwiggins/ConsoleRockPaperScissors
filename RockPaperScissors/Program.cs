using System;
using System.Drawing;
using Console = Colorful.Console;

namespace RockPaperScissors
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Game description
            PrintGameDescription();

            // User prompts
            string? numGamesStr = GetUserInput("? How many games would you like to play? Best of:");

            while (string.IsNullOrEmpty(numGamesStr) || int.Parse(numGamesStr) % 2 == 0)
            {
                if (string.IsNullOrEmpty(numGamesStr))
                    numGamesStr = GetUserInput("* Please enter a valid amount of games:");
                else if (int.Parse(numGamesStr) % 2 == 0)
                    numGamesStr = GetUserInput("* Please enter an odd amount of games:");
            }

            int numGames = int.Parse(numGamesStr);
            Console.WriteLine($"Awesome. Best {numGames/2 + 1} of {numGames} wins. Good luck!");
            Console.WriteLine();

            // Game logic
            PlayGame(numGames);
        }

        // --------------------------------------------------------------------------------------------------------------

        static string? GetUserInput(string prompt)
        {
            Console.Write(prompt + " ");
            string? inputLine = Console.ReadLine();

            return string.IsNullOrEmpty(inputLine) ? null : inputLine;
        }

        public static void PlayGame(int numGames)
        {
            // Game variables
            int currentTurn = 0;
            int userScore = 0;
            int oppScore = 0;
            int bestOfNum = numGames/2 + 1;

            // Game loop
            while (userScore != bestOfNum && oppScore != bestOfNum)
            {
                string userChoice = GetUserInput("Choose (R)Rock ✊, (P)Paper 🖐️, (S)Scissors ✌️:").ToLower();
                string oppChoice;

                if (string.IsNullOrEmpty(userChoice) || (userChoice != "r" && userChoice != "p" && userChoice != "s"))
                {
                    Console.WriteLine("Please enter a valid selection (R, P, or S).");
                    Console.WriteLine();
                    continue;
                }
                else {
                    Console.WriteLine();
                    oppChoice = GenerateOpponentChoice();
                    
                    Console.WriteLine($"YOU: ({userChoice.ToUpper()})");
                    Console.WriteLine(ConvertChoiceToArt(userChoice));

                    Console.WriteLine($"OPPONENT: ({oppChoice.ToUpper()})");
                    Console.WriteLine(ConvertChoiceToArt(oppChoice));
                    Console.WriteLine();

                    IncrementScore(userChoice, oppChoice, ref userScore, ref oppScore);
                    Console.WriteLine($"Your Score: {userScore}");
                    Console.WriteLine($"Opponent Score: {oppScore}");
                    Console.WriteLine();

                    currentTurn++;
                }
            }

            // End game stats
            PrintGameOverStats(userScore, oppScore);
        }

        public static string GenerateOpponentChoice()
        {
            Random random = new Random();
            int randomNum = random.Next(0, 3); // Generates a number between 0 (inclusive) and 3 (exclusive)

            if (randomNum == 0) return "r";
            else if (randomNum == 1) return "p";
            else return "s";
        }

        public static void IncrementScore(string userChoice, string oppChoice, ref int userScore, ref int oppScore)
        {
            if (userChoice == "r")
            {
                if (oppChoice == "p")
                {
                    Console.WriteLine("UH OH!");
                    oppScore++;
                }
                else if (oppChoice == "s")
                {
                    Console.WriteLine("NICE!");
                    userScore++;
                }
                else { Console.WriteLine("TIE!"); }
            }
            else if (userChoice == "p")
            {
                if (oppChoice == "s")
                {
                    Console.WriteLine("OH NO!");
                    oppScore++;
                }
                else if (oppChoice == "r")
                {
                    Console.WriteLine("DUB!");
                    userScore++;
                }
                else { Console.WriteLine("TIE!"); }
            }
            else
            {
                if (oppChoice == "r")
                {
                    Console.WriteLine("OOPS!");
                    oppScore++;
                }
                else if (oppChoice == "p")
                {
                    Console.WriteLine("GOOD JOB!");
                    userScore++;
                }
                else { Console.WriteLine("TIE!"); }
            }
        }

        public static string ConvertChoiceToArt(string choice)
        {
            // Rock
            if (choice == "r")
                return "\r\n    _______\r\n---'   ____)\r\n      (_____)\r\n      (_____)\r\n      (____)\r\n---.__(___)\r\n";

            // Paper
            else if (choice == "p")
                return "\r\n    _______\r\n---'   ____)____\r\n          ______)\r\n          _______)\r\n         _______)\r\n---.__________)\r\n";

            // Scissors
            else
                return "\r\n    _______\r\n---'   ____)____\r\n          ______)\r\n       __________)\r\n      (____)\r\n---.__(___)\r\n";
        }

        public static void PrintGameDescription()
        {
            Console.WriteLine("\r\n\r\n██████████████████████████\r\n█▄─▄▄▀█─▄▄─█─▄▄▄─█▄─█─▄███\r\n██─▄─▄█─██─█─███▀██─▄▀████\r\n▀▄▄▀▄▄▀▄▄▄▄▀▄▄▄▄▄▀▄▄▀▄▄▀▀▀\r\n█████████████████████████████████\r\n█▄─▄▄─██▀▄─██▄─▄▄─█▄─▄▄─█▄─▄▄▀███\r\n██─▄▄▄██─▀─███─▄▄▄██─▄█▀██─▄─▄███\r\n▀▄▄▄▀▀▀▄▄▀▄▄▀▄▄▄▀▀▀▄▄▄▄▄▀▄▄▀▄▄▀▀▀\r\n██████████████████████████████████████████████\r\n█─▄▄▄▄█─▄▄▄─█▄─▄█─▄▄▄▄█─▄▄▄▄█─▄▄─█▄─▄▄▀█─▄▄▄▄█\r\n█▄▄▄▄─█─███▀██─██▄▄▄▄─█▄▄▄▄─█─██─██─▄─▄█▄▄▄▄─█\r\n▀▄▄▄▄▄▀▄▄▄▄▄▀▄▄▄▀▄▄▄▄▄▀▄▄▄▄▄▀▄▄▄▄▀▄▄▀▄▄▀▄▄▄▄▄▀\r\n");
            Console.WriteLine("Welcome to console Rock, Paper, Scissors by Logan Wiggins.");
            Console.WriteLine("Let's get started! (Type Ctrl+C at any time to end the game)");
            Console.WriteLine();
        }

        public static void PrintGameOverStats(int userScore, int oppScore)
        {
            Console.WriteLine("\r\n█████▀█████████████████████\r\n█─▄▄▄▄██▀▄─██▄─▀█▀─▄█▄─▄▄─█\r\n█─██▄─██─▀─███─█▄█─███─▄█▀█\r\n▀▄▄▄▄▄▀▄▄▀▄▄▀▄▄▄▀▄▄▄▀▄▄▄▄▄▀\r\n████████████████████████\r\n█─▄▄─█▄─█─▄█▄─▄▄─█▄─▄▄▀█\r\n█─██─██▄▀▄███─▄█▀██─▄─▄█\r\n▀▄▄▄▄▀▀▀▄▀▀▀▄▄▄▄▄▀▄▄▀▄▄▀\r\n");
            Console.WriteLine("FINAL SCORE:");
            Console.WriteLine($"Your Score: {userScore}");
            Console.WriteLine($"Opponent Score: {oppScore}");

            if (userScore > oppScore)
                Console.WriteLine("__   __           __        ___       \r\n\\ \\ / /__  _   _  \\ \\      / (_)_ __  \r\n \\ V / _ \\| | | |  \\ \\ /\\ / /| | '_ \\ \r\n  | | (_) | |_| |   \\ V  V / | | | | |\r\n  |_|\\___/ \\__,_|    \\_/\\_/  |_|_| |_|");
            else
                Console.WriteLine("__   __            _                   \r\n\\ \\ / /__  _   _  | |    ___  ___  ___ \r\n \\ V / _ \\| | | | | |   / _ \\/ __|/ _ \\\r\n  | | (_) | |_| | | |__| (_) \\__ \\  __/\r\n  |_|\\___/ \\__,_| |_____\\___/|___/\\___|");

            Console.WriteLine();
        }
    }
}