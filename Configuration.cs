namespace StardewNotification
{
	public class Configuration
    {
        public bool notifyBirthdays { get; set; } = true;
        public bool notifyBirthdayReminder { get; set; } = true;
        public int birthdayReminderTime { get; set; } = 1700; // 5:00 pm
        public bool notifyFestivals { get; set; } = true;
        public bool notifyTravelingMerchant { get; set; } = true;
        public bool notifyToolUpgrade { get; set; } = true;
        public bool notifyQueenOfSauce { get; set; } = true;
        public bool notifyMaxLuck { get; set; } = true;
        public bool notifySeasonalForage { get; set; } = true;
        public bool notifyFarmCave { get; set; } = true;
        public bool notifyShed { get; set; } = true;
    }
}
