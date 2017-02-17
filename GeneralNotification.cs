using System;

using StardewValley;

namespace StardewNotification
{
    public class GeneralNotification
    {
        public void DoNewDayNotifications()
        {
            CheckForBirthday();
            CheckForFestival();
            CheckForMaxLuck();
            CheckForQueenOfSauce();
            CheckForSeasonal();
            CheckForToolUpgrade();
            CheckForTravelingMerchant();
        }

        public void DoBirthdayReminder()
        {
            var character = Utility.getTodaysBirthdayNPC(Game1.currentSeason, Game1.dayOfMonth);
            if (!ReferenceEquals(character, null) && Game1.player.friendships[character.name][3] != 1)
            {
                Util.showMessage(string.Format(Message.BIRTHDAY_REMINDER, character.name));
            }
        }

        private void CheckForBirthday()
        {
            if (!Util.Config.notifyBirthdays) return;
            var character = Utility.getTodaysBirthdayNPC(Game1.currentSeason, Game1.dayOfMonth);
            if (ReferenceEquals(character, null)) return;
            Util.showMessage(string.Format(Message.BIRTHDAY, character.name));

        }

        private void CheckForTravelingMerchant()
        {
            if (!Util.Config.notifyTravelingMerchant || Game1.dayOfMonth % 7 % 5 != 0) return;
            Util.showMessage(Message.TRAVELING_MERCHANT);
        }

        private void CheckForToolUpgrade()
        {
            if (!Util.Config.notifyToolUpgrade) return;
            if (!ReferenceEquals(Game1.player.toolBeingUpgraded, null) && Game1.player.daysLeftForToolUpgrade <= 0)
                Util.showMessage(string.Format(Message.TOOL_PICKUP, Game1.player.toolBeingUpgraded.name));
        }

        private void CheckForMaxLuck()
        {
            if (!Util.Config.notifyMaxLuck || Game1.dailyLuck < 0.07) return;
            Util.showMessage(Message.LUCKY_DAY);
        }


        private void CheckForQueenOfSauce()
        {
            if (!Util.Config.notifyQueenOfSauce) return;
            var dayName = Game1.shortDayNameFromDayOfSeason(Game1.dayOfMonth);
            if (!dayName.Equals(Message.SUN)) return;
            Util.showMessage(Message.QUEEN_OF_SAUCE);
        }

        private void CheckForFestival()
        {
            if (!Util.Config.notifyFestivals || !Utility.isFestivalDay(Game1.dayOfMonth, Game1.currentSeason)) return;
            var festivalName = GetFestivalName();
            Util.showMessage(string.Format(Message.FESTIVAL_MSG, festivalName));
            if (!festivalName.Equals(Message.WINTER_STAR)) return;
            var rng = new Random((int)(Game1.uniqueIDForThisGame / 2UL) - Game1.year);
            var santa = Utility.getRandomTownNPC(rng, Utility.getFarmerNumberFromFarmer(Game1.player)).name;
            Util.showMessage(string.Format(Message.SECRET_SANTA_REMINDER, santa));
        }

        private string GetFestivalName()
        {
            var season = Game1.currentSeason;
            var day = Game1.dayOfMonth;
            switch (season)
            {
                case Message.SPRING:
                    if (day == 13) return Message.EGG_FESTIVAL;
                    if (day == 24) return Message.FLOWER_DANCE;
                    break;
                case Message.SUMMER:
                    if (day == 11) return Message.LUAU;
                    if (day == 28) return Message.MOONLIGHT_JELLIES;
                    break;
                case Message.FALL:
                    if (day == 16) return Message.VALLEY_FAIR;
                    if (day == 27) return Message.SPIRIT_EVE;
                    break;
                case Message.WINTER:
                    if (day == 8) return Message.ICE_FESTIVAL;
                    if (day == 25) return Message.WINTER_STAR;
                    break;
                default:
                    break;
            }
            return Message.FESTIVAL;
        }

        private void CheckForSeasonal()
        {
            if (!Util.Config.notifySeasonalForage) return;
            string seasonal = null;
            var dayOfMonth = Game1.dayOfMonth;
            switch (Game1.currentSeason)
            {
                case Message.SPRING:
                    if (dayOfMonth > 14 && dayOfMonth < 19) seasonal = Message.SALMONBERRY;
                    break;
                case Message.SUMMER:
                    if (dayOfMonth > 11 && dayOfMonth < 15) seasonal = Message.SEASHELLS;
                    break;
                case Message.FALL:
                    if (dayOfMonth > 7 && dayOfMonth < 12) seasonal = Message.BLACKBERRY;
                    break;
                default:
                    break;
            }
            if (!ReferenceEquals(seasonal, null))
            {
                Util.showMessage($"{seasonal}");
            }
        }
    }
}
