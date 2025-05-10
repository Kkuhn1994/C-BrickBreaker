using Avalonia.Controls.Shapes;
using Avalonia.Controls;
using Avalonia;
using System.Threading.Tasks;
using System;

namespace MyGameApp
{
    public class Player
    {
        public Rectangle PlayerRectangle { get; private set; }
        private double speed;
        private double movement;
        private double boundaryLeft;
        private double boundaryRight;
        private bool touchPlayer = true;

        public double BoundaryLeft
        {
            get => boundaryLeft;
            set => boundaryLeft = value;
        }

        public double BoundaryRight
        {
            get => boundaryRight;
            set => boundaryRight = value;
        }
        public double Movement
        {
            get => movement;
            set => movement = value;
        }

        public bool TouchPlayer
        {
            get => touchPlayer;
            set => touchPlayer = value;
        }



        public Player(Rectangle rectangle, double speed)
        {
            PlayerRectangle = rectangle;
            this.speed = speed;
            boundaryLeft = 0;
            boundaryRight = 200;
        }

        public void MoveLeft()
        {
            movement= -speed;
            Canvas.SetLeft(PlayerRectangle, Canvas.GetLeft(PlayerRectangle) - speed);
            boundaryLeft -= speed;
            boundaryRight -= speed;
            ResetMovementWithDelay();
        }

        public void MoveRight()
        {
            movement = speed;
            Canvas.SetLeft(PlayerRectangle, Canvas.GetLeft(PlayerRectangle) + speed);
            boundaryLeft += speed;
            boundaryRight += speed;
            ResetMovementWithDelay();
        }

        public void ResetMovementWithDelay()
        {
            
            if(touchPlayer == true) {
                Task.Run(async () =>
                {
                    await Task.Delay(5); 
                    movement = 0;
                });
            } else {
                Console.WriteLine("long delay");
                Task.Run(async () =>
                {
                    await Task.Delay(100); 
                    movement = 0;
                });
            }
        }
    }
}