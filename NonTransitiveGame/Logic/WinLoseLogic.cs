namespace NonTransitiveGame.Logic
{
	public static class WinLoseLogic
	{
		public static int[,] GenerateWinLoseTableForUser(int countOfArguments)
		{
			int[,] winLoseTable = new int[countOfArguments, countOfArguments];
			int p = countOfArguments / 2;

			for (int i = 1; i <= countOfArguments; i++)
			{
				for (int j = 1; j <= countOfArguments; j++)
				{
					winLoseTable[i - 1, j - 1] = Math.Sign((i - j + p + countOfArguments) % countOfArguments - p);
				}
			}

			return winLoseTable;
		}

		public static string DetermineWinner(int computerMoveOrder, int userMoveOrder, int[,] winLoseTable)
		{
			return winLoseTable[computerMoveOrder, userMoveOrder] switch
			{
				1 => "User",
				-1 => "Computer",
				_ => "Friendship"
			};
		}
	}
}
