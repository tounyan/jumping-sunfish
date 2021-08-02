using System;
using System.Collections.Generic;
using SplashKitSDK;

public class JumpingSunfish
{
    private int _level, _numLives = 5, _score = 0;
    private const float _volume = 0.5f;
    private Window _gameWindow;
    private Fish _fish;
    //private SoundEffect _sfx = new SoundEffect("ding", "ding.mp3");

    private Bitmap _background = new Bitmap("background", "game-bg.png");
    private Bitmap _border = new Bitmap("border", "game-border.png");
    private Bitmap _heartBitmap = new Bitmap("heart", "heart.png");
    private Bitmap _pauseBitmap = new Bitmap("PauseButton", "button-pause.png");
    private Bitmap _pauseBitmapInactive = new Bitmap("PauseButtonInactive", "button-pause-disable.png");
    private Bitmap _resumeBitmap = new Bitmap("ResumeButton", "button-resume.png");
    private Bitmap _resumeBitmapInactive = new Bitmap("ResumeButtonInactive", "button-resume-disable.png");
    private Bitmap _quitBitmap = new Bitmap("QuitButton", "button-quit.png");
    
    private SoundEffect _ding = new SoundEffect("ding", "ding.mp3");
    private SoundEffect _bump = new SoundEffect("bump", "bump.wav");
    private SoundEffect _gameTheme = new SoundEffect("GameTheme", "theme.wav");


    //private Color _bgColor = Color.Black;
    private Font _font = SplashKit.LoadFont("PressStart2P", "PressStart2P.ttf");
    private Button _pauseButton = new Button(825, 400, "Pause");
    private Button _resumeButton = new Button(825, 500, "Resume");
    private Button _quitButton = new Button(825, 600, "Quit");
    private List<Seaweed> _seaweeds = new List<Seaweed>();
    private List<Seaweed> _deleteSeaweed = new List<Seaweed>();
    
    public int Score { get { return _score; } }
    public bool Quit { get { return ( _gameWindow.CloseRequested || _numLives == 0 || _quitButton.IsClicked() ); } }
    public SoundEffect GameTheme { get { return _gameTheme; } }
    
    public JumpingSunfish(Window gameWindow, int level)
    {
        _gameWindow = gameWindow;
        _level = level;
        _fish = new Fish(_gameWindow);
    }

    /*public void Play()
    {
        //AddPipe();
        while ( ! Quit ) HandleInput();
    }*/
    public void Draw()
    {
        int space = 770;

        SplashKit.DrawBitmap(_background, 50, 50);
        foreach (Seaweed seaweed in _seaweeds) seaweed.Draw();
        SplashKit.DrawBitmap(_border, 0, 0);
        _fish.Draw();

        SplashKit.DrawText($"Score: {_score}", Color.Orange, _font, 15, space, 50);
        SplashKit.DrawText("Lives", Color.Orange, _font, 15, space, 100);
        for ( int i = 0; i < _numLives; i++ )
        {
            _heartBitmap.Draw(space, 150);
            space += 40;
        }
        
        if ( _pauseButton.Active ) _pauseButton.Draw(_pauseBitmap);
        else _pauseButton.Draw(_pauseBitmapInactive);
        if ( _resumeButton.Active ) _resumeButton.Draw(_resumeBitmap);
        else _resumeButton.Draw(_resumeBitmapInactive);
        _quitButton.Draw(_quitBitmap);
        
        _gameWindow.Refresh();
    }

    public void HandleInput()
    {
        /*if ( _pauseButton.IsClicked() )
        {
            Console.WriteLine("Pause!");
            _pauseButton.Active = false;
            _resumeButton.Active = true;
        }
        if ( _resumeButton.IsClicked() )
        {
            Console.WriteLine("Resume!");
            _resumeButton.Active = false;
            _pauseButton.Active = true;
        }
        if ( _resumeButton.Active == false )
        {
            Draw();
            Update();
        }
        if ( _pauseButton.Active == false )
        {
            Draw();
        }*/
        SplashKit.ProcessEvents();
        _resumeButton.Active = false;
        _pauseButton.Active = true;
        while ( ! _pauseButton.IsClicked() )
        {
            if ( ! _gameTheme.IsPlaying ) _gameTheme.Play(_volume);
            SplashKit.ProcessEvents();
            if ( Quit ) break;
            Draw();
            Update();
        }
        //SplashKit.ProcessEvents();
        _pauseButton.Active = false;
        _resumeButton.Active = true;
        while ( ! _resumeButton.IsClicked() ) 
        {
            if ( ! _gameTheme.IsPlaying ) _gameTheme.Play(_volume);
            SplashKit.ProcessEvents();
            if ( Quit ) break;
            Draw();
        }
    }

