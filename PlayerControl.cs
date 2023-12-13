using SplashKitSDK;
namespace checking;

public class Player
{
    private Bitmap _player;
    private Sprite _playerImage;

    private Bitmap _playerTwo;
    private Sprite _playerTwoImage;

    private int _moveSpeed = 5;

    public Player(string bitmapName, string fileName, int startX, int startY)
    {
        _player = new Bitmap("player", "player.png");
        _playerImage = new Sprite(_player);
        SplashKit.MoveSpriteTo(_playerImage, 250, 600);

        _playerTwo = new Bitmap("playerTwo", "playertwo.png");
        _playerTwoImage = new Sprite(_playerTwo);
        SplashKit.MoveSpriteTo(_playerTwoImage, 750, 600);
    }


}

    