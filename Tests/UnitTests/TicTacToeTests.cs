namespace UnitTests;

public class TicTacToeTests
{
    private Game game;
    public TicTacToeTests()
    {
        game = new();
    }

    [Fact]
    public void When_PositionIs_LessThenZero()
    {
        Assert.Throws<InvalidPotitionException>(() => game.Play(Player.Player1, "-1", "0"));
    }

    [Fact]
    public void When_SamePlayer_PlaysConsicutiveChanses()
    {
        game.Play(Player.Player1, "1", "1");
        Assert.Throws<InvalidPlayException>(() => game.Play(Player.Player1, "0", "0"));

    }

    [Fact]
    public void When_Player_PlaysSamePosition()
    {
        game.Play(Player.Player1, "1", "1");
        Assert.Throws<InvalidPlayException>(() => game.Play(Player.Player2, "1", "1"));

    }

    [Fact]
    public void After_Two_FairGamePlay()
    {

        char[,] expected = new char[3, 3];
        expected[0, 0] = 'O';
        expected[1, 1] = 'X';
        game = game.Play(Player.Player1, "1", "1");
        game = game.Play(Player.Player2, "0", "0");

        Assert.Equal(expected, game.GetGameBoard());

    }

    [Fact]
    public void Check_If_Player_WinsTheGame()
    {
        char[,] expected = new char[3, 3];
        expected[0, 0] = 'X';
        expected[0, 1] = 'X';
        expected[0, 2] = 'X';
        expected[1, 1] = 'O';
        expected[1, 2] = 'O';

        game = game.Play(Player.Player1, "0", "0");
        game = game.Play(Player.Player2, "1", "1");
        game = game.Play(Player.Player1, "0", "1");
        game = game.Play(Player.Player2, "1", "2");
        game = game.Play(Player.Player1, "0", "2");

        Assert.True(game.GameCompleted());
        Assert.Equal(Player.Player1, game.Winner());
    }

}