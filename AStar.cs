using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms_for_solving_mazes
{
    public class NodeAStar
    {
        public int row, column;
        public int f, g, h;
        public NodeAStar parent;

        public NodeAStar(int r, int c)
        {
            row = r;
            column = c;
        }
    }

    public static class AStar
    {
        private static int startRow, startColumn, endRow, endColumn;
        private static List<NodeAStar> open, close;

        public static void FindPath(char[,] maze)
        {
            startRow = 31;
            startColumn = 0;
            endRow = 1;
            endColumn = 32;
            open = new List<NodeAStar>();
            close = new List<NodeAStar>();

            NodeAStar start = new NodeAStar(startRow, startColumn);
            start.g = 0;
            start.h = hValue(start.row, start.column, endRow, endColumn);
            start.f = start.g + start.h;
            start.parent = null;
            open.Add(start);
            NodeAStar current = start;

            while (open.Count > 0)
            {
                int minValue = int.MaxValue;
                foreach (NodeAStar n in open)
                {
                    if (n.f < minValue)
                    {
                        minValue = n.f;
                        current = n;
                    }
                }

                close.Add(current);
                open.Remove(current);
                maze[current.row, current.column] = '?';

                if (current.row == endRow && current.column == endColumn)
                    break;

                List<NodeAStar> neighbors = GetNeighbors(current);

                foreach (NodeAStar neighbor in neighbors)
                {
                    if (IsValid(neighbor, maze))
                    {
                        bool flagClose = false;
                        foreach (NodeAStar c in close)
                        {
                            if (c.row == neighbor.row && c.column == neighbor.column)
                            {
                                flagClose = true;
                                break;
                            }
                        }
                        if (flagClose == true)
                            continue;

                        bool flagOpen = false;
                        NodeAStar equal = null;
                        foreach (NodeAStar o in open)
                        {
                            if (o.row == neighbor.row && o.column == neighbor.column)
                            {
                                flagClose = true;
                                equal = o;
                                break;
                            }
                        }

                        if (flagOpen == false)
                        {
                            neighbor.g = current.g + 1;
                            neighbor.h = hValue(neighbor.row, neighbor.column, endRow, endColumn);
                            neighbor.f = neighbor.g + neighbor.h;
                            neighbor.parent = current;

                            open.Add(neighbor);
                        }
                        else
                        {
                            if (neighbor.g + equal.h < equal.f)
                            {
                                open.Remove(equal);
                                neighbor.g = current.g + 1;
                                neighbor.h = hValue(neighbor.row, neighbor.column, endRow, endColumn);
                                neighbor.f = neighbor.g + neighbor.h;
                                neighbor.parent = current;
                                open.Add(neighbor);
                            }
                        }
                    }
                }
            }

            Backtrack(current, maze);
        }

        static int hValue(int currentX, int currentY, int endX, int endY)
        {
            int h = Math.Abs(endX - currentX) + Math.Abs(endY - currentY);
            return h;
        }

        static List<NodeAStar> GetNeighbors(NodeAStar c)
        {
            List<NodeAStar> neighbors = new List<NodeAStar>();

            int[,] moves = { { -1, 0 }, { 0, 1 }, { 1, 0 }, { 0, -1 } };

            for (int i = 0; i < 4; i++)
            {
                NodeAStar next = new NodeAStar(c.row + moves[i, 0], c.column + moves[i, 1]);
                neighbors.Add(next);
            }

            return neighbors;
        }

        static bool IsValid(NodeAStar n, char[,] maze)
        {
            if (n.row < 0 || n.row > 32 || n.column < 0 || n.column > 32)
            
                return false;
            
            else
            {
                if (maze[n.row, n.column] == '.')
                    return true;
                
                else
                    return false;
            }
        }

        static void Backtrack(NodeAStar current, char[,] maze)
        {
            while (current != null)
            {
                maze[current.row, current.column] = 'X';
                current = current.parent;
            }
        }
    }
}
