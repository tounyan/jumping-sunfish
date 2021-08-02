using System;
using SplashKitSDK;

public class Fish
{
    private const int _x = 70, _up = 12;//_numLives = 5, _score = 0;
    private const double _gravity = 0.03, _maxVelocity = 8;
    private double _y, _velocity = 0;
    private Bitmap _fishBitmap = new Bitmap("fish", "sunfish.png");
    //private Bitmap _heartBitmap = new Bitmap("heart", "heart.png");

    public int X { get { return _x; } }
    //public double Y { get { return _y; } }
    //public int Width { get { return 80; } }
    public int Height { get { return 80; } }
    //public int Score { get { return _score; } }
    //public int Score { get; set; }
    /*public Circle CollisionCircle
    {
        get { return SplashKit.CircleAt(_x + Width/2, _y + Height/2, 35); } 
    }*/
    //public bool IsDead { get; private set; }

    public Fish(Window window)
    {
        //IsDead = false;
        _y = ( window.Height - Height ) / 2;
    }

    public void Draw()
    {
        //int space = 770;
        SplashKit.DrawBitmap(_fishBitmap, _x, _y);
        //SplashKit.DrawText($"Score: {_score}", Color.Orange, "Arial", 20, space, 50);
        //SplashKit.DrawText($"Score: {Score}", Color.Orange, "Arial", 20, space, 50);

        //SplashKit.DrawText($"Lives: {_numLives}", Color.Orange, "Arial", 20, space, 100);
        /*SplashKit.DrawText("Lives", Color.Orange, "Arial", 20, space, 100);
        for ( int i = 0; i < _numLives; i++ )
        {
            _heartBitmap.Draw(space, 150);
            space += 40;
        }*/
    }
    
    public void HandleInput()
    {
        if ( _velocity < _maxVelocity ) _velocity += _gravity;
        _y += _velocity;
        SplashKit.ProcessEvents();
        if ( SplashKit.KeyDown(KeyCode.SpaceKey) ) _y -= _up;
        if (_y > ( 750 - Height ) ) _y = 750 - Height;
    }

    public bool CollidedWith(Seaweed seaweed)
    {
        /*if ( SplashKit.BitmapRectangleCollision(_birdBitmap, _x, _y, pipe.CollisionRectangle1) || SplashKit.BitmapRectangleCollision(_birdBitmap, _x, _y, pipe.CollisionRectangle2) )
        {
            _numLives -= 1;
            //if ( _numLives == 0 ) IsDead = true;
            return true;
        }
        else 
        {
            if ( _x == pipe.X ) _score +=1;
            return false;
        }
        else return false;*/
        //return ( SplashKit.BitmapRectangleCollision(_fishBitmap, _x, _y, pipe.CollisionRectangle1) || SplashKit.BitmapRectangleCollision(_fishBitmap, _x, _y, pipe.CollisionRectangle2) );
        return ( SplashKit.BitmapCollision(_fishBitmap, _x, _y, seaweed.SeaweedDown, seaweed.X, seaweed.UpperY - seaweed.Height) || SplashKit.BitmapCollision(_fishBitmap, _x, _y, seaweed.SeaweedUp, seaweed.X, seaweed.LowerY) );
    }

    public bool CollidedWith(Item item) 
    {
        /*if ( item is Gem && SplashKit.BitmapCollision(_birdBitmap, _x, _y, item.GemBitmap, item.X, item.Y) ) 
        {
            item.Active = false;
            item = null;
            _score = _score + 100;
        }

        if ( item is Potion && SplashKit.BitmapCollision(_birdBitmap, _x, _y, item.PotionBitmap, item.X, item.Y) )
        {
            item.Active = false;
            item = null;
            _numLives = _numLives + 1;
        } 

        if ( item is Poison && SplashKit.BitmapCollision(_birdBitmap, _x, _y, item.PoisonBitmap, item.X, item.Y) )
        {
            item.Active = false;
            item = null;
            _numLives = _numLives - 1;
        }*/
        return ( item != null && item.Active && SplashKit.BitmapCollision(_fishBitmap, _x, _y, item.Bitmap, item.X, item.Y ) );
    }

    /*public void AddScore(Pipe pipe)
    {
        if ( _x == pipe.X && ! CollidedWith(pipe) ) _score +=1;
    }*/
}