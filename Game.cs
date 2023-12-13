using System;
using checking;
using SplashKitSDK;



public class Game
{


    private int _MoveSpeed = 5;

    private bool _IsPlayerOneActive = true;
    private bool _IsBothPlayerActive = false;

    private int _PlayerOneScore = 0;
    private int _PlayerTwoScore = 0;

    private bool _GameEnded = false;

    public bool GameEnded
    {
        get { return _GameEnded; }
        set { _GameEnded = value; }
    }

    private Window _SpriteWindow;

    private string? _WeaponImageFilename;
    private List<Sprite> _Weapons = new List<Sprite>(); // List of weapons
    private bool _WeaponSelected = false;

    private Bitmap _StartPage;
    private Button _BulletButton;
    private Button _BombButton; // New button for bomb

    private Bitmap _Rock;
    private List<Sprite> _EnemyList = new List<Sprite>(); // List of EnemyeS sprites

    private Bitmap _backGround;
    private Sprite _Image;

    private Bitmap _player;
    private Sprite _playerImage;

    private Bitmap _playerTwo;
    private Sprite _playerTwoImage;


    public Game(Window gameWindow) // Modify constructor
    {

        _SpriteWindow = gameWindow;

        _Rock = new Bitmap("rock", "rock.png");

        _backGround = new Bitmap("backGround", "backGround.png");
        _Image = new Sprite(_backGround);

        _player = new Bitmap("player", "player.png");
        _playerImage = new Sprite(_player);
        SplashKit.MoveSpriteTo(_playerImage, 250, 600);

        _playerTwo = new Bitmap("playerTwo", "playertwo.png");
        _playerTwoImage = new Sprite(_playerTwo);
        SplashKit.MoveSpriteTo(_playerTwoImage, 750, 600);

        _WeaponImageFilename = null; // Default to no weapon
        _BulletButton = new Button("Bullet", 300, 600, 150, 50);
        _BombButton = new Button("Bomb", 600, 600, 150, 50); // New bomb button

        _StartPage = new Bitmap("start", "start.png");

    }

    public bool WeaponSelected
    {
        get { return _WeaponSelected; }
    }

    public void SetWeapon(string weaponImageFilename)
    {
        _WeaponImageFilename = weaponImageFilename;
    }

