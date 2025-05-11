using Avalonia.Controls.Shapes;
using Avalonia.Controls;
using Avalonia;
using System.Threading.Tasks;
using System;

namespace MyGameApp
{
        public class Blocks {
        private MainWindow _mainWindow;

        public Blocks(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
        }
        private int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 };
        private bool[] numbersHit = new bool[13]; // Automatisch mit false initialisiert
        private double zeroX = 50;
        private double zeroY = 500;


        public bool checkTopBottomCollision(double xCoord, double yCoord) {
            foreach(var element in numbers) {
                double leftBlockBoundary = zeroX + ((element - 1) * 55);
                double rightBlockBoundary = zeroX + ((element - 1) * 55) + 50;
                if(xCoord > leftBlockBoundary && xCoord < rightBlockBoundary)
                {
                    return checkTopBottomCollisionBlock(yCoord, element);
                }
            }
            return false;
        }

        public bool checkTopBottomCollisionBlock(double yCoord, int blockNr) {
   
            if(yCoord >= 480 && yCoord <= 500)
            {
                _mainWindow.hideBlock(blockNr);
                return true;
            }
            return false;
        }

    

        // private bool hitTopOrBottomBorder(double blockBottom) {
        //     double ballBottom = Canvas.GetBottom(ball.BallEllipse);

        //     if(ballBottom > blockBottom + 3 | ballBottom < blockBottom + 47)
        //     {
        //         return true;
        //     }
        //     return false;
        // }

        // public bool hitBlock(int number) {
        //     Console.WriteLine(number);
        //     double coordinateXLeft = zeroX + ((number - 1) * 55);
        //     double coordinateYBottom = zeroY;

        //     if(hitTopOrBottomBorder(coordinateYBottom))
        //     {

        //         return true;
        //     }
        //     return false;
        // }

    }
}