namespace Amerikano
{
    internal static class Languages
    {
        internal static int language = (int)LanguagesEnum.English;
        internal static string[] Language = new string[] { "English", "Türkçe" };
        internal static string[] ChooseLanguage = new string[] { "Choose the language", "Dili seçin" };

        internal static string[] Main = new string[] { "Main", "Giriş" };
        internal static string[] Game = new string[] { "Game", "Oyun" };
        internal static string[] PlayerSettings = new string[] { "Player Settings", "Oyuncu Ayarları" };

        internal static string[] PlayerSettingsButton = new string[] { "Add / remove player", "Oyuncu ekle / kaldır" };
        internal static string[] PlayerSelection = new string[] { "Player Selection", "Oyuncu Seçimi" };
        internal static string[] PlayerLimit = new string[] { "\nPick a minimum of 3, a maximum of 6 players", "\nEn az 3, en fazla 6 oyuncu seçin." };
        internal static string[] ResumeGame = new string[] { "RESUME", "DEVAM ET" };
        internal static string[] NewGame = new string[] { "NEW GAME", "YENİ OYUN" };
        internal static string[] PlayingOrder = new string[] { "Playing order: ", "Oynama sıralaması: " };

        internal static string[] Lock = new string[] { "LOCK", "KİLİTLE" };

        internal static string[] Question = new string[] { "Are you sure?", "Emin misiniz?" };
        internal static string[] Description = new string[] { "Selected player(s) are going to be deleted.", "Seçili oyuncu(lar) silinecek." };
        internal static string[] Yes = new string[] { "Yes", "Evet" };
        internal static string[] No = new string[] { "No", "Hayır" };

        internal static string[] Total = new string[] { "TOTAL", "TOPLAM" };

        internal static string[,] GameTypes = new string[,]
        {
            {
                "3 SETS OF 3s",
                "3 TIERCES",
                "2 TIERCES - 1 SET OF 3s",
                "2 SETS OF 3s - 1 TIERCE",
                "2 SETS OF 3s - 2 TIERCES",
                "2 SETS OF 4s",
                "2 QUARTES",
                "1 SET OF 4s - 1 QUARTE",
                "1 QUINTE - 1 SET OF 3s",
                "SEXTETTE FLUSH",
                "SEPTETTE FLUSH",
                "DOUBLE",
                "FROM HAND"
            },
            {
                "3 3'LÜ KÜT",
                "3 3'LÜ SERİ",
                "2 3'LÜ SERİ 1 3'LÜ KÜT",
                "2 3'LÜ KÜT 1 3'LÜ SERİ",
                "2 3'LÜ KÜT 2 3'LÜ SERİ",
                "2 4'LÜ KÜT",
                "2 4'LÜ SERİ",
                "4'LÜ KÜT 4'LÜ SERİ",
                "5'Lİ SERİ 3'LÜ KÜT",
                "6'LI SERİ",
                "7'Lİ SERİ",
                "ÇITÇIT",
                "ELDEN"
            }
        };

        internal static string[] PlayerInfo = new string[] { "Enter Player Info", "Oyuncu Bilgilerini Girin" };
        internal static string[] PlayerName = new string[] { "Player name: ", "Oyuncu adı: " };
        internal static string[] FirstNick = new string[] { "First nickname: ", "Birinci lakap: " };
        internal static string[] SecondNick = new string[] { "Second nickname: ", "İkinci lakap: " };
        internal static string[] AddPlayer = new string[] { "Add", "Ekle" };
        internal static string[] Players = new string[] { "Players", "Oyuncular" };
        internal static string[] DeletePlayer = new string[] { "Delete", "Sil" };
    }

    internal enum LanguagesEnum
    {
        English,
        Turkish
    }
}
