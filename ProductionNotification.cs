using System;
using System.Collections.Generic;

using StardewValley;

namespace StardewNotification
{
	public class ProductionNotification
	{
		/// <summary>
		/// Production notification.
		/// Handles notifications for all production machines like
		/// Bee House, Cheese Press, Keg, etc. 
		/// </summary>

		public void CheckProductionAroundFarm()
		{
			foreach (var location in Game1.locations)
			{
				if (location.Name.Equals(Contants.FARM)) CheckFarmProductions(location);
				else if (location.Name.Equals(Contants.SHED)) CheckShedProductions(location);
				// else if (location.Name.Equals("Greenhouse")) CheckGreenhouseProductions(location);
			}
		}

		public void CheckFarmProductions(GameLocation farm)
		{
			if (!Util.Config.notifyFarm) return;
			var counter = new Dictionary<string, Pair<StardewValley.Object, int>>();
			foreach (var pair in farm.objects)
			{
				var obj = pair.Value;
				if (!obj.readyForHarvest) continue;
				if (counter.ContainsKey(obj.name))
					counter[obj.name].Second++;
				else
					counter.Add(obj.name, new Pair<StardewValley.Object, int>(obj, 1));
			}
			foreach (var pair in counter)
			{
				Util.ShowHarvestableMessage(pair);
			}
		}

		public void CheckShedProductions(GameLocation location)
		{
			if (!Util.Config.notifyShed || !(location is StardewValley.Locations.BuildableGameLocation)) return;
			foreach (var building in (location as StardewValley.Locations.BuildableGameLocation).buildings)
			{
				if (!(building.indoors is Shed)) continue;
				var counter = new Dictionary<string, Pair<StardewValley.Object, int>>();
				foreach (var pair in building.indoors.Objects)
				{
					if (!pair.Value.readyForHarvest) continue;
					if (counter.ContainsKey(pair.Value.name))
						counter[pair.Value.name].Second++;
					else
						counter.Add(pair.Value.name, new Pair<StardewValley.Object, int>(pair.Value, 1));
				}
				foreach (var pair in counter)
				{
					Util.ShowHarvestableMessage(pair);
				}
			}
		}

		public void CheckGreenhouseProductions(GameLocation greenHouse)
		{
			// if (!Util.Config.notifyGreenhouse) return;
			foreach (var obj in greenHouse.objects)
			{
				//TODO: Greenhouse
			}
		}

		public void CheckCellarProductions(GameLocation cellar)
		{
			//TODO: Cellar
		}
	}
}
