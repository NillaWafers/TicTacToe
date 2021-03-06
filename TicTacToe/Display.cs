﻿using TicTacToe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    //public class IOException : SystemException
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
            Console.WriteLine("1. All user inputs must be of a valid format.");
            Console.WriteLine("2. Typical game play input should be in the following format:.");
            Console.WriteLine("   X,Y,Z where the letters represent their respective coordinates.");
            Console.WriteLine("3. While playing a game the following may be typed in:");
            Console.WriteLine("");
            Console.WriteLine("EXIT - to return to menu.\nQUIT - to quit the game.\nSAVE - to save current status.");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Press any key to return to the Main Menu.");
        }

        public static void RenderStats()
        {
            //try
            //{
            //    string text = System.IO.File.ReadAllText(@"C:\Users\Blair Thompson\Desktop\CIT 255\TicTacToe\SaveStats.txt");
            //}
            //// Catch the IOException generated if the 
            //// specified part of the file is locked.
            //catch (IOException e)
            //{
            //    Console.WriteLine(
            //        "{0}: The write operation could not " +
            //        "be performed because the specified " +
            //        "part of the file is locked.",
            //        e.GetType().Name);
            //}
            //string text = System.IO.File.ReadAllText(@"C:\Users\Blair Thompson\Desktop\CIT 255\TicTacToe\SaveStats.txt");
            //var playerWin = text.Split(new char[] { ',' }, 2);
            //string gamesPlayed = text.Substring(text.LastIndexOf(',') + 1);
            Console.Clear();
            Console.WriteLine("*******************************************");
            Console.WriteLine("**********3D TIC-TAC-TOE WIN STATS*********");
            Console.WriteLine("*******************************************");
            Console.WriteLine("");
            Console.WriteLine("Winning Stats.... Player One Wins: {0}","0");
            Console.WriteLine("Total number of games played: {0}","0");
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
            Console.WriteLine("Stats will automatically be overwritten when saved.");
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
            Console.ForegroundColor = ConsoleColor.Blue;
            row = "Y = 1" + h_gap + "Y = 3";
            Console.WriteLine(row);
            row = "X→ Z↓" + h_gap + "X→ Z↑";
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

                for (byte z = (byte)gridDisplays[(byte)SliceBounds.Lower].GetLength(1); z > 0; z--)
                {
                    row = row + gridDisplays[2][z - 1, x];
                }

                row = DrawGridPairRow(row);
            }

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            row = "FRONT" + h_gap + "BACK";
            Console.WriteLine(row);
            Console.ForegroundColor = ConsoleColor.Blue;
            row = "X = 1" + h_gap + "X = 3";
            Console.WriteLine(row);
            row = "Y↓ Z→" + h_gap + "Y↓ Z←";
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
            Console.ForegroundColor = ConsoleColor.Blue;
            row = "Z = 1" + h_gap + "Z = 3";
            Console.WriteLine(row);
            row = "X→ Y↓" + h_gap + "X← Y↓";
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
