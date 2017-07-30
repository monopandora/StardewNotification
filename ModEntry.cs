using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;

namespace StardewNotification
{
    /// <summary>
    /// The mod entry point
    /// </summary>
    class ModEntry : Mod
    {
		private HarvestNotification harvestableNotification;
		private GeneralNotification generalNotification;
		private ProductionNotification productionNotification;

        public override void Entry(IModHelper helper)
        {
            Util.Config = helper.ReadConfig<Configuration>();
			Util.Monitor = Monitor;
			harvestableNotification = new HarvestNotification();
			generalNotification = new GeneralNotification();
			productionNotification = new ProductionNotification();

            SaveEvents.AfterLoad += ReceiveLoadedGame;
            //MenuEvents.MenuClosed += ReceiveMenuClosed;
            TimeEvents.AfterDayStarted += DailyNotifications;
            TimeEvents.TimeOfDayChanged += ReceiveTimeOfDayChanged;
            LocationEvents.CurrentLocationChanged += ReceiveCurrentLocationChanged;
        }

        private void ReceiveLoadedGame(object sender, EventArgs e)
        {
            // Check for new save
            if (Game1.currentSeason.Equals(Constants.SPRING) && Game1.dayOfMonth == 0 && Game1.year == 1)
                return;
			generalNotification.DoNewDayNotifications();
			harvestableNotification.CheckHarvestsAroundFarm();
        }

        private void DailyNotifications(object sender, EventArgs e)
        {
			generalNotification.DoNewDayNotifications();
			harvestableNotification.CheckHarvestsAroundFarm();
        }

        private void ReceiveTimeOfDayChanged(object sender, EventArgsIntChanged e)
        {
            if (Util.Config.notifyBirthdayReminder && e.NewInt == Util.Config.birthdayReminderTime)
				generalNotification.DoBirthdayReminder();
        }

        private void ReceiveCurrentLocationChanged(object sender, EventArgsCurrentLocationChanged e)
        {
			if (!e.NewLocation.name.Equals(Constants.FARM) || Game1.timeOfDay == 2400) return;
			harvestableNotification.CheckHarvestsOnFarm();
			productionNotification.CheckProductionAroundFarm();
        }
    }
}
