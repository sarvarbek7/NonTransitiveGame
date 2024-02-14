using Spectre.Console;

namespace NonTransitiveGame.Rendering
{
	public static class Renderer
	{
		public static void RenderMainMenu(string computerMoveHash, string[] args)
		{
			AnsiConsole.Write(new Markup($"[bold black on yellow]HMAC: {computerMoveHash}[/]\n"));
			Console.WriteLine("Available moves:");

			for (int i = 1; i <= args.Length; i++)
			{
				Console.WriteLine($"{i} - {args[i - 1]}");
			}

			Console.WriteLine("0 - exit");
			Console.WriteLine("? - help");
		}

		/// <summary>
		/// Render win lose table by given arguments and winLoseTable
		/// </summary>
		/// <param name="args"></param>
		/// <param name="winLoseTable">n * n multidimentional array. (note: n is same with args length)</param>
		public static void RenderWinLoseTable(string[] args, int[,] winLoseTable)
		{
			int countOfArgs = args.Length;

			var spectreTable = new Table();

			for (int i = 0; i <= countOfArgs; i++)
			{
				List<Markup> rowCells = [];

				for (int j = 0; j <= countOfArgs; j++)
				{
					if (i is 0 && j is 0)
					{
						//rowCells.Add("v PC\\User >");
						spectreTable.AddColumn("v PC\\User >");
					}
					else if (i is 0 && j is not 0)
					{
						//rowCells.Add(args[j - 1]);
						spectreTable.AddColumn(args[j - 1]);
					}
					else if (i is not 0 && j is 0)
					{
						rowCells.Add(new Markup($"{args[i - 1]}"));
					}
					else if (i is not 0 && j is not 0)
					{
						Markup resultMarkup = winLoseTable[i - 1, j - 1] switch
						{
							1 => new Markup("[green]Win[/]"),
							-1 => new Markup("[red]Lose[/]"),
							0 => new Markup("[grey]Draw[/]"),
							_ => new Markup("Something went wrong")
						};

						rowCells.Add(resultMarkup);
					}
				}
				if (i is not 0)
				{
					spectreTable.AddRow(rowCells.ToArray());
				}
			}

			spectreTable.Border = TableBorder.Square;

			AnsiConsole.Write(spectreTable);
		}
	}
}
