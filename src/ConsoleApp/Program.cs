
using TicTacToeLib;

Game game = new Game();
while (game.IsInPlay())
{
   var completed = PlayFor(Player.Player1);

    if (completed)
        break;

    completed =  PlayFor(Player.Player2);
    
    if(completed)
        break;
}

bool PlayFor(Player player)
{
    Console.WriteLine($"{player} enter your position(x,y)(0-2)");

    var chance = Console.ReadLine();

    if(string.IsNullOrEmpty(chance))
    {
        Console.WriteLine("Plesae provide valid input");
        PlayFor(player);
    }

    var positions = chance.Split(",");

    if(positions.Length < 2)
    {
        Console.WriteLine("Plesae provide valid input");
        PlayFor(player);
    }

    try
    {
        game = game.Play(player, positions[0], positions[1]);
        PrintBoard(game.GetGameBoard());
        if(game.GameCompleted())
        {
            Console.WriteLine();

            if (game.Winner() is not null)
                Console.WriteLine($"{game.Winner()} won the game.");
            else
                Console.WriteLine($"Game is draw.");

            return true;
        }


    }
    catch(Exception ex)
    {
        Console.WriteLine(ex.Message);
        PlayFor(player);
    }
    return false;
}

void PrintBoard(char[,] chars)
{
    for(int i = 0; i <= 2; i++)
    {
        for (int j = 0; j <= 2; j++)
        {
            if (j == 2)
                Console.Write("|");
            if (chars[i, j] == '\0')
            {
                Console.Write("   ");
                if (j == 0)
                    Console.Write("|");
                continue;
            }

            Console.Write($" {chars[i, j]} ");

            if (j == 0)
                Console.Write("|");
        }
        Console.WriteLine();
        if (i<2)
            Console.WriteLine("-----------");
    }
}