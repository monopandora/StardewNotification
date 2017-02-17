using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

using StardewValley;
using StardewModdingAPI;

namespace StardewNotification
{
	public class HarvestableNotification
	{

		private const int MUSHROOM_CAVE = 2;
		private const int FRUIT_CAVE = 1;

		public void DoFarmHarvestables(GameLocation currentLocation)
		{
			var counter = new Dictionary<string, Pair<StardewValley.Object, int>>();
			foreach (var pair in currentLocation.objects)
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

		public void DoFarmBuildingHarvestables()
		{
			foreach (GameLocation location in Game1.locations)
			{
				//PrintLocation(location);
				DoShedHarvestables(location);
				DoFarmCaveHarvestables(location);
			}
		}

		public void DoShedHarvestables(GameLocation location)
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

		public void DoFarmCaveHarvestables(GameLocation location)
		{
			if (!Util.Config.notifyFarmCave || !location.name.Equals(Message.FARMCAVE)) return;
			if (Game1.player.caveChoice == MUSHROOM_CAVE)
			{
				var numReadyForHarvest = 0;
				foreach (var pair in location.Objects)
				{
					if (pair.Value.readyForHarvest) numReadyForHarvest++;
				}
				if (numReadyForHarvest > 0)
					Util.ShowFarmCaveMessage(location);
			}
			else if (Game1.player.caveChoice == FRUIT_CAVE && location.Objects.Count > 0)
			{
				Util.ShowFarmCaveMessage(location);
			}
		}

		/*
		public void DoGreenhouseHarvestables(GameLocation location)
		{
			if (!location.Name.Equals("Greenhouse")) return;
			foreach (var obj in location.objects)
			{
			}
		}
		*/

		public void PrintLocation(GameLocation location)
		{
			Util.Monitor.Log(string.Format("Location: {0}", location.Name), LogLevel.Info);
			if (location.Name.Equals("Greenhouse"))
			{
				Util.Monitor.Log(string.Format("Unique Name: {0}", location.uniqueName), LogLevel.Info);
				Util.Monitor.Log(string.Format("IsOutdoors: {0}", location.IsOutdoors), LogLevel.Info);
				Util.Monitor.Log(string.Format("IsStructure: {0}", location.isStructure), LogLevel.Info);
				Util.Monitor.Log(string.Format("IsFarm: {0}", location.IsFarm), LogLevel.Info);
				Util.Monitor.Log(string.Format("Objects Count: {0}", location.objects.Count), LogLevel.Info);
			}
		}
	}
}
