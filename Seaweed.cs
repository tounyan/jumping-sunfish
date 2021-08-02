using System;
using SplashKitSDK;

public class Seaweed
{
    private int _x = 700, _upperY = 0, _lowerY = 0, _speed;
    private const int _width = 70, _height = 350, _minSpace = 250;
    private Bitmap _seaweedUp = new Bitmap("seaweed", "seaweed-up.png");
    private Bitmap _seaweedDown = new Bitmap("seaweed", "seaweed-down.png");
    //private bool _active = true;

    public int X { get { return _x; } }
    public int UpperY { get { return _upperY; } }
    public int LowerY { get { return _lowerY; } }
    public int Height { get { return _height; } }
    public Bitmap SeaweedUp { get { return _seaweedUp; } }
    public Bitmap SeaweedDown { get { return _seaweedDown; } }
    public Item Item { get; private set; }
    //public Rectangle CollisionRectangle1 { get { return SplashKit.RectangleFrom(_x, 50, Width, _upperY); } }
    //public Rectangle CollisionRectangle2 { get { return SplashKit.RectangleFrom(_x, _lowerY, Width, 750 - _lowerY); } }
    public bool IsOffScreen { get { return (_x < 0 ); } }

    public Seaweed(int level)
    {
        while ( _upperY < 100 ) _upperY = SplashKit.Rnd(350);
        while ( ( _lowerY - _upperY ) < _minSpace ) _lowerY = SplashKit.Rnd(600);
        _speed = level*2;

        int x = SplashKit.Rnd(10);
        switch (x)
        {
            case 0:
                Item = new Pearl(_x, _upperY, _lowerY, _width);
                break;
            case 1:
                Item = new Shrimp(_x, _upperY, _lowerY, _width);
                break;
            case 2:
                Item = new Poison(_x, _upperY, _lowerY, _width);
                break;
            default:
                break;
        }
    }

    public void Draw()
    {
        SplashKit.DrawBitmap(_seaweedDown, _x, _upperY - _height);
        SplashKit.DrawBitmap(_seaweedUp, _x, _lowerY);
        //SplashKit.FillRectangle(Color.GreenYellow, _x, 50, _width, _upperY);
        //SplashKit.FillRectangle(Color.GreenYellow, _x, _lowerY, Width, 750 - _lowerY);
        if ( Item is Pearl && Item.Active == true ) ((Pearl)Item).Draw();
        if ( Item is Shrimp && Item.Active == true ) ((Shrimp)Item).Draw();
        if ( Item is Poison && Item.Active == true ) ((Poison)Item).Draw();
        /*if ( _active )
        {
            SplashKit.FillRectangle(Color.GreenYellow, _x, 50, Width, _upperY);
            SplashKit.FillRectangle(Color.GreenYellow, _x, _lowerY, Width, 750 - _lowerY);
            if ( Item is Gem && Item.Active == true ) ((Gem)Item).Draw();
            if ( Item is Potion && Item.Active == true ) ((Potion)Item).Draw();
            if ( Item is Poison && Item.Active == true ) ((Poison)Item).Draw();
        }*/
    }

    public void Update()
    {
        _x -= _speed;
        if ( Item != null && Item.Active == true ) Item.X -= _speed;
        //if ( _x < 50 ) _active = false;
    }
}