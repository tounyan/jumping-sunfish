using System;
using SplashKitSDK;

public abstract class Item
{
    private int _width = 40, _height = 40;
    protected Bitmap _pearlBitmap = new Bitmap("pearl", "pearl.png");
    protected Bitmap _poisonBitmap = new Bitmap("poison", "poison.png");
    protected Bitmap _shrimpBitmap = new Bitmap("shrimp", "shrimp.png");
    
    public int X { get; set; }
    public int Y { get; private set; }
    public Bitmap Bitmap
    {
        get
        {
            if ( this is Pearl ) return _pearlBitmap;
            if ( this is Poison ) return _poisonBitmap;
            if ( this is Shrimp ) return _shrimpBitmap;
            return null;
        }
    }
    //public int Width { get { return 40; } }
    //public int Height { get {return 40; } }
    //public Bitmap PearlBitmap { get { return _pearlBitmap; } }
    //public Bitmap PoisonBitmap { get { return _poisonBitmap; } }
    //public Bitmap ShrimpBitmap { get { return _shrimptionBitmap; } }
    public bool Active { get; set; }

    public Item(int x, int upperY, int lowerY, int seaweedWidth)
    {
        const int space = 10;
        Active = true;
        X = x + seaweedWidth/2 - _width/2;
        if ( SplashKit.Rnd() < 0.5 ) Y = upperY + space;
        else Y = lowerY - _height - space;
    }

    public abstract void Draw();
}

public class Pearl : Item
{
    public Pearl(int x, int upperY, int lowerY, int width) : base(x, upperY, lowerY, width)
    {}

    public override void Draw()
    {
        SplashKit.DrawBitmap(_pearlBitmap, X, Y);
    }
}

public class Shrimp : Item
{
    public Shrimp(int x, int upperY, int lowerY, int width) : base(x, upperY, lowerY, width)
    {}

    public override void Draw()
    {
        SplashKit.DrawBitmap(_shrimpBitmap, X, Y);
    }
}

public class Poison : Item
{
    public Poison(int x, int upperY, int lowerY, int width) : base(x, upperY, lowerY, width)
    {}

    public override void Draw()
    {
        SplashKit.DrawBitmap(_poisonBitmap, X, Y);
    }
}