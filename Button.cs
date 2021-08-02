using System;
using SplashKitSDK;

public class Button
{
    private int _x, _y;
    private const int _width = 100, _height = 50;
    //private Color _activeColor = Color.Orange;
    //private Color _inactiveColor = Color.Gray;
    //private Color _textColor = Color.Black;
    //private Bitmap _bitmap;
    private Rectangle _clickArea;


    public string Caption { get; private set; }
    public bool Active { get; set; }

    public Button(int x, int y, string caption)
    {
        _x = x;
        _y = y;
        Caption = caption;
        _clickArea = SplashKit.RectangleFrom(x, y, _width, _height);
        //_bitmap = bitmap;
        Active = true;
    }

    public void Draw(Bitmap bitmap)
    {
        SplashKit.DrawBitmap(bitmap, _x, _y);
    }

    public bool IsClicked()
    {
        return ( Active == true && SplashKit.PointInRectangle(SplashKit.MousePosition(), _clickArea) && SplashKit.MouseClicked(MouseButton.LeftButton) );
    }
}