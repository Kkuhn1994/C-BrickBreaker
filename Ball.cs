using Avalonia.Controls.Shapes;
using Avalonia.Controls;
using Avalonia;
using System.Threading.Tasks;
using System;

namespace MyGameApp
{
    public class Ball
    {
        public Ellipse BallEllipse { get; private set; }
        private Player player;
        private Blocks blocks;
        private double speedX;
        private double speedY;
        private double windowHeight;
        private double windowWidth;

        public Ball(Ellipse ellipse, double _windowHeight, double _windowWidth, Player _player, Blocks _blocks)
        {
            BallEllipse = ellipse;
            windowHeight = _windowHeight;
            windowWidth = _windowWidth;
            player = _player;
            blocks = _blocks;
            speedX = 0;
            speedY = 0;
        }

        public void startBall() {
            player.TouchPlayer = false;
            speedY = 5;
        }

        public async Task Move()
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
                if(checkLosePoint()) return;

            }
        }

        public void placeNewBall() {
            Canvas.SetBottom(BallEllipse,20);
            Canvas.SetLeft(BallEllipse, (player.BoundaryRight + player.BoundaryLeft) / 2);
            speedY = 0;
            speedX = 0;
            player.TouchPlayer = true;
        }

        private void playerInteraction()
        {
            Console.WriteLine(player.Movement);
            if(player.TouchPlayer == true)
            {                    
                speedX = player.Movement;
                Console.WriteLine("Player touch");
            }
            else if(playerHitsBall())
            {
                // Console.WriteLine("Player Collision");
                changeDirection("top_or_bottom");
                speedX += player.Movement / 2;
            }
        }

        private void blockHits()
        {
            if(blocks.checkTopBottomCollisionHorizontal(Canvas.GetLeft(BallEllipse),Canvas.GetBottom(BallEllipse)))
            {
                changeDirection("top_or_bottom");
            }
            if(blocks.checkLeftRightCollisionVertical(Canvas.GetLeft(BallEllipse),Canvas.GetBottom(BallEllipse)))
            {
                changeDirection("");
            }
        }

        private void wallHits()
        {
            if(Canvas.GetBottom(BallEllipse) >= windowHeight)
            {
                player.TouchPlayer = false;
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
            if(ballPositionX >= player.BoundaryLeft && ballPositionX <= player.BoundaryRight)
            {
                return true;
            }
            return false;
        }



        private async void changeDirection(string siteOfCollision) {
            if(siteOfCollision == "top_or_bottom") {
                speedY = -speedY;
                // await Task.Delay(20); 
            }
            else {
                speedX = -speedX;
                // await Task.Delay(20); 
            }
        }
    }
}