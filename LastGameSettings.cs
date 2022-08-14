using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Amerikano
{
    internal static class LastGameSettings
    {
        internal static string FilePath { get; set; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "GameData.txt");


        internal static void GetScores()
        {
            List<string> allLines;

            if (File.Exists(FilePath))
                allLines = File.ReadAllLines(FilePath).ToList();
            else
                return;


            string[] playersLine = allLines[13].Split(',');

            if (playersLine.Length > GameCalculations.allPlayers.Count ||
                Array.ConvertAll(playersLine, id => int.Parse(id)).Max() > GameCalculations.allPlayers.Select(player => player.Id).Max())
                return;

            List<PlayerInfo> players = new List<PlayerInfo>();

            foreach (string id in playersLine)
                players.Add(GameCalculations.allPlayers.Where(player => id == player.Id.ToString()).ToList()[0]);

            MainPage.newGame = new GameCalculations(players);

            if (App.tabbedPage1.Children.Count == 1)
                App.tabbedPage1.Children.Add(new GamePage());
            else
                App.tabbedPage1.Children[1] = new GamePage();

            App.tabbedPage1.CurrentPage = App.tabbedPage1.Children[1];


            for (short i = 0; i < GameCalculations.gameTypeCount; i++)
            {
                for (short j = 0; j < players.Count; j++)
                {
                    string[] oneLine = allLines[i].Split(',');

                    if (Int32.Parse(oneLine[j]) == 0)
                        if (oneLine.Where(score => score != "0").Count() > 0)
                            GamePage.entries[j, i].Text = "-";
                        else
                            GamePage.entries[j, i].Text = String.Empty;

                    else
                        GamePage.entries[j, i].Text = oneLine[j];
                }
            }


            if (allLines[14].Length == 0)
                return;
            string[] gamesPlayedLine = allLines[14].Split(',');
            foreach (string game in gamesPlayedLine)
                GamePage.ButtonOfTypes_Clicked(GamePage.buttonsOfTypes[Int32.Parse(game) - 1], EventArgs.Empty);


            return;
        }


        internal static void SetScores(short[,] gameScores, List<PlayerInfo> players, List<string> gamesPlayed)
        {
            List<string> lines = new List<string>();

            for (short i = 0; i < GameCalculations.gameTypeCount; i++)
            {
                string oneLine = String.Empty;

                for (short j = 0; j < players.Count; j++)
                {
                    oneLine += gameScores[j, i].ToString();
                    oneLine += ",";
                }
                oneLine = oneLine.Remove(oneLine.Length - 1);

                lines.Add(oneLine);
            }


            string player = String.Empty;
            foreach (PlayerInfo playerInfo in players)
            {
                player += playerInfo.Id.ToString();
                player += ",";
            }
            player = player.Remove(player.Length - 1);
            lines.Add(player);


            string gamePlayed = string.Empty;
            foreach (string game in gamesPlayed)
            {
                gamePlayed += game;
                gamePlayed += ",";
            }
            if (gamesPlayed.Count != 0)
                gamePlayed = gamePlayed.Remove(gamePlayed.Length - 1);
            lines.Add(gamePlayed);


            File.WriteAllLines(FilePath, lines);
        }
    }
}
