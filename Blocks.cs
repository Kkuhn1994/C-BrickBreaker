using Avalonia.Controls.Shapes;
using Avalonia.Controls;
using Avalonia;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Linq;

namespace MyGameApp
{
        public class Blocks {
        private MainWindow _mainWindow;

        public Blocks(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
        }
        private List<int> numbers = Enumerable.Range(1, 33).ToList();  
        private double zeroX = 105;
        private double zeroY = 500;

        public bool checkTopBottomCollisionHorizontal(double xCoord, double yCoord) {

            foreach(var element in numbers) {

                double leftBlockBoundary = zeroX + (((element - 1) % 11) * 55);
                double rightBlockBoundary = zeroX + (((element - 1) % 11) * 55) + 50;
                double topBlockBoundary = zeroY + ((element / 12) * 25) + 20;
                double bottomBlockBoundary = zeroY + ((element / 12) * 25);

                if(xCoord > leftBlockBoundary - 20 && xCoord < rightBlockBoundary)
                {
                    bool blockWasHit = checkTopBottomCollisionVertical(yCoord, element, bottomBlockBoundary, topBlockBoundary);
                    if(blockWasHit) {
                        numbers.Remove(element);
                        return blockWasHit;
                    }
                    
                }
            }
            return false;
        }

        public bool checkLeftRightCollisionVertical(double xCoord, double yCoord) {

            foreach(var element in numbers) {

                double leftBlockBoundary = zeroX + (((element - 1) % 11) * 55);
                double rightBlockBoundary = zeroX + (((element - 1) % 11) * 55) + 50;
                double topBlockBoundary = zeroY + ((element / 12) * 25) + 20;
                double bottomBlockBoundary = zeroY + ((element / 12) * 25);

                if(yCoord < topBlockBoundary  && yCoord > bottomBlockBoundary)
                {
                    Console.WriteLine(element);
                    Console.WriteLine(xCoord);
                    bool blockWasHit = checkLeftRightCollisionBlockHorizontal(xCoord, element, leftBlockBoundary, rightBlockBoundary);
                    if(blockWasHit) {
                        numbers.Remove(element);
                        return blockWasHit;
                    }
                }
            }
            return false;
        }



        public bool checkTopBottomCollisionVertical(double yCoord, int blockNr, double bottomBlockBoundary, double topBlockBoundary) {
   

            if(yCoord >= bottomBlockBoundary - 20 && yCoord <= bottomBlockBoundary)
            {
                Console.Write("top collision ");
                Console.WriteLine(blockNr);
                _mainWindow.hideBlock(blockNr);
                return true;
            }
            if(yCoord <= topBlockBoundary && yCoord >= bottomBlockBoundary)
            {
                Console.Write("bottom collision ");
                Console.WriteLine(blockNr);
                _mainWindow.hideBlock(blockNr);
                return true;
            }
            return false;
        }

        public bool checkLeftRightCollisionBlockHorizontal(double xCoord, int blockNr, double leftBlockBoundary, double rightBlockBoundary) {
            if(leftBlockBoundary - 20 <= xCoord && xCoord <= leftBlockBoundary)
            {
                Console.Write("left collision ");
                Console.WriteLine(blockNr);
                _mainWindow.hideBlock(blockNr);
                return true;
            }
            if(xCoord >= rightBlockBoundary && xCoord <= rightBlockBoundary + 20)
            {
                Console.Write("right collision ");
                Console.WriteLine(blockNr);
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