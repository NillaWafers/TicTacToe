using TicTacToe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class ConsoleRenderer
    {
        private char[][,] gridDisplays;

        public static void RenderMenu()
        {
            Console.Clear();
            Console.WriteLine("*******************************************");
            Console.WriteLine("*********WELCOME TO 3D TIC-TAC-TOE*********");
            Console.WriteLine("*******************************************");
            Console.WriteLine("1. Play Game");
            Console.WriteLine("2. Show Rules");
            Console.WriteLine("3. Show Win Stats");
            Console.WriteLine("9. Reset Win Stats");
            Console.WriteLine("Q. Quit");
        }

        public static void RenderRules()
        {
            Console.Clear();
            Console.WriteLine("*******************************************");
            Console.WriteLine("************3D TIC-TAC-TOE RULES***********");
            Console.WriteLine("*******************************************");
            Console.WriteLine("");
            Console.WriteLine("1.This is the first rule.");
            Console.WriteLine("2.This is the second rule.");
            Console.WriteLine("");
            Console.WriteLine("Press any key to return to the Main Menu.");
        }

        public static void RenderStats()
        {
            Console.Clear();
            Console.WriteLine("*******************************************");
            Console.WriteLine("**********3D TIC-TAC-TOE WIN STATS*********");
            Console.WriteLine("*******************************************");
            Console.WriteLine("");
            Console.WriteLine("Winning Stats....");
            Console.WriteLine("Something else here: ");
            Console.WriteLine("Remember to display/read stuff from file.");
            Console.WriteLine("");
            Console.WriteLine("Press any key to return to the Main Menu.");
        }

        public static void ResetStats()
        {
            Console.Clear();
            Console.WriteLine("*******************************************");
            Console.WriteLine("**********3D TIC-TAC-TOE WIN STATS*********");
            Console.WriteLine("*******************************************");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Are you sure you want to reset win stats? (Y/N)");
        }

        public ConsoleRenderer()
        {
            gridDisplays = new char[(byte)SliceBounds.Upper + 1][,] { new char[(byte)SliceBounds.Upper + 1, (byte)SliceBounds.Upper + 1]
																				, new char[(byte)SliceBounds.Upper + 1, (byte)SliceBounds.Upper + 1]
																				, new char[(byte)SliceBounds.Upper + 1, (byte)SliceBounds.Upper + 1]};

            for (byte y = 0; y < gridDisplays.Length; y++)
            {
                for (byte x = 0; x < gridDisplays[y].GetLength(0); x++)
                {
                    for (byte z = 0; z < gridDisplays[y].GetLength(1); z++)
                    {
                        gridDisplays[y][x, z] = '-';
                    }
                }
            }
        }

        public void UpdateGraphics(bool _playerOne, YXZ _lastTaken)
        {
            if (_playerOne)
                gridDisplays[_lastTaken.Y][_lastTaken.X, _lastTaken.Z] = 'X';
            else
                gridDisplays[_lastTaken.Y][_lastTaken.X, _lastTaken.Z] = 'O';
        }

        public void RenderGraphics(bool _clear)
        {
            const string gap = "      ";
            const string h_gap = "   ";

            if (_clear)
                Console.Clear();

            Console.ForegroundColor = ConsoleColor.Red;
            string row = "TOP    BOTTOM";
            Console.WriteLine(row);
            Console.ResetColor();
            row = "";

            //Top and bottom Y slices
            for (byte x = 0; x < gridDisplays[(byte)SliceBounds.Lower].GetLength(0); x++)
            {
                for (byte z = 0; z < gridDisplays[(byte)SliceBounds.Lower].GetLength(1); z++)
                {
                    row += gridDisplays[0][z, x];
                }

                row += gap;

                for (byte z = 0; z < gridDisplays[(byte)SliceBounds.Lower].GetLength(1); z++)
                {
                    row = row + gridDisplays[2][z, x];
                }

                row = DrawGridPairRow(row);
            }

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            row = "FRONT" + h_gap + "BACK";
            Console.WriteLine(row);
            row = "";

            //Front and back Z slices
            for (byte y = 0; y < gridDisplays.Length; y++)
            {
                for (byte x = 0; x < gridDisplays[(byte)SliceBounds.Lower].GetLength(0); x++)
                {
                    row += gridDisplays[y][0, x];
                }

                row += gap;

                for (byte x = (byte)(gridDisplays[(byte)SliceBounds.Upper].GetLength(0)); x > 0; x--)
                {
                    row += gridDisplays[y][2, x - 1];
                }

                row = DrawGridPairRow(row);
            }

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            row = "LEFT" + h_gap + " " + "RIGHT";
            Console.WriteLine(row);
            row = "";

            //Left and Right X slices
            for (byte y = 0; y < gridDisplays.Length; y++)
            {
                for (byte z = (byte)(gridDisplays[(byte)SliceBounds.Upper].GetLength(0)); z > 0; z--)
                {
                    row += gridDisplays[y][z - 1, 0];
                }

                row += gap;

                for (byte z = 0; z < gridDisplays[(byte)SliceBounds.Lower].GetLength(0); z++)
                {
                    row += gridDisplays[y][z, 2];
                }

                row = DrawGridPairRow(row);
            }

            //Center is pretty easy, though
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("CENTER");
            Console.WriteLine();
            row = "  " + gridDisplays[(byte)SliceBounds.Middle][(byte)SliceBounds.Middle, (byte)SliceBounds.Middle];
            row = DrawGridPairRow(row);

            Console.Write(Environment.NewLine);
        }

        private string DrawGridPairRow(string _row)
        {
            for (byte b = 0; b < _row.Length; b++)
            {
                Console.ResetColor();

                if (_row[b] == 'X')
                    Console.ForegroundColor = ConsoleColor.Green;

                if (_row[b] == 'O')
                    Console.ForegroundColor = ConsoleColor.Yellow;

                Console.Write(_row[b]);
            }

            Console.ResetColor();
            Console.Write(Environment.NewLine);

            return "";
        }
    }
}
