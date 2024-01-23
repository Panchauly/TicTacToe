using System.Collections.Generic;

namespace TicTacToeLib;

public class Game
{
    private PlayValue[] _plays { get; }
    private int _index;
    private bool _gameCompleted = false;
    private Player? _winner;

    public Game()
    {
        _plays = new PlayValue[9];
        _index = 0;
    }

    private Game(PlayValue[] plays, int index, bool GameCompleted, Player? Winner)
    {
        _plays = plays;
        _index = index;
        _gameCompleted = GameCompleted;
        _winner = Winner;
    }

    public Game Play(Player player, string x, string y)
    {

        if (_gameCompleted)
            throw new InvalidPlayException("Game is already completed");


        int _x = player.Check_PositionIsValid(x, "x");
        int _y = player.Check_PositionIsValid(y, "y");

        var play = PlayValue.Create(player, _x, _y);

        if (_index == 0 || play.IsValidPlay(_plays.Take(_index), _index - 1))
        {

            _plays[_index] = play;

            if (_index < 8)
                _index++;
            else
                _gameCompleted = true;
        }

        if (_index > 4)
        {
            _gameCompleted = _plays.Where(c => c.Symbol == play.Symbol).HaveWon();

            if (_gameCompleted)
                _winner = player;
        }

        return new Game(_plays, _index, _gameCompleted, _winner);

    }

    public char[,] GetGameBoard() => _plays.Take(_index).ToBoard();
    public bool IsInPlay() => !_gameCompleted;
    public bool GameCompleted() => _gameCompleted;
    public Player? Winner() => _winner;

}

internal struct PlayValue
{
    private PlayValue(int x, int y, GameSymbol symbol)
    {
        X = x;
        Y = y;
        Symbol = symbol;
    }

    internal int X { get; }
    internal int Y { get; }
    internal GameSymbol Symbol { get; }
    internal static PlayValue Create(Player player, int x, int y)
    {
        return new PlayValue(x, y, player.GetPlayerSymbol());
    }


}

internal enum GameSymbol
{
    O,
    X
}



public enum Player
{
    Player1,
    Player2
}

public class InvalidPotitionException : Exception
{
    public InvalidPotitionException(Player palyer, string potionName) : base($"Invalid {potionName} for {palyer}") { }
}

public class InvalidPlayerException : Exception
{
    public InvalidPlayerException(Player palyer) : base($"Invalid player {palyer}") { }
}
public class InvalidSymbolException : Exception
{
    public InvalidSymbolException(string symbol) : base($"Invalid {symbol}") { }
}

public class InvalidPlayException : Exception
{
    public InvalidPlayException(string message) : base($"Invalid {message}") { }
}

internal static class ExtentionMethods
{
    internal static int Check_PositionIsValid(this Player player, string postion, string positionName)
    {
        if (!int.TryParse(postion, out int _x) || _x < 0 || _x > 2)
            throw new InvalidPotitionException(player, positionName);
        return _x;
    }

    internal static GameSymbol GetPlayerSymbol(this Player player) => player switch
    {
        Player.Player1 => GameSymbol.X,
        Player.Player2 => GameSymbol.O,
        _ => throw new InvalidPlayerException(player)
    };

    internal static Player GetPlayer(this GameSymbol symbol) => symbol switch
    {
        GameSymbol.X => Player.Player1,
        GameSymbol.O => Player.Player2,
        _ => throw new InvalidSymbolException(symbol.ToString())

    };

    internal static char[,] ToBoard(this IEnumerable<PlayValue> positions)
    {
        Char[,] board = new char[3, 3];

        foreach (var item in positions)
        {
            board[item.X, item.Y] = item.Symbol.ToString()[0];
        }

        return board;
    }
    internal static bool IsValidPlay(this PlayValue play, IEnumerable<PlayValue> plays, int index)
    {
        if (plays.ElementAt(index).Symbol == play.Symbol)
            throw new InvalidPlayException($"{play.Symbol.GetPlayer()} already took there chanse");

        if (plays.Any(c => c.X == play.X && c.Y == play.Y))
            throw new InvalidPlayException($"{play.Symbol.GetPlayer()} already have the position({play.X},{play.X})");

        return true;
    }


    internal static (Dictionary<string, int>,bool) CheckTheWinningPotition(
        this Dictionary<string, int> values,
        string key)
    {
        if(!values.TryGetValue(key, out int value))
        {
            values[key] = 1;
            return (values,false);
        }

        values[key] = value + 1;

        if (values[key] >= 3)
            return (values, true);

        return (values, false);
    }
   
    internal static bool HaveWon(this IEnumerable<PlayValue> plays)
    {
        Dictionary<string, int> potitionsCount = new Dictionary<string, int>();
        bool hasWon = false;


        foreach(var item in plays)
        {

            (potitionsCount,hasWon) = potitionsCount.CheckTheWinningPotition($"X-{item.X}");

            if (hasWon)
                return hasWon;

            (potitionsCount, hasWon) = potitionsCount.CheckTheWinningPotition($"Y-{item.Y}");
            if (hasWon)
                return hasWon;

            if (item.X == item.Y)
                (potitionsCount, hasWon) = potitionsCount.CheckTheWinningPotition($"XY-0");

            if (hasWon)
                return hasWon;

            if (item.X == plays.Count() - 1 - item.Y)
                (potitionsCount, hasWon) = potitionsCount.CheckTheWinningPotition($"YX-0");

            if (hasWon)
                return hasWon;
        }

        return hasWon;
    }


    internal record PositionCount(int position, int count);
}