    public void GenerateFallingRocks(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Sprite enemyS = new Sprite(_Rock);
            int randomX = SplashKit.Rnd(_SpriteWindow.Width - enemyS.Width);
            int randomY = -SplashKit.Rnd(800);
            SplashKit.MoveSpriteTo(enemyS, randomX, randomY);
            _EnemyList.Add(enemyS);
        }
    }


    public void HandleStartPageInput()
    {
        if (_BulletButton.IsClicked())
        {
            _WeaponImageFilename = "bullet.png";
            _WeaponSelected = true;
        }

        if (_BombButton.IsClicked()) // Check if bomb button is clicked
        {
            _WeaponImageFilename = "bomb.png"; // Set weapon to bomb
            _WeaponSelected = true;
        }
    }

    public void RestartGame()
    {
        _PlayerOneScore = 0;
        _PlayerTwoScore = 0;
        _WeaponSelected = false;
        _Weapons.Clear();
        _EnemyList.Clear();
        GenerateFallingRocks(45);
    }

    public void StartPage()
    {
        _SpriteWindow.Clear(Color.White);

        // Draw background
        SplashKit.DrawBitmap(_StartPage, 0, 0);

        // Draw buttons with improved appearance
        _BulletButton.DrawImproved();
        _BombButton.DrawImproved();

        // Draw the text
        string startText = "Press Tab to Play 2 Player";
        int textX = (_SpriteWindow.Width - SplashKit.TextWidth(startText, "Arial", 36)) / 2; // Centered horizontally
        int textY = 500; // Adjust the Y coordinate as needed
        SplashKit.DrawText(startText, Color.White, "Arial", 24, textX, textY);

        _SpriteWindow.Refresh(60);
    }

    public void Update()
    {

        if (SplashKit.KeyTyped(KeyCode.TabKey))
        {
            _IsPlayerOneActive = !_IsPlayerOneActive;
            _IsBothPlayerActive = !_IsBothPlayerActive; //false and false = true 
        }

        if (!_WeaponSelected)
        {
            StartPage();
            HandleStartPageInput();
            return;
        }

        if (SplashKit.KeyDown(KeyCode.EscapeKey))
        {
            EndGame();
            return;
        }

        // Draw both players
        SplashKit.DrawSprite(_playerImage);
        SplashKit.DrawSprite(_playerTwoImage);

        SplashKit.ProcessEvents();

        if (_IsPlayerOneActive)
        {

            if (SplashKit.KeyDown(KeyCode.RightKey))
            {
                _playerImage.X += 5; // Move player one right
            }

            if (SplashKit.KeyDown(KeyCode.LeftKey))
            {
                _playerImage.X -= 5; // Move player one left
            }

            if (SplashKit.KeyDown(KeyCode.UpKey))
            {
                _playerImage.Y -= 5; // Move player one up
            }

            if (SplashKit.KeyDown(KeyCode.DownKey))
            {
                _playerImage.Y += 5; // Move player one down
            }
            if (_playerImage.X < 0)
            {
                _playerImage.X = 0;
            }
            else if (_playerImage.X + _playerImage.Width > _SpriteWindow.Width)
            {
                _playerImage.X = _SpriteWindow.Width - _playerImage.Width;
            }

            if (_playerImage.Y < 0)
            {
                _playerImage.Y = 0;
            }
            else if (_playerImage.Y + _playerImage.Height > _SpriteWindow.Height)
            {
                _playerImage.Y = _SpriteWindow.Height - _playerImage.Height;
            }
        }

        if (_IsBothPlayerActive)

        {

            if (SplashKit.KeyDown(KeyCode.RightKey))
            {
                _playerImage.X += 5; // Move player one right
            }

            if (SplashKit.KeyDown(KeyCode.LeftKey))
            {
                _playerImage.X -= 5; // Move player one left
            }

            if (SplashKit.KeyDown(KeyCode.UpKey))
            {
                _playerImage.Y -= 5; // Move player one up
            }

            if (SplashKit.KeyDown(KeyCode.DownKey))
            {
                _playerImage.Y += 5; // Move player one down
            }
            if (SplashKit.KeyDown(KeyCode.DKey))
            {
                _playerTwoImage.X += 5; // Move player two right
            }

            if (SplashKit.KeyDown(KeyCode.AKey))
            {
                _playerTwoImage.X -= 5; // Move player two left
            }

            if (SplashKit.KeyDown(KeyCode.WKey))
            {
                _playerTwoImage.Y -= 5; // Move player two up
            }

            if (SplashKit.KeyDown(KeyCode.SKey))
            {
                _playerTwoImage.Y += 5; // Move player two down
            }
            if (_playerImage.X < 0)
            {
                _playerImage.X = 0;
            }
            else if (_playerImage.X + _playerImage.Width > _SpriteWindow.Width)
            {
                _playerImage.X = _SpriteWindow.Width - _playerImage.Width;
            }

            if (_playerImage.Y < 0)
            {
                _playerImage.Y = 0;
            }
            else if (_playerImage.Y + _playerImage.Height > _SpriteWindow.Height)
            {
                _playerImage.Y = _SpriteWindow.Height - _playerImage.Height;
            }

            if (_playerTwoImage.X < 0)
            {
                _playerTwoImage.X = 0;
            }
            else if (_playerTwoImage.X + _playerTwoImage.Width > _SpriteWindow.Width)
            {
                _playerTwoImage.X = _SpriteWindow.Width - _playerTwoImage.Width;
            }

            if (_playerTwoImage.Y < 0)
            {
                _playerTwoImage.Y = 0;
            }
            else if (_playerTwoImage.Y + _playerTwoImage.Height > _SpriteWindow.Height)
            {
                _playerTwoImage.Y = _SpriteWindow.Height - _playerTwoImage.Height;
            }

            if (SplashKit.KeyTyped(KeyCode.SpaceKey))
            {
                if (_WeaponImageFilename != null && _WeaponImageFilename == "bullet.png")
                {
                    Bitmap weaponImage = new Bitmap("weapon", _WeaponImageFilename);
                    Sprite weapon = new Sprite(weaponImage);
                    SplashKit.MoveSpriteTo(weapon, _playerImage.X + (_playerImage.Width / 2) - (weaponImage.Width / 2), _playerImage.Y);
                    _Weapons.Add(weapon);
                    _PlayerOneScore++;
                }
                
                else if (_WeaponImageFilename != null && _WeaponImageFilename == "bomb.png")
                {
                    Bitmap weaponImage = new Bitmap("weapon", _WeaponImageFilename);
                    Sprite weapon = new Sprite(weaponImage);
                    SplashKit.MoveSpriteTo(weapon, _playerImage.X + (_playerImage.Width / 2) - (weaponImage.Width / 2), _playerImage.Y);
                    _Weapons.Add(weapon);
                    _PlayerOneScore++;
                }
            }

            if (SplashKit.KeyTyped(KeyCode.LeftShiftKey))
            {
                if (_WeaponImageFilename != null && _WeaponImageFilename == "bullet.png")
                {
                    Bitmap weaponImageTwo = new Bitmap("weaponTwo", _WeaponImageFilename);
                    Sprite weaponTwo = new Sprite(weaponImageTwo);
                    SplashKit.MoveSpriteTo(weaponTwo, _playerTwoImage.X + (_playerTwoImage.Width / 2) - (weaponImageTwo.Width / 2), _playerTwoImage.Y);
                    _Weapons.Add(weaponTwo);
                    _PlayerTwoScore++; // Increase Player Two's score
                }
                
                else if (_WeaponImageFilename != null && _WeaponImageFilename == "bomb.png")
                {
                    Bitmap weaponImageTwo = new Bitmap("weaponTwo", _WeaponImageFilename);
                    Sprite weaponTwo = new Sprite(weaponImageTwo);
                    SplashKit.MoveSpriteTo(weaponTwo, _playerTwoImage.X + (_playerTwoImage.Width / 2) - (weaponImageTwo.Width / 2), _playerTwoImage.Y);
                    _Weapons.Add(weaponTwo);
                    _PlayerTwoScore++; // Increase Player Two's score

                }
            }


            foreach (Sprite enemy in _EnemyList)
            {
                enemy.Y += 2; // Move down at a speed of 5
                SplashKit.DrawSprite(enemy);

                // Check for collision with player one
                if (SplashKit.SpriteCollision(_playerImage, enemy))
                {
                    EndGame();
                    return; // Exit the Update method after ending the game
                }

                // Check for collision with player two

                if (SplashKit.SpriteCollision(_playerTwoImage, enemy))
                {
                    EndGame();
                    return; // Exit the Update method after ending the game
                }

                // Reset position when it goes out of window
                if (enemy.Y > _SpriteWindow.Height)
                {
                    int randomX = SplashKit.Rnd(_SpriteWindow.Width - enemy.Width);
                    int randomY = -SplashKit.Rnd(800); // Randomize Y coordinate
                    SplashKit.MoveSpriteTo(enemy, randomX, randomY);
                }
            }
        }

        // Fire bullets based on weapon choice
        if (SplashKit.KeyTyped(KeyCode.SpaceKey)) 
        {
            if (_WeaponImageFilename != null && _WeaponImageFilename == "bullet.png")
            {

                Bitmap weaponImage = new Bitmap("weapon", _WeaponImageFilename);
                Sprite weapon = new Sprite(weaponImage);
                SplashKit.MoveSpriteTo(weapon, _playerImage.X + (_playerImage.Width / 2) - (weaponImage.Width / 2), _playerImage.Y);
                _Weapons.Add(weapon);
             
           
            }
            else if (_WeaponImageFilename != null && _WeaponImageFilename == "bomb.png")
            {

                // Create bomb bitmap and sprite
                Bitmap weaponImage = new Bitmap("weapon", _WeaponImageFilename);
                Sprite weapon = new Sprite(weaponImage);
                SplashKit.MoveSpriteTo(weapon, _playerImage.X + (_playerImage.Width / 2) - (weaponImage.Width / 2), _playerImage.Y);
                _Weapons.Add(weapon);
       
            }
        }

        if (SplashKit.KeyDown(KeyCode.QKey))
        {
            if (!_GameEnded) // Check if the game has not ended
            {
                _SpriteWindow.Close(); // Close the window
            }
            return;
        }

        // Add this new block to handle 'R' key
        if (SplashKit.KeyTyped(KeyCode.RKey))
        {
            RestartGame();
            return;
        }

        _SpriteWindow.Clear(Color.White);
        SplashKit.DrawSprite(_Image);

        List<Sprite> rocksToRemove = new List<Sprite>();
        List<Sprite> bulletsToRemove = new List<Sprite>();

        foreach (Sprite enemy in _EnemyList)
        {
            enemy.Y += 5; // Move down at a speed of 5
            SplashKit.DrawSprite(enemy);

            // Check for collision with player one
            if (SplashKit.SpriteCollision(_playerImage, enemy))
            {
                EndGame();
                return; // Exit the Update method after ending the game
            }

            // Reset position when it goes out of window
            if (enemy.Y > _SpriteWindow.Height)
            {
                int randomX = SplashKit.Rnd(_SpriteWindow.Width - enemy.Width);
                int randomY = -SplashKit.Rnd(800); // Randomize Y coordinate
                SplashKit.MoveSpriteTo(enemy, randomX, randomY);
            }
        }

        foreach (Sprite weapon in _Weapons)
        {
            foreach (Sprite enemy in _EnemyList)
            {
                if (SplashKit.SpriteCollision(weapon, enemy))
                {
                    if (_IsPlayerOneActive)
                    {
                        _PlayerOneScore++; // Increase Player One's score

                    }
                

                    // Create an explosion effect
                    Bitmap explosionImage = new Bitmap("explosion", "explosion.png");
                    Sprite explosion = new Sprite(explosionImage);
                    SplashKit.MoveSpriteTo(explosion, enemy.X, enemy.Y);
                    SplashKit.DrawSprite(explosion);

                    rocksToRemove.Add(enemy);
                    bulletsToRemove.Add(weapon);
                    break; // Exit loop to prevent concurrent modification
                }
            }

            // Move bullets up the screen
            weapon.Y -= 8;
            SplashKit.DrawSprite(weapon);
        }
        foreach (Sprite rock in rocksToRemove)
        {
            _EnemyList.Remove(rock);
        }

        foreach (Sprite bullet in bulletsToRemove)
        {
            _Weapons.Remove(bullet);
        }

        SplashKit.DrawSprite(_playerImage);
        SplashKit.DrawSprite(_playerTwoImage);

        // Draw scores at the top corners
        string playerOneScoreText = "Player 1 Score: " + _PlayerOneScore;
        string playerTwoScoreText = "Player 2 Score: " + _PlayerTwoScore;

        SplashKit.DrawText(playerOneScoreText, Color.White, 20, 10);
        SplashKit.DrawText(playerTwoScoreText, Color.White, 820, 10);

        _SpriteWindow.Refresh(60);

    }

    private void EndGame()
    {
        // Stop music

        _SpriteWindow.Clear(Color.Black);
        string gameOverText = "Game Over!";
        string restartText = "Press 'R' to Restart The Game.";

        SplashKit.DrawText(gameOverText, Color.White, "Arial", 40, 500, 500);

        // Display scores in a table
        int startX = 400;
        int startY = 550;
        int spacing = 30;

        // Display the current game scores
        int playerOneScoreY = startY + spacing;
        int playerTwoScoreY = playerOneScoreY + spacing;

        SplashKit.DrawText("Player One Score: " + _PlayerOneScore, Color.White, "Arial", 20, startX, playerOneScoreY);
        SplashKit.DrawText("Player Two Score: " + _PlayerTwoScore, Color.White, "Arial", 20, startX, playerTwoScoreY);

        SplashKit.DrawText(restartText, Color.White, "Arial", 20, 300, playerTwoScoreY + spacing);

        _SpriteWindow.Refresh(60);

        bool restart = false;
        while (!restart)
        {
            // Process events inside the loop
            SplashKit.ProcessEvents();

            if (SplashKit.KeyTyped(KeyCode.RKey))
            {
                restart = true;
                RestartGame();
                _SpriteWindow.Clear(Color.Black);
                _SpriteWindow.Refresh(60);
                return; // Add return statement after restarting the game
            }
        }
    }
}