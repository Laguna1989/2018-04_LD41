using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JamTemplate
{
	public class ResourceGainer
	{

		private long baseCost;

		public float moneyIncome;
		public float researchIncome;

		public long amount = 0;

		public Type type;

        public static float totalBuildingsBuilt { get; set; } = 0;

		public long nextCost()
		{
			long value = 0;

			value = (long)Math.Round((baseCost * Math.Pow(1.07, amount)));

			return value;
		}

		public enum Type
		{
			#region Money
			Squire,
			Farmer,
			Knight,
			Feudal_Lord,
			Church,
			Gold_Mine,
			Diamond_Mine,
			#endregion

			Alchemy_Lab

		}

		public ResourceGainer(Type type)
		{
			this.type = type;
			switch (type)
			{
				case Type.Squire:
					baseCost = 10;

					moneyIncome = 0.1f;
					researchIncome = 0;
					break;

				case Type.Farmer:
					baseCost = 50;

					moneyIncome = 0.5f;
					researchIncome = 0;
					break;

				case Type.Knight:
					baseCost = 200;

					moneyIncome = 2f;
					researchIncome = 0;
					break;

				case Type.Feudal_Lord:
					baseCost = 500;

					moneyIncome = 4;
					researchIncome = 0;
					break;

				case Type.Church:
					baseCost = 2500;

					moneyIncome = 8;
					researchIncome = 0;
					break;

				case Type.Gold_Mine:
					baseCost = 7500;

					moneyIncome = 30;
					researchIncome = 0;
					break;

				case Type.Diamond_Mine:
					baseCost = 15000;

					moneyIncome = 40;
					researchIncome = 0;
					break;

				case Type.Alchemy_Lab:
					baseCost = 300;

					moneyIncome = 0;
					researchIncome = 1;
					break;


			}

			baseCost = baseCost / 5;
		}

		public void Add()
		{

			long amount = Resources.amountSelected;
			long cost = nextCost() * amount;
			if (Resources.DecreaseMoney(cost))
			{
                totalBuildingsBuilt += amount;

                this.amount += amount;
				Console.WriteLine(string.Format("Added {0} of {1} at the price of {2}", amount, type, cost));
			}
			else
			{
				Console.WriteLine("You don't have enough money: " + cost.ToString());
			}
		}

	}
}
