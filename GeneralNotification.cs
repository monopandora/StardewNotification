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
            CheckForToolUpgrade();
            CheckForTravelingMerchant();
        }

        public void DoBirthdayReminder()
        {
            var character = Utility.getTodaysBirthdayNPC(Game1.currentSeason, Game1.dayOfMonth);
            if (!ReferenceEquals(character, null) && Game1.player.friendships[character.name][3] != 1)
            {
                Util.showMessage(string.Format(Contants.BIRTHDAY_REMINDER, character.name));
            }
        }

        private void CheckForBirthday()
        {
            if (!Util.Config.notifyBirthdays) return;
            var character = Utility.getTodaysBirthdayNPC(Game1.currentSeason, Game1.dayOfMonth);
            if (ReferenceEquals(character, null)) return;
            Util.showMessage(string.Format(Contants.BIRTHDAY, character.name));

        }

        private void CheckForTravelingMerchant()
        {
            if (!Util.Config.notifyTravelingMerchant || Game1.dayOfMonth % 7 % 5 != 0) return;
            Util.showMessage(Contants.TRAVELING_MERCHANT);
        }

        private void CheckForToolUpgrade()
        {
            if (!Util.Config.notifyToolUpgrade) return;
            if (!ReferenceEquals(Game1.player.toolBeingUpgraded, null) && Game1.player.daysLeftForToolUpgrade <= 0)
                Util.showMessage(string.Format(Contants.TOOL_PICKUP, Game1.player.toolBeingUpgraded.name));
        }

        private void CheckForMaxLuck()
        {
            if (!Util.Config.notifyMaxLuck || Game1.dailyLuck < 0.07) return;
            Util.showMessage(Contants.LUCKY_DAY);
        }


        private void CheckForQueenOfSauce()
        {
            if (!Util.Config.notifyQueenOfSauce) return;
            var dayName = Game1.shortDayNameFromDayOfSeason(Game1.dayOfMonth);
            if (!dayName.Equals(Contants.SUN)) return;
            Util.showMessage(Contants.QUEEN_OF_SAUCE);
        }

        private void CheckForFestival()
        {
            if (!Util.Config.notifyFestivals || !Utility.isFestivalDay(Game1.dayOfMonth, Game1.currentSeason)) return;
            var festivalName = GetFestivalName();
            Util.showMessage(string.Format(Contants.FESTIVAL_MSG, festivalName));
            if (!festivalName.Equals(Contants.WINTER_STAR)) return;
            var rng = new Random((int)(Game1.uniqueIDForThisGame / 2UL) - Game1.year);
            var santa = Utility.getRandomTownNPC(rng, Utility.getFarmerNumberFromFarmer(Game1.player)).name;
            Util.showMessage(string.Format(Contants.SECRET_SANTA_REMINDER, santa));
        }

        private string GetFestivalName()
        {
            var season = Game1.currentSeason;
            var day = Game1.dayOfMonth;
            switch (season)
            {
                case Contants.SPRING:
                    if (day == 13) return Contants.EGG_FESTIVAL;
                    if (day == 24) return Contants.FLOWER_DANCE;
                    break;
                case Contants.SUMMER:
                    if (day == 11) return Contants.LUAU;
                    if (day == 28) return Contants.MOONLIGHT_JELLIES;
                    break;
                case Contants.FALL:
                    if (day == 16) return Contants.VALLEY_FAIR;
                    if (day == 27) return Contants.SPIRIT_EVE;
                    break;
                case Contants.WINTER:
                    if (day == 8) return Contants.ICE_FESTIVAL;
                    if (day == 25) return Contants.WINTER_STAR;
                    break;
                default:
                    break;
            }
            return Contants.FESTIVAL;
        }
    }
}
