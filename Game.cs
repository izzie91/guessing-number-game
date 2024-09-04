using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace guessing_number_game;

public class Game
{
    private Random _rnd = new Random();
    private readonly int _generatedRandomNumber;
    private bool _gameIsWon = false;

    public event EventHandler<GuessNumberDataEventArgs> GameEvent;

    public Game()
    {
        // Generate a random number between 1 and 6 (like a dice)
        _generatedRandomNumber = _rnd.Next(1, 6);
        Console.WriteLine("My generated number is " + _generatedRandomNumber);
    }

    public void CompareUsersNumber(int guessedNumber)
    {      
        string message = "Wrong, keep going! :(";

        if (guessedNumber == _generatedRandomNumber)
        {
            message = "You guessed it!! Well done! :)";
            _gameIsWon = true;
        }      

        OnNumberEvaluated(new GuessNumberDataEventArgs
        {
            GuessNumberGameOutputMessage = message,
            GameIsWon = _gameIsWon
        });
    }

    protected virtual void OnNumberEvaluated(GuessNumberDataEventArgs e) // event raising method
    {
        EventHandler<GuessNumberDataEventArgs> handler = GameEvent;
        if (handler != null)
        {
            handler(this, e);
        }
    }
}

public class GuessNumberDataEventArgs : EventArgs
{
    public string GuessNumberGameOutputMessage { get; set; }

    public bool GameIsWon { get; set; }
}

