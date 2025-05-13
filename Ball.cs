using Avalonia.Controls.Shapes;
using Avalonia.Controls;
using Avalonia;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace MyGameApp
{
    public class Ball
    {
        public Ellipse BallEllipse { get; private set; }
        private Player _player;
        private Blocks _blocks;
        private MainWindow _mainWindow;
        private double speedX;
        private double speedY;
        private double windowHeight;
        private double windowWidth;

        public Ball(Ellipse ellipse, double _windowHeight, double _windowWidth, Player player, Blocks blocks, MainWindow mainWindow)
        {
            BallEllipse = ellipse;
            windowHeight = _windowHeight;
            windowWidth = _windowWidth;
            _player = player;
            _blocks = blocks;
            _mainWindow = mainWindow;
            speedX = 0;
            speedY = 0;
        }

        public void startBall() 
        {
            _player.TouchPlayer = false;
            speedY = 5;
        }

        public async Task<bool> Move()
        {
            while (true)
            {
                await Task.Delay(20);
                blockHits();
                wallHits();
                playerInteraction();

                await Avalonia.Threading.Dispatcher.UIThread.InvokeAsync(() =>
                {
                    double newTop = Canvas.GetBottom(BallEllipse) + speedY;
                    double newLeft = Canvas.GetLeft(BallEllipse) + speedX;
                    Canvas.SetBottom(BallEllipse, newTop);
                    Canvas.SetLeft(BallEllipse, newLeft);
                });
                if(checkLosePoint()) 
                {
                    return false;
                }
                bool isWin = await checkWin();
                if(isWin)
                {
                    return isWin;
                }
            }
            return false;
        }

        public void placeNewBall() 
        {
            Canvas.SetBottom(BallEllipse,20);
            Canvas.SetLeft(BallEllipse, (_player.BoundaryRight + _player.BoundaryLeft) / 2);
            speedY = 0;
            speedX = 0;
            _player.TouchPlayer = true;
        }

        private void playerInteraction()
        {
            // Console.WriteLine(player.Movement);
            if(_player.TouchPlayer == true)
            {                    
                speedX = _player.Movement;
                // Console.WriteLine("Player touch");
            }
            else if(playerHitsBall())
            {
                // Console.WriteLine("Player Collision");
                changeDirection("top_or_bottom");
                speedX += _player.Movement / 2;
            }
        }

        private void blockHits()
        {
            if(_blocks.checkTopBottomCollisionHorizontal(Canvas.GetLeft(BallEllipse),Canvas.GetBottom(BallEllipse)))
            {
                changeDirection("top_or_bottom");
            }
            if(_blocks.checkLeftRightCollisionVertical(Canvas.GetLeft(BallEllipse),Canvas.GetBottom(BallEllipse)))
            {
                changeDirection("");
            }
        }

        private void wallHits()
        {
            if(Canvas.GetBottom(BallEllipse) >= windowHeight)
            {
                _player.TouchPlayer = false;
                // Console.WriteLine("Top Collision");
                changeDirection("top_or_bottom");
            }
            if(Canvas.GetLeft(BallEllipse) >= windowWidth | Canvas.GetLeft(BallEllipse) <= 0)
            {
                // Console.WriteLine("Wall Collision");
                changeDirection("");
            }
        }

        private bool checkLosePoint()
        {
            if(Canvas.GetBottom(BallEllipse) < -100)
            {
                // Console.WriteLine("score");
                return true;
            }
            return false;
        }

        private async Task<bool> checkWin()
        {
            if(!_blocks.Numbers.Any())
            {
                int menueOptionPressed = await _mainWindow.ShowWinMenue();
                restartGame(menueOptionPressed);
                return true;
            }
            return false;
        }

        private void restartGame(int menueOptionPressed)
        {
            _mainWindow.Score = 0;
            _blocks.Numbers = Enumerable.Range(1, 33).ToList(); 
            _blocks.loadMap();
            _mainWindow.StartBallMovement();
            placeNewBall();
        }


        private bool playerHitsBall() 
        {
            if(playerHorizontalCollisionCheck() && playerVerticalCollisionCheck())
            {
                return true;
            }
            return false;
        }

        private bool playerHorizontalCollisionCheck() 
        {
            if(Canvas.GetBottom(BallEllipse) <= 10 && Canvas.GetBottom(BallEllipse) >= 0)
            {
                return true;
            }
            return false;
        }

        private bool playerVerticalCollisionCheck() 
        {
            double ballPositionX = Canvas.GetLeft(BallEllipse);
            if(ballPositionX >= _player.BoundaryLeft && ballPositionX <= _player.BoundaryRight)
            {
                return true;
            }
            return false;
        }

        private void changeDirection(string siteOfCollision) 
        {
            if(siteOfCollision == "top_or_bottom") 
            {
                speedY = -speedY;
                // await Task.Delay(20); 
            }
            else 
            {
                speedX = -speedX;
                // await Task.Delay(20); 
            }
        }
    }
}