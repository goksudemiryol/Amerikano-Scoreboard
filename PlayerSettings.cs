using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Amerikano
{
    internal static class PlayerSettings
    {
        internal static string FilePath { get; set; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AllPlayers.txt");

        internal static List<PlayerInfo> GetPlayersFromFile()
        {
            List<PlayerInfo> players = new List<PlayerInfo>();

            List<string> lines = File.Exists(FilePath) ? File.ReadAllLines(FilePath).ToList() : new List<string>
            {
                "Id,Name,Nicknames[0];Nicknames[1];Nicknames[2]",
                "001,Dilek,Dilek;MAVİŞ;DİŞÇİ",
                "002,Selim,Selim;BARON;UZAYLI",
                "003,Zeynep,Zeynep;KUZU;BÜCÜR",
                "004,Fuat,Fuat;GARDROP;HIYAR",
                "005,Shinsuke,Shinsuke;FIRE!;BO!",
                "006,Ingrid,Ingrid;BLOSSOM;KNIGHT",
                "007,Augustin,Augustin;TWINKLE;GENIE"
            };
            lines?.RemoveAt(0);

            foreach (string line in lines)
            {
                string[] oneLine = line.Split(',');
                string[] nicknames = oneLine[2].Split(';');
                players.Add(new PlayerInfo()
                {
                    Id = int.Parse(oneLine[0]),
                    Name = oneLine[1],
                    Nicknames = nicknames.Where(nick => nick != String.Empty).ToArray()
                });
            }

            return players;
        }

        internal static void SetPlayersToFile(List<PlayerInfo> players)
        {
            List<string> lines = new List<string>()
            {
                "Id,Name,Nicknames[0];Nicknames[1];Nicknames[2]"
            };

            foreach (PlayerInfo player in players)
            {
                if (player.Nicknames.Length == 3)
                    lines.Add($"{player.Id},{player.Name},{player.Nicknames[0]};{player.Nicknames[1]};{player.Nicknames[2]}");
                else if (player.Nicknames.Length == 2)
                    lines.Add($"{player.Id},{player.Name},{player.Nicknames[0]};{player.Nicknames[1]};");
                else
                    lines.Add($"{player.Id},{player.Name},{player.Nicknames[0]};;");
            }

            File.WriteAllLines(FilePath, lines);
        }
    }
}
