using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JamTemplate
{
	public static class Resources
	{
		public const float INCOME_TICK = 5f; // The amount of time in seconds it takes for the idleResearchIcome and idleMoneyIncome to be added in Game.cs

		public static long money { get; set; } = 0;
		public static long research { get; set; } = 0;

		public static long moneyPerClick { get; set; } = 1;
		public static long idleMoneyIncome { get; set; } = 0;

		public static long idleResearchIncome { get; set; } = 0;

		public static void UpdateIdleIncome()
		{
			money += idleMoneyIncome;
			research += idleResearchIncome;

		}

		public static void UpdateMoney()
		{
			money += moneyPerClick;
		}
	}
}
