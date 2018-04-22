using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JamTemplate.Tower
{
    class ResearchManager
    {
        public static float CritChance = 0.0f;
        public static long CritChanceCost = 100;
        public static void IncreaseCritChance()
        {

			if (Resources.DecreaseResearch(CritChanceCost))
			{
				CritChance += 1;
				CritChanceCost += (long)(100 + CritChanceCost * 0.1);
			}

		}

        public static float CritFactor = 1.75f;
        public static long CritFactorCost = 100;
        public static void IncreaseCritFactor()
        {
			if (Resources.DecreaseResearch(CritFactorCost))
			{
				CritFactor += 0.2f;
				CritFactorCost += (long)(100 + CritFactorCost * 0.1);
			}
        }

        public static float FreezeChance = 0.0f;
        public static long FreezeChanceCost = 100;
        public static void IncreaseFreezeChance()
        {
			if (Resources.DecreaseResearch(FreezeChanceCost))
			{
				FreezeChance += 0.75f;
				FreezeChanceCost += (long)(100 + FreezeChanceCost * 0.1);
			}
        }

        public static float FreezeDuration = 0.75f;
        public static long FreezeDurationCost = 100;
        public static void IncreaseFreezeDuration()
        {
			if (Resources.DecreaseResearch(FreezeDurationCost))
			{
				FreezeDuration += 0.075f;
				FreezeDurationCost += (long)(100 + FreezeDurationCost * 0.1);
			}
        }

        public static float GoldChance = 0.0f;
        public static long GoldChanceCost = 100;
        public static void IncreaseGoldChance()
        {
			if (Resources.DecreaseResearch(GoldChanceCost))
			{
				GoldChance += 1;
				GoldChanceCost += (long)(100 + GoldChanceCost * 0.1);
			}
        }
        
    }
}
