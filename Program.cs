using System;
using System.IO;
using SplashKitSDK;

public class Program
{
    private static int _score;
    private static int _highScore;
    private static bool _newHighScore;
    private static Font _font = SplashKit.LoadFont("PressStart2P", "PressStart2P.ttf");

    private static Bitmap _easyBitmap = new Bitmap("easy", "button-easy.png");
    private static Bitmap _mediumBitmap = new Bitmap("medium", "button-medium.png");
    private static Bitmap _hardBitmap = new Bitmap("hard", "button-hard.png");
    private static Bitmap _newBitmap = new Bitmap ("New", "button-new.png");
    private static Bitmap _quitBitmap = new Bitmap("quit", "button-quit2.png");

    private static Bitmap _background = new Bitmap("background", "background_window.png");
    private static Bitmap _title = new Bitmap("title", "title.png");
    private static Bitmap _congrat = new Bitmap("congrat", "new-high-score.png");
    private static Bitmap _yourScore = new Bitmap("YourScore", "your-score.png");

    private static SoundEffect _menuTheme = new SoundEffect("MenuTheme","menu.wav");

    public static void Main()
    {
        Window gameWindow = new Window("Jumping Sunfish", 1000, 800);
        NewGame(gameWindow);
    }

    public static void NewGame(Window gameWindow)
    {
        int level = BeginScreen(gameWindow);
        _newHighScore = false;
        JumpingSunfish game = new JumpingSunfish(gameWindow, level);
        //game.AddPipe();
        while ( ! game.Quit ) game.HandleInput();
        if ( game.GameTheme.IsPlaying ) game.GameTheme.Stop();
        _score = game.Score;
        using ( FileStream data = new FileStream("High_score.txt", FileMode.Open, FileAccess.Read) )
        {
            using ( StreamReader read = new StreamReader(data) ) _highScore = SplashKit.ConvertToInteger(read.ReadLine());
            data.Close();
        }
        EndScreen(gameWindow, game);
    }

    public static int BeginScreen(Window gameWindow)
    {
        int level = 0;
        Button easyButton = new Button(450, 300, "Easy");
        Button mediumButton = new Button(450, 400, "Medium");
        Button hardButton = new Button(450, 500, "Hard");
        
        
        while ( level == 0 && ! gameWindow.CloseRequested )
        {
            if ( ! _menuTheme.IsPlaying ) _menuTheme.Play();                        
            SplashKit.DrawBitmap(_background, 0, 0);
            SplashKit.DrawBitmap(_title, 283, 110);

            easyButton.Draw(_easyBitmap);
            mediumButton.Draw(_mediumBitmap);
            hardButton.Draw(_hardBitmap);
            
            gameWindow.Refresh();

            SplashKit.ProcessEvents();
            if ( easyButton.IsClicked() ) level = 1;
            if ( mediumButton.IsClicked() ) level = 2;
            if ( hardButton.IsClicked() ) level = 3;
            //if ( gameWindow.CloseRequested ) gameWindow.Close();
        }
        _menuTheme.Stop();
        return level;
    }

    public static void EndScreen(Window gameWindow, JumpingSunfish game)
    {
        Button newButton = new Button(300, 500, "New Game");
        Button quitButton = new Button(600, 500, "Quit");
        NewHighScore(game);

        while ( ! ( gameWindow.CloseRequested || quitButton.IsClicked() ) )
        {
            if ( ! _menuTheme.IsPlaying ) _menuTheme.Play();
            SplashKit.DrawBitmap(_background, 0, 0);
            SplashKit.DrawBitmap(_title, 283, 110);
            if ( _newHighScore ) SplashKit.DrawBitmap(_congrat, 319, 270);
            else SplashKit.DrawBitmap(_yourScore, 372, 306);
            SplashKit.DrawText($"{_score}", Color.Yellow, _font, 30, 450, 350);

            newButton.Draw(_newBitmap);
            quitButton.Draw(_quitBitmap);
            gameWindow.Refresh();

            SplashKit.ProcessEvents();
            if ( newButton.IsClicked() ) 
            {
                _menuTheme.Stop();
                NewGame(gameWindow);
            }
            //if ( quitButton.IsClicked() ) gameWindow.Close();   
            //if ( gameWindow.CloseRequested ) gameWindow.Close();
        }
    }

    public static void NewHighScore(JumpingSunfish game)
    {
        if ( game.Score > _highScore ) 
            {
                _highScore = game.Score;
                using ( FileStream data = File.Create("High_score.txt") )
                {
                    using ( StreamWriter write = new StreamWriter(data) ) write.WriteLine(_highScore);
                    data.Close();
                }
                _newHighScore = true;
            }
    }
}