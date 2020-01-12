using System;
using System.IO;

namespace MazeProject
{
    class Program
    {
        private int _currentPositionY, _currentPositionX;
        private bool _hasMoved = false;
       
        static void Main(string[] args)
        {
            Program mazeProg = new Program();
            try
            { 
                using (StreamReader sr = new StreamReader("RPAMaze.txt"))
                {
                    string firstLine = sr.ReadLine();
                    string line = "";
                    while (sr.Peek() != -1)
                    {
                        line = sr.ReadToEnd();
                    }

                    int [,]mazeArray = mazeProg.CreateArray(line,10,10);
                    mazeProg.FindPath(mazeArray);
                }
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        //Go recursively till exit is found
         void FindPath(int [,] mazeArray)
        {
            Display(mazeArray);
            Console.WriteLine("Current position is [" + currentPositionY + "," + currentPositionX + "]");
            Console.WriteLine();
            for (int high = 0; high <= mazeArray.GetUpperBound(0); ++high)
            {
                for (int row = 0; row <= mazeArray.GetUpperBound(1); ++row)
                {
                    if (mazeArray[high, row] == 0)
                    {
                        //check up if can move
                        if (high == currentPositionY - 1 && row == currentPositionX)
                        {
                            Console.WriteLine("Moved UP");
                            UpdatePosition(mazeArray, high, row);
                        }
                        //check right if can move
                        if (high == currentPositionY && row == currentPositionX + 1 )
                        {
                            Console.WriteLine("Moved RIGHT");
                            UpdatePosition(mazeArray, high, row);
                        }
                        //check left if can move
                        if (high == currentPositionY && row == currentPositionX - 1 )
                        {
                            Console.WriteLine("Moved LEFT");
                            UpdatePosition(mazeArray, high, row);
                        }
                        //check down if can move
                        if (high == currentPositionY + 1 && row == currentPositionX )
                        {
                            Console.WriteLine("Moved DOWN");
                           UpdatePosition(mazeArray, high, row);
                        }

                       // UpdatePosition(mazeArray, high, row);
                    }
                }
            }           
        }

        void UpdatePosition(int[,] mazeArray, int high, int row)
        {
            //move the player
            mazeArray[high, row] = 2;
            //Updating current position to the new moved one
            currentPositionY = high;
            currentPositionX = row;

            FindPath(mazeArray);
        }

        void Display(int[,] array)
        {
            for (int y = 0; y <= array.GetUpperBound(0); y++)
            {
                for (int x = 0; x <= array.GetUpperBound(1); x++)
                {
                    if(array[y,x] == 2)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(array[y, x]);
                        Console.Write(" ");
                        Console.ResetColor();

                        if(!_hasMoved)
                        {
                            currentPositionY = y;
                            currentPositionX = x;
                            _hasMoved = true;
                        }
                    }
                    else
                    {
                        Console.Write(array[y, x]);
                        Console.Write(" ");
                    }
                   
                }
                Console.WriteLine();
            }
        }

        int[,] CreateArray(string data,int sizeX, int sizeY)
        {
            int[,] array = new int[sizeX, sizeY];
            int x = 0;
            int y = 0;

            foreach (string line in data.Split(new string[1] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (IsNumeric(line) && (line.Trim().Length > 0))
                {
                    y = 0;
                    foreach (string number in line.Split(' '))
                    {
                        if (!string.IsNullOrEmpty(number))
                        {
                            array[x, y++] = int.Parse(number);
                        }
                    }
                    x++;
                }
            }
            return array;
        }

        private bool IsNumeric(string line)
        {
            foreach (char c in line)
                if (!"0123456789 ".Contains(c))
                    return false;

            return true;
        }

        int currentPositionY
        {
            get { return _currentPositionY; }
            set { _currentPositionY = value; }
        }

        int currentPositionX
        {
            get { return _currentPositionX; }
            set { _currentPositionX = value; }
        }
    }
}
