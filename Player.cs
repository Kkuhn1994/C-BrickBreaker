using Avalonia.Controls.Shapes;
using Avalonia.Controls;
using Avalonia;

namespace MyGameApp
{
    public class Player
    {
        public Rectangle PlayerRectangle { get; private set; }
        private double speed;

        public Player(Rectangle rectangle, double speed)
        {
            PlayerRectangle = rectangle;
            this.speed = speed;
        }

        public void MoveLeft()
        {
            Canvas.SetLeft(PlayerRectangle, Canvas.GetLeft(PlayerRectangle) - speed);
        }

        public void MoveRight()
        {
            Canvas.SetLeft(PlayerRectangle, Canvas.GetLeft(PlayerRectangle) + speed);
        }
    }
}