using System;
using System.Collections.Generic;

namespace Amerikano
{
    internal class GameCalculations
    {
        //------------ Constants ------------//

        internal const byte gameTypeCount = 13;

        internal static List<PlayerInfo> allPlayers = PlayerSettings.GetPlayersFromFile();

        //----------------------------------------------------------------------------------------------------//

        internal static List<PlayerInfo> Players { get; set; }

        internal short[,] GameScores { get; set; }
        internal int[] TotalScores { get; set; }

        internal static int gameTracker = 0;
        internal static List<string> gameTypeTracker;

        internal GameCalculations(List<PlayerInfo> Players)
        {
            GameCalculations.Players = Players ?? allPlayers;
            this.GameScores = new short[GameCalculations.Players.Count, gameTypeCount];
            this.TotalScores = new int[GameCalculations.Players.Count];

            gameTracker = 0;
            gameTypeTracker = new List<string>();
        }

        internal int CalculateTotalScore(int player)
        {
            int[] TotalScores = new int[Players.Count];

            for (int i = 0; i < gameTypeCount; i++)
                TotalScores[player] += this.GameScores[player, i];

            return TotalScores[player];
        }

        internal void Update(Xamarin.Forms.Entry entry, string oldTextValue, string newTextValue)
        {
            string i_ = entry.ClassId.Split('-')[0];
            string j_ = entry.ClassId.Split('-')[1];

            if (newTextValue == String.Empty || newTextValue == "+" || newTextValue == "-")
            {
                this.GameScores[Int32.Parse(j_) - 1, Int32.Parse(i_) - 1] = 0;
                this.TotalScores[Int32.Parse(j_) - 1] = CalculateTotalScore(Int32.Parse(j_) - 1);
                GamePage.labels[Int32.Parse(j_) - 1].Text = this.TotalScores[Int32.Parse(j_) - 1].ToString();
                return;
            }

            var regex = new System.Text.RegularExpressions.Regex(@"^[\+-]?\d+$");

            if (!regex.IsMatch(newTextValue) || Int32.Parse(newTextValue) < -32768 || Int32.Parse(newTextValue) > 32767)
            {
                entry.Text = oldTextValue;
                return;
            }

            this.GameScores[Int32.Parse(j_) - 1, Int32.Parse(i_) - 1] = Int16.Parse(newTextValue);
            this.TotalScores[Int32.Parse(j_) - 1] = CalculateTotalScore(Int32.Parse(j_) - 1);
            GamePage.labels[Int32.Parse(j_) - 1].Text = this.TotalScores[Int32.Parse(j_) - 1].ToString();

        }

        internal static void IDSetter()
        {
            for (int i = 0; i < allPlayers.Count; i++)
            {
                allPlayers[i].Id = i + 1;
            }
        }

        internal void SetScores()
        {
            LastGameSettings.SetScores(GameScores, GameCalculations.Players, GameCalculations.gameTypeTracker);
        }
    }
}
