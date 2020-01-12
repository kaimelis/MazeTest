using System;
using System.Collections.Generic;

namespace MazeProject
{
    class Program
    {

        int _currentPositionY, _currentPositionX;
        bool _hasMoved = false;

        static void Main(string[] args)
        {
            int[,] mazeArray = new int[,]
            {
            { 1 ,1, 1, 1, 1, 1, 1, 1, 1, 1},
            { 0, 0, 0, 1, 1, 0, 1, 1, 1, 1},
            { 1, 1, 0, 0, 1, 0, 1, 1, 1, 1},
            { 1, 1, 1, 0, 0, 0, 0, 0, 2, 1},
            { 1, 1, 1, 0, 1, 1, 0, 1, 0, 1},
            { 1, 1, 1, 0, 1, 1, 0, 0, 0, 1},
            { 1, 1, 1, 0, 1, 1, 1, 1, 1, 1},
            { 1, 0, 0, 0, 0, 0, 0, 0, 1, 1},
            { 1, 1, 1, 1, 1, 1, 1, 0, 1, 1},
            { 1, 1, 1, 1, 1, 1, 1, 0, 1, 1},
            };

            Program mazeProg = new Program();
            mazeProg.CheckPath(mazeArray);



        }
        //Go recursively till exit is found
         void CheckPath(int [,] mazeArray)
        {
            Display(mazeArray);
            Console.WriteLine("Current position is " + currentPositionY + ";" + currentPositionX);
            Console.WriteLine();
            for (int high = 0; high <= mazeArray.GetUpperBound(0); ++high)
            {
                for (int row = 0; row <= mazeArray.GetUpperBound(1); ++row)
                {
                    //check up if can move
                    if (high == currentPositionY - 1 && row == currentPositionX && mazeArray[high, row] == 0)
                    {
                        Console.WriteLine("Found a way to go UP " + high + ";" + row);
                        mazeArray[high, row] = 2;

                        //Updating current position to the new moved one
                        currentPositionY = high;
                        currentPositionX = row;

                        Console.WriteLine("New position is " + currentPositionY + ";" + currentPositionX);
                        CheckPath(mazeArray);
                    }
                    //check right if can move
                    else if (high == currentPositionY && row == currentPositionX + 1 && mazeArray[high, row] == 0)
                    {
                        Console.WriteLine("Found a way to go RIGHT " + high + ";" + row);
                        mazeArray[high, row] = 2;

                        //Updating current position to the new moved one
                        currentPositionY = high;
                        currentPositionX = row;

                        Console.WriteLine("New position is " + currentPositionY + ";" + currentPositionX);
                        CheckPath(mazeArray);
                    }
                    //check left if can move
                    else if (high == currentPositionY && row == currentPositionX - 1 && mazeArray[high, row] == 0)
                    {
                        Console.WriteLine("Found a way to go LEFT " + high + ";" + row);
                        mazeArray[high, row] = 2;

                        //Updating current position to the new moved one
                        currentPositionY = high;
                        currentPositionX = row;

                        Console.WriteLine("New position is " + currentPositionY + ";" + currentPositionX);
                        CheckPath(mazeArray);
                    }
                    //check down if can move
                    else if (high == currentPositionY + 1 && row == currentPositionX && mazeArray[high, row] == 0)
                    {
                        Console.WriteLine("Found a way to go DOWN " + high + ";" + row);
                        mazeArray[high, row] = 2;

                        //Updating current position to the new moved one
                        currentPositionY = high;
                        currentPositionX = row;

                        Console.WriteLine("New position is " + currentPositionY + ";" + currentPositionX);
                        CheckPath(mazeArray);
                    }
                }
            }
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
