namespace W10.Services;

public class GameEngine
{
    private readonly Menu _menu;

    public GameEngine(Menu menu)
    {
        _menu = menu;
    }

    public void Run()
    {
        _menu.Show();
    }
}