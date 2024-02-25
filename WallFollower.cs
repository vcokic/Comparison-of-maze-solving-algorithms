using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms_for_solving_mazes
{
    public static class WallFollower
    {
        private static int row = 31, column = 0;
        private static string direction = "right";

        public static void FindPath(char[,] maze)
        {
            maze[row, column] = 'X';

            if (WallOnRight(maze) && !WallOnFront(maze))
                GoForward(maze);

            else if (WallOnRight(maze) && WallOnFront(maze))
            {
                if (direction == "right")
                    direction = "up";

                else if (direction == "up")
                    direction = "left";

                else if (direction == "left")
                    direction = "down";

                else if (direction == "down")
                    direction = "right";
            }

            else if (WallOnRight(maze) == false)
            {
                if (direction == "right")
                    direction = "down";
                
                else if (direction == "up")
                    direction = "right";
                
                else if (direction == "left")
                    direction = "up";
                
                else if (direction == "down")
                    direction = "left";
                
                GoForward(maze);
            }

            if (column == 32)
                return;
            
            else
                FindPath(maze);
        }

        private static bool WallOnRight(char[,] maze)
        {
            if (direction == "right" && maze[row + 1, column] == '#')
                return true;
            
            else if (direction == "up" && maze[row, column + 1] == '#')
                return true;
            
            else if (direction == "left" && maze[row - 1, column] == '#')
                return true;
            
            else if (direction == "down" && maze[row, column - 1] == '#')
                return true;
            
            return false;
        }

        private static bool WallOnFront(char[,] maze)
        {
            if (direction == "right" && maze[row, column + 1] == '#')
                return true;
            
            else if (direction == "up" && maze[row - 1, column] == '#')
                return true;
            
            else if (direction == "left" && maze[row, column - 1] == '#')
                return true;
            
            else if (direction == "down" && maze[row + 1, column] == '#')
                return true;
            
            return false;
        }

        private static void GoForward(char[,] maze)
        {
            if (direction == "right")
            {
                column++;
                maze[row, column] = 'X';
            }

            else if (direction == "up")
            { 
                row--;
                maze[row, column] = 'X';
            }

            else if (direction == "left")
            {
                column--;
                maze[row, column] = 'X';
            }

            else if (direction == "down")
            {
                row++;
                maze[row, column] = 'X';
            }
        }
    }
}
