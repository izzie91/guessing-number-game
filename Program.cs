namespace guessing_number_game
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var instructions = """
                I will roll a dice to get a reference number.
                Then, you roll the dice and see if you get the same number as mine.
                You have 3 chances.
                """;
            Console.WriteLine(instructions);
            Console.WriteLine();

            int counter = 0;
            bool gameIsWon = false;

            Game game = new Game();

            // Subscribe to the GameEvent
            game.GameEvent += EventHandlerMethod;

            do
            {
                counter++;
                Console.WriteLine("Please input your number choice:");
                int userGuessedNumber = Int32.Parse(Console.ReadLine());

                if (userGuessedNumber > 6)
                {
                    Console.WriteLine("This is not a number of a dice. You have lost!");
                    break;
                }

                game.CompareUsersNumber(userGuessedNumber);

            } while (gameIsWon == false && counter < 3);

            // Unsubscribe from the GameEvent after the game is over
            game.GameEvent -= EventHandlerMethod;

            void EventHandlerMethod(object sender, GuessNumberDataEventArgs args)
            {
                if (!args.GameIsWon && counter == 3)
                {
                    Console.WriteLine("You have lost :(!");
                    return;
                }
                Console.WriteLine(args.GuessNumberGameOutputMessage);
                gameIsWon = args.GameIsWon;
            }
        }
    }
}
