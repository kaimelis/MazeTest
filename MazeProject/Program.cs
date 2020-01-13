﻿using System;
using System.IO;

/// <summary>
/// The program that solves mazes. Any size of the array works.
/// </summary>
namespace MazeProject
{
    class Program
    {
        /// <summary>
        /// Current position of the player that will be used to check further movement.
        /// </summary>
        private int _currentPositionY, _currentPositionX;
        /// <summary>
        /// A bool that shows if the first move was done.
        /// </summary>
        private bool _hasMoved = false;
       
        /// <summary>
        /// Main function that runs the main loop
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Program mazeProg = new Program();
            try
            { 
                //Reading the file that maze is stored in
                using (StreamReader sr = new StreamReader("../../../RPAMaze.txt"))
                {
                    //Reading first line of the file and storing it.
                    string firstLine = sr.ReadLine();
                    //Loop that reads the maze file but skips the first line.
                    string line = "";
                    while (sr.Peek() != -1)
                    {
                        line = sr.ReadToEnd();
                    }

                    //creating the maze array that was given in a file
                    int [,]mazeArray = mazeProg.CreateArray(line,20,20);

                    //finding a path in a maze. Recursive method.
                    mazeProg.FindPath(mazeArray);
                }
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Checks if there is a free path to walk on. 
        /// 0 is a walkable tile.
        /// </summary>
        /// <param name="mazeArray">Two dimensional array that gets the newest version of the maze</param>
        private void FindPath(int [,] mazeArray)
        {
            //Display the newest maze version with visual path
            Display(mazeArray);
            Console.WriteLine("Current position is [" + CurrentPositionY + "," + CurrentPositionX + "]");
            Console.WriteLine();
            //going through array first is the columns
            for (int high = 0; high <= mazeArray.GetUpperBound(0); ++high)
            {
                //going through array and checking the rows
                for (int row = 0; row <= mazeArray.GetUpperBound(1); ++row)
                {
                    //checking if there is a walkable tile in sight
                    if (mazeArray[high, row] == 0)
                    {
                        //check up if the player can move
                        if (high == CurrentPositionY - 1 && row == CurrentPositionX)
                        {
                            Console.WriteLine("Moved UP");
                            UpdatePosition(mazeArray, high, row);
                        }
                        //check right if the player can move
                        if (high == CurrentPositionY && row == CurrentPositionX + 1 )
                        {
                            Console.WriteLine("Moved RIGHT");
                            UpdatePosition(mazeArray, high, row);
                        }
                        //check left if the player can move
                        if (high == CurrentPositionY && row == CurrentPositionX - 1 )
                        {
                            Console.WriteLine("Moved LEFT");
                            UpdatePosition(mazeArray, high, row);
                        }
                        //check down if the player can move
                        if (high == CurrentPositionY + 1 && row == CurrentPositionX )
                        {
                           Console.WriteLine("Moved DOWN");
                           UpdatePosition(mazeArray, high, row);
                        }
                    }
                }
            }           
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param MazeArrayData="mazeArray"> </param>
        /// <param Columns="high"> </param>
        /// <param Rows="row"> </param>
       private void UpdatePosition(int[,] mazeArray, int high, int row)
        {
            //move the player
            mazeArray[high, row] = 2;
            //Updating current position to the new moved one
            CurrentPositionY = high;
            CurrentPositionX = row;

            FindPath(mazeArray);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        private void Display(int[,] array)
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
                            CurrentPositionY = y;
                            CurrentPositionX = x;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="sizeX"></param>
        /// <param name="sizeY"></param>
        /// <returns></returns>
        private int[,] CreateArray(string data,int sizeX, int sizeY)
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private bool IsNumeric(string line)
        {
            foreach (char c in line)
                if (!"0123456789 ".Contains(c))
                    return false;

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        private int CurrentPositionY
        {
            get { return _currentPositionY; }
            set { _currentPositionY = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        private int CurrentPositionX
        {
            get { return _currentPositionX; }
            set { _currentPositionX = value; }
        }
    }
}
