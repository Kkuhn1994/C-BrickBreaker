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
        private double speedX;
        private double speedY;
        private double windowHeight;
        private double windowWidth;
        private bool touchPlayer = true;

        public Ball(Ellipse ellipse, double _windowHeight, double _windowWidth)
        {
            BallEllipse = ellipse;
            windowHeight = _windowHeight;
            windowWidth = _windowWidth;
            speedX = 0;
            speedY = 0;
        }

        public void startBall() {
            speedY = 10;
        }

        public async Task Move(Player player)
        {
            while (true)
            {
                Console.WriteLine(Canvas.GetBottom(BallEllipse));
                if(Canvas.GetBottom(BallEllipse) >= windowHeight | playerHitsBall(player))
                {
                    changeDirection("top_or_bottom");
                }
                if(Canvas.GetLeft(BallEllipse) >= windowWidth | Canvas.GetLeft(BallEllipse) <= 0)
                {
                    changeDirection("top_or_bottom");
                }
                await Avalonia.Threading.Dispatcher.UIThread.InvokeAsync(() =>
                {
                    double newTop = Canvas.GetBottom(BallEllipse) + speedY;
                    double newLeft = Canvas.GetLeft(BallEllipse) + speedX;
                    Canvas.SetBottom(BallEllipse, newTop);
                    Canvas.SetLeft(BallEllipse, newLeft);
                });


                // Verzögerung für den nächsten Update-Zyklus
                await Task.Delay(5);  // Teste mit 5ms oder 10ms
            }
        }

        public bool playerHitsBall(Player player) {
            double ballPositionX = Canvas.GetLeft(BallEllipse);

            Console.WriteLine(player.BoundaryLeft);
            Console.WriteLine(player.BoundaryRight);
                        Console.WriteLine(player.BoundaryRight);
            Console.WriteLine(player.BoundaryLeft);
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