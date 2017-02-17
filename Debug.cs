using System;

using StardewValley;
using StardewModdingAPI;

namespace StardewNotification
{
	public static class Debug
	{

		public static bool DEBUG = true;

		public static void printMailForTomorrow()
		{
			if (!DEBUG) return;
			Util.Monitor.Log("Mail For Tomorrow =============================", LogLevel.Info);
			foreach (var mail in Game1.player.mailForTomorrow)
			{
				Util.Monitor.Log(mail, LogLevel.Info);
			}
		}

		public static void printMailReceived()
		{
			if (!DEBUG) return;
			Util.Monitor.Log("Mail Received =============================", LogLevel.Info);
			foreach (var mail in Game1.player.mailReceived)
			{
				Util.Monitor.Log(mail, LogLevel.Info);
			}
		}

		public static void printPlayerEventsSeen()
		{
			if (!DEBUG) return;
			Util.Monitor.Log("Player Events Seen ========================", LogLevel.Info);
			foreach (var v in Game1.player.eventsSeen)
			{
				Util.Monitor.Log(string.Format("{0}", v), LogLevel.Info);
			}
		}
	}
}
