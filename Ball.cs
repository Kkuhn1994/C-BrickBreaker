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
        private double speedX;
        private double speedY;
        private double windowHeight;
        private double windowWidth;

        public Ball(Ellipse ellipse, double _windowHeight, double _windowWidth, Player _player)
        {
            BallEllipse = ellipse;
            windowHeight = _windowHeight;
            windowWidth = _windowWidth;
            player = _player;
            speedX = 0;
            speedY = 0;
        }

        public void startBall() {
            player.TouchPlayer = false;
            speedY = 10;
        }

        public async Task Move()
        {
            while (true)
            {
                if(Canvas.GetBottom(BallEllipse) >= windowHeight)
                {
                    player.TouchPlayer = false;
                    Console.WriteLine("Top Collision");
                    changeDirection("top_or_bottom");
                }
                if(Canvas.GetLeft(BallEllipse) >= windowWidth | Canvas.GetLeft(BallEllipse) <= 0)
                {
                    Console.WriteLine("Wall Collision");
                    changeDirection("");
                }
                if(player.TouchPlayer == true)
                {                    
                    speedX = player.Movement;
                    Console.WriteLine("Player touch");
                }
                else if(playerHitsBall(player))
                {
                    Console.WriteLine("Player Collision");
                    changeDirection("top_or_bottom");
                    speedX += player.Movement;
                    Console.WriteLine(speedX);
                }

                await Avalonia.Threading.Dispatcher.UIThread.InvokeAsync(() =>
                {
                    double newTop = Canvas.GetBottom(BallEllipse) + speedY;
                    double newLeft = Canvas.GetLeft(BallEllipse) + speedX;
                    Canvas.SetBottom(BallEllipse, newTop);
                    Canvas.SetLeft(BallEllipse, newLeft);
                });
                await Task.Delay(5);
            }
        }


        public bool playerHitsBall(Player player) {

            double ballPositionX = Canvas.GetLeft(BallEllipse);
            if(Canvas.GetBottom(BallEllipse) <= 10 &&
                (ballPositionX >= player.BoundaryLeft && ballPositionX <= player.BoundaryRight))
            {
                return true;
            }
            return false;
        }

        public void changeDirection(string siteOfCollision) {
            if(siteOfCollision == "top_or_bottom") {
                speedY = -speedY;
            }
            else {
                speedX = -speedX;
            }
        }
    }
}