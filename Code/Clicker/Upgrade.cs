using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JamTemplate
{
	public class Upgrade
	{
		public Type type;
		public bool used = false;
		public long cost;

		public delegate void Function();
		public Function function;

		public enum Type
		{
			Bronze_Rod,
			Silver_Rod,
			Gold_Rod,
			Platinum_Rod,
			Diamond_Rod
		}

		public Upgrade(Type upgradeType)
		{
			type = upgradeType;
			switch (upgradeType)
			{
				case Type.Bronze_Rod:
					function = Bronze_Rod;
					break;

				case Type.Silver_Rod:
					function = Silver_Rod;
					break;

				case Type.Gold_Rod:
					function = Gold_Rod;
					break;

				case Type.Platinum_Rod:
					function = Platinum_Rod;
					break;

				case Type.Diamond_Rod:
					function = Diamond_Rod;
					break;
			}
		}

		public void Buy()
		{
			if (used == false)
			{
				if (Resources.DecreaseMoney(cost))
				{
					function();
					used = true;
				}
				else
					Console.WriteLine("You don't have enough money: " + cost.ToString());
			}
			else
				Console.WriteLine("Upgrade already bought");
		}

		#region Functions

		private static void Bronze_Rod()
		{
			
		}
		private void Silver_Rod()
		{

		}
		private void Gold_Rod()
		{

		}
		private void Platinum_Rod()
		{

		}
		private void Diamond_Rod()
		{

		}


		#endregion

	}
}
