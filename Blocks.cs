using Avalonia.Controls.Shapes;
using Avalonia.Controls;
using Avalonia;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Linq;
using System.IO;

namespace MyGameApp
{
    public class Blocks 
    {
        private MainWindow _mainWindow;

        public Blocks(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
        }
        private List<int> numbers = Enumerable.Range(1, 33).ToList();  
        private double zeroX = 105;
        private double zeroY = 500;

        public IEnumerable<int> Numbers
        {
            get => numbers;
            set
            {
                if (value != null)
                    numbers = new List<int>(value);
            }
        }

        public bool checkTopBottomCollisionHorizontal(double xCoord, double yCoord) 
        {
            foreach(var element in numbers) 
            {
                double leftBlockBoundary = zeroX + (((element - 1) % 11) * 55);
                double rightBlockBoundary = zeroX + (((element - 1) % 11) * 55) + 50;
                double topBlockBoundary = zeroY + ((element / 12) * 25) + 20;
                double bottomBlockBoundary = zeroY + ((element / 12) * 25);

                if(xCoord > leftBlockBoundary - 20 && xCoord < rightBlockBoundary)
                {
                    bool blockWasHit = checkTopBottomCollisionVertical(yCoord, element, bottomBlockBoundary, topBlockBoundary);
                    if(blockWasHit) 
                    {
                        removeBlock(element);
                        return blockWasHit;
                    }     
                }
            }
            return false;
        }

        public bool checkLeftRightCollisionVertical(double xCoord, double yCoord) 
        {
            foreach(var element in numbers) 
            {

                double leftBlockBoundary = zeroX + (((element - 1) % 11) * 55);
                double rightBlockBoundary = zeroX + (((element - 1) % 11) * 55) + 50;
                double topBlockBoundary = zeroY + ((element / 12) * 25) + 20;
                double bottomBlockBoundary = zeroY + ((element / 12) * 25);

                if(yCoord < topBlockBoundary  && yCoord > bottomBlockBoundary)
                {
                    // Console.WriteLine(element);
                    // Console.WriteLine(xCoord);
                    bool blockWasHit = checkLeftRightCollisionBlockHorizontal(xCoord, element, leftBlockBoundary, rightBlockBoundary);
                    if(blockWasHit) 
                    {
                        removeBlock(element);
                        return blockWasHit;
                    }
                }
            }
            return false;
        }

        private bool checkTopBottomCollisionVertical(double yCoord, int blockNr, double bottomBlockBoundary, double topBlockBoundary) 
        {
            if(yCoord >= bottomBlockBoundary - 20 && yCoord <= bottomBlockBoundary)
            {
                // Console.Write("bottom collision ");
                // Console.WriteLine(blockNr);
                return true;
            }
            if(yCoord <= topBlockBoundary && yCoord >= bottomBlockBoundary)
            {
                // Console.Write("top collision ");
                // Console.WriteLine(blockNr);
                return true;
            }
            return false;
        }

        private bool checkLeftRightCollisionBlockHorizontal(double xCoord, int blockNr, double leftBlockBoundary, double rightBlockBoundary) 
        {
            if(leftBlockBoundary - 20 <= xCoord && xCoord <= leftBlockBoundary)
            {
                // Console.Write("left collision ");
                // Console.WriteLine(blockNr);
                return true;
            }
            if(xCoord >= rightBlockBoundary && xCoord <= rightBlockBoundary + 20)
            {
                // Console.Write("right collision ");
                // Console.WriteLine(blockNr);
                return true;
            }
            return false;
        }

        public void loadMap() 
        {
            try
            {
                string projectDirectory = System.IO.Path.GetFullPath(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", ".."));
                string fullPath = System.IO.Path.Combine(projectDirectory, "map.txt");
                string[] lines = File.ReadAllLines(fullPath);
                initBlocks(lines);
            }
            catch
            {
                Console.WriteLine("File not found");
            }
        }

        private void initBlocks(string[] blockLines)
        {
            int blockNr = 1;
            int lineNr = 0;

            foreach(string line in mirrorMapData(blockLines))
            {
                blockNr = initLineOfBlocks(lineNr, blockNr, line);
                lineNr ++;
            }
        }

        private int initLineOfBlocks(int lineNr, int blockNr, string line)
        {
            foreach (var block in line)
            {
                _mainWindow.resetBlock(blockNr);
                if(block == '0')
                {
                    removeBlock(blockNr);
                }
                blockNr ++;
            }
            return blockNr;
        }

        private void removeBlock(int blockNr)
        {
            _mainWindow.hideBlock(blockNr);
            numbers.Remove(blockNr);
        }

        private string[] mirrorMapData(string[] blockLines)
        {
            return blockLines.Reverse().ToArray();
        }
    }
}