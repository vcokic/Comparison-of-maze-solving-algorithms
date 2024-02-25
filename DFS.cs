using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms_for_solving_mazes
{
    public class NodeDFS
    {
        public int row, column;
        public bool up, right, down, left;

        public NodeDFS(int r, int c)
        {
            row = r;
            column = c;
            up = false;
            right = false;
            down = false;
            left = false;
        }
    }

    public static class DFS
    {
        private static int row, column, endRow, endColumn;
        private static NodeDFS start;
        private static List<NodeDFS> path;

        public static void FindPath(char[,] maze)
        {
            row = 31;
            column = 0;
            endRow = 1;
            endColumn = 32;
            start = new NodeDFS(row, column);
            path = new List<NodeDFS>();
            
            path.Add(start);
            maze[row, column] = '?';

            NodeDFS next;

            while (row != endRow || column != endColumn)
            {
                if (path[path.Count - 1].up == false)
                {
                    path[path.Count - 1].up = true;
                    if (CanIGoUp(path[path.Count - 1], maze) == true)
                    {
                        row -= 1;
                        next = new NodeDFS(row, column);
                        path.Add(next);
                        maze[row, column] = '?';
                    }
                }

                else if (path[path.Count - 1].right == false)
                {
                    path[path.Count - 1].right = true;
                    if (CanIGoRight(path[path.Count - 1], maze) == true)
                    {
                        column += 1;
                        next = new NodeDFS(row, column);
                        path.Add(next);
                        maze[row, column] = '?';
                    }
                }

                else if (path[path.Count - 1].down == false)
                {
                    path[path.Count - 1].down = true;
                    if (CanIGoDown(path[path.Count - 1], maze) == true)
                    {
                        row += 1;
                        next = new NodeDFS(row, column);
                        path.Add(next);
                        maze[row, column] = '?';
                    }
                }

                else if (path[path.Count - 1].left == false)
                {
                    path[path.Count - 1].left = true;
                    if (CanIGoLeft(path[path.Count - 1], maze) == true)
                    {
                        column -= 1;
                        next = new NodeDFS(row, column);
                        path.Add(next);
                        maze[row, column] = '?';
                    }
                }

                else
                {
                    path.RemoveAt(path.Count - 1);
                    row = path[path.Count - 1].row;
                    column = path[path.Count - 1].column;
                }
            }

            FinalPath(maze);
        }

        public static bool IsValid(int row, int column)
        {
            if (row < 0 || row > 32 || column < 0 || column > 32)
                return false;

            else
                return true;
        }

        private static bool CanIGoUp(NodeDFS check, char[,] maze)
        {
            if (IsValid(check.row, check.column))
            {
                if (maze[check.row - 1, check.column] == '.')
                    return true;
                
                else
                    return false;
            }

            else
                return false;
        }

        private static bool CanIGoRight(NodeDFS check, char[,] maze)
        {
            if (IsValid(check.row, check.column))
            {
                if (maze[check.row, check.column + 1] == '.')
                    return true;
                
                else
                    return false;
            }

            else
                return false;
        }

        private static bool CanIGoDown(NodeDFS check, char[,] maze)
        {
            if (IsValid(check.row, check.column))
            {
                if (maze[check.row + 1, check.column] == '.')
                    return true;
                
                else
                    return false;    
            }

            else
                return false;
        }

        private static bool CanIGoLeft(NodeDFS check, char[,] maze)
        {
            if (IsValid(check.row, check.column))
            {
                if (maze[check.row, check.column - 1] == '.')
                    return true;
                
                else
                    return false;    
            }

            else
                return false;
        }

        private static void FinalPath(char[,] maze)
        {
            foreach (NodeDFS n in path)
                maze[n.row, n.column] = 'X';
        }
    }
}
