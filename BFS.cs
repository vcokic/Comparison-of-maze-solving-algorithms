using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms_for_solving_mazes
{
    public class NodeBFS
    {
        public int row;
        public int column;
        public int distance;
        public NodeBFS parent;

        public NodeBFS(int r, int c, int d)
        {
            row = r;
            column = c;
            distance = d;
        }
    }

    public static class BFS
    {
        private static int startRow, startColumn, endRow, endColumn;
        private static int[] rowMoves = { -1, 0, 1, 0 }, columnMoves = { 0, 1, 0, -1 };
        private static List<NodeBFS> finalPath;

        public static void FindPath(char[,] maze)
        {
            startRow = 31;
            startColumn = 0;
            endRow = 1;
            endColumn = 32;
            finalPath = new List<NodeBFS>();

            bool[,] visited = new bool[33, 33];
            List<NodeBFS> path = new List<NodeBFS>();
            NodeBFS start = new NodeBFS(startRow, startColumn, 0);
            path.Add(start);
            finalPath.Add(start);

            while (path.Count != 0)
            {
                NodeBFS current = path[0];
                maze[current.row, current.column] = '?';

                if (current.row == endRow && current.column == endColumn)
                    break;
                else
                {
                    path.Remove(current);
                    for (int i = 0; i < 4; i++)
                    {
                        int row = current.row + rowMoves[i];
                        int col = current.column + columnMoves[i];

                        if (IsValid(row, col) && maze[row, col] == '.' && !visited[row, col])
                        {
                            visited[row, col] = true;
                            maze[row, col] = '?';
                            NodeBFS next = new NodeBFS(row, col, current.distance + 1);
                            next.parent = current;
                            path.Add(next);
                            finalPath.Add(next);
                        }
                    }
                }
            }
            Backtrack(finalPath, maze);
        }

        static bool IsValid(int row, int column)
        {
            if (row < 0 || row > 32 || column < 0 || column > 32)
                return false;
            
            else
                return true;
        }

        static void Backtrack(List<NodeBFS> path, char[,] maze)
        {
            NodeBFS current = path[path.Count - 1];
            NodeBFS parent = current.parent;

            while (current.row != endRow && current.column != endColumn)
            {
                path.Remove(current);
                current = path[path.Count - 1];
                parent = current.parent;

            }
            maze[current.row, current.column] = 'X';

            while (path.Count > 1)
            {
                if (current.row == parent.row && current.column == parent.column)
                {
                    maze[current.row, current.column] = 'X';
                    parent = current.parent;
                    path.Remove(current);
                    current = path[path.Count - 1];
                }

                else
                {
                    path.Remove(current);
                    current = path[path.Count - 1];
                }
            }

            maze[path[0].row, path[0].column] = 'X';
        }
    }
}
