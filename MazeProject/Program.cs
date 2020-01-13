using System;
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
        /// Array size that is read from the file
        /// </summary>
        private int _arraySizeX, _arraySizeY;
        /// <summary>
        /// A bool that shows if the first move was done.
        /// </summary>
        private bool _hasMoved = false;

        /// <summary>
        /// holds all information that needs to be writen to the file
        /// </summary>
        private string writeTrail;

        /// <summary>
        /// A bool that checks if the maze solver found the solution
        /// </summary>
        private bool _hasFinished = false;
       
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
                    string[] arraySize = firstLine.Split(' ');
                    int[] convertedSize = Array.ConvertAll<string, int>(arraySize, int.Parse);
                    //Loop that reads the maze file but skips the first line.
                    string line = "";
                    while (sr.Peek() != -1)
                    {
                        line = sr.ReadToEnd();
                    }

                    //creating the maze array that was given in a file
                    int [,]mazeArray = mazeProg.CreateArray(line, convertedSize[0], convertedSize[1]);



                    //finding a path in a maze. Recursive method.
                    //mazeProg.FindPath(mazeProg.DefineStartPosition(mazeArray));
                    mazeProg.FindPath(mazeArray);
                    mazeProg.CreateTrailFile();
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
            if (mazeArray == null)
                return;
            //Display the newest maze version with visual path
            Display(mazeArray);
            Console.WriteLine("Current position is [" + CurrentPositionY + "," + CurrentPositionX + "]");
            Console.WriteLine();
            writeTrail += "\n";
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
                            Console.WriteLine("Moved UP to coordinates [" + high + "," + row + "]");
                            writeTrail += "Player Moved UP to coordinates [" + high + "," + row + "]";
                            UpdatePosition(mazeArray, high, row);
                        }
                        //check right if the player can move
                        if (high == CurrentPositionY && row == CurrentPositionX + 1 )
                        {
                            Console.WriteLine("Moved RIGHT [" + high + "," + row + "]");
                            writeTrail += "Player Moved RIGHT to coordinates [" + high + "," + row + "]";
                            UpdatePosition(mazeArray, high, row);
                        }
                        //check left if the player can move
                        if (high == CurrentPositionY && row == CurrentPositionX - 1 )
                        {
                            Console.WriteLine("Moved LEFT [" + high + "," + row + "]");
                            writeTrail += "Player Moved LEFT to coordinates [" + high + "," + row + "]";
                            UpdatePosition(mazeArray, high, row);
                        }
                        //check down if the player can move
                        if (high == CurrentPositionY + 1 && row == CurrentPositionX )
                        {
                            Console.WriteLine("Moved DOWN [" + high + "," + row + "]");
                            writeTrail += "Player Moved DOWN to coordinates [" + high + "," + row + "]";
                            UpdatePosition(mazeArray, high, row);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Function that updates the current position and updates the path
        /// </summary>
        /// <param MazeArrayData="mazeArray"> </param>
        /// <param Columns="high"> </param>
        /// <param Rows="row"> </param>
       private void UpdatePosition(int[,] mazeArray, int high, int row)
        {
            if (mazeArray == null)
                return;

            //move the player
            mazeArray[high, row] = 2;
            //Updating current position to the new moved one
            CurrentPositionY = high;
            CurrentPositionX = row;
            FindPath(mazeArray);
            if ((CurrentPositionX == 0 || CurrentPositionX == _arraySizeX || CurrentPositionY == 0 || CurrentPositionY == _arraySizeY) && !_hasFinished)
            {
                _hasFinished = true;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Exit was found at coordinates [" + CurrentPositionY + "," + CurrentPositionX + "]");
                Console.ResetColor();
                GetFinalMaze(mazeArray);
                return;
            }
        }

        /// <summary>
        /// A function that display the maze in the console.
        /// 2 is shown in red.
        /// </summary>
        /// <param name="array">maze array. To know what to display</param>
        private void Display(int[,] array)
        {
            if (array == null)
                return;
            for (int y = 0; y <= array.GetUpperBound(0); y++)
            {
                for (int x = 0; x <= array.GetUpperBound(1); x++)
                {
                    if(array[y,x] == 2)
                    {
                        //Before solving the maze need to find current position and then ignore it with a bool
                        if (!_hasMoved)
                        {
                            _hasMoved = true;
                            CurrentPositionY = y;
                            CurrentPositionX = x;
                            writeTrail += "Players starting position is [" + CurrentPositionY + "," + CurrentPositionX + "]";
                        }

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(array[y, x]);
                        Console.Write(" ");
                        Console.ResetColor();

                       
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
        /// A funtion that creates an array from string data
        /// </summary>
        /// <param FileInfo="data"></param>
        /// <param ArraySizeX="sizeX"></param>
        /// <param ArraySizeY="sizeY"></param>
        /// <returns></returns>
        private int[,] CreateArray(string data,int sizeX, int sizeY)
        {
            int[,] array = new int[sizeX, sizeY];
            int x = 0;
            int y = 0;
            _arraySizeX = sizeX;
            _arraySizeY = sizeY;

            foreach (string line in data.Split(new string[1] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {
                //Need to check if data given contains only numbers
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

        private void GetFinalMaze(int[,] mazeArray)
        {
            if (mazeArray == null)
                return;
            writeTrail += "\n";
            for (int y = 0; y <= mazeArray.GetUpperBound(0); y++)
            {
                for (int x = 0; x <= mazeArray.GetUpperBound(1); x++)
                {
                        writeTrail += mazeArray[y, x] + " ";
                }
                writeTrail += "\n";
            }
        }

        /// <summary>
        /// Function that creates a file and writes the maze solver logic
        /// </summary>
        private void CreateTrailFile()
        {
            try
            {
                //Writting the trail into file
                using (StreamWriter file = new StreamWriter("../../../Log.txt"))
                {
                    file.Write(writeTrail);
                }
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Help function that checks if string contains numbers
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
        /// Getter and setter for currentPosition Y
        /// </summary>
        private int CurrentPositionY
        {
            get { return _currentPositionY; }
            set { _currentPositionY = value; }
        }

        /// <summary>
        /// Getter and setter for currentPosition X
        /// </summary>
        private int CurrentPositionX
        {
            get { return _currentPositionX; }
            set { _currentPositionX = value; }
        }
    }
}
