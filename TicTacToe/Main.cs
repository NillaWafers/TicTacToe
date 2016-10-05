using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using TicTacToe;

namespace TicTacToe
{
	class TicTacToe
	{
        private const string pat = @"^[123]";

		static void Main(string[] args)
		{
            char k = '~';
			do
			{
				ConsoleRenderer.RenderMenu();

				if ((k = Char.ToUpper(Console.ReadKey().KeyChar)) == '1')
				{
                    k = '~';
					GameLoop();
				}
                if (k == '2')
                {
                    ConsoleRenderer.RenderRules();
                    Console.ReadKey();
                }
                if (k == '3')
                {
                    ConsoleRenderer.RenderStats();
                    Console.ReadKey();
                }
                if (k == '9')
                {
                    ConsoleRenderer.ResetStats();
                    //if Y then do stuff else k = '~'
                    Console.ReadKey();
                }
				if (k == 'Q')
				{
					return;
				}
                k = '~';
			} while (k == '~');
		}

		static void GameLoop()
		{
			char k = 'Y';
            int playerWin = 0;
            int gamesPlayed = 0;

			do
			{
				Game(playerWin,gamesPlayed);
				Console.WriteLine();
				Console.WriteLine("Would you like to play again (Y/N)?");

				do
				{
					k = Char.ToUpper(Console.ReadKey().KeyChar);
                    if (k == 'Y')
                    {
                        gamesPlayed++;
                    }

					if (k != 'Y' && k != 'N')
						Console.WriteLine("Please answer Y or N");
				}
				while (k != 'Y' && k != 'N');
			}
			while (k == 'Y');
		}

		private static void Game(int playerWin,int gamesPlayed)
		{
			bool playerOne = false;
			string[] inputArray;
			byte x;
			byte y;
			byte z;
			bool validIndex;
			bool draw = false;
			string error = "";
			ConsoleRenderer graphics = new ConsoleRenderer();

			PlayCube[][,] gameCube = Initialize();

			Console.Clear();

            //Gameplay
			while (!Referee.PlayerWon(gameCube, playerOne) && !(draw = Referee.Draw(gameCube)))
			{
				validIndex = true;
				inputArray = null;
				x = 0;
				y = 0;
				z = 0;

				graphics.RenderGraphics(true);

				if (error != "")
				{
					Console.WriteLine(error);
					error = "";
					Console.WriteLine();
				}

				if (playerOne = !playerOne)
					Console.WriteLine("Player 1: Place your X (use X,Y,Z coords)");

				else
					Console.WriteLine("Player 2: Place your O (use X,Y,Z coords)");

				while (inputArray == null)
				{
					inputArray = Console.ReadLine().Split(new char[] { ',' });
				}

				if (inputArray[0].ToUpper() == "QUIT")
				{
					return;
				}

				if (inputArray[0].ToUpper() == "EXIT")
				{
					Environment.Exit(0);
				}

                if (inputArray[0].ToUpper() == "SAVE")
                {
                    // Example #2: Write one string to a text file.
                    string text = playerWin+","+gamesPlayed;
                    // WriteAllText creates a file, writes the specified string to the file,
                    // and then closes the file.    You do NOT need to call Flush() or Close().
              //System.IO.File.WriteAllText(@"C:\Users\Blair Thompson\Desktop\CIT 255\TicTacToe\SaveStats.txt", text);
                    Console.WriteLine("Stats have been saved.");
                }

				for (int i = 0; i < inputArray.Length; i++)
				{
					if (!Regex.Match(inputArray[i], pat).Success)
					{
						validIndex = false;
						break;
					}
				}
                
                //checking and flagging.q
				if (validIndex && inputArray.Length == 3)
				{
					x = (byte)(Convert.ToByte(inputArray[0]) - 1);
					y = (byte)(Convert.ToByte(inputArray[1]) - 1);
					z = (byte)(Convert.ToByte(inputArray[2]) - 1);

					if (!gameCube[y][x, z].TakeSpace(playerOne))
					{
						playerOne = !playerOne;

						error = "That space is already occupied";
					}

					else
					{
						graphics.UpdateGraphics(playerOne, new YXZ(y, x, z));
					}
				}

				else
				{
					playerOne = !playerOne;

					error = "Please specify an index between 1 and 3 for all axis";
				}

				Console.WriteLine();
			}

			graphics.RenderGraphics(true);

			if (draw)
				Console.WriteLine("***************CAT**************");

			else
			{
				if (playerOne)
                {
					Console.WriteLine("************PLAYER 1************");
                }
				else
                {
					Console.WriteLine("************PLAYER 2************");
                }
			}

			Console.WriteLine("*******HAS WON THE GAME*********");
		}

		private static PlayCube[][,] Initialize()
		{
			PlayCube[][,] gameCube = new PlayCube[3][,] { new PlayCube[3, 3], new PlayCube[3, 3], new PlayCube[3, 3] };

			for (byte y = 0; y < gameCube.Length; y++)
			{
				for (byte x = 0; x < gameCube[y].GetLength(0); x++)
				{
					for (byte z = 0; z < gameCube[y].GetLength(1); z++)
					{
						gameCube[y][x, z] = new PlayCube();
					}
				}
			}

			return gameCube;
		}
	}
}
