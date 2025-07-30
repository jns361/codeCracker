namespace codeCracker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameLoop();
        }

        static string keepPlaying = "y";

        static void GameLoop()
        {

            while (keepPlaying == "y")
            {
                Console.Clear();
                int guessLimit = DifficultyChoice();
                //Console.Write(guessLimit);                                                                        (Debugline)
                string secretCode = RandomCode();

                Console.WriteLine("\nThe 4-digit secret code is generated.");

                int guessCount = 0;

                while (true)

                {
                    guessCount++;

                    string userGuess = GetUserGuess();

                    var (correctPosition, wrongPosition) = EvaluateGuess(userGuess, secretCode);                    //get both variables out of one methodcall by making use of tuples


                    Console.WriteLine($"{correctPosition} digit/s are correct and on the right position!");
                    Console.WriteLine($"{wrongPosition} digit/s are correct but on the wrong position!");

                    //winning, no tries left or keep playing
                    if (correctPosition == 4)                                                                       //check if 4/4 digits are guessed in the correct position
                    {
                        Console.WriteLine("You got the code right!");
                        Console.WriteLine($"It was {userGuess}.");
                        break;
                    }

                    if (correctPosition != 4)
                    {
                        if (guessCount == guessLimit)
                        {
                            Console.WriteLine("\nNo more tries left.");
                            Console.WriteLine($"The code was {secretCode}.");
                            break;
                        }
                        else if (guessCount == guessLimit - 1)
                        {
                            Console.WriteLine("\nOnly 1 try left!");
                        }
                    }
                    else
                    {
                        continue;
                    }


                }

                while (true)
                {
                    Console.Write("\nDo you want to play again? \nY or N: ");
                    keepPlaying = Console.ReadLine().ToLower();
                    if (keepPlaying != "y" && keepPlaying != "n")
                    {
                        Console.WriteLine("Invalid input!");
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        static string GetUserGuess()
        {
            while (true)
            {
                Console.Write("\nGuess: ");
                string userGuess = Console.ReadLine();

                if (userGuess.Length == 4 && int.TryParse(userGuess, out _))
                {
                    return userGuess;
                }
                else
                {
                    Console.WriteLine("Wrong input. Check and enter again!");
                    continue;
                }
            }
        }


        static (int correctPosition, int wrongPosition) EvaluateGuess(string userGuess, string secretCode)
        {
            int correctPosition = 0;
            int wrongPosition = 0;

            char[] secretArray = secretCode.ToCharArray();
            char[] guessArray = userGuess.ToCharArray();

            for (int i = 0; i < secretCode.Length; i++)
            {
                if (userGuess[i] == secretCode[i])
                {
                    correctPosition++;
                    secretArray[i] = '*';
                    guessArray[i] = '#';
                }
            }

            for (int i = 0; i < guessArray.Length; i++)
            {
                if (guessArray[i] != '#')
                {
                    int index = Array.IndexOf(secretArray, guessArray[i]);
                    if (index != -1)
                    {
                        wrongPosition++;
                        secretArray[index] = '*';
                    }
                }
            }
            return (correctPosition, wrongPosition);
        }

        static Random generator = new Random();

        static string RandomCode()
        {
            return generator.Next(1000, 10000).ToString();
        }


        static int DifficultyChoice()
        {
            string choice = string.Empty;
            while (true)
            {
                Console.WriteLine("Choose your difficulty:\n1 - Easy -> 15 guesses" +
                    "\n2 - Medium -> 10 guesses\n3 - Hard -> 6 guesses");

                Console.Write("Choice: ");
                choice = Console.ReadLine();

                if (choice != "1" && choice != "2" && choice != "3")
                {
                    Console.WriteLine("No accepted choice! You may only enter '1', '2' or '3'!\n");
                    continue;
                }
                else
                {
                    break;
                }
            }

            //declaring the amount of guesses to a variable to handle it easier
            int intChoice = int.Parse(choice);
            if (intChoice == 1)
            {
                int guessLimit = 15;
                return guessLimit;
            }
            else if (intChoice == 2)
            {
                int guessLimit = 10;
                return guessLimit;
            }
            else
            {
                int guessLimit = 6;
                return guessLimit;
            }

        }
    }
}
