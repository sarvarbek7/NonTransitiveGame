namespace NonTransitiveGame.Logic
{
	public class RandomMoveLogic
	{
		public static int MakeRandomMoveForComputer(int countOfArgs)
		{
			var rand = new Random();

			return rand.Next(1, countOfArgs);
		}
	}
}
