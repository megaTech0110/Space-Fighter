using SplashKitSDK;

public class Button
{
    private string _Text;
    public float X { get; private set; }
    public float Y { get; private set; }
    public float Width { get; private set; }
    public float Height { get; private set; }

    public Button(string text, float x, float y, float width, float height)
    {
        _Text = text;
        X = x;
        Y = y;
        Width = width;
        Height = height;
    }

    public bool IsClicked()
    {
        return SplashKit.PointInRectangle(
            new Point2D { X = SplashKit.MouseX(), Y = SplashKit.MouseY() },
            new Rectangle { X = X, Y = Y, Width = Width, Height = Height }
        );
    }

    public void DrawImproved()
    {
        Color textColor = Color.White;
        Color buttonColor = Color.Black;

        // Draw button with improved appearance
        SplashKit.FillRectangle(buttonColor, X, Y, Width, Height);
        SplashKit.DrawText(_Text, textColor, X + 10, Y + 10);
    }
}