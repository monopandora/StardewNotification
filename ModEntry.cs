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
    // TODO: Beach notifications
	// TODO: Cellar notifications
	// TODO: Greenhouse notifications
    class ModEntry : Mod
    {
		private HarvestableNotification harvestableNotification;
		private GeneralNotification generalNotification;

        public override void Entry(IModHelper helper)
        {
            Util.Config = helper.ReadConfig<Configuration>();
			Util.Monitor = Monitor;
			harvestableNotification = new HarvestableNotification();
			generalNotification = new GeneralNotification();

			#pragma warning disable 0618
            PlayerEvents.LoadedGame += ReceiveLoadedGame;
            MenuEvents.MenuClosed += ReceiveMenuClosed;
            TimeEvents.TimeOfDayChanged += ReceiveTimeOfDayChanged;
            LocationEvents.CurrentLocationChanged += ReceiveCurrentLocationChanged;
        }

        private void ReceiveLoadedGame(object sender, EventArgs e)
        {
            // Check for new save
            if (Game1.currentSeason.Equals(Message.SPRING) && Game1.dayOfMonth == 0 && Game1.year == 1)
                return;

			/*
			Debug.printMailReceived();
			Debug.printMailForTomorrow();
			Debug.printPlayerEventsSeen();
			if (!Game1.player.mailForTomorrow.Contains("ccPantry"))
			{
				Game1.player.mailForTomorrow.Add("ccPantry");
			}
			*/

			generalNotification.DoNewDayNotifications();
        }

        private void ReceiveMenuClosed(object sender, EventArgsClickableMenuClosed e)
        {
            if (e.PriorMenu.GetType() == typeof(StardewValley.Menus.ShippingMenu) ||
                e.PriorMenu.GetType() == typeof(StardewValley.Menus.LevelUpMenu) ||
                e.PriorMenu.GetType() == typeof(StardewValley.Menus.SaveGameMenu))
            {
				generalNotification.DoNewDayNotifications();
            }
        }

        private void ReceiveTimeOfDayChanged(object sender, EventArgsIntChanged e)
        {
            if (Util.Config.notifyBirthdayReminder && e.NewInt == Util.Config.birthdayReminderTime)
				generalNotification.DoBirthdayReminder();
        }

        private void ReceiveCurrentLocationChanged(object sender, EventArgsCurrentLocationChanged e)
        {
			if (!e.NewLocation.name.Equals(Message.FARM) || Game1.timeOfDay == 2400) return;
			harvestableNotification.DoFarmHarvestables(e.NewLocation);
			harvestableNotification.DoFarmBuildingHarvestables();
        }
    }
}
