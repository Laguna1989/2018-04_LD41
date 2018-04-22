using JamUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JamTemplate.Tower
{
    class ResearchManager
    {
        public static float CritChance = 0.0f;
        public static long CritChanceCost = GP.ResearchCritChanceBaseCost;
        
        public static void IncreaseCritChance()
        {

			if (Resources.DecreaseResearch(CritChanceCost))
			{
				CritChance += 2;
				CritChanceCost += (long)(GP.ResearchCritChanceBaseCost + CritChanceCost * 0.1);
			}

		}

        public static float CritFactor = 1.75f;
        public static long CritFactorCost = GP.ResearchCritFactorBaseCost;
        public static void IncreaseCritFactor()
        {
			if (Resources.DecreaseResearch(CritFactorCost))
			{
				CritFactor += 0.35f;
				CritFactorCost += (long)(GP.ResearchCritFactorBaseCost + CritFactorCost * 0.1);
			}
        }

        public static float FreezeChance = 0.0f;
        public static long FreezeChanceCost = GP.ResearcFreezeChanceBaseCost;
        public static void IncreaseFreezeChance()
        {
			if (Resources.DecreaseResearch(FreezeChanceCost))
			{
				FreezeChance += 1.5f;
				FreezeChanceCost += (long)(GP.ResearcFreezeChanceBaseCost + FreezeChanceCost * 0.1);
			}
        }

        public static float FreezeDuration = 0.75f;
        public static long FreezeDurationCost = GP.ResearcFreezeDurationBaseCost;
        public static void IncreaseFreezeDuration()
        {
			if (Resources.DecreaseResearch(FreezeDurationCost))
			{
				FreezeDuration += 0.125f;
				FreezeDurationCost += (long)(GP.ResearcFreezeDurationBaseCost + FreezeDurationCost * 0.1);
			}
        }

        public static float GoldChance = 0.0f;
        public static long GoldChanceCost = GP.GoldDropChanceBaseCost;
        public static void IncreaseGoldChance()
        {
			if (Resources.DecreaseResearch(GoldChanceCost))
			{
				GoldChance += 2.5f;
				GoldChanceCost += (long)(GP.GoldDropChanceBaseCost + GoldChanceCost * 0.1);
			}
        }
        
    }
}