    /*public void Update()
    {
        //
        _bird.HandleInput();
        foreach (Pipe pipe in _pipes)
        {
            _bird.CollidedWith(pipe);
            if ( pipe.Item != null ) _bird.CollidedWith(pipe.Item);
            _bird.AddScore(pipe);
            pipe.Update();
        }
        if ( _pipes[_pipes.Count - 1].X < 500 ) AddPipe();
        _bird.StayOnWindow();//
        _bird.HandleInput();
        foreach ( Pipe pipe in _pipes ) 
        {
            pipe.Update();
            if ( _bird.CollidedWith(pipe.Item) )
            {
                if ( pipe.Item is Gem ) 
                {
                    pipe.Item.Active = false;
                //pipe.Item = null;
                    _score += 100;
                }
                if ( pipe.Item is Potion )
                {
                    pipe.Item.Active = false;
                //pipe.Item = null;
                    if ( _numLives < 5 ) _numLives += 1;
                } 
                if ( pipe.Item is Poison )
                {
                    pipe.Item.Active = false;
                    //item = null;
                    if ( _numLives > 0 ) _numLives = _numLives - 1;
                }
            }
            if ( pipe.IsOffScreen ) _deleteList.Add(pipe);
            if ( _bird.CollidedWith(pipe) )
            {
                foreach (Pipe pipe2 in _pipes) _deleteList.Add(pipe2);
                break;
            }
            else if ( _bird.X == pipe.X ) _score += 1;
            pipe.Update();
            if ( pipe.IsOffScreen ) _deleteList.Add(pipe);
        }

        foreach ( Pipe pipe in _deleteList ) 
            {
                for ( int i = 0; i < _pipes.Count; i++ ) 
                {
                    if ( _pipes[i] == pipe ) _pipes.Remove(_pipes[i]);
                }
            }
        if ( _pipes.Count == 0 || _pipes[_pipes.Count - 1].X < 500 ) AddPipe();
    }*/

    public void Update()
    {
        if ( _seaweeds.Count == 0 || _seaweeds[_seaweeds.Count - 1].X < 500 ) AddSeaweed();
        _fish.HandleInput();
        foreach ( Seaweed seaweed in _seaweeds ) 
        {
            seaweed.Update();
            if ( seaweed.IsOffScreen ) _deleteSeaweed.Add(seaweed);
        }
        if ( _fish.CollidedWith(_seaweeds[0].Item) )
            {
                if ( _seaweeds[0].Item is Pearl ) 
                {
                    _ding.Play();
                    _seaweeds[0].Item.Active = false;
                //pipe.Item = null;
                    _score += 100;
                }
                if ( _seaweeds[0].Item is Shrimp )
                {
                    _ding.Play();
                    _seaweeds[0].Item.Active = false;
                //pipe.Item = null;
                    if ( _numLives < 5 ) _numLives += 1;
                } 
                if ( _seaweeds[0].Item is Poison )
                {
                    _bump.Play();
                    _seaweeds[0].Item.Active = false;
                    //item = null;
                    if ( _numLives > 0 ) _numLives = _numLives - 1;
                }
            }
        
        if ( _fish.CollidedWith(_seaweeds[0]) )
        {
            _bump.Play();
            _numLives -= 1;
            foreach (Seaweed seaweed in _seaweeds) _deleteSeaweed.Add(seaweed);
        }
        else if ( _fish.X == _seaweeds[0].X ) _score += 1;

        foreach ( Seaweed seaweed in _deleteSeaweed ) 
            {
                for ( int i = 0; i < _seaweeds.Count; i++ ) 
                {
                    if ( _seaweeds[i] == seaweed ) _seaweeds.Remove(_seaweeds[i]);
                }
            }
    }

    public void AddSeaweed()
    {
        Seaweed seaweed = new Seaweed(_level);
        _seaweeds.Add(seaweed);
    }
}