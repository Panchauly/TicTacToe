using TicTacToeLib;

namespace MauiApplication;

public partial class MainPage : ContentPage
{
    Game _game;
    Player _player;
    Button _button;
    public MainPage()
    {
        InitializeComponent();
        _game = new Game();
        _player = Player.Player1;
    }

    private void Button_Clicked_00(object sender, EventArgs e)
    {
        _button = (Button)sender;
        try
        {
            Play(0,0);

        }
        catch { }
    }

    private void Play(int x,int y)
    {
        _game.Play(_player, x.ToString(), y.ToString());
        _button.Text = _game.GetGameBoard()[x, y].ToString();

        _player = _player == Player.Player1 ? Player.Player2 : Player.Player1;

        if (_game.GameCompleted())
            lblWinner.Text = $"winner is {_game.Winner()}";
    }

    private void Button_Clicked_01(object sender, EventArgs e)
    {
        _button = (Button)sender;
        try
        {
            Play(0,1);

        }
        catch { }
    }
    
    private void Button_Clicked_02(object sender, EventArgs e)
    {
        _button = (Button)sender;
        try
        {
            Play(0,2);
        }
        catch { }
    }

    private void Button_Clicked_10(object sender, EventArgs e)
    {
        _button = (Button)sender;
        try
        {
            Play(1,0);

        }
        catch { }
    }

    private void Button_Clicked_11(object sender, EventArgs e)
    {
        _button = (Button)sender;
        try
        {
            Play(1,1);


        }
        catch { }
    }

    private void Button_Clicked_12(object sender, EventArgs e)
    {
        _button = (Button)sender;
        try
        {
            Play(1,2);


        }
        catch { }
    }

    private void Button_Clicked_20(object sender, EventArgs e)
    {
        _button = (Button)sender;
        try
        {
            Play(2,0);


        }
        catch { }
    }

    private void Button_Clicked_21(object sender, EventArgs e)
    {

        _button = (Button)sender;
        try
        {
            Play(2,1);


        }
        catch { }
    }

    private void Button_Clicked_22(object sender, EventArgs e)
    {

        _button = (Button)sender;
        try
        {
            Play(2,2);

        }
        catch { }
    }
}

