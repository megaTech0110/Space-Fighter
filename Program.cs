using SplashKitSDK;

using System;
using System.Collections.Generic;

namespace checking
{
    public class Program
    {
        public static void Main()
        {
            Window mywin = new Window("Game", 1000, 1300);
            List<int> scores = new List<int>();

            for (int i = 0; i < 5; i++)
            {
                Game myGame = new Game(mywin);

                while (!myGame.WeaponSelected)
                {
                    SplashKit.ProcessEvents();
                    myGame.HandleStartPageInput();
                    myGame.StartPage();
                }

                myGame.GenerateFallingRocks(25);

                while (!mywin.CloseRequested)
                {
                    myGame.Update();

                }
                
                mywin.Clear(Color.Black); // Clear the window after each game
            }

            Console.WriteLine("Scores of 5 games played:");
            foreach (int score in scores)
            {
                Console.WriteLine(score);
            }
        }
    }
}
