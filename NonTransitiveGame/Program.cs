using NonTransitiveGame.Encryption;
using NonTransitiveGame.Logic;
using NonTransitiveGame.Rendering;

using Spectre.Console;

public class Program
{
	private static void Main(string[] args)
	{
		int countOfArgs = args.Length;

		if (countOfArgs is 0)
		{
			AnsiConsole.Write(new Markup("[bold on red]Arguments can not be empty. (note: they must be odd)[/]"));
			return;
		}

		if (countOfArgs % 2 is 0)
		{
			AnsiConsole.Write(new Markup("[bold on red]Arguments can not be even. Please provide odd arguments[/]"));
			return;
		}

		int computerMoveOrder = RandomMoveLogic.MakeRandomMoveForComputer(countOfArgs);
		string computerMove = args[computerMoveOrder - 1];

		byte[] key = HmacKey.GenerateKey();

		byte[] hash = Hmac.HashData(computerMove, key);

		string computerMoveHash = Convert.ToHexString(hash);

		int[,] winLoseTable = WinLoseLogic.GenerateWinLoseTableForUser(countOfArgs);

		bool isGameOn = true;

		while (isGameOn)
		{
			Renderer.RenderMainMenu(computerMoveHash, args);

			Console.Write("Enter your move: ");

			string? userInput = Console.ReadLine();

			if (userInput == null)
			{
				Console.WriteLine("Input can not be empty. Please choose valid option");
				Task.Delay(500).Wait();
				AnsiConsole.Clear();
				continue;
			}

			if (userInput == "?")
			{
				Renderer.RenderWinLoseTable(args, winLoseTable);
				Console.WriteLine("\nTo go previous page, press anything.\n");
				Console.ReadKey();
				AnsiConsole.Clear();
				continue;
			}

			if (int.TryParse(userInput, out int choice))
			{
				if (choice is < 0 || choice > countOfArgs)
				{
					Console.WriteLine($"{choice} is not available option please try again");
					Task.Delay(500).Wait();
					AnsiConsole.Clear();
					continue;
				}

				if (choice is 0)
				{
					return;
				}

				string userMove = args[choice - 1];
				Console.WriteLine($"Your move: {userMove}");
				Console.WriteLine($"Computer move: {computerMove}");

				string winner =
					WinLoseLogic.DetermineWinner(computerMoveOrder - 1, choice - 1, winLoseTable);

				Console.WriteLine($"{winner} wins.\n");
				AnsiConsole.Write(new Markup($"[bold black on yellow]HMAC Key: {Convert.ToHexString(key)}[/]\t"));
				AnsiConsole.Write(new Markup("([bold on cyan]HMACSHA256[/] hashing algorithm is used.)"));
				return;
			}
		}
	}
}