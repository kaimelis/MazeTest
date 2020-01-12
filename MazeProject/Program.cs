using System;
using System.Collections.Generic;
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
            int[,] mazeArray = new int[,]
            {
            { 1 ,1, 1, 1, 1, 1, 1, 1, 1, 1},
            { 0, 0, 0, 1, 0, 0, 1, 1, 1, 1},
            { 1, 1, 0, 0, 1, 0, 1, 1, 1, 1},
            { 1, 1, 1, 0, 0, 0, 0, 0, 1, 1},
            { 1, 1, 1, 0, 1, 1, 0, 1, 0, 1},
            { 1, 1, 1, 0, 1, 1, 0, 0, 0, 1},
            { 1, 1, 1, 0, 1, 1, 1, 0, 1, 1},
            { 1, 0, 0, 0, 0, 0, 0, 0, 1, 1},
            { 1, 1, 1, 1, 1, 1, 2, 0, 1, 1},
            { 1, 1, 1, 1, 1, 1, 1, 0, 1, 1},
            };

            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader("RPAMaze.txt"))
                {
                    // Read the stream to a string, and write the string to the console.
                    String line = sr.ReadToEnd();
                    Console.WriteLine(line);
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

         //   mazeProg.FindPath(mazeArray);
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
                            Console.WriteLine("Found a path UP");
                            UpdatePosition(mazeArray, high, row);
                        }
                        //check right if can move
                        if (high == currentPositionY && row == currentPositionX + 1 )
                        {
                            Console.WriteLine("Found a path RIGHT");
                            UpdatePosition(mazeArray, high, row);
                        }
                        //check left if can move
                        if (high == currentPositionY && row == currentPositionX - 1 )
                        {
                            Console.WriteLine("Found a path LEFT");
                            UpdatePosition(mazeArray, high, row);
                        }
                        //check down if can move
                        if (high == currentPositionY + 1 && row == currentPositionX )
                        {
                            Console.WriteLine("Found a path DOWN");
                            UpdatePosition(mazeArray, high, row);
                        }
                    }
                    //if there is nothing on the left side
                    //else if (mazeArray[high, row] == 2)
                    //{
                    //    Console.WriteLine("Need to go back");
                    //}
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


        int[,] CreateArray(string data)
        {
            int[,] array = new int[10,10];
            int i = 0, j = 0;

            foreach (string line in data.Split(new string[1] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {
                if ((IsNumeric(line) && (line.Trim().Length > 0))) // If valid numeric line and not empty line
                {
                    j = 0;
                    foreach (string number in line.Split(' '))
                        if (!string.IsNullOrEmpty(number))
                            array[i, j++] = int.Parse(number);

                    i++;
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
