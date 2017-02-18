using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

using StardewValley;
using StardewModdingAPI;

namespace StardewNotification
{
	public class HarvestNotification
	{

		private const int MUSHROOM_CAVE = 2;
		private const int FRUIT_CAVE = 1;

		public void CheckHarvestsAroundFarm()
		{
			CheckSeasonalHarvests();
			CheckFarmCaveHarvests(Game1.getLocationFromName(Constants.FARMCAVE));
		}

		public void CheckFarmCaveHarvests(GameLocation farmcave)
		{
			if (!Util.Config.notifyFarmCave) return;
			if (Game1.player.caveChoice == MUSHROOM_CAVE)
			{
				var numReadyForHarvest = 0;
				foreach (var pair in farmcave.Objects)
				{
					if (pair.Value.readyForHarvest) numReadyForHarvest++;
				}
				if (numReadyForHarvest > 0)
					Util.ShowFarmCaveMessage(farmcave);
			}
			else if (Game1.player.caveChoice == FRUIT_CAVE && farmcave.Objects.Count > 0)
			{
				Util.ShowFarmCaveMessage(farmcave);
			}
		}

		public void CheckSeasonalHarvests()
		{
			if (!Util.Config.notifySeasonalForage) return;
			string seasonal = null;
			var dayOfMonth = Game1.dayOfMonth;
			switch (Game1.currentSeason)
			{
				case Constants.SPRING:
					if (dayOfMonth > 14 && dayOfMonth < 19) seasonal = Constants.SALMONBERRY;
					break;
				case Constants.SUMMER:
					if (dayOfMonth > 11 && dayOfMonth < 15) seasonal = Constants.SEASHELLS;
					break;
				case Constants.FALL:
					if (dayOfMonth > 7 && dayOfMonth < 12) seasonal = Constants.BLACKBERRY;
					break;
				default:
					break;
			}
			if (!ReferenceEquals(seasonal, null))
			{
				Util.showMessage($"{seasonal}");
			}
		}

		public void CheckBeachHarvests()
		{
			//TODO: Beach
		}
	}
}
