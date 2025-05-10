using Avalonia.Controls.Shapes;
using Avalonia.Controls;
using Avalonia;

namespace MyGameApp
{
    public class Player
    {
        public Rectangle PlayerRectangle { get; private set; }
        private double speed;
        private double boundaryLeft;
        private double boundaryRight;
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
        public Player(Rectangle rectangle, double speed)
        {
            PlayerRectangle = rectangle;
            this.speed = speed;
            boundaryLeft = 0;
            boundaryRight = 200;
        }

        public void MoveLeft()
        {
            Canvas.SetLeft(PlayerRectangle, Canvas.GetLeft(PlayerRectangle) - speed);
            boundaryLeft -= speed;
            boundaryRight -= speed;
        }

        public void MoveRight()
        {
            Canvas.SetLeft(PlayerRectangle, Canvas.GetLeft(PlayerRectangle) + speed);
            boundaryLeft += speed;
            boundaryRight += speed;
        }
    }
